﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareLawInterpolator
{
    class Interpolator
    {
        private List<Point> Points = new List<Point>();
        private float A, B, C;
        private float MaxAmp, MinAmp;

        public void Initialize()
        {
            Console.WriteLine("Введите 3 точки в формате: (ток, А) (кол-во отключений)");
            for (int i = 1; i <= 3; i++)
            {
                Console.Write("Точка №{0}: ", i);
                string line = Console.ReadLine();
                line.Trim();
                string[] nums = line.Split(' ');
                Points.Add(new Point(float.Parse(nums[0]), float.Parse(nums[1])));
                Console.WriteLine();
            }
            GetParams();
            MaxAmp = (GetMax(Points) as Point).Amp;
            MinAmp = (GetMin(Points) as Point).Amp;
            Console.WriteLine("Введите количество промежуточных точек в диапазоне");
            Console.WriteLine("в диапазоне от {0} до {1}!", MinAmp, MaxAmp);
            GetResults(int.Parse(Console.ReadLine()));
        }
        private void GetResults(int PointsNumber)
        {
            if (PointsNumber > 3)
            {
                Console.WriteLine("Промежуточные точки графика:");
                int StepValue = (int)((MaxAmp - MinAmp) / (PointsNumber - 3));
                int StepNumber = 1;
                while (GetAmp(StepValue, StepNumber) < MaxAmp)
                {
                    Points.Add(Result(GetAmp(StepValue, StepNumber)));
                    StepNumber++;
                }
                Points.Sort();
                foreach (Point p in Points)
                {
                    p.ToString();
                }
            }
            else
            {
                Console.WriteLine("Количество должно быть >3 !");
                GetResults(PointsNumber);
            }

        }
        private float GetAmp(int step, int num)
        {
            return (num * step + MinAmp);
        }
        private Point Result(float amp)
        {
            float count = A * amp * amp + B * amp + C;
            return new Point(amp, count);
        }

        private void GetParams()
        {
            GetA();
            GetB();
            GetC();
        }

        private void GetA()
        {
            float one = (Points[2].Count - Points[0].Count) / ((Points[2].Amp - Points[0].Amp) * (Points[2].Amp - Points[1].Amp));
            float two = (Points[1].Count - Points[0].Count) / ((Points[1].Amp - Points[0].Amp) * (Points[2].Amp - Points[1].Amp));
            A = one - two;
        }

        private void GetB()
        {
            float one = (Points[1].Count - Points[0].Count) / (Points[1].Amp - Points[0].Amp);
            float two = A * (Points[1].Amp + Points[0].Amp);
            B = one - two;
        }

        private void GetC()
        {
            C = Points[0].Count - B * Points[0].Amp - A * Points[0].Amp;
        }
        public void Reset()
        {
            Points.Clear();
            Initialize();
        }

        private T GetMax<T>(List<T> list) where T : IComparable
        {
            T result = list[0];
            foreach (T p in list)
            {
                if (result.CompareTo(p) > 0)
                    result = p;
            }
            return result;

        }

        private T GetMin<T>(List<T> list) where T : IComparable
        {
            T result = list[0];
            foreach (T p in list)
            {
                if (result.CompareTo(p) < 0)
                    result = p;
            }
            return result;

        }
    }
}
