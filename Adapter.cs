using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace szachy_wpf
{
    public class Adapter
    {
        public struct ChangeErrorMode : IDisposable
        {
            [Flags]
            public enum
                ErrorModes
            {
                Default = 0x0,
                FailCriticalErrors = 0x1,
                NoGpFaultErrorBox = 0x2,
                NoAlignmentFaultExcept = 0x4,
                NoOpenFileErrorBox = 0x8000
            }
            private int _oldMode;
            public ChangeErrorMode(ErrorModes mode)
            {
                _oldMode = SetErrorMode((int)mode);
            }
            void IDisposable.Dispose()
            {
                SetErrorMode(_oldMode);
            }
            [DllImport("kernel32.dll")]
            private static extern int SetErrorMode(int newMode);

        }
        private System.Diagnostics.Process process = null;
        public int[] answer;
        public Adapter()
        {
            answer = new int[4];
            try
            {
                process = new System.Diagnostics.Process();
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.ErrorDialog = false;
                process.StartInfo.FileName = "PortfishNet40.exe";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(p_OutputDataReceived);
                process.Exited += new EventHandler(p_Exited);
                using (ChangeErrorMode newnewerr = new ChangeErrorMode(ChangeErrorMode.ErrorModes.FailCriticalErrors | ChangeErrorMode.ErrorModes.NoGpFaultErrorBox))
                    process.Start();
                process.EnableRaisingEvents = true;
                process.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Send("uci");
        }
        void p_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            Console.WriteLine("####################DATA RECIVED");
            if (e.Data != null && e.Data != "")
            {
                string line = e.Data.ToString().Trim();
                Console.WriteLine("--> " + line);
                if (line.Contains("mate"))
                {
                    MessageBoxResult result = MessageBox.Show("Przegrales");
                    process.WaitForExit();
                }
                if (line.StartsWith("bestmove"))
                {
                    string[] token = line.Split(new char[] { ' ' });
                    if (token.Length > 1)
                    {
                        Console.WriteLine(token[1]);
                        char tmp = token[1][0];
                        answer[0] = (int)tmp - 97;
                        answer[1] = 8 - (int)Char.GetNumericValue(token[1][1]);
                        char tmp2 = token[1][2];
                        answer[2] = (int)tmp2 - 97;
                        answer[3] = 8 - (int)Char.GetNumericValue(token[1][3]);
                    }
                }
            }
        }
        public void Send(string command)
        {
            this.process.StandardInput.WriteLine(command);
        }
        public void ComputeAnswer(Board board, char fc, int fi, char tc, int ti)
        {
            if (board.startpos)
            {
                Send("ucinewgame");
                Send("position startpos moves " + fc.ToString() + fi.ToString() + tc.ToString() + ti.ToString());
            }
            else
            {
                try
                {
                    Send("ucinewgame");
                    Send("position fen " + board.FEN());
                    Console.WriteLine("####################COMPUTE ANSWER TRY");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Send("go");
            Thread.Sleep(100);
        }
        void p_Exited(object sender, EventArgs e)
        {
            if (process != null)
            {
                Process[] Notepads = Process.GetProcessesByName("Portfish");
                foreach (Process CurrentNotepad in Notepads)
                {
                    CurrentNotepad.Kill();
                }
                MessageBoxResult result = MessageBox.Show("Przegrales");
                Console.WriteLine("Exited with code:{0} ", process.ExitCode);
            }
            else
                Console.WriteLine("exited");
        }
        void exit()
        {
            process.WaitForExit();
        }
    }
}