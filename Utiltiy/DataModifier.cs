using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Utiltiy
{
    public static class DataModifier
    {
        public static string GenerateRandom()
        {
            Random random = new Random();
            string genRandom = random.Next(1503, 2599).ToString();
            return genRandom;
        }

        public static long  GenerateRandomNumber()
        {
            Random random = new Random();
            long genRandom = DateTime.Now.Ticks + random.Next(1530, 2590);
            return genRandom;
        }




    }
}
