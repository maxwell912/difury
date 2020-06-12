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
    }
}
