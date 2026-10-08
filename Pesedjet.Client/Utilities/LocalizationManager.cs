using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using Pesedjet.Client.Resources;

namespace Pesedjet.Client.Utilities;

public class LocalizationManager : INotifyPropertyChanged
{
    private const string SettingsFile = "appsettings.json";
    
    public static LocalizationManager Instance { get; } = new LocalizationManager();

    public event PropertyChangedEventHandler? PropertyChanged;

    private LocalizationManager()
    {
        LoadLanguagePreference();
    }
    
    public string this[string key] => Strings.ResourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? $"[{key}]";

    public void ChangeLanguage(string cultureCode)
    {
        var culture = new CultureInfo(cultureCode);
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.CurrentCulture = culture;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
        
        SaveLanguagePreference(cultureCode);
    }

    private void SaveLanguagePreference(string cultureCode)
    {
        var settings = new { Language = cultureCode };
        var json = JsonSerializer.Serialize(settings);
        File.WriteAllText(SettingsFile, json);
    }

    private void LoadLanguagePreference()
    {
        string cultureCode = "es-MX";
        
        if (File.Exists(SettingsFile))
        {
            var json = File.ReadAllText(SettingsFile);
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("Language", out var lang))
            {
                cultureCode = lang.GetString() ?? "es-MX";
            }
        }
        
        var culture = new CultureInfo(cultureCode);
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.CurrentCulture = culture;
    }
}