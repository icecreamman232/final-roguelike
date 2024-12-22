
using System;
using System.Text;
using MoreMountains.Tools;
using UnityEngine;
using Random = System.Random;

namespace SGGames.Scripts.Common
{
    public static class RandomController
    {
        private static Random m_random;

        private static byte[] m_rawSeedArray = new byte[5];
        private static string m_seed;

        public static string GetSeed()
        {
            return m_seed;
        }

        public static void SetSeed()
        {
            m_rawSeedArray[0] = (byte)UnityEngine.Random.Range(48, 58); // 0-9
            m_rawSeedArray[1] = (byte)UnityEngine.Random.Range(48, 58); // 0-9
            m_rawSeedArray[2] = (byte)UnityEngine.Random.Range(65, 91); //A-Z (Capitalized)
            m_rawSeedArray[3] = (byte)UnityEngine.Random.Range(65, 91); //A-Z (Capitalized)
            m_rawSeedArray[4] = (byte)UnityEngine.Random.Range(65, 91); //A-Z (Capitalized)

            //Convert raw seed to text-type seed
            m_seed = ASCII_TO_STRING(new ReadOnlySpan<byte>(m_rawSeedArray));
            
            var sum = 0;
            foreach (var seedValue in m_rawSeedArray)
            {
                sum += seedValue;
            }
            
            m_random = new Random(sum);
            Debug.Log($"SEED {m_seed}");
        }

        public static string ASCII_TO_STRING(ReadOnlySpan<byte> input)
        {
            return Encoding.ASCII.GetString(input);
        }
        
        public static string GetUniqueID()
        {
            return m_random.Next(Int32.MaxValue).ToString();
        }
    }
}
