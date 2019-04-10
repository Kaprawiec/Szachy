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
namespace szachy_wpf
{

    static class Factory
    {
        public static Block Get(int id, Canvas C)
        {
            switch (id)
            {
                case 0:
                    return new Block1(C);
                case 1:
                    return new Block2(C);
                default:
                    return new Block2(C);
            }
        }
    }

    public class Board
    {
        public Block[,] board;
         public bool startpos;
        public char c;
        public Board(Canvas C1)
        {
            startpos = true;
            board = new Block[8, 8];
            for (int x = 0, c = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0) board[x, y] = Factory.Get(0, C1);
                        else board[x, y] = Factory.Get(1, C1);
                    }
                    else
                    {
                        if (x % 2 == 0) board[x, y] = Factory.Get(1, C1);
                        else board[x, y] = Factory.Get(0, C1);
                    }
                    board[x, y].x = x;
                    board[x, y].y = y;
                    board[x, y].number = 8-y;
                    board[x, y].character = (char)(x +97);
                }
            }
        }
        public void draw(Canvas C, DrawingContext dc)
        {
            Color S = Colors.Yellow;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board[x, y].selected) board[x, y].draw(C,S,dc);
                    else board[x, y].draw(C, board[x, y].setColor,dc);
                }
            }
        }
        public string FEN()
        {
            string f="";
            for(int i=0;i<8;i++)
            {
                for(int j=0;j<8;j++)
                {
                    f += board[j, i].c;
                }
                f += '/';
            }
            f = f.Remove(f.Length - 1);
            f += " b KQkq - 0 1";
            return f;
        }
        public void AV()
        {
            string f = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    f += board[j, i].av;
                }
                f += '/';
                Console.WriteLine(f);
                f = "";
            }
        }
    }
}
