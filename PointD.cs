using System;
using System.Collections.Generic;
using System.Text;

namespace difury
{
    class PointD
    {
        public Decimal X { get; }
        public Decimal Y { get; }

        public PointD(Decimal x, Decimal y)
        {
            X = x;
            Y = y;
        }

        public PointD(double x, double y)
        {
            X = Convert.ToDecimal(x) + decimal.Zero;
            Y = Convert.ToDecimal(y);
        }

        public PointD(int x, int y)
        {
            X = Convert.ToDecimal(x);
            Y = Convert.ToDecimal(y);
        }

        public override string ToString()
        {
            return (this.X, this.Y).ToString();
        }
    }
}
