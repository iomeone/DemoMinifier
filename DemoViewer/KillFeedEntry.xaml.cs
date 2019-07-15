using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoMinifier.Models;
using DemoMinifier.Models.Events;

namespace DemoViewer
{
    /// <summary>
    /// Interaction logic for KillFeedEntry.xaml
    /// </summary>
    public partial class KillFeedEntry : UserControl
    {
        Color ctColor = Color.FromRgb(50, 150, 250);
        Color tColor = Color.FromRgb(250, 200, 50);

        public KillFeedEntry()
        {
            InitializeComponent();
        }

        public KillFeedEntry(Player killer, Player victim, Weapon weapon, bool hs, bool wallbang)
        {
            InitializeComponent();

            textAttackerName.Text = killer.Name;
            textAttackerName.Foreground = new SolidColorBrush(killer.Team == Team.CounterTerrorist ? ctColor : tColor);

            textVictimName.Text = victim.Name;
            textVictimName.Foreground = new SolidColorBrush(victim.Team == Team.CounterTerrorist ? ctColor : tColor);

            imageHeadshot.Height = hs ? 12 : 0;
            imageWallbang.Height = wallbang ? 12 : 0;

            imageWeapon.Source = Utility.GetWeaponIcon(weapon);
        }
    }
}
