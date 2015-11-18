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

namespace EveHQSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool settingsWindowOpen = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (!settingsWindowOpen)
            {
                Settings settingsWindow = new Settings();
                settingsWindow.Show();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //EveHQSharp.Fitting.Fitting fittingWindow = new EveHQSharp.Fitting.Fitting();
            //TestGrid.Children.Add();
        }
    }
}
