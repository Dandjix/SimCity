using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;

public static class ScuffedCompression
{
    public static bool WriteCompressed(string path, string content)
    {
        try
        {
            // Convert the string to bytes
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);

            // Open the file stream to write
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                // Use GZipStream to compress and write the data
                using (GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Compress))
                {
                    compressionStream.Write(contentBytes, 0, contentBytes.Length);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error while writing compressed data: {ex.Message}");
            return false;
        }
    }

    public static bool ReadCompressed(string path, out string content)
    {
        content = "";
        try
        {
            // Open the file stream to read
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // Use GZipStream to read and decompress the data
                using (GZipStream decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        decompressionStream.CopyTo(memoryStream);
                        byte[] decompressedBytes = memoryStream.ToArray();

                        // Convert the decompressed bytes back to a string
                        content = Encoding.UTF8.GetString(decompressedBytes);
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error while reading compressed data: {ex.Message}");
            return false;
        }
    }
}
