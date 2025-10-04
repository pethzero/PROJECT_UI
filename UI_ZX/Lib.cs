using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace UI_ZX
{
    internal static class Lib
    {

        public class DuplicateGroup
        {
            public int group { get; set; }
            public List<string> list { get; set; } = new List<string>();
            public int total { get; set; }
        }

        public class DuplicateData
        {
            public List<DuplicateGroup> detail { get; set; } = new List<DuplicateGroup>();
            public int total_all { get; set; }
        }

        public class DuplicateResult
        {
            public DuplicateData data { get; set; } = new DuplicateData();
        }


        /// <summary>
        /// คำนวณ Hash ของไฟล์ (รองรับ MD5, SHA1, SHA256, SHA512)
        /// </summary>
        public static string GetFileHash(string filePath, string hashAlgo = "MD5")
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("ไม่พบไฟล์", filePath);

            using var stream = File.OpenRead(filePath);
            using var algo = HashAlgorithm.Create(hashAlgo.ToUpper());

            if (algo == null)
                throw new ArgumentException("ไม่รองรับอัลกอริทึมนี้: " + hashAlgo);

            byte[] hashBytes = algo.ComputeHash(stream);

            // แปลงเป็น string hex
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        /// <summary>
        /// เปรียบเทียบไฟล์ 2 ไฟล์ว่าเหมือนกันหรือไม่ (ด้วย Hash)
        /// </summary>
        public static bool CompareFiles(string file1, string file2, string hashAlgo = "MD5")
        {
            string hash1 = GetFileHash(file1, hashAlgo);
            string hash2 = GetFileHash(file2, hashAlgo);

            return string.Equals(hash1, hash2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// ค้นหาไฟล์ที่ซ้ำกันในโฟลเดอร์ (ตามค่า Hash)
        /// คืนค่าเป็น Dictionary<string, List<string>>
        /// Key = Hash, Value = รายการไฟล์ที่มีค่า Hash นั้น
        /// </summary>
        public static Dictionary<string, List<string>> FindDuplicateFiles(string folderPath, string hashAlgo = "MD5")
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException("ไม่พบโฟลเดอร์: " + folderPath);

            var result = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            foreach (var file in Directory.GetFiles(folderPath))
            {
                try
                {
                    string hash = GetFileHash(file, hashAlgo);

                    if (!result.ContainsKey(hash))
                        result[hash] = new List<string>();

                    result[hash].Add(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ไฟล์ {file} อ่านไม่สำเร็จ: {ex.Message}");
                }
            }

            // คืนเฉพาะไฟล์ที่ซ้ำ (มีมากกว่า 1)
            return result.Where(r => r.Value.Count > 1)
                         .ToDictionary(r => r.Key, r => r.Value);
        }


        /// <summary>
        /// Export รายงานไฟล์ซ้ำเป็น JSON
        /// </summary>
        public static string ExportDuplicateToJson(string folderPath, string saveFolder, string hashAlgo = "MD5")
        {
            var duplicates = FindDuplicateFiles(folderPath, hashAlgo);

            var result = new DuplicateResult();
            int groupIndex = 1;
            int totalAll = 0;

            foreach (var kv in duplicates)
            {
                var group = new DuplicateGroup
                {
                    group = groupIndex,
                    list = kv.Value.Select(Path.GetFileName).ToList(),
                    total = kv.Value.Count
                };

                result.data.detail.Add(group);
                totalAll += group.total;
                groupIndex++;
            }

            result.data.total_all = totalAll;

            // แปลงเป็น JSON (format สวยงาม)
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(result, options);

            // กำหนดชื่อไฟล์ เช่น duplicates.json
            string fileName = Path.Combine(saveFolder, "duplicates.json");
            File.WriteAllText(fileName, json);

            return fileName; // คืน path ที่ save
        }
    }
}
