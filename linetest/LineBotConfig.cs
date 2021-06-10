using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linetest
{
    public class LineBotConfig
    {
        public string channelSecret { get; set; }
        public string accessToken { get; set; }
        public string MSTranslatorTextKey { get; set; }
        public string AdminUserId { get; set; }
    }
}
