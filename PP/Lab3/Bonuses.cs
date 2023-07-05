using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec03LibN
{
    internal class BonusA : IBonus
    {
        public float cost1hour { get; set; }
        public float A { get; set; }

        public float calc(float number_hours)
        {
            return (number_hours + A) * cost1hour;
        }
        public BonusA(float cost1hour, float a = 0.0f)
        {
            this.cost1hour = cost1hour;
            A = a;
        }
    }
    internal class BonusB : IBonus
    {
        public float cost1hour { get; set; }
        public float X { get; set; }
        public float A { get; set; }

        public float calc(float number_hours)
        {
            return (number_hours + A) * X * cost1hour;
        }
        public BonusB(float cost1hour, float x, float a = 0.0f)
        {
            this.cost1hour = cost1hour;
            X = x;
            A = a;
        }
    }
    internal class BonusC : IBonus
    {
        public float cost1hour { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float A { get; set; }

        public float calc(float number_hours)
        {
            return ((number_hours + A) * X * cost1hour) + Y;
        }
        public BonusC(float cost1hour, float x, float y, float a = 0.0f)
        {
            this.cost1hour = cost1hour;
            X = x;
            Y = y;
            A = a;
        }
    }
}
