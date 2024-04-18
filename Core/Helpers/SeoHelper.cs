using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class SeoHelper
    {
        public static string ConverToSeo(this string url, string langCode)
        {
            url = url.ToLowerInvariant();
            if (langCode == "ru-RU")
                url = url.Replace("а", "a").Replace("б", "b").Replace("в", "v").Replace("г", "g").Replace("д", "d").Replace("е", "e").Replace("ё", "yo").Replace("ж", "zh").Replace("з", "z").Replace("и", "i").Replace("й", "y").Replace("к", "k").Replace("л", "l").Replace("м", "m").Replace("н", "n").Replace("о", "o").Replace("п", "p").Replace("р", "r").Replace("с", "s").Replace("т", "t").Replace("у", "u").Replace("ф", "f").Replace("х", "kh").Replace("ц", "ts").Replace("ч", "ch").Replace("ш", "sh").Replace("щ", "sch").Replace("ъ", "").Replace("ы", "y").Replace("ь", "").Replace("э", "e").Replace("ю", "yu").Replace("я", "ya");
            else if (langCode == "az-AZ" || langCode == "en-US")
                url = url.Replace("ə", "e").Replace("ı", "i").Replace("ö", "o").Replace("ş", "s").Replace("ü", "u").Replace("ç", "c").Replace("ğ", "g");

            return Regex.Replace(url, @"[^a-z0-9]", "-").Trim('-');
        }
    }
}
