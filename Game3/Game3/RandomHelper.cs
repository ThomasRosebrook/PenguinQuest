using System;

namespace Game3
{
    public static class RandomHelper
    {
        static Random random = new Random();

        /// <summary>
        /// Returns a non negative random integer
        /// </summary>
        /// <returns>an integer between 0 (inclusive) and max integer value</returns>
        public static int Next() => random.Next();

        /// <summary>
        /// Returns a non negative random float
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static float NextFloat(float minValue, float maxValue)
        {
            return minValue + (float)random.NextDouble() * (maxValue - minValue);
        }

        /// <summary>
        /// Returns a number between 0 (inclusive) and a given max value (exclusive)
        /// </summary>
        /// <param name="maxValue">the maximum possible integer to return (exclusive)</param>
        /// <returns>an integer between 0 (inclusive) and the maxValue (exclusive)</returns>
        public static int Next(int maxValue) => random.Next(maxValue);

        /// <summary>
        /// Returns a number between a given minimum value (inclusive) and a given max value (exclusive)
        /// </summary>
        /// <param name="minValue">the minimum possible integer to return (inclusive)</param>
        /// <param name="maxValue">the maximum value limit for integers to return</param>
        /// <returns>an integer between the minValue (inclusive) and the maxValue (exclusive)</returns>
        public static int Next(int minValue, int maxValue) => random.Next(minValue, maxValue + 1);
    }
}
