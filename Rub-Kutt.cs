using System;
using System.Collections.Generic;

namespace difury
{
    class Rub_Kutt
    {
        Decimal h;
        Func<decimal, decimal, decimal> f;
        Func<decimal, decimal, decimal> g;

        public Rub_Kutt(Func<decimal, decimal, decimal> f, Func<decimal, decimal, decimal> g)
        {
            this.f = f;
            this.g = g;
        }


        public IEnumerable<PointD> GetPoints(PointD start, Decimal p, Decimal h)
        {
            this.h = h;

            Decimal prev_x = start.X;
            Decimal prev_y = start.Y;

            Decimal next_x;
            Decimal next_y;

            while (true)
            {
                try
                {
                    next_x = prev_x + (k1(prev_x, prev_y) + 2 * k2(prev_x, prev_y) + 2 * k3(prev_x, prev_y) + k4(prev_x, prev_y)) / 6;
                    next_y = prev_y + (l1(prev_x, prev_y) + 2 * l2(prev_x, prev_y) + 2 * l3(prev_x, prev_y) + l4(prev_x, prev_y)) / 6;
                    prev_x = next_x;
                    prev_y = next_y;
                }
                catch (Exception) { yield break; }

                yield return new PointD(next_x, next_y);
            }
        
        }

        #region
        Decimal k1(Decimal x, Decimal y)
        {
            return h * f(x, y);
        }

        Decimal l1(Decimal x, Decimal y)
        {
            return h * g(x, y);
        }

        Decimal k2(Decimal x, Decimal y)
        {
            return h * f(x + k1(x, y) / 2, y + l1(x, y) / 2);
        }

        Decimal l2(Decimal x, Decimal y)
        {
            return h * g(x + k1(x, y) / 2, y + l1(x, y) / 2);
        }

        Decimal k3(Decimal x, Decimal y)
        {
            return h * f(x + k2(x, y) / 2, y + l2(x, y) / 2);
        }

        Decimal l3(Decimal x, Decimal y)
        {
            return h * g(x + k2(x, y) / 2, y + l2(x, y) / 2);
        }

        Decimal k4(Decimal x, Decimal y)
        {
            return h * f(x + k3(x, y), y + l3(x, y));
        }

        Decimal l4(Decimal x, Decimal y)
        {
            return h * g(x + k3(x, y), y + l3(x, y));
        }
        #endregion
    }
}
