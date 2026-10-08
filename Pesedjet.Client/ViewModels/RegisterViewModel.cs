using System;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Services;

namespace Pesedjet.Client.ViewModels;

public partial class RegisterViewModel : ViewModelBase
{
    private const int MinimumGametagLength = 3;
    private const int MaximumGametagLength = 50;
    private const int MinimumPasswordLength = 8;
    private const int MinimumRequiredAge = 12;

    [ObservableProperty]
    private string _fullName = string.Empty;

    [ObservableProperty]
    private string _gametag = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? _birthDate = DateTimeOffset.Now.AddYears(-MinimumRequiredAge);
    public DateTimeOffset MaximumAllowedBirthDate => DateTimeOffset.Now.AddYears(-MinimumRequiredAge);

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _confirmPassword = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _hasError;

    // Visual feedback flags for invalid field borders
    [ObservableProperty]
    private bool _isFullNameInvalid;

    [ObservableProperty]
    private bool _isGametagInvalid;

    [ObservableProperty]
    private bool _isBirthDateInvalid;

    [ObservableProperty]
    private bool _isEmailInvalid;

    [ObservableProperty]
    private bool _isPasswordInvalid;

    [ObservableProperty]
    private bool _isConfirmPasswordInvalid;

    public RegisterViewModel()
    {
    }

    [RelayCommand]
    public void Register()
    {
        ClearValidationState();

        if (!ValidatePresenceAndFormats())
        {
            return;
        }

        if (!ValidateAge())
        {
            return;
        }

        if (!ValidatePasswordComplexityAndMatch())
        {
            return;
        }

        // TODO: Invoke remote service to send 2FA SMTP email and launch GUI-VerificacionDobleFactor modal.
    }

    [RelayCommand]
    public void Cancel()
    {
        ClearForm();
        // TODO: Navigate back to GUI-IniciarSesion view.
    }

    private bool ValidatePresenceAndFormats()
    {
        bool hasMissingFields = false;

        if (string.IsNullOrWhiteSpace(FullName))
        {
            IsFullNameInvalid = true;
            hasMissingFields = true;
        }

        string trimmedGametag = Gametag?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(trimmedGametag) ||
            trimmedGametag.Length < MinimumGametagLength ||
            trimmedGametag.Length > MaximumGametagLength ||
            !GametagRegex().IsMatch(trimmedGametag))
        {
            IsGametagInvalid = true;
            hasMissingFields = true;
        }

        string trimmedEmail = Email?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(trimmedEmail) || !EmailRegex().IsMatch(trimmedEmail))
        {
            IsEmailInvalid = true;
            hasMissingFields = true;
        }

        if (!BirthDate.HasValue)
        {
            IsBirthDateInvalid = true;
            hasMissingFields = true;
        }

        if (hasMissingFields)
        {
            ShowError(LocalizationManager.Instance["Register.Error.RequiredFields"]);
            return false;
        }

        return true;
    }

    private bool ValidateAge()
    {
        if (!BirthDate.HasValue)
        {
            IsBirthDateInvalid = true;
            return false;
        }

        DateTime today = DateTime.Today;
        DateTime selectedDate = BirthDate.Value.DateTime;
        int calculatedAge = today.Year - selectedDate.Year;

        if (selectedDate.Date > today.AddYears(-calculatedAge))
        {
            calculatedAge--;
        }

        if (calculatedAge < MinimumRequiredAge)
        {
            IsBirthDateInvalid = true;
            ShowError(LocalizationManager.Instance["Register.Error.Underage"]);
            return false;
        }

        return true;
    }

    private bool ValidatePasswordComplexityAndMatch()
    {
        bool meetsComplexity = !string.IsNullOrEmpty(Password) &&
                               Password.Length >= MinimumPasswordLength &&
                               PasswordRegex().IsMatch(Password);

        bool passwordsMatch = Password == ConfirmPassword && !string.IsNullOrEmpty(Password);

        if (!meetsComplexity || !passwordsMatch)
        {
            IsPasswordInvalid = true;
            IsConfirmPasswordInvalid = true;
            ShowError(LocalizationManager.Instance["Register.Error.InvalidPassword"]);
            return false;
        }

        return true;
    }

    private void ClearValidationState()
    {
        HasError = false;
        ErrorMessage = string.Empty;
        IsFullNameInvalid = false;
        IsGametagInvalid = false;
        IsBirthDateInvalid = false;
        IsEmailInvalid = false;
        IsPasswordInvalid = false;
        IsConfirmPasswordInvalid = false;
    }

    private void ClearForm()
    {
        ClearValidationState();
        FullName = string.Empty;
        Gametag = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        ConfirmPassword = string.Empty;
        BirthDate = DateTimeOffset.Now.AddYears(-MinimumRequiredAge);
    }

    private void ShowError(string message)
    {
        HasError = true;
        ErrorMessage = message;
    }

    [GeneratedRegex(@"^[a-zA-Z0-9]+$", RegexOptions.None, matchTimeoutMilliseconds: 250)]
    private static partial Regex GametagRegex();

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.None, matchTimeoutMilliseconds: 250)]
    private static partial Regex EmailRegex();

    [GeneratedRegex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$", RegexOptions.None, matchTimeoutMilliseconds: 250)]
    private static partial Regex PasswordRegex();
}