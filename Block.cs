using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace szachy_wpf
{

    public abstract class Block
    {
        public abstract Color setColor { get; }
        public int x { get; set; }
        public int y { get; set; }
        public System.Windows.Shapes.Rectangle rect;
        public System.Windows.Shapes.Rectangle rect2;
        public bool selected;
        public Image i;
        public int av; //0-free 1-white 2-black
        public int number;
        public char character;
        public char c;

        public void draw(Canvas C, Color color, DrawingContext dc)
        {
            rect.Stroke = new SolidColorBrush(color);
            rect.Fill = new SolidColorBrush(color);
            rect.Width = 64;
            rect.Height = 64;
            Canvas.SetLeft(rect, x * 64);
            Canvas.SetTop(rect, y * 64);
            C.Children.Add(rect);
            selected = false;
            
        }
    }
    public class Block1 : Block
    {
        public Block1(Canvas C)
        {
            rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.Fill = new SolidColorBrush(Colors.Black);
            rect.Width = 64;
            rect.Height = 64;
            av = 0;
            c = '1';

        }
        public override Color setColor
        {
            get
            {
                return Colors.Brown;
            }
        }
    }
    public class Block2 : Block
    {
        public Block2(Canvas C)
        {
            rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.Fill = new SolidColorBrush(Colors.Black);
            rect.Width = 64;
            rect.Height = 64;
            av = 0;
            c = '1';
        }
        public override Color setColor
        {
            get
            {
                return Colors.Tan;
            }
        }
    }
}
