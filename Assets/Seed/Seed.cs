//using System.Security.Cryptography;
//using System.Text;
//using System;
//using UnityEngine;

//public static class Seed 
//{
//    private const int seedLength = 32;
//    private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

//    public static string GenerateSeed()
//    {
//        StringBuilder sb = new StringBuilder(seedLength);
//        for (int i = 0; i < seedLength; i++)
//        {
//            int index = UnityEngine.Random.Range(0, chars.Length);
//            sb.Append(chars[index]);
//        }
//        return sb.ToString();
//    }

//    public static int Random(string seed,string offset = "")
//    {
//        int random = GenerateNumericSeed(seed+offset);
//        return random;
//    }

//    private static int GenerateNumericSeed(string seedString)
//    {
//        // Create a hash of the seed string
//        using (var md5 = MD5.Create())
//        {
//            // Compute hash bytes
//            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(seedString));

//            // Convert first 4 bytes of the hash to an integer
//            // Ensure to use BitConverter.ToInt32 and consider endianness
//            int seed = BitConverter.ToInt32(hashBytes, 0);

//            // Return the seed, ensuring it's positive
//            return Math.Abs(seed);
//        }
//    }
//}
