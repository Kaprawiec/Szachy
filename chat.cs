using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix;
using Matrix.Xmpp.Client;
using Matrix.Xmpp;
using System.Windows.Controls;
using szachy_menu;

namespace szachy_wpf
{
    class chat
    {
        
        private XmppClient xmppclient;
        private MucManager _MucManager;
        public  chat()
        {

            xmppclient = new XmppClient();
            xmppclient.OnRosterEnd += xmppclient_OnRosterEnd;
            xmppclient.OnBeforeSasl += xmppclient_OnBeforeSasl;
            xmppclient.OnMessage += xmppclient_OnMessage;
            xmppclient.OnReceiveXml += xmppclient_OnReceiveXml;
            xmppclient.OnSendXml +=  xmppclient_OnSendXml;
            xmppclient.OnLogin += xmppclient_OnLogin;
            StaticMethods.SetLicense();
            xmppclient.StartTls = false;
            
            xmppclient.Password = "qwerty1";
            xmppclient.Username = "gracz1";
            xmppclient.XmppDomain = "localhost";
            xmppclient.Port = 5222;
            xmppclient.AutoPresence = true;
            xmppclient.Open();
            
        }
        private void xmppclient_OnRosterEnd(object sender, Matrix.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("\n Zalogowano \n");
            _MucManager = new MucManager(xmppclient);
            //_MucManager.EnterRoom(new Jid("szachy@conference.pawel"), "gracz1");
            xmppclient.SendPresence(Show.Chat);
             _MucManager.EnterRoomAsync(new Jid("szachy@conference.pawel"), "gracz1");
            _MucManager.EnterRoomAsync(new Jid("wynik@conference.pawel"), "gracz1");
        }
        private void xmppclient_OnLogin(object sender, Matrix.EventArgs e)
        {
            
            
        }
        private void xmppclient_OnMessage(object sender, MessageEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Zalogowano \n");
            System.Diagnostics.Debug.WriteLine(string.Format("{0} \n", e.Message.Body));
            System.Diagnostics.Debug.WriteLine(e.Message.From.Bare);
            if (!string.IsNullOrEmpty( e.Message.Body))
            {
                if (e.Message.From.Bare =="wynik@conference.pawel" )
                {
                    int a;
                    if (int.TryParse(e.Message.Body, out a))
                    {
                        StaticMethods.ListOfPoints.Add(a);
                    }    
                }
                else
                {
                    if (e.Message.From.Bare == "szachy@conference.pawel")
                    {
                        StaticMethods.ListOfMessages.Add(string.Format("Wysłał: {0} \n {1}", e.Message.From.Resource, e.Message.Body.ToString()));
                    }
                    else
                    {
                        StaticMethods.lista.AddToEnd(string.Format(" {0} \n", e.Message.Body));
                        
                       
                    }
                    //if (e.Message.From.Bare == "gracz2@pawel")
                    //{
                    //    StaticMethods.OdebraneRuchy.Clear();
                    //    StaticMethods.command = e.Message.Body;
                    //    string com = e.Message.Body;
                    //    List<string> listastringow = new List<string>();
                    //    listastringow= com.Split(' ').ToList();
                    //    foreach (string item in listastringow)
                    //    {
                    //        int a;
                    //        if (int.TryParse(item,out a))
                    //        {
                    //            StaticMethods.OdebraneRuchy.Add(a);
                    //        }
                            
                    //    }
                    //}


                }


            }

        }
        private void xmppclient_OnSendXml(object sender, TextEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Send :\n{0} \n", e.Text));
        }
        private void xmppclient_OnReceiveXml(object sender, TextEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Rec: \n{0} \n", e.Text));
        }
        private void xmppclient_OnBeforeSasl(object sender, Matrix.Xmpp.Sasl.SaslEventArgs e)
        {
            e.Auto = false;
            e.SaslMechanism = Matrix.Xmpp.Sasl.SaslMechanism.Plain;
        }

        public void SendMessage( string message)
        {
            var msg = new Matrix.Xmpp.Client.Message();
            msg.To = "szachy@conference.pawel";
            msg.Body = message;
            msg.Type = MessageType.GroupChat;
            xmppclient.Send(msg);
        }

        public void SendMove(string move, string reciver)
        {
            
            var msg = new Matrix.Xmpp.Client.Message();
            msg.To = reciver;
            msg.Body = move;
            msg.Type = MessageType.Chat;
            xmppclient.Send(msg);
        }
    }
}
