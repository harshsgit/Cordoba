using System.Collections.Generic;

namespace CordobaModels
{
    public class Common
    {
        public static readonly Dictionary<string, object> CenterColumnStyle = new Dictionary<string, object> { { "align", "center" }, { "style", "text-align:center;vertical-align:middle !important;" } };

        public const string DateTimeFormatWithoutSecond = "MM/dd/yyyy hh:mm tt";
        public const string DateFormate = "dd/MM/yyyy";
    }


}