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

namespace DemoViewer
{
    /// <summary>
    /// Interaction logic for PlayerDot.xaml
    /// </summary>
    public partial class PlayerDot : UserControl
    {
        Color ctColor = Color.FromRgb(50, 150, 250);
        Color tColor = Color.FromRgb(250, 200, 50);

        public PlayerDot()
        {
            InitializeComponent();
        }

        bool lastSide = false;
        public void SetSide(bool t)
        {
            if (lastSide == t)
                return;

            if (t)
            {
                ellipseBig.Fill = new SolidColorBrush(tColor);
                ellipseSmall.Fill = new SolidColorBrush(tColor);
                textName.Foreground = new SolidColorBrush(tColor);
            }
            else
            {
                ellipseBig.Fill = new SolidColorBrush(ctColor);
                ellipseSmall.Fill = new SolidColorBrush(ctColor);
                textName.Foreground = new SolidColorBrush(ctColor);
            }

            lastSide = t;
        }

        public void SetFlashed(bool flashed)
        {
            ellipseFlashed.Opacity = flashed ? 1 : 0;
        }

        public void SetVisible(bool visible)
        {
            ellipseVisible.Opacity = visible ? 1 : 0;
        }

        public void SetName(string name)
        {
            textName.Text = name;
        }
    }
}
