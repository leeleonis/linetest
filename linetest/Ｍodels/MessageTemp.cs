using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linetest.Models
{
    public class ImgTemp
    {
        /// <summary>
        /// 關鍵字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 圖片路徑
        /// </summary>
        public Uri uri { get; set; }
    }
    public class MSTranslator
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 語言
        /// </summary>
        public string language { get; set; }
    }
    public class MyFun
    {
        internal static Dictionary<string, string> Languagelist()
        {
            var SupportLanguage = new Dictionary<string, string>();
            SupportLanguage.Add("中文", "zh-Hant");
            SupportLanguage.Add("英文", "en");
            SupportLanguage.Add("日文", "ja");
            SupportLanguage.Add("西班牙文", "es");
            SupportLanguage.Add("韓文", "ko");
            SupportLanguage.Add("印度文", "hi");
            SupportLanguage.Add("希伯來文", "he");
            SupportLanguage.Add("克林貢文", "tlh");
            SupportLanguage.Add("馬來文", "ms");
            SupportLanguage.Add("越南文", "vi");
            SupportLanguage.Add("菲律賓文", "fil");
            SupportLanguage.Add("泰文", "th");
            SupportLanguage.Add("德文", "de");
            SupportLanguage.Add("法文", "fr");
            SupportLanguage.Add("義大利文", "it");
            SupportLanguage.Add("俄文", "ru");
            SupportLanguage.Add("烏克蘭文", "uk");
            return SupportLanguage;
        }


        internal static List<ImgTemp> ImgTemplist()
        {
            var list = new List<ImgTemp>();
            list.Add(new ImgTemp
            {
                Key = "/垃圾",
                uri = new Uri("https://i.imgur.com/mm1JcL0.jpg")
            });
            list.Add(new ImgTemp
            {
                Key = "/表演",
                uri = new Uri("https://i.imgur.com/K8SuJIo.jpg")
            });
            list.Add(new ImgTemp
            {
                Key = "/不早說",
                uri = new Uri("https://i.imgur.com/wyTqq7Q.gif")
            });
            list.Add(new ImgTemp
            {
                Key = "/父子",
                uri = new Uri("https://i.imgur.com/jFzEq8g.jpg")
            });
            list.Add(new ImgTemp
            {
                Key = "/讚",
                uri = new Uri("https://i.imgur.com/3EnpqZl.jpg")
            });
        
            return list;
        }
    }
}
