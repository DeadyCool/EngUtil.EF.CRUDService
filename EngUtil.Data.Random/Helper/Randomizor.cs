using System;
using System.Collections.Generic;

namespace EngUtil.Mock.Helper
{
    public static class Randomizor
    {
        private static readonly Random _random = new Random(Environment.TickCount);
        private static readonly object _syncLock = new object();

        public static int RandomNumber(int min, int max)
        {
            lock(_syncLock)
            {
                return _random.Next(min, max);
            }
        }

        public static List<T> ShufflizeList<T>(List<T> inputList)
        {
            List<T> randomList = new List<T>();
            while (inputList.Count > 0)
            {
                var randomIndex = RandomNumber(0, inputList.Count);
                randomList.Add(inputList[randomIndex]);
                inputList.RemoveAt(randomIndex);
            }
            return randomList;
        }
    }
}
