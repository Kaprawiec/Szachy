using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix;
using szachy_menu;

namespace szachy_wpf
{
    static public class StaticMethods
    {
        static public List<string> ListOfMessages= new List<string>();
        static public int LenghtListOfMessages = 0;
        static public List<int> ListOfPoints = new List<int>();
        static public SimpleList lista = new SimpleList();
        static public List<int> OdebraneRuchy = new List<int>(); 
        static public string command;
        static public void SetLicense()
        {
            Matrix.License.LicenseManager.SetLicense(@"eJxkkFtTwjAQhf+Kw6ujSYsUdNaMXDpDAbkX0LeVpCVj25QkrcCvFxXvLzt7
            9tvLmYWBXIvMiLNdmmTmtoLxhVGRfUEtbpIPVGEw1ooXaxtwNrMFlwrIdwUm
            BWZW2j1zgHzl0C6MVanQDIaYCuaXmBRolQbyrqGt0hyz/SeQKjs7WQHyycBP
            USbMYCLM3Q9nl/zY9MGOzV+HwpyjFf4ul1p0jhlzqVOnDr0C8g9BYDoiVczq
            4rjrJOAt/p73qEurQP4AmMk4Q1towcRishz0MVxQB683q3m8cef3XR1O+XjR
            NA/uY1S/2vo6ldhc5v2tkHI17CrPC1U2PMQPh36j3qi2JpQ6/Hzqr/kyaDTL
            HEcvq5l46j+Xa+6XW6W4UDMvsPOy8zQKh0VWjdE/kHC+wKgkpter8YFLt07L
            m5ruY2sf+RslV7U06FHXq8VRXbid+BbIt28gp3ezVwE=");
        }
        static public void Declarations()
        {
            ListOfMessages.Clear();
            ListOfPoints.Clear();
        }
        
         
    }
}
