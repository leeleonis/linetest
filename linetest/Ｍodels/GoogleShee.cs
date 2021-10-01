using linetest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linetest
{
    public class GoogleShee
    {
        internal static void SaveMsg(string text)
        {
            var gsh = new GoogleSheetsHelper("client_secret.json", "15YK4sgOmEyfKO5EGdbM41ceuRhAOiZpEI-BO1S7oeSo");
        }
    }
}
