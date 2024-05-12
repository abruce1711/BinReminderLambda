using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder
{
    public static class AppConfig
    {
        #region council specific
        public const string AUTH_URL = "https://www.fife.gov.uk/api/citizen";
        public const string BIN_URL = "https://www.fife.gov.uk/api/custom?action=powersuite_bin_calendar_collections&actionedby=bin_calendar&loadform=true&access=citizen&locale=en";
        public const string OBJECT_ID_URL = "https://www.fife.gov.uk/api/widget?action=propertysearch&actionedby=ps_3SHSN93&loadform=true&access=citizen&locale=en";
        public const string UPRN_BASE_URL = "https://www.fife.gov.uk/api/getobjectdata?objecttype=property&objectid=";
        #endregion

        #region whatsapp specific  
        private static readonly string appId = Environment.GetEnvironmentVariable("WHATSAPP_APP_ID");
        public static readonly string WHATSAPP_API_URL = $"https://graph.facebook.com/v15.0/{appId}/messages";
        public static readonly string DEFAULT_TEMPLATE = "bin_collection_reminder_within_a_week";
        public static readonly string TOMORROW_TEMPLATE = "bin_collection_reminder_tomorrow";
        public static readonly string ERROR_TEMPLATE = "error_message";

        public static readonly string WHATSAPP_AUTH_TOKEN = Environment.GetEnvironmentVariable("WHATSAPP_AUTH_TOKEN");
        #endregion


        #region emojis
        public const string BROWN = "🟤";
        public const string GREEN = "🟢";
        public const string BLUE = "🔵";
        public const string GREY = "⚫";
        #endregion
    }
}
