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
    /// Interaction logic for FireCircle.xaml
    /// </summary>
    public partial class FireCircle : UserControl
    {
        public FireCircle()
        {
            InitializeComponent();
        }

        public void SetRadius(double radius)
        {
            ellipseFire.Width = radius;
            ellipseFire.Height = radius;
        }
    }
}
