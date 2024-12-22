
using System;
using UnityEngine;
using Random = System.Random;

namespace SGGames.Scripts.Common
{
    public static class RandomController
    {
        private static Random m_random;

        private static int m_seed;

        public static int GetSeed()
        {
            return m_seed;
        }

        public static void SetSeed(int seed)
        {
            m_seed = seed;
            m_random = new Random(seed);
            Debug.Log($"SEED {seed}");
        }
        
        public static string GetUniqueID()
        {
            return m_random.Next(Int32.MaxValue).ToString();
        }
    }
}
