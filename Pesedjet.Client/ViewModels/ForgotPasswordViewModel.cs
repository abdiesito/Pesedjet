using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Utilities;

namespace Pesedjet.Client.ViewModels;

public partial class ForgotPasswordViewModel : ViewModelBase
{
    private const int VerificationCodeLength = 6;
    private const int MinimumPasswordLength = 8;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _verificationCode = string.Empty;

    [ObservableProperty]
    private string _newPassword = string.Empty;

    [ObservableProperty]
    private string _confirmPassword = string.Empty;

    [ObservableProperty]
    private bool _isCodeSent;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _hasError;

    [ObservableProperty]
    private bool _isEmailInvalid;

    [ObservableProperty]
    private bool _isCodeInvalid;

    [ObservableProperty]
    private bool _isPasswordInvalid;

    [ObservableProperty]
    private bool _isConfirmPasswordInvalid;

    public ForgotPasswordViewModel()
    {
    }

    [RelayCommand]
    public void SendCode()
    {
        ClearValidationState();

        string trimmedEmail = Email?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(trimmedEmail) || !EmailRegex().IsMatch(trimmedEmail))
        {
            IsEmailInvalid = true;
            ShowError(LocalizationManager.Instance["ForgotPassword.Error.InvalidEmail"]);
            return;
        }

        // TODO: Call backend service 
        IsCodeSent = true;
    }

    [RelayCommand]
    public void ResetPassword()
    {
        ClearValidationState();

        string trimmedCode = VerificationCode?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(trimmedCode) ||
            trimmedCode.Length != VerificationCodeLength ||
            !NumericCodeRegex().IsMatch(trimmedCode))
        {
            IsCodeInvalid = true;
            ShowError(LocalizationManager.Instance["ForgotPassword.Error.InvalidCode"]);
            return;
        }

        bool meetsComplexity = !string.IsNullOrEmpty(NewPassword) &&
                               NewPassword.Length >= MinimumPasswordLength &&
                               PasswordRegex().IsMatch(NewPassword);

        bool passwordsMatch = NewPassword == ConfirmPassword && !string.IsNullOrEmpty(NewPassword);

        if (!meetsComplexity || !passwordsMatch)
        {
            IsPasswordInvalid = true;
            IsConfirmPasswordInvalid = true;
            ShowError(LocalizationManager.Instance["ForgotPassword.Error.InvalidPassword"]);
            return;
        }

        // TODO: Call backend service to validate code and update password.
        // On success, navigate back to GUI-IniciarSesion.
    }

    [RelayCommand]
    public void Cancel()
    {
        ClearForm();
        // TODO: Navigate back to GUI-IniciarSesion view.
    }

    private void ClearValidationState()
    {
        HasError = false;
        ErrorMessage = string.Empty;
        IsEmailInvalid = false;
        IsCodeInvalid = false;
        IsPasswordInvalid = false;
        IsConfirmPasswordInvalid = false;
    }

    private void ClearForm()
    {
        ClearValidationState();
        Email = string.Empty;
        VerificationCode = string.Empty;
        NewPassword = string.Empty;
        ConfirmPassword = string.Empty;
        IsCodeSent = false;
    }

    private void ShowError(string message)
    {
        HasError = true;
        ErrorMessage = message;
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.None, matchTimeoutMilliseconds: 250)]
    private static partial Regex EmailRegex();

    [GeneratedRegex(@"^\d+$", RegexOptions.None, matchTimeoutMilliseconds: 250)]
    private static partial Regex NumericCodeRegex();

    [GeneratedRegex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$", RegexOptions.None, matchTimeoutMilliseconds: 250)]
    private static partial Regex PasswordRegex();
}