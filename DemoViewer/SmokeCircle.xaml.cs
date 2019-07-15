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
    /// Interaction logic for SmokeCircle.xaml
    /// </summary>
    public partial class SmokeCircle : UserControl
    {
        public SmokeCircle()
        {
            InitializeComponent();
        }

        public void SetRadius(double radius)
        {
            ellipseSmoke.Width = radius;
            ellipseSmoke.Height = radius;
        }
    }
}
