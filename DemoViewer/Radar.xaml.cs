using DemoMinifier.Models;
using DemoMinifier.Models.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
using System.Windows.Threading;

namespace DemoViewer
{
    /// <summary>
    /// Interaction logic for Radar.xaml
    /// </summary>
    public partial class Radar : UserControl
    {
        float mapX, mapY, scale;
        Canvas canvas;

        public Radar()
        {
            InitializeComponent();

            canvas = (Canvas)this.FindName("canvasMain");
        }

        public void SetMap(string mapName)
        {
            var lines = File.ReadAllLines(System.IO.Path.Combine("overviews", System.IO.Path.GetFileName(mapName) + ".txt"));

            var file = lines
                .First(a => a.Contains("\"material\""))
                .Split('"')[3];

            if (File.Exists(file + "_radar_spectate.png"))
                file += "_radar_spectate.png";
            else if (File.Exists(file + "_radar.png"))
                file += "_radar.png";
            else
                file += ".png";

            mapX = float.Parse(lines
                .First(a => a.Contains("\"pos_x\""))
                .Split('"')[3], CultureInfo.InvariantCulture);
            mapY = float.Parse(lines
                .First(a => a.Contains("\"pos_y\""))
                .Split('"')[3], CultureInfo.InvariantCulture);
            scale = float.Parse(lines
                .First(a => a.Contains("\"scale\""))
                .Split('"')[3], CultureInfo.InvariantCulture);

            var uri = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, file));
            var bitmap = new BitmapImage(uri);
            imageBackground.Source = bitmap;
        }

        Queue<SmokeCircle> smokes = new Queue<SmokeCircle>();

        public void SmokeStarted(DemoMinifier.Models.Vector position)
        {
            SmokeCircle circle = new SmokeCircle();

            canvas.Children.Add(circle);
            smokes.Enqueue(circle);

            Point radarPosition = MapPoint(position);
            double radius = (MapPoint(position) - MapPoint(position + new DemoMinifier.Models.Vector(288, 0, 0))).Length;
            circle.SetRadius(radius);
            double left = radarPosition.X - radius / 2d;
            double top = radarPosition.Y - radius / 2d;

            Canvas.SetLeft(circle, left);
            Canvas.SetTop(circle, top);
        }

        public void SmokeEnded(DemoMinifier.Models.Vector position)
        {
            if (smokes.Count == 0)
                return;

            SmokeCircle circle = smokes.Dequeue();
            canvas.Children.Remove(circle);
        }

        Queue<FireCircle> molotovs = new Queue<FireCircle>();

        public void FireStarted(DemoMinifier.Models.Vector position)
        {
            FireCircle circle = new FireCircle();

            canvas.Children.Add(circle);
            molotovs.Enqueue(circle);

            Point radarPosition = MapPoint(position);
            double radius = (MapPoint(position) - MapPoint(position + new DemoMinifier.Models.Vector(288, 0, 0))).Length;
            circle.SetRadius(radius);
            double left = radarPosition.X - radius / 2d;
            double top = radarPosition.Y - radius / 2d;

            Canvas.SetLeft(circle, left);
            Canvas.SetTop(circle, top);
        }

        public void FireEnded(DemoMinifier.Models.Vector position)
        {
            if (molotovs.Count == 0)
                return;

            FireCircle circle = molotovs.Dequeue();
            canvas.Children.Remove(circle);
        }

        public void ResetNades()
        {
            while (smokes.Count > 0)
                canvas.Children.Remove(smokes.Dequeue());

            while (molotovs.Count > 0)
                canvas.Children.Remove(molotovs.Dequeue());
        }

        Dictionary<long, PlayerDot> playerDots = new Dictionary<long, PlayerDot>();

        public void UpdatePlayer(DemoMinifier.Models.Player player, bool visible)
        {
            PlayerDot dot;
            if (playerDots.ContainsKey(player.SteamID))
                dot = playerDots[player.SteamID];
            else
            {
                dot = new PlayerDot();
                playerDots.Add(player.SteamID, dot);
                canvas.Children.Add(dot);
            }

            Point radarPosition = MapPoint(player.Position);
            dot.Opacity = player.IsAlive ? 1 : 0;

            dot.SetSide(player.Team == DemoMinifier.Models.Team.Terrorist);
            dot.SetName(player.Name);
            dot.SetFlashed(player.FlashDuration > 0);
            dot.SetVisible(visible);

            double left = radarPosition.X - 31d / 2d;
            double top = radarPosition.Y - 31d / 2d;


            Canvas.SetLeft(dot, left);
            Canvas.SetTop(dot, top);
        }

        public void PlayerKilled(Player killer, Player victim, Weapon weapon, bool hs, bool wallbang)
        {
            KillFeedEntry entry = new KillFeedEntry(killer, victim, weapon, hs, wallbang);
            DockPanel.SetDock(entry, Dock.Right);
            stackKillfeed.Children.Add(entry);

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(5)
            };

            timer.Tick += delegate (object sender, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                stackKillfeed.Children.Remove(entry);
            };

            timer.Start();
        }

        public Point MapPoint(DemoMinifier.Models.Vector vec)
        {
            return new Point(
                (int)((vec.X - mapX) / scale),
                (int)((mapY - vec.Y) / scale)
            );
        }
    }
}
