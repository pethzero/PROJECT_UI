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




        /// </summary>

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

        public static Dictionary<string, List<string>> FindDuplicateFilesWithProgress(
    string folderPath,
    string hashAlgo = "MD5",
    IProgress<int>? progress = null,
    CancellationToken? token = null)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException("ไม่พบโฟลเดอร์: " + folderPath);

            var result = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            var files = Directory.GetFiles(folderPath);
            int total = files.Length;

            for (int i = 0; i < total; i++)
            {
                token?.ThrowIfCancellationRequested();

                var file = files[i];
                try
                {
                    string hash = GetFileHash(file, hashAlgo);

                    if (!result.ContainsKey(hash))
                        result[hash] = new List<string>();

                    result[hash].Add(file);
                }
                catch (Exception ex)
                {
                    // ข้ามไฟล์ที่อ่านไม่ได้
                }

                // รายงาน progress
                progress?.Report((int)((i + 1) * 100.0 / total));
            }

            // คืนเฉพาะไฟล์ที่ซ้ำ (มีมากกว่า 1)
            return result.Where(r => r.Value.Count > 1)
                         .ToDictionary(r => r.Key, r => r.Value);
        }

        // ฟังก์ชันใหม่: ExportDuplicateToJsonWithProgress
        public static string ExportDuplicateToJsonWithProgress(
            string folderPath,
            string saveFolder,
            string hashAlgo = "MD5",
            IProgress<int>? progress = null,
            CancellationToken? token = null)
        {
            var duplicates = FindDuplicateFilesWithProgress(folderPath, hashAlgo, progress, token);

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


        /// <summary>
        /// เปรียบเทียบ 2 โฟลเดอร์ (โดยเทียบชื่อไฟล์)
        /// คืนค่า JSON-like model: detail_duplicate, detail_old, detail_new
        /// </summary>
        // ใช้ hash ของไฟล์ในการเทียบ
        public static object CompareFolderNew(string folderMaster, string folderSub, string hashAlgo = "MD5")
        {
            var masterFiles = Directory.GetFiles(folderMaster);
            var subFiles = Directory.GetFiles(folderSub);

            var masterHashes = masterFiles
                .Select(f => new { File = Path.GetFileName(f), Hash = GetFileHash(f, hashAlgo) })
                .ToList();

            var subHashes = subFiles
                .Select(f => new { File = Path.GetFileName(f), Hash = GetFileHash(f, hashAlgo) })
                .ToList();

            // 🔹 กลุ่มที่ซ้ำกับ master
            var duplicateWithMaster = subHashes
                .Where(sh => masterHashes.Any(mh => mh.Hash == sh.Hash))
                .GroupBy(sh => sh.Hash)
                .Select((grp, i) => new
                {
                    group = i + 1,
                    folder_master = masterHashes
                        .Where(mh => mh.Hash == grp.Key)
                        .Select(mh => mh.File)
                        .ToList(),
                    folder_sub = grp.Select(sh => sh.File).ToList()
                })
                .ToList();

            // 🔹 กลุ่มที่ไม่ซ้ำกับ master
            var subNotInMaster = subHashes
                .Where(sh => !masterHashes.Any(mh => mh.Hash == sh.Hash))
                .ToList();

            // แบ่งภายใน sub ที่ไม่ซ้ำ master
            var subDuplicateItself = subNotInMaster
                .GroupBy(sh => sh.Hash)
                .Where(grp => grp.Count() > 1)
                .Select((grp, i) => new
                {
                    group = i + 1,
                    detail = grp.Select(sh => sh.File).ToList()
                })
                .ToList();

            var subNoDuplicateItself = subNotInMaster
                .GroupBy(sh => sh.Hash)
                .Where(grp => grp.Count() == 1)
                .Select(grp => grp.First().File)
                .ToList();

            return new
            {
                folder_sub_duplicate_foldermaster = duplicateWithMaster,
                folder_sub_no_duplicate_foldermaster = new
                {
                    file_sub_duplicate_itself = subDuplicateItself,
                    file_sub_no_duplicate_itself = subNoDuplicateItself
                }
            };
        }


        // ต้องมี using System.Text; (อยู่แล้วในไฟล์ของคุณ)

        /// <summary>
        /// สร้างโฟลเดอร์ภายใต้ LocationSave และคัดลอกไฟล์ตาม compareResult
        /// compareResult ต้องมีโครงสร้างเดียวกับที่ CompareFolderNew คืน (folder_sub_no_duplicate_foldermaster...)
        /// คืนค่า path ของโฟลเดอร์ที่สร้าง
        /// </summary>
        public static string ExportSubFolderCompareResultToLocationSave(string folderSub, object compareResult, string locationSave)
        {

            // 2. สร้างชื่อโฟลเดอร์ใหม่: <ชื่อโฟลเดอร์sub>_yyyyMMdd
            string subName = Path.GetFileName(Path.GetFullPath(folderSub).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            if (string.IsNullOrWhiteSpace(subName)) subName = "folderSub";
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string newFolder = Path.Combine(locationSave, $"{subName}_{datePart}");
            Directory.CreateDirectory(newFolder);

            // 3. แปลง compareResult เป็น JSON string (pretty) และเขียนไฟล์ log ในโฟลเดอร์ใหม่
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(compareResult, options);
            File.WriteAllText(Path.Combine(newFolder, "compare_result.json"), json, Encoding.UTF8);

            // 4. Parse JSON เพื่อนำชื่อไฟล์มา copy
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            // ป้องกันกรณี property หาย ให้เช็คก่อน
            if (root.TryGetProperty("folder_sub_no_duplicate_foldermaster", out JsonElement subNoDupEl))
            {
                // 4.1 คัดลอก file_sub_no_duplicate_itself (copy ทุกตัว)
                if (subNoDupEl.TryGetProperty("file_sub_no_duplicate_itself", out JsonElement fileNoDupEl) &&
                    fileNoDupEl.ValueKind == JsonValueKind.Array)
                {
                    foreach (var e in fileNoDupEl.EnumerateArray())
                    {
                        string? fileName = e.GetString();
                        if (string.IsNullOrEmpty(fileName)) continue;
                        string src = Path.Combine(folderSub, fileName);
                        if (File.Exists(src))
                        {
                            string dest = Path.Combine(newFolder, fileName);
                            CopyWithRenameIfExists(src, dest);
                        }
                    }
                }

                // 4.2 คัดลอกไฟล์จาก file_sub_duplicate_itself (เฉพาะตัวแรกของแต่ละ group)
                if (subNoDupEl.TryGetProperty("file_sub_duplicate_itself", out JsonElement fileDupItselfEl) &&
                    fileDupItselfEl.ValueKind == JsonValueKind.Array)
                {
                    foreach (var grp in fileDupItselfEl.EnumerateArray())
                    {
                        // grp ควรมี property "detail" ซึ่งเป็น array รายชื่อไฟล์
                        if (grp.ValueKind != JsonValueKind.Object) continue;
                        if (!grp.TryGetProperty("detail", out JsonElement detailEl)) continue;
                        if (detailEl.ValueKind != JsonValueKind.Array) continue;

                        // เอาตัวแรกของ detail
                        var first = detailEl.EnumerateArray().Select(x => x.GetString()).FirstOrDefault(s => !string.IsNullOrEmpty(s));
                        if (string.IsNullOrEmpty(first)) continue;

                        string src = Path.Combine(folderSub, first);
                        if (File.Exists(src))
                        {
                            string dest = Path.Combine(newFolder, first);
                            CopyWithRenameIfExists(src, dest);
                        }
                    }
                }
            }
            else
            {
                // ถ้าไม่มี property ที่คาดไว้ จะไม่คัดลอกอะไร แต่ JSON file ถูกเขียนแล้ว
            }

            return newFolder;
        }

        /// <summary>
        /// คัดลอกไฟล์ ถ้ามีปลายทางอยู่แล้ว จะเติม _1, _2 ... ต่อท้ายชื่อไฟล์เพื่อไม่ให้ overwrite
        /// </summary>
        private static void CopyWithRenameIfExists(string src, string dest)
        {
            string dir = Path.GetDirectoryName(dest) ?? "";
            string nameOnly = Path.GetFileNameWithoutExtension(dest);
            string ext = Path.GetExtension(dest);

            string currentDest = dest;
            int counter = 1;
            while (File.Exists(currentDest))
            {
                currentDest = Path.Combine(dir, $"{nameOnly}_{counter}{ext}");
                counter++;
            }

            // สุดท้าย copy
            File.Copy(src, currentDest, false);
        }



        public static string ExportSubFolderCompareResultToLocationSaveWithProgress(
            string folderSub,
            object compareResult,
            string locationSave,
            IProgress<int>? progress = null,
            CancellationToken? token = null)
        {
            string subName = Path.GetFileName(Path.GetFullPath(folderSub).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            if (string.IsNullOrWhiteSpace(subName)) subName = "folderSub";
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string newFolder = Path.Combine(locationSave, $"{subName}_{datePart}");
            Directory.CreateDirectory(newFolder);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(compareResult, options);
            File.WriteAllText(Path.Combine(newFolder, "compare_result.json"), json, Encoding.UTF8);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            int totalFiles = 0;
            int copied = 0;

            // นับจำนวนไฟล์ทั้งหมดที่จะ copy
            if (root.TryGetProperty("folder_sub_no_duplicate_foldermaster", out JsonElement subNoDupEl))
            {
                if (subNoDupEl.TryGetProperty("file_sub_no_duplicate_itself", out JsonElement fileNoDupEl) &&
                    fileNoDupEl.ValueKind == JsonValueKind.Array)
                {
                    totalFiles += fileNoDupEl.GetArrayLength();
                }
                if (subNoDupEl.TryGetProperty("file_sub_duplicate_itself", out JsonElement fileDupItselfEl) &&
                    fileDupItselfEl.ValueKind == JsonValueKind.Array)
                {
                    totalFiles += fileDupItselfEl.GetArrayLength();
                }
            }

            // 4.1 คัดลอก file_sub_no_duplicate_itself (copy ทุกตัว)
            if (subNoDupEl.TryGetProperty("file_sub_no_duplicate_itself", out JsonElement fileNoDupEl2) &&
                fileNoDupEl2.ValueKind == JsonValueKind.Array)
            {
                foreach (var e in fileNoDupEl2.EnumerateArray())
                {
                    token?.ThrowIfCancellationRequested();
                    string? fileName = e.GetString();
                    if (string.IsNullOrEmpty(fileName)) continue;
                    string src = Path.Combine(folderSub, fileName);
                    if (File.Exists(src))
                    {
                        string dest = Path.Combine(newFolder, fileName);
                        CopyWithRenameIfExists(src, dest);
                    }
                    copied++;
                    progress?.Report((int)((copied * 100.0) / Math.Max(1, totalFiles)));
                }
            }

            // 4.2 คัดลอกไฟล์จาก file_sub_duplicate_itself (เฉพาะตัวแรกของแต่ละ group)
            if (subNoDupEl.TryGetProperty("file_sub_duplicate_itself", out JsonElement fileDupItselfEl2) &&
                fileDupItselfEl2.ValueKind == JsonValueKind.Array)
            {
                foreach (var grp in fileDupItselfEl2.EnumerateArray())
                {
                    token?.ThrowIfCancellationRequested();
                    if (grp.ValueKind != JsonValueKind.Object) continue;
                    if (!grp.TryGetProperty("detail", out JsonElement detailEl)) continue;
                    if (detailEl.ValueKind != JsonValueKind.Array) continue;

                    var first = detailEl.EnumerateArray().Select(x => x.GetString()).FirstOrDefault(s => !string.IsNullOrEmpty(s));
                    if (string.IsNullOrEmpty(first)) continue;

                    string src = Path.Combine(folderSub, first);
                    if (File.Exists(src))
                    {
                        string dest = Path.Combine(newFolder, first);
                        CopyWithRenameIfExists(src, dest);
                    }
                    copied++;
                    progress?.Report((int)((copied * 100.0) / Math.Max(1, totalFiles)));
                }
            }

            return newFolder;
        }


        /// <summary>
        /// ข้อมูลโฟลเดอร์พร้อมขนาดรวม (หน่วย byte)
        /// </summary>
        public class FolderSizeInfo
        {
            public string Path { get; set; } = "";
            public long SizeBytes { get; set; }
            public string SizeReadable => FormatBytes(SizeBytes);
        }

        /// <summary>
        /// คำนวณหาว่าโฟลเดอร์ใดกินพื้นที่มากที่สุด (top N)
        /// </summary>
        /// <param name="rootFolder">โฟลเดอร์หลัก เช่น D:\PROJECT</param>
        /// <param name="topN">จำนวน top folder ที่ต้องการ</param>
        public static List<FolderSizeInfo> ProcessFindTopFolder(string rootFolder, int topN = 5)
        {
            if (!Directory.Exists(rootFolder))
                throw new DirectoryNotFoundException($"ไม่พบโฟลเดอร์: {rootFolder}");

            var subFolders = Directory.GetDirectories(rootFolder);
            var results = new List<FolderSizeInfo>();

            Console.WriteLine($"🔍 กำลังวิเคราะห์ {subFolders.Length} โฟลเดอร์ย่อยใน {rootFolder}...");

            int count = 0;
            foreach (var folder in subFolders)
            {
                count++;
                try
                {
                    long size = GetDirectorySize(folder);
                    results.Add(new FolderSizeInfo { Path = folder, SizeBytes = size });
                    Console.WriteLine($"[{count}/{subFolders.Length}] {folder} -> {FormatBytes(size)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ อ่านโฟลเดอร์ไม่สำเร็จ: {folder} ({ex.Message})");
                }
            }

            var topFolders = results.OrderByDescending(r => r.SizeBytes)
                                    .Take(topN)
                                    .ToList();

            Console.WriteLine("\n📊 Top " + topN + " โฟลเดอร์ที่ใช้พื้นที่มากที่สุด:");
            foreach (var item in topFolders)
                Console.WriteLine($"- {item.Path} : {item.SizeReadable}");

            // (Optional) Export JSON ไว้ใน rootFolder
            string jsonPath = Path.Combine(rootFolder, "top_folders.json");
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(jsonPath, JsonSerializer.Serialize(topFolders, jsonOptions));

            Console.WriteLine($"\n✅ บันทึกผลไว้ที่: {jsonPath}");

            return topFolders;
        }

        /// <summary>
        /// คำนวณขนาดรวมของไฟล์ทั้งหมดในโฟลเดอร์ (รวม subfolder)
        /// </summary>
        private static long GetDirectorySize(string folder)
        {
            long size = 0;
            try
            {
                var files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);
                foreach (var f in files)
                {
                    try
                    {
                        var info = new FileInfo(f);
                        size += info.Length;
                    }
                    catch { }
                }
            }
            catch { }
            return size;
        }

        /// <summary>
        /// แปลง byte → หน่วยอ่านง่าย เช่น 12.3 GB
        /// </summary>
        private static string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }


        public class FileSizeInfo
        {
            public string Path { get; set; } = "";
            public long Size { get; set; }
            public string SizeReadable => Lib.FormatSize(Size);
        }

        public static List<FileSizeInfo> ProcessFindTopFiles(string rootFolder, int topN)
        {
            if (!Directory.Exists(rootFolder))
                throw new DirectoryNotFoundException("ไม่พบโฟลเดอร์: " + rootFolder);

            var files = Directory
                .EnumerateFiles(rootFolder, "*", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .Select(fi => new FileSizeInfo { Path = fi.FullName, Size = fi.Length })
                .OrderByDescending(f => f.Size)
                .Take(topN)
                .ToList();

            return files;
        }

        /// <summary>
        /// แปลงขนาดไฟล์เป็นหน่วยอ่านง่าย เช่น KB, MB, GB
        /// </summary>
        public static string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }




        public static List<FileSizeInfo> ProcessFindTopFilesWithProgress(string rootFolder, int topN, IProgress<int> progress, CancellationToken token)
        {
            var allFiles = Directory.EnumerateFiles(rootFolder, "*", SearchOption.AllDirectories).ToList();
            int total = allFiles.Count;
            int processed = 0;

            var results = new List<FileSizeInfo>();

            foreach (var file in allFiles)
            {
                if (token.IsCancellationRequested)
                {
                    // รายงาน progress แล้วหยุดเงียบ ๆ
                    progress?.Report(100);
                    break;
                }
                try
                {
                    var fi = new FileInfo(file);
                    results.Add(new FileSizeInfo { Path = fi.FullName, Size = fi.Length });
                }
                catch { /* ข้ามไฟล์ที่เข้าไม่ได้ */ }

                processed++;
                int percent = (int)((processed / (double)total) * 100);
                progress?.Report(percent);
            }

            return results
                .OrderByDescending(f => f.Size)
                .Take(topN)
                .ToList();
        }

        public static string ExportTopFileResult(List<FileSizeInfo> files, string sourceFolder, string saveFolder)
        {
            string exportPath = Path.Combine(saveFolder, $"TopFiles_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            using (var sw = new StreamWriter(exportPath, false, Encoding.UTF8))
            {
                sw.WriteLine($"📁 Top Files from: {sourceFolder}");
                sw.WriteLine($"Generated: {DateTime.Now}");
                sw.WriteLine(new string('-', 70));

                int index = 1;
                long totalSize = 0;

                foreach (var file in files)
                {
                    sw.WriteLine($"{index}. {file.Path}");
                    sw.WriteLine($"   Size: {Lib.FormatSize(file.Size)}");
                    sw.WriteLine();
                    totalSize += file.Size;
                    index++;
                }

                sw.WriteLine(new string('-', 70));
                sw.WriteLine($"Total Size: {Lib.FormatSize(totalSize)}");
            }

            return exportPath;
        }



        public static List<FolderSizeInfo> ProcessFindTopFolderWithProgress(
            string rootFolder, int topN, IProgress<int> progress, CancellationToken token)
        {
            var allFolders = Directory.EnumerateDirectories(rootFolder, "*", SearchOption.AllDirectories).ToList();
            int total = allFolders.Count;
            int processed = 0;

            var results = new List<FolderSizeInfo>();

            foreach (var folder in allFolders)
            {
                if (token.IsCancellationRequested)
                {
                    // รายงาน progress แล้วหยุดเงียบ ๆ
                    progress?.Report(100);
                    break;
                }

                try
                {
                    long folderSize = GetFolderSize(folder, token);
                    results.Add(new FolderSizeInfo { Path = folder, SizeBytes = folderSize });
                }
                catch
                {
                    // ข้ามโฟลเดอร์ที่เข้าถึงไม่ได้
                }

                processed++;
                int percent = (int)((processed / (double)total) * 100);
                progress?.Report(percent);
            }

            return results
                .OrderByDescending(f => f.SizeBytes)
                .Take(topN)
                .ToList();
        }

        private static long GetFolderSize(string folder, CancellationToken token)
        {
            long size = 0;

            try
            {
                foreach (var file in Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories))
                {
                    token.ThrowIfCancellationRequested();

                    try
                    {
                        var fi = new FileInfo(file);
                        size += fi.Length;
                    }
                    catch
                    {
                        // ไฟล์เข้าถึงไม่ได้
                    }
                }
            }
            catch
            {
                // โฟลเดอร์เข้าถึงไม่ได้
            }

            return size;
        }


        public static string ExportTopFolderResult(List<FolderSizeInfo> folders, string sourceFolder, string saveFolder)
        {
            string exportPath = Path.Combine(saveFolder, $"TopFolders_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            using (var sw = new StreamWriter(exportPath, false, Encoding.UTF8))
            {
                sw.WriteLine($"📂 Top Folders from: {sourceFolder}");
                sw.WriteLine($"Generated: {DateTime.Now}");
                sw.WriteLine(new string('-', 70));

                int index = 1;
                long totalSize = 0;

                foreach (var folder in folders)
                {
                    sw.WriteLine($"{index}. {folder.Path}");
                    sw.WriteLine($"   Size: {FormatSize(folder.SizeBytes)}");
                    sw.WriteLine();
                    totalSize += folder.SizeBytes;
                    index++;
                }

                sw.WriteLine(new string('-', 70));
                sw.WriteLine($"Total Size: {FormatSize(totalSize)}");
            }

            return exportPath;
        }





    }
}

