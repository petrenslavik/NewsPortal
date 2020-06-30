using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NewsPortal.Managers
{
    public class UrlManager
    {
        public static string GenerateToken(string title)
        {
            string token = title;
            token = Transliteration(token);
            token = token.ToLower();
            // invalid chars 
            token = Regex.Replace(token, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            token = Regex.Replace(token, @"\s+", " ").Trim();
            // cut and trim 
            token = token.Substring(0, token.Length <= 50 ? token.Length : 50).Trim();
            token = Regex.Replace(token, @"\s", "-"); // hyphens   

            return token;
        }

        private static string Transliteration(string str)
        {
            string[] lat_up = { "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "Kh", "Ts", "Ch", "Sh", "Shch", "\"", "Y", "'", "E", "Yu", "Ya", "I", "Ye", "Yi", "" };
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y", "'", "e", "yu", "ya", "i", "ye", "yi" };
            string[] cyrillic_up = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я", "І", "Є", "Ї", "_" };
            string[] cyrillic_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я", "і", "є", "ї" };
            for (int i = 0; i <= 35; i++)
            {
                str = str.Replace(cyrillic_up[i], lat_up[i]);
                str = str.Replace(cyrillic_low[i], lat_low[i]);
            }
            return str;
        }

        //public static string GenerateToken(DateTime date)
        //{
        //    string token = date.ToString("MM-dd-yyyy"); 
        //    return token;
        //}
    }
}