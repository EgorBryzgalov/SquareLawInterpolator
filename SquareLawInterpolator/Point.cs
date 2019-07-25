using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareLawInterpolator
{
    class Point : IComparer<Point>, IComparable<Point>
    {
        public float Amp { get; set; }
        public float Count { get; set; }


        public int Compare(Point x, Point y)
        {
            return x.Amp.CompareTo(y.Amp);
        }

        int IComparable<Point>.CompareTo(Point other)
        {
            return this.Amp.CompareTo(other.Amp);
        }

        public Point(float amp, float count)
        {
            Amp = amp;
            Count = count;
        }

        public override string ToString()
        {
            return string.Format("[Ток={0}, Комм.={1}]", Amp, Count);
        }

    }
}
