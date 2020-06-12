using System;
using System.Collections.Generic;

namespace difury
{
    class Rub_Kutt
    {
        Decimal p;
        Decimal h;

        public IEnumerable<PointD> GetPoints(Decimal x0, Decimal y0, Decimal p, Decimal h)
        {
            this.p = p;
            this.h = h;

            Decimal prev_x = x0;
            Decimal prev_y = y0;

            Decimal next_x;
            Decimal next_y;

            while (true)
            {
                next_x = prev_x + (k1(prev_x, prev_y) + 2 * k2(prev_x, prev_y) + 2 * k3(prev_x, prev_y) + k4(prev_x, prev_y)) / 6;
                next_y = prev_y + (l1(prev_x, prev_y) + 2 * l2(prev_x, prev_y) + 2 * l3(prev_x, prev_y) + l4(prev_x, prev_y)) / 6;
                yield return new PointD(next_x, next_y);
                prev_x = next_x;
                prev_y = next_y;
            }
        
        }

        #region
        Decimal f(Decimal a, Decimal b)
        {
            return 1 - a * b;
        }

        Decimal g(Decimal a, Decimal b)
        {
            return p * b * (a - 2 / (b + 1));
        }

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
