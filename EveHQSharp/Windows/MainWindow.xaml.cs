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
        public MainWindow()
        {
            InitializeComponent();
            fitterTab.Visibility = Visibility.Collapsed;
            industryTab.Visibility = Visibility.Collapsed;
            pilotManagerTab.Visibility = Visibility.Collapsed;

        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Core.Classes.WindowStates.settingsWindowOpen)
            {
                Settings settingsWindow = new Settings();
                settingsWindow.Show();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void fitterButton_Click(object sender, RoutedEventArgs e)
        {
            if (fitterTab.Visibility != Visibility.Visible)
            {
                fitterTab.Visibility = Visibility.Visible;
                Fitting.FittingUserControl fittingWindow = new EveHQSharp.Fitting.FittingUserControl();
                fitterGrid.Children.Add(fittingWindow);
            }
        }

        private void pilotManagerButton_Click(object sender, RoutedEventArgs e)
        {
            if (pilotManagerTab.Visibility != Visibility.Visible)
            {
                pilotManagerTab.Visibility = Visibility.Visible;
                PilotManager.UserControls.PilotManagerUserControl skillManagerWindow = new PilotManager.UserControls.PilotManagerUserControl();
                pilotManagerGrid.Children.Add(skillManagerWindow);
            }
        }

        private void industryButton_Click(object sender, RoutedEventArgs e)
        {
            if (industryTab.Visibility != Visibility.Visible)
            {
                industryTab.Visibility = Visibility.Visible;
                Industry.UserControls.IndustryUserControl industryWindow = new Industry.UserControls.IndustryUserControl();
                industryGrid.Children.Add(industryWindow);
            }

        }

        private void manageAPIButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Core.Classes.WindowStates.manageAPIWindowOpen)
            {
                EveAPI.Windows.ManageAPI manageAPIWindow = new EveAPI.Windows.ManageAPI();
                manageAPIWindow.Show();
            }
        }
    }
}
