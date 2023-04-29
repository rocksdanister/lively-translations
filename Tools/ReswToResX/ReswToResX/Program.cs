using System.Data;

namespace ReswToResX
{
    /// <summary>
    /// Convert Crowdin Lively.WinUI.UI resw -> Lively core resx.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            string srcDir = @"D:\Translations\Resw";
            string destDir = @"D:\Translations\Resx";

            string[] dirs = Directory.GetDirectories(srcDir, "*", SearchOption.TopDirectoryOnly);
            foreach (var dir in dirs)
            {
                var name = Path.GetFileName(dir);
                if (map.TryGetValue(name, out var value))
                {
                    //Resources.resw -> Resources.value.resx
                    var src = Path.Combine(dir, "Resources.resw");
                    var dest = Path.Combine(destDir, $"Resources.{value}.resx");
                    File.Copy(src, dest, false);
                    Console.WriteLine($"SUCCESS: {Path.GetFileName(dir)} -> {Path.GetFileName(dest)}");
                }
                else
                {
                    Console.WriteLine($"WARNING: {Path.GetFileName(dir)} not found");
                }
            }
            Console.ReadKey();
        }

        static readonly IDictionary<string, string> map = new Dictionary<string, string>(){
            {"af-ZA", "af-ZA"},
            {"ar-AE", "ar"},
            {"az-Latn", "az"},
            {"bg-BG", "bg"},
            {"ca-ES", "ca"},
            {"cs-CZ", "cs-CZ"},
            {"da-DK", "da-DK"},
            {"de-DE", "de"},
            {"el-GR", "el"},
            {"es-ES", "es"},
            {"es-MX", "es-MX"},
            {"fa-IR", "fa-IR"},
            {"fi-FI", "fi"},
            {"fil-PH", "fil"},
            {"fr-FR", "fr"},
            {"he-IL", "he"},
            {"hi-IN", "hi"},
            {"hu-HU", "hu"},
            {"id-ID", "id"},
            {"it-IT", "it"},
            {"ms-MY", "ms"},
            {"nb-NO", "nb-NO"},
            {"nl-NL", "nl-NL"},
            {"pl-PL", "pl"},
            {"pt-PT", "pt"},
            {"pt-BR", "pt-BR"},
            {"ro-RO", "ro"},
            {"ru-RU", "ru"},
            {"sr-Cyrl", "sr-Cyrl"},
            {"sr-Latn", "sr"},
            {"sv-SE", "sv"},
            {"tr-TR", "tr"},
            {"uk-UA", "uk"},
            {"vi-VN", "vi"},
            {"zh-CN", "zh"},
            {"zh-Hant", "zh-Hant"},
            {"ko-KR", "ko"},
            {"ja-JP", "ja"},
            {"lt-LT", "lt"},
        };
    }
}