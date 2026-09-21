using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text.Json;
using Pesedjet.Client.Resources; // Asegúrate de que aquí están tus Strings.resx

namespace Pesedjet.Client.Services;

public class LocalizationManager : INotifyPropertyChanged
{
    private const string SettingsFile = "appsettings.json";
    
    // Implementación Singleton para acceder desde cualquier parte (XAML o C#)
    public static LocalizationManager Instance { get; } = new LocalizationManager();

    public event PropertyChangedEventHandler? PropertyChanged;

    private LocalizationManager()
    {
        LoadLanguagePreference();
    }

    // Indexador: Permite que XAML haga Binding directamente a las claves del .resx
    public string this[string key] => Strings.ResourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? $"[{key}]";

    public void ChangeLanguage(string cultureCode)
    {
        // Cambiar el hilo actual a la nueva cultura
        var culture = new CultureInfo(cultureCode);
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.CurrentCulture = culture;
        
        // El disparador "Item" notifica a XAML que todas las propiedades del indexador (this[]) han cambiado
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
        
        SaveLanguagePreference(cultureCode);
    }

    private void SaveLanguagePreference(string cultureCode)
    {
        // Persistir el idioma en un archivo local
        var settings = new { Language = cultureCode };
        var json = JsonSerializer.Serialize(settings);
        File.WriteAllText(SettingsFile, json);
    }

    private void LoadLanguagePreference()
    {
        string cultureCode = "es-MX"; // Cultura base definida en el diccionario
        
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