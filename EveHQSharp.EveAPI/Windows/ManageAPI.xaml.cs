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
using System.Windows.Shapes;

namespace EveHQSharp.EveAPI.Windows
{
    /// <summary>
    /// Interaction logic for ManageAPI.xaml
    /// </summary>
    public partial class ManageAPI : Window
    {
        public ManageAPI()
        {
            Core.Classes.WindowStates.manageAPIWindowOpen = true;
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Core.Classes.WindowStates.manageAPIWindowOpen = false;
        }
    }
}
