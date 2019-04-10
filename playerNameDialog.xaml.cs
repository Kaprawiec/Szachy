using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace szachy_menu
{
    /// <summary>
    /// Interaction logic for playerNameDialog.xaml
    /// </summary>
    public partial class playerNameDialog : Window
    {
        public playerNameDialog()
        {
            InitializeComponent();
        }

        private void joinGame_Click(object sender, RoutedEventArgs e)
        {
            if (joinGame.IsChecked == true)
            {
                newPol.IsEnabled = false;
                addIP.IsEnabled = true;
            }
            else
            {
                newPol.IsEnabled = true;
                addIP.IsEnabled = false;
            }
        }

        private void newPol_Click(object sender, RoutedEventArgs e)
        {
            if(newPol.IsChecked == true)
            {
                string name = Dns.GetHostName();
                IPHostEntry dnsAdresses = Dns.GetHostEntry(name);
              // string name = Dns.GetHostName();
               //var dnsAdresses = Dns.GetHostEntry(name).AddressList;
             // dnsAdresses[1], masz typ IpAdress[]
            List<String> ipList = new List<String>();

            foreach (IPAddress ip in dnsAdresses.AddressList)
            {
                ipList.Add(ip.ToString());
                //Console.WriteLine(ip.ToString());
            }
            String i = ipList.ElementAt(2);
            ipAdress.Content = i.ToString();
            joinGame.IsEnabled = false;
            }
            else
            {
                joinGame.IsEnabled = true;
                ipAdress.Content = "";
            }
        }
    }
}
