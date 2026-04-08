using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab6_task_1_and_2
{
    /// <summary>
    /// Тема 6. Файлове введення-виведення.
    /// 1 — ієрархічна структура каталогу (файли та папки).
    /// 2 — пошук заданого файлу, повна інформація про місце розташування кожного збігу.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Тема 6. Файлове введення-виведення ===");
                Console.WriteLine("1 — Завдання 1: ієрархічна структура певної директорії (файли та папки)");
                Console.WriteLine("2 — Завдання 2: пошук заданого файлу; повна інформація про знайдені екземпляри");
                Console.WriteLine("0 — вихід");
                Console.Write("Оберіть пункт: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine();
                        RunTask1_DirectoryTree();
                        break;
                    case "2":
                        Console.WriteLine();
                        RunTask2_FileSearch();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невідомий пункт. Введіть 1, 2 або 0.");
                        break;
                }
            }
        }

        #region Спільна робота зі шляхами

        /// <summary>
        /// Прибирає символи, через які Path / Directory.Exists кидають «Illegal characters in path»
        /// (керуючі коди, таб, перенос рядка після копіювання, «невидимі» напрямкові символи юнікоду тощо).
        /// </summary>
        static string StripIllegalPathCharacters(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;
            var invalid = Path.GetInvalidPathChars();
            var sb = new StringBuilder(path.Length);
            foreach (var c in path)
            {
                if (Array.IndexOf(invalid, c) >= 0)
                    continue;
                if (c >= '\u202A' && c <= '\u202E')
                    continue;
                if (c >= '\u2066' && c <= '\u2069')
                    continue;
                sb.Append(c);
            }
            return sb.ToString();
        }

        static string SanitizeClipboardPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;
            path = path.Trim();
            while (path.Length >= 2 && path[0] == '"' && path[path.Length - 1] == '"')
                path = path.Substring(1, path.Length - 2).Trim();
            path = path.Trim('\u201C', '\u201D', '\'', '`', '\u00B4');
            foreach (var c in new[] { '\uFEFF', '\u200B', '\u200E', '\u200F' })
                path = path.Replace(new string(c, 1), string.Empty);
            path = path.Replace('\u00A0', ' ').Replace('\u2007', ' ').Replace('\u202F', ' ');
            path = StripIllegalPathCharacters(path);
            path = path.Trim();
            return string.IsNullOrEmpty(path) ? null : path;
        }

        /// <summary>
        /// Перетворює введення на існуючий каталог: або сам шлях до папки, або батьківська папка, якщо вказано файл.
        /// </summary>
        static bool TryResolveToExistingDirectory(string userInput, out string directoryFullPath, out string infoNote)
        {
            directoryFullPath = null;
            infoNote = null;
            var s = SanitizeClipboardPath(userInput);
            if (s == null)
                return false;

            s = Environment.ExpandEnvironmentVariables(s);
            s = StripIllegalPathCharacters(s).Trim();
            if (string.IsNullOrEmpty(s))
                return false;

            try
            {
                if (Directory.Exists(s))
                {
                    directoryFullPath = new DirectoryInfo(s).FullName;
                    return true;
                }

                if (File.Exists(s))
                {
                    directoryFullPath = new FileInfo(s).DirectoryName;
                    infoNote = "Вказано шлях до файлу (не до папки). Показуємо каталог, де він лежить: " + directoryFullPath;
                    return !string.IsNullOrEmpty(directoryFullPath);
                }

                string viaCwd;
                try
                {
                    viaCwd = Path.GetFullPath(s);
                }
                catch (ArgumentException)
                {
                    return false;
                }

                if (Directory.Exists(viaCwd))
                {
                    directoryFullPath = new DirectoryInfo(viaCwd).FullName;
                    return true;
                }

                if (File.Exists(viaCwd))
                {
                    directoryFullPath = new FileInfo(viaCwd).DirectoryName;
                    infoNote = "Вказано шлях до файлу. Використовуємо каталог: " + directoryFullPath;
                    return !string.IsNullOrEmpty(directoryFullPath);
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }

            return false;
        }

        static void PrintPathResolveHint(string rawInput)
        {
            try
            {
                var clean = SanitizeClipboardPath(rawInput);
                if (clean != null)
                {
                    var expanded = Environment.ExpandEnvironmentVariables(clean);
                    expanded = StripIllegalPathCharacters(expanded).Trim();
                    if (!string.IsNullOrEmpty(expanded))
                        Console.WriteLine("Розпізнаний шлях після очищення: " + Path.GetFullPath(expanded));
                }
            }
            catch
            {
                Console.WriteLine("(Не вдалося показати нормалізований шлях — у рядку можуть бути зайві символи після копіювання.)");
            }

            Console.WriteLine("Підказка: у Провіднику відкрийте папку → клац по рядку шляху → Ctrl+C; або Shift+ПКМ по папці → «Копіювати як шлях».");
        }

        static string NormalizeSearchFileName(string line)
        {
            var s = SanitizeClipboardPath(line);
            if (s == null)
                return null;
            s = s.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var name = Path.GetFileName(s);
            return string.IsNullOrEmpty(name) ? null : name;
        }

        /// <summary>
        /// Рядок виглядає як ім'я файлу без шляху (типово переплутаний з полем «папка»).
        /// </summary>
        static bool LooksLikeFileNameOnly(string s)
        {
            s = SanitizeClipboardPath(s);
            if (s == null)
                return false;
            if (s.IndexOfAny(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }) >= 0)
                return false;
            if (s.Length >= 2 && s[1] == ':')
                return false;
            return true;
        }

        /// <summary>
        /// Стандартно: рядок A — каталог (або файл у каталозі), рядок B — ім'я шуканого файлу.
        /// Якщо місцями переплутано (ім'я у A, папка у B) — виправляємо автоматично.
        /// </summary>
        static bool TryParseSearchInputs(string lineA, string lineB, out string rootDirectory, out string fileName, out string autoFixNote)
        {
            rootDirectory = null;
            fileName = null;
            autoFixNote = null;

            if (TryResolveToExistingDirectory(lineA, out rootDirectory, out var noteA))
            {
                fileName = NormalizeSearchFileName(lineB);
                if (string.IsNullOrEmpty(fileName))
                    return false;
                if (noteA != null)
                    autoFixNote = noteA;
                return true;
            }

            if (TryResolveToExistingDirectory(lineB, out rootDirectory, out var noteB)
                && LooksLikeFileNameOnly(lineA))
            {
                fileName = NormalizeSearchFileName(lineA);
                if (string.IsNullOrEmpty(fileName))
                    return false;
                autoFixNote = "Рядки було переплутано: взято папку з другого поля, ім'я файлу — з першого.";
                if (noteB != null)
                    autoFixNote += Environment.NewLine + noteB;
                return true;
            }

            return false;
        }

        #endregion

        #region Завдання 1

        /// <summary>
        /// Завдання 1. Вивід на екран усієї ієрархічної структури заданої директорії (файли та папки).
        /// </summary>
        static void RunTask1_DirectoryTree()
        {
            Console.WriteLine("--- Завдання 1. Ієрархія каталогу ---");
            Console.WriteLine("Потрібен шлях до папки, структуру якої показати.");
            Console.WriteLine("(Якщо вставите шлях до файлу, буде показано папку, що його містить.)");
            Console.Write("Шлях: ");
            var raw = Console.ReadLine();

            if (!TryResolveToExistingDirectory(raw, out var root, out var note))
            {
                Console.WriteLine("Каталог не знайдено або шлях некоректний.");
                PrintPathResolveHint(raw);
                return;
            }

            if (note != null)
                Console.WriteLine(note);

            Console.WriteLine();
            Console.WriteLine("Ієрархія від: " + root);
            Console.WriteLine(new string('─', 60));

            try
            {
                PrintTree(root, "", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка виводу: " + ex.Message);
            }
        }

        static void PrintTree(string directoryPath, string indent, bool isLast)
        {
            var label = Path.GetFileName(directoryPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            if (string.IsNullOrEmpty(label))
                label = directoryPath;

            Console.WriteLine(indent + (isLast ? "└── " : "├── ") + label + Path.DirectorySeparatorChar);
            var childIndent = indent + (isLast ? "    " : "│   ");

            string[] subdirs;
            try
            {
                subdirs = Directory.GetDirectories(directoryPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(childIndent + "[підкаталоги недоступні: " + ex.Message + "]");
                subdirs = Array.Empty<string>();
            }

            string[] files;
            try
            {
                files = Directory.GetFiles(directoryPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(childIndent + "[файли недоступні: " + ex.Message + "]");
                files = Array.Empty<string>();
            }

            var orderedDirs = subdirs.OrderBy(d => d, StringComparer.OrdinalIgnoreCase).ToList();
            var orderedFiles = files.OrderBy(f => f, StringComparer.OrdinalIgnoreCase).ToList();
            int total = orderedDirs.Count + orderedFiles.Count;
            int index = 0;

            foreach (var dir in orderedDirs)
            {
                index++;
                PrintTree(dir, childIndent, index == total);
            }

            foreach (var file in orderedFiles)
            {
                index++;
                Console.WriteLine(childIndent + (index == total ? "└── " : "├── ") + Path.GetFileName(file));
            }
        }

        #endregion

        #region Завдання 2

        /// <summary>
        /// Завдання 2. Пошук заданого файлу; повна інформація про місце розташування знайдених екземплярів.
        /// </summary>
        static void RunTask2_FileSearch()
        {
            Console.WriteLine("--- Завдання 2. Пошук файлу ---");
            Console.WriteLine("A) Папка, у якій шукати (рекурсивно, включно з підпапками).");
            Console.WriteLine("B) Ім'я файлу — як у Провіднику (з розширенням), наприклад «Firefox.exe» або «readme.txt».");
            Console.WriteLine();
            Console.Write("A. Папка: ");
            var lineA = Console.ReadLine();
            Console.Write("B. Ім'я файлу: ");
            var lineB = Console.ReadLine();

            if (!TryParseSearchInputs(lineA, lineB, out var root, out var searchName, out var fixNote))
            {
                Console.WriteLine();
                Console.WriteLine("Не вдалося визначити папку пошуку або ім'я файлу.");
                Console.WriteLine("Очікується: A — шлях до існуючої папки; B — лише ім'я файлу (або повний шлях — візьметься лише ім'я).");
                if (!TryResolveToExistingDirectory(lineA, out _, out _))
                    PrintPathResolveHint(lineA);
                return;
            }

            if (fixNote != null)
                Console.WriteLine(fixNote);

            Console.WriteLine();
            Console.WriteLine("Пошук «" + searchName + "» від каталогу: " + root);

            var found = new List<string>();
            try
            {
                SearchFilesRecursive(root, searchName, found);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка під час пошуку: " + ex.Message);
                return;
            }

            Console.WriteLine();
            if (found.Count == 0)
            {
                Console.WriteLine("Файлів з іменем «" + searchName + "» не знайдено.");
                return;
            }

            Console.WriteLine("Знайдено екземплярів: " + found.Count);
            Console.WriteLine(new string('─', 60));

            int n = 1;
            foreach (var fullPath in found.OrderBy(p => p, StringComparer.OrdinalIgnoreCase))
                PrintFoundFileDetails(n++, fullPath);
        }

        static void SearchFilesRecursive(string directory, string fileNameExact, List<string> results)
        {
            string[] files;
            try
            {
                files = Directory.GetFiles(directory);
            }
            catch (Exception)
            {
                return;
            }

            foreach (var f in files)
            {
                if (string.Equals(Path.GetFileName(f), fileNameExact, StringComparison.OrdinalIgnoreCase))
                    results.Add(f);
            }

            string[] subdirs;
            try
            {
                subdirs = Directory.GetDirectories(directory);
            }
            catch (Exception)
            {
                return;
            }

            foreach (var sub in subdirs)
                SearchFilesRecursive(sub, fileNameExact, results);
        }

        /// <summary>
        /// Повна інформація про місце розташування знайденого файлу (шлях, том, каталог, властивості).
        /// </summary>
        static void PrintFoundFileDetails(int index, string fullPath)
        {
            Console.WriteLine();
            Console.WriteLine("─── Екземпляр №" + index + " ───");
            Console.WriteLine("Повний шлях (розташування): " + fullPath);

            try
            {
                var fi = new FileInfo(fullPath);
                Console.WriteLine("Корінь диска / ресурсу:     " + Path.GetPathRoot(fi.FullName));
                Console.WriteLine("Каталог (папка):            " + fi.DirectoryName);
                Console.WriteLine("Ім'я файлу:                 " + fi.Name);
                Console.WriteLine("Розширення:                 " + fi.Extension);
                Console.WriteLine("Розмір:                     " + fi.Length + " байт (" + (fi.Length / 1024.0).ToString("N2") + " КіБ)");
                Console.WriteLine("Створено:                   " + fi.CreationTime);
                Console.WriteLine("Остання зміна:              " + fi.LastWriteTime);
                Console.WriteLine("Останній доступ:            " + fi.LastAccessTime);
                Console.WriteLine("Атрибути:                   " + fi.Attributes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не вдалося зчитати властивості: " + ex.Message);
            }
        }

        #endregion
    }
}
