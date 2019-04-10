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
using szachy_wpf;

namespace szachy_menu
{
    /// <summary>
    /// Interaction logic for Versus.xaml
    /// </summary>
    public partial class Versus : Window
    {
        Board b;
        DrawingContext dc;
        List<Piece> whitePieces;
        List<Piece> blackPieces;
        bool selected;
        int sx, sy;
        Adapter adapter;
        chat client;
        System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public Versus()
        {
            InitializeComponent();

            client = new chat();
            this.LbMessages.ItemsSource = StaticMethods.ListOfMessages;
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            myDispatcherTimer.Tick += new EventHandler(Each_Tick);
            myDispatcherTimer.Start();


            b = new Board(C1);
            b.draw(C1, dc);
            initpieces();
            adapter = new Adapter();
            foreach (var p in whitePieces)
            {
                p.draw(C1);
            }
            foreach (var p in blackPieces)
            {
                p.draw(C1);
            }
        }
        void initpieces()
        {
            PieceBuilder pc;
            pc = new PawnBuilder();
            whitePieces = new List<Piece>();
            blackPieces = new List<Piece>();
            for (int x = 0; x < 8; x++)
            {
                whitePieces.Add(pc.BuildPiece(true, x, 6, new PawnMovement()));
                b.board[x, 6].av = 1;
                blackPieces.Add(pc.BuildPiece(false, x, 1, new PawnMovement()));
                b.board[x, 1].av = 2;
            }
            pc = new RookBuilder();
            whitePieces.Add(pc.BuildPiece(true, 0, 7, new RookMovement()));
            b.board[0, 7].av = 1;
            whitePieces.Add(pc.BuildPiece(true, 7, 7, new RookMovement()));
            b.board[7, 7].av = 1;
            blackPieces.Add(pc.BuildPiece(false, 0, 0, new RookMovement()));
            b.board[0, 0].av = 2;
            blackPieces.Add(pc.BuildPiece(false, 7, 0, new DefaultMovement()));
            b.board[7, 0].av = 2;
            pc = new KnightBuilder();
            whitePieces.Add(pc.BuildPiece(true, 1, 7, new KnightMovement()));
            b.board[1, 7].av = 1;
            whitePieces.Add(pc.BuildPiece(true, 6, 7, new KnightMovement()));
            b.board[6, 7].av = 1;
            blackPieces.Add(pc.BuildPiece(false, 1, 0, new KnightMovement()));
            b.board[1, 0].av = 2;
            blackPieces.Add(pc.BuildPiece(false, 6, 0, new KnightMovement()));
            b.board[6, 0].av = 2;
            pc = new BishopBuilder();
            whitePieces.Add(pc.BuildPiece(true, 2, 7, new BishopMovement()));
            b.board[2, 7].av = 1;
            whitePieces.Add(pc.BuildPiece(true, 5, 7, new BishopMovement()));
            b.board[5, 7].av = 1;
            blackPieces.Add(pc.BuildPiece(false, 2, 0, new BishopMovement()));
            b.board[2, 0].av = 2;
            blackPieces.Add(pc.BuildPiece(false, 5, 0, new BishopMovement()));
            b.board[5, 0].av = 2;
            pc = new QueenBuilder();
            whitePieces.Add(pc.BuildPiece(true, 3, 7, new QueenMovement()));
            b.board[3, 7].av = 1;
            blackPieces.Add(pc.BuildPiece(false, 3, 0, new QueenMovement()));
            b.board[3, 0].av = 2;
            pc = new KingBuilder();
            whitePieces.Add(pc.BuildPiece(true, 4, 7, new KingMovement()));
            b.board[4, 7].av = 1;
            blackPieces.Add(pc.BuildPiece(false, 4, 0, new KingMovement()));
            b.board[4, 0].av = 2;
            foreach (var w in whitePieces)
            {
                b.board[w.x, w.y].c = w.c;
            }
            foreach (var w in blackPieces)
            {
                b.board[w.x, w.y].c = w.c;
            }


        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxMessage.Text) || string.IsNullOrWhiteSpace(TbxMessage.Text))
            {

            }
            else
            {
                client.SendMessage(TbxMessage.Text);
                TbxMessage.Text = string.Empty;
            }

        }

        private void Click(object sender, MouseButtonEventArgs e)
        {
            Point x = e.MouseDevice.GetPosition(this);
            int xx = (int)x.X / 64;
            int yy = (int)x.Y / 64;



            if (b.board[xx, yy].av == 1 && selected == false)
            {
                b.board[xx, yy].selected = true;
                selected = true;
                sx = xx;
                sy = yy;
            }
            else if (selected)//jesli zaznaczony
            {
                //foreach (var p in whitePieces) // znajdz ktory
                Piece p = whitePieces.Find(item => item.x == sx && item.y == sy);
                {
                    if (p.x == sx && p.y == sy) //jw
                    {
                        if (p.movement(b, sx, sy, xx, yy, true)) // sprawdz logike
                        {
                            if (b.board[xx, yy].av == 2) // zbijanie
                            {
                                for (int bl = 0; bl < blackPieces.Count; bl++)
                                {
                                    if (blackPieces[bl].x == xx && blackPieces[bl].y == yy)
                                    {
                                        blackPieces.RemoveAt(bl);
                                    }
                                }
                            }
                            p.x = xx;
                            p.y = yy;
                            b.board[xx, yy].av = 1;
                            b.board[sx, sy].av = 0;
                            b.board[sx, sy].c = '1';
                            b.board[xx, yy].c = p.c;


                            selected = false;
                            client.SendMove(string.Format("{0} {1} {2} {3}", sx, sy, xx, yy), "gracz2@pawel");
                            //adapter.ComputeAnswer(b, b.board[sx, sy].character, b.board[sx, sy].number, b.board[xx, yy].character, b.board[xx, yy].number);
                            b.startpos = false;

                            //ruch komputera
                            foreach (var bl in blackPieces)
                            {
                                if (bl.x == adapter.answer[0] && bl.y == adapter.answer[1])
                                {

                                    if (b.board[adapter.answer[2], adapter.answer[3]].av == 1)
                                    {

                                        for (int wh = 0; wh < whitePieces.Count; wh++)
                                        {
                                            if (whitePieces[wh].x == adapter.answer[2] && whitePieces[wh].y == adapter.answer[3])
                                            {
                                                whitePieces.RemoveAt(wh);
                                            }
                                        }
                                    }
                                    bl.x = adapter.answer[2];
                                    bl.y = adapter.answer[3];
                                    b.board[adapter.answer[0], adapter.answer[1]].c = '1';
                                    b.board[adapter.answer[2], adapter.answer[3]].c = bl.c;
                                    b.board[adapter.answer[0], adapter.answer[1]].av = 0;
                                    b.board[adapter.answer[2], adapter.answer[3]].av = 2;
                                }
                            }
                            Console.WriteLine(b.FEN());
                            b.AV();
                        }
                        else selected = false;

                    }
                    else selected = false;
                }
            }
            else selected = false;

            C1.Children.Clear();
            b.draw(C1, dc);
            foreach (var p in whitePieces)
            {
                p.draw(C1);
            }
            foreach (var p in blackPieces)
            {
                p.draw(C1);
            }
        }


        public void Each_Tick(object sender, EventArgs e)
        {
            if (StaticMethods.LenghtListOfMessages != StaticMethods.ListOfMessages.Count)
            {
                LbMessages.ItemsSource = null;
                StaticMethods.LenghtListOfMessages += 1;
                this.LbMessages.DataContext = StaticMethods.ListOfMessages;
                LbMessages.ItemsSource = StaticMethods.ListOfMessages;
                System.Diagnostics.Debug.WriteLine(StaticMethods.ListOfMessages.Count);
            }


        }
    }
}
