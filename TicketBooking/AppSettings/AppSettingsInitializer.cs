using System.Text.Json;

namespace TicketBooking.AppSettings;

public static class AppSettingsInitializer
{
    private static AppSettingsModel? _appSettings;

    public static AppSettingsModel AppSettingsInstance()
    {
        if (_appSettings is null)
        {
            string appSettingsJson = File.ReadAllText(@"..\..\..\AppSettings\appsettings.json");
            _appSettings = JsonSerializer.Deserialize<AppSettingsModel>(appSettingsJson);
        }

        return _appSettings;
    }
}
