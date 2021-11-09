using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Utils
{
    public static class RandomNumber
    {
        public static double GetRandomNumber(double minium, double maxium)
        {
            Random random = new Random();
            double r = random.NextDouble() * (maxium - minium) + minium;
            return Math.Round(r, 2);
        }
        public static int GetRandomNumber(int minium, int maxium)
        {
            Random random = new Random();
            return random.Next(minium, maxium);
        }
    }
}
