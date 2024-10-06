using System.Text.Json;

namespace TicketBooking.AppSettings;

public class AppSettingsInitializer
{
    private static AppSettingsModel? _appSettings;

    private AppSettingsInitializer()
    {
        string appSettingsJson = File.ReadAllText(@"..\..\..\AppSettings\appsettings.json");
        _appSettings = JsonSerializer.Deserialize<AppSettingsModel>(appSettingsJson);
    }

    public static AppSettingsModel AppSettingsInstance()
    {
        if (_appSettings == null) new AppSettingsInitializer();

        return _appSettings;
    }
}