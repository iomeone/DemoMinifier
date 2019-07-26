using DemoMinifier;
using DemoMinifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DemoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Dictionary<long, PlayerHudEntryCT> ctPlayerEntries = new Dictionary<long, PlayerHudEntryCT>();
        Dictionary<long, PlayerHudEntryT> tPlayerEntries = new Dictionary<long, PlayerHudEntryT>();

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            MinifiedDemo demo = MinifiedDemo.LoadCompressed("red-canids-vs-pain-nuke.minidem");
            MinifiedParser parser = new MinifiedParser(demo);

            radarMain.SetMap(parser.Map);

            parser.SmokeNadeStarted += (o, e) => radarMain.SmokeStarted(e.Position);
            parser.SmokeNadeEnded += (o, e) => radarMain.SmokeEnded(e.Position);
            parser.FireNadeStarted += (o, e) => radarMain.FireStarted(e.Position);
            parser.FireNadeEnded += (o, e) => radarMain.FireEnded(e.Position);
            parser.RoundStart += (o, e) => radarMain.ResetNades();
            parser.RoundEnd += (o, e) => radarMain.ResetNades();
            parser.PlayerKilled += (o, e) =>
            {
                Player killer = parser.PartipatingPlayers.First(p => p.SteamID == e.KillerSteamID);
                Player victim = parser.PartipatingPlayers.First(p => p.SteamID == e.VictimSteamID);
                radarMain.PlayerKilled(killer, victim, e.Weapon, e.Headshot, e.PenetratedObjects > 0);
            };
            parser.TickDone += (o, e) =>
            {
                foreach (var player in parser.PartipatingPlayers)
                {
                    radarMain.UpdatePlayer(player, false);

                    if (player.Team == Team.CounterTerrorist) //Player is a CT
                    {
                        if (tPlayerEntries.ContainsKey(player.SteamID)) //Handle if player's team has changed
                        {
                            var oldEntry = tPlayerEntries[player.SteamID];
                            stackPanelTerrorists.Children.Remove(oldEntry);
                            tPlayerEntries.Remove(player.SteamID);
                        }

                        //Get or create new CT entry
                        PlayerHudEntryCT ctEntry;
                        if (ctPlayerEntries.ContainsKey(player.SteamID))
                            ctEntry = ctPlayerEntries[player.SteamID];
                        else
                        {
                            ctEntry = new PlayerHudEntryCT();
                            stackPanelCounterTerrorists.Children.Add(ctEntry);
                            ctPlayerEntries.Add(player.SteamID, ctEntry);
                        }

                        //Update player entry UI
                        ctEntry.SetName(player.Name);
                        ctEntry.SetHP(player.HP);
                        ctEntry.SetMoney(player.Money);
                        ctEntry.SetArmor(player.Armor, player.HasHelmet);
                        ctEntry.SetDefuseKit(player.HasDefuseKit);
                        ctEntry.SetPrimary(player.RawWeapons.Values.FirstOrDefault(w => w.Class == EquipmentClass.Rifle || w.Class == EquipmentClass.SMG || w.Class == EquipmentClass.Heavy));
                        ctEntry.SetSecondary(player.RawWeapons.Values.FirstOrDefault(w => w.Class == EquipmentClass.Pistol));
                        ctEntry.SetSmokeGrenade(player.HasSmokeGrenade);
                        ctEntry.SetHEGrenade(player.HasHighExplosiveGrenade);
                        ctEntry.SetFlashGrenadeCount(player.FlasbangCount);
                        ctEntry.SetMolotov(player.HasMolotov);
                        ctEntry.SetIncendiary(player.HasIncendiary);
                    }
                    else //Player is a T
                    {
                        if (ctPlayerEntries.ContainsKey(player.SteamID)) //Handle if player's team has changed
                        {
                            var oldEntry = ctPlayerEntries[player.SteamID];
                            stackPanelCounterTerrorists.Children.Remove(oldEntry);
                            ctPlayerEntries.Remove(player.SteamID);
                        }

                        //Get or create new T entry
                        PlayerHudEntryT tEntry;
                        if (tPlayerEntries.ContainsKey(player.SteamID))
                            tEntry = tPlayerEntries[player.SteamID];
                        else
                        {
                            tEntry = new PlayerHudEntryT();
                            stackPanelTerrorists.Children.Add(tEntry);
                            tPlayerEntries.Add(player.SteamID, tEntry);
                        }

                        //Update player entry UI
                        tEntry.SetName(player.Name);
                        tEntry.SetHP(player.HP);
                        tEntry.SetMoney(player.Money);
                        tEntry.SetArmor(player.Armor, player.HasHelmet);
                        tEntry.SetBomb(false);
                        tEntry.SetPrimary(player.RawWeapons.Values.FirstOrDefault(w => w.Class == EquipmentClass.Rifle || w.Class == EquipmentClass.SMG || w.Class == EquipmentClass.Heavy));
                        tEntry.SetSecondary(player.RawWeapons.Values.FirstOrDefault(w => w.Class == EquipmentClass.Pistol));
                        tEntry.SetSmokeGrenade(player.HasSmokeGrenade);
                        tEntry.SetHEGrenade(player.HasHighExplosiveGrenade);
                        tEntry.SetFlashGrenadeCount(player.FlasbangCount);
                        tEntry.SetMolotov(player.HasMolotov);
                        tEntry.SetIncendiary(player.HasIncendiary);
                    }
                }
            };


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(0);
            timer.Tick += (o, args) => parser.ParseNextTick();
            timer.Start();
        }
    }
}
