using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace szachy_wpf
{

    interface PieceBuilder
    {
        Piece BuildPiece(bool white, int xx, int yy, IMovementStrategy str);
    }
    public interface IMovementStrategy
    {
        bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white);
    }
    public class PawnMovement : IMovementStrategy
    {
        public bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            if (white == true)
            {
                if (toy + 1 >= fromy && fromx == tox && b.board[tox, toy].av == 0)
                    return true;
                else if ((tox == fromx - 1 || tox == fromx + 1) && b.board[tox, toy].av == 2 && toy + 1 == fromy)
                    return true;
                else if (toy + 2 >= fromy && fromx == tox && b.board[tox, toy].av == 0 && fromy == 6)
                    return true;
                else return false;
            }
            else
            {
                if ((toy == fromy - 1 || toy == fromy + 1) && tox == fromx && b.board[tox, toy].av == 0)
                    return true;
                else if (toy == fromy && (tox == fromx + 1 || tox == fromx - 1) && b.board[tox, toy].av == 0)
                    return true;
                else if (toy == fromy + 2 && tox == fromx && b.board[tox, toy].av == 0 && fromy == 5)
                    return true;
                else if (toy == fromy + 1 && (tox == fromx - 1 || tox == fromx + 1) && b.board[tox, toy].av == 1)
                    return true;
                else return false;
            }
        }
    }
    public class KingMovement : IMovementStrategy
    {
        public bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            if (white == true)
            {
                if ((toy == fromy - 1 || toy == fromy + 1) && (tox == fromx + 1 || tox == fromx - 1) && b.board[tox, toy].av != 1)
                    return true;
                else if ((toy == fromy + 1 || toy == fromy - 1) && (tox == fromx) && b.board[tox, toy].av != 1)
                    return true;
                else if (toy == fromy && (tox == fromx + 1 || tox == fromx - 1) && b.board[tox, toy].av != 1)
                    return true;
                else return false;
            }
            else
            {
                if ((toy == fromy - 1 || toy == fromy + 1) && (tox == fromx + 1 || tox == fromx - 1) && b.board[tox, toy].av != 1)
                    return true;
                else if ((toy == fromy + 1 || toy == fromy - 1) && (tox == fromx) && b.board[tox, toy].av != 1)
                    return true;
                else if (toy == fromy && (tox == fromx + 1 || tox == fromx - 1) && b.board[tox, toy].av != 1)
                    return true;
                else return false;
            }
        }
    }
    public class KnightMovement : IMovementStrategy
    {
        public bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            if (white == true)
            {
                if (toy == fromy + 2 && (tox == fromx + 1 || tox == fromx - 1) && b.board[tox, toy].av != 1)
                    return true;
                else if (toy == fromy - 2 && (tox == fromx - 1 || tox == fromx + 1) && b.board[tox, toy].av != 1)
                    return true;
                else if (toy == fromy + 1 && (tox == fromx + 2 || tox == fromx - 2) && b.board[tox, toy].av != 1)
                    return true;
                else if (toy == fromy - 1 && (tox == fromx - 2 || tox == fromx + 2) && b.board[tox, toy].av != 1)
                    return true;
                else return false;
            }
            else
            {
                if (toy == fromy + 2 && (tox == fromx + 1 || tox == fromx - 1) && b.board[tox, toy].av != 1)
                    return true;
                else if (toy == fromy - 2 && (tox == fromx - 1 || tox == fromx + 1) && b.board[tox, toy].av != 1)
                    return true;
                else if (toy == fromy + 1 && (tox == fromx + 2 || tox == fromx - 2) && b.board[tox, toy].av != 1)
                    return true;
                else if (toy == fromy - 1 && (tox == fromx - 2 || tox == fromx + 2) && b.board[tox, toy].av != 1)
                    return true;
                else return false;
            }
        }
    }
    public class RookMovement : IMovementStrategy
    {
        int distance = 0, j;
        public bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            if (white == true)
            {
                if (toy > fromy && tox == fromx && b.board[tox, toy].av != 1)
                {
                    distance = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distance; i++)
                    {
                        if (b.board[tox, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distance)
                        return true;
                    else
                        return false;
                }
                else if (toy < fromy && tox == fromx && b.board[tox, toy].av != 1)
                {
                    distance = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distance; i++)
                    {
                        if (b.board[tox, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distance)
                        return true;
                    else
                        return false;
                }
                else if (toy == fromy && tox > fromx && b.board[tox, toy].av != 1)
                {
                    distance = tox - fromx;
                    j = 0;
                    for (int i = 1; i <= distance; i++)
                    {
                        if (b.board[fromx + i, toy].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distance)
                        return true;
                    else
                        return false;
                }
                else if (toy == fromy && tox < fromx && b.board[tox, toy].av != 1)
                {
                    distance = fromx - tox;
                    j = 0;
                    for (int i = 1; i <= distance; i++)
                    {
                        if (b.board[fromx - i, toy].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distance)
                        return true;
                    else
                        return false;
                }
                else return false;
            }
            else
            {
                if (toy > fromy && tox == fromx && b.board[tox, toy].av != 1)
                {
                    distance = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distance; i++)
                    {
                        if (b.board[tox, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distance)
                        return true;
                    else
                        return false;
                }
                else if (toy < fromy && tox == fromx && b.board[tox, toy].av != 1)
                {
                    distance = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distance; i++)
                    {
                        if (b.board[tox, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distance)
                        return true;
                    else
                        return false;
                }
                else if (toy == fromy && tox > fromx && b.board[tox, toy].av != 1)
                {
                    distance = tox - fromx;
                    j = 0;
                    for (int i = 1; i <= distance; i++)
                    {
                        if (b.board[fromx + i, toy].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distance)
                        return true;
                    else
                        return false;
                }
                else if (toy == fromy && tox < fromx && b.board[tox, toy].av != 1)
                {
                    distance = fromx - tox;
                    j = 0;
                    for (int i = 1; i <= distance; i++)
                    {
                        if (b.board[fromx - i, toy].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distance)
                        return true;
                    else
                        return false;
                }
                else return false;
            }
        }
    }
    public class BishopMovement : IMovementStrategy
    {
        int distanceY, j = 0;
        public bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            if (white == true)
            {
                if (toy < fromy && tox < fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx - i, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy > fromy && tox > fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx + i, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy < fromy && tox > fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx + i, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy > fromy && tox < fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx - i, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else return false;
            }
            else
            {
                if (toy < fromy && tox < fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx - i, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy > fromy && tox > fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx + i, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy < fromy && tox > fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx + i, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy > fromy && tox < fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx - i, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else return false;
            }
        }
    }
    public class QueenMovement : IMovementStrategy
    {
        int distanceY, j = 0;
        public bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            if (white == true)
            {
                if (toy < fromy && tox < fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx - i, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy > fromy && tox > fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx + i, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy < fromy && tox > fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx + i, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy > fromy && tox < fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx - i, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy > fromy && tox == fromx && b.board[tox, toy].av != 1)
                {
                    distanceY = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[tox, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else
                        return false;
                }
                else if (toy < fromy && tox == fromx && b.board[tox, toy].av != 1)
                {
                    distanceY = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[tox, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else
                        return false;
                }
                else if (toy == fromy && tox > fromx && b.board[tox, toy].av != 1)
                {
                    distanceY = tox - fromx;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx + i, toy].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else
                        return false;
                }
                else if (toy == fromy && tox < fromx && b.board[tox, toy].av != 1)
                {
                    distanceY = fromx - tox;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx - i, toy].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else
                        return false;
                }
                else return false;
            }
            else
            {
                if (toy < fromy && tox < fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx - i, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy > fromy && tox > fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx + i, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy < fromy && tox > fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx + i, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy > fromy && tox < fromx && b.board[tox, toy].av != 1 && (Math.Abs(fromx - tox) == Math.Abs(fromy - toy)))
                {
                    distanceY = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx - i, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else return false;
                }
                else if (toy > fromy && tox == fromx && b.board[tox, toy].av != 1)
                {
                    distanceY = toy - fromy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[tox, fromy + i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else
                        return false;
                }
                else if (toy < fromy && tox == fromx && b.board[tox, toy].av != 1)
                {
                    distanceY = fromy - toy;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[tox, fromy - i].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else
                        return false;
                }
                else if (toy == fromy && tox > fromx && b.board[tox, toy].av != 1)
                {
                    distanceY = tox - fromx;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx + i, toy].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else
                        return false;
                }
                else if (toy == fromy && tox < fromx && b.board[tox, toy].av != 1)
                {
                    distanceY = fromx - tox;
                    j = 0;
                    for (int i = 1; i <= distanceY; i++)
                    {
                        if (b.board[fromx - i, toy].av == 0)
                            j++;
                    }
                    if (b.board[tox, toy].av == 2)
                        j++;
                    if (j >= distanceY)
                        return true;
                    else
                        return false;
                }
                else return false;
            }
        }
    }
    public class DefaultMovement : IMovementStrategy
    {
        public bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            return true;
        }
    }
    class PawnBuilder : PieceBuilder
    {
        Pawn p;
        public Piece BuildPiece(bool white, int xx, int yy, IMovementStrategy str)
        {
            p = new Pawn(xx, yy);
            if (white)
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\pawn_w.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'P';
                p.ims = str;
            }
            else
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\pawn_b.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'p';
                p.ims = str;
            }
            return p;
        }
    }
    class RookBuilder : PieceBuilder
    {
        Rook p;
        public Piece BuildPiece(bool white, int xx, int yy, IMovementStrategy str)
        {
            p = new Rook(xx, yy);
            if (white)
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\rook_w.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'R';
                p.ims = str;
            }
            else
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\rook_b.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'r';
                p.ims = str;
            }
            return p;
        }
    }
    class KnightBuilder : PieceBuilder
    {
        Knight p;
        public Piece BuildPiece(bool white, int xx, int yy, IMovementStrategy str)
        {
            p = new Knight(xx, yy);
            if (white)
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\knight_w.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'N';
                p.ims = str;
            }
            else
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\knight_b.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'n';
                p.ims = str;
            }
            return p;
        }
    }
    class BishopBuilder : PieceBuilder
    {
        Bishop p;
        public Piece BuildPiece(bool white, int xx, int yy, IMovementStrategy str)
        {
            p = new Bishop(xx, yy);
            if (white)
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\bishop_w.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'B';
                p.ims = str;
            }
            else
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\bishop_b.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'b';
                p.ims = str;
            }
            return p;
        }
    }
    class QueenBuilder : PieceBuilder
    {
        Queen p;
        public Piece BuildPiece(bool white, int xx, int yy, IMovementStrategy str)
        {
            p = new Queen(xx, yy);
            if (white)
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\queen_w.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'Q';
                p.ims = str;
            }
            else
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\queen_b.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'q';
                p.ims = str;
            }
            return p;
        }
    }
    class KingBuilder : PieceBuilder
    {
        King p;
        public Piece BuildPiece(bool white, int xx, int yy, IMovementStrategy str)
        {
            p = new King(xx, yy);
            if (white)
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\king_w.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'K';
                p.ims = str;
            }
            else
            {
                p.texture.ImageSource = new BitmapImage(new Uri("images\\king_b.png", UriKind.Relative));
                p.rect.Fill = p.texture;
                p.c = 'k';
                p.ims = str;
            }
            return p;
        }
    }




    public abstract class Piece
    {
        public int x;
        public int y;
        public char c;
        public Rectangle rect;
        public ImageBrush texture;
        public abstract bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white);
        public IMovementStrategy ims;
        public void draw(Canvas C)
        {
            Canvas.SetLeft(rect, x * 64);
            Canvas.SetTop(rect, y * 64);
            C.Children.Add(rect);

        }

    }
    public class Pawn : Piece
    {
        public Pawn(int xx, int yy)
        {
            x = xx;
            y = yy;
            rect = new Rectangle();
            rect.Width = 64;
            rect.Height = 64;
            texture = new ImageBrush();
        }
        public override bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            return ims.movement(b, fromx, fromy, tox, toy, white);
        }
    }
    public class Rook : Piece
    {
        public Rook(int xx, int yy)
        {
            x = xx;
            y = yy;
            rect = new Rectangle();
            rect.Width = 64;
            rect.Height = 64;
            texture = new ImageBrush();
        }
        public override bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            return ims.movement(b, fromx, fromy, tox, toy, white);
        }
    }
    public class Knight : Piece
    {
        public Knight(int xx, int yy)
        {
            x = xx;
            y = yy;
            rect = new Rectangle();
            rect.Width = 64;
            rect.Height = 64;
            texture = new ImageBrush();
        }
        public override bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            return ims.movement(b, fromx, fromy, tox, toy, white);
        }
    }
    public class Bishop : Piece
    {
        public Bishop(int xx, int yy)
        {
            x = xx;
            y = yy;
            rect = new Rectangle();
            rect.Width = 64;
            rect.Height = 64;
            texture = new ImageBrush();
        }
        public override bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            return ims.movement(b, fromx, fromy, tox, toy, white);
        }
    }
    public class Queen : Piece
    {
        public Queen(int xx, int yy)
        {
            x = xx;
            y = yy;
            rect = new Rectangle();
            rect.Width = 64;
            rect.Height = 64;
            texture = new ImageBrush();
        }
        public override bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            return ims.movement(b, fromx, fromy, tox, toy, white);
        }
    }
    public class King : Piece
    {
        public King(int xx, int yy)
        {
            x = xx;
            y = yy;
            rect = new Rectangle();
            rect.Width = 64;
            rect.Height = 64;
            texture = new ImageBrush();
        }
        public override bool movement(Board b, int fromx, int fromy, int tox, int toy, bool white)
        {
            return ims.movement(b, fromx, fromy, tox, toy, white);
        }
    }
}
