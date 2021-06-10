using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linetest.Ｍodels
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
    public class MyFun
    {
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
            return list;
        }
    }
}
