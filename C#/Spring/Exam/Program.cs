using System.Diagnostics;
using System.IO;

namespace Exam
{
    abstract class Figure
    {
        private int price;

        public int Price
        {
            get { return price; }
            set
            {
                if (value > 0)
                {
                    price = value;
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter("../../../File.txt"))
                        writer.WriteLine("Установлены значения по умолчанию для объекта " + this.GetType());
                }
            }
        }

        public virtual double Square()
        {
            return Price;
        }
    }
    class StandartFigure : Figure
    {
        private double l;

        public double L
        {
            get { return l; }
            set
            {
                if (value > 0)
                {
                    l = value;
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter("../../../File.txt"))
                        writer.WriteLine("Установлены значения по умолчанию для объекта " + this.GetType());
                }
            }
        }

        private double h;

        public double H
        {
            get { return h; }
            set
            {
                if (value > 0)
                {
                    h = value;
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter("../../../File.txt"))
                        writer.WriteLine("Установлены значения по умолчанию для объекта " + this.GetType());
                }
            }
        }
        public override double Square()
        {
            return L * H;
        }
    }
    class Round : Figure
    {
        private double r;

        public double R
        {
            get { return r; }
            set
            {
                if (value > 0)
                {
                    r = value;
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter("../../../File.txt", true))
                        writer.WriteLine("Установлены значения по умолчанию для объекта " + this.GetType());
                }
            }
        }

        public override double Square()
        {
            return Math.PI * Math.Pow(R, 2);
        }
    }
    static class Painter
    {
        public static event Action Zoom;
        static public void Paint()
        {
           Zoom();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            StandartFigure standartFigure = new() { H= 1, L = 2, Price= 3 };
            Round round = new() { Price = 5, R = 2 };
            Console.WriteLine(standartFigure.Square());
            Console.WriteLine(round.Square());
            standartFigure.Price= -5;
            round.R= 0;
            List<Figure> figures= new List<Figure>() { standartFigure, round };
            figures.Add(new StandartFigure() { H = 5, L = 10, Price = 2});
            figures.Add(new Round() { R = 6, Price = 10});
            foreach(Figure figure in figures)
            {
                if(figure is Round round1)
                {
                    Console.WriteLine(round1.Square());
                }
                else if(figure is StandartFigure isStandartFigure)
                {
                    Console.WriteLine(isStandartFigure.Square());
                }
            }
            foreach (Figure figure in figures)
            {
                if (figure is Round round1)
                {
                    Painter.Zoom += () => { round1.Price = round1.Price / 2; };
                }
                else if (figure is StandartFigure isStandartFigure)
                {
                    Painter.Zoom += () => { isStandartFigure.Price = isStandartFigure.Price / 2; };
                }
            }
            foreach (Figure figure in figures)
            {
                Console.WriteLine(figure.Price);
            }
            Painter.Paint();
            foreach(Figure figure in figures)
            {
                Console.WriteLine(figure.Price);
            }
            Console.WriteLine(figures.Where(figure => figure is Round).Max(figure => figure.Square()) + figures.Where(figure => figure is StandartFigure).Min(figure => figure.Square()));
        }
    }
}