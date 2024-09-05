using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TicketBooking.AppSettings;

public class AppSettingsInitializer
{
    private static AppSettingsModel? _appSettings;

    public static AppSettingsModel AppSettingsInstance
    {
        get
        {
            if (_appSettings is null)
            {
                string json = File.ReadAllText("..\\..\\..\\AppSettings\\appsettings.json");
                _appSettings = JsonSerializer.Deserialize<AppSettingsModel>(json);
            }

            return _appSettings;
        }
    }
}
