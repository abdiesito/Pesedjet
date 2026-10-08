using System;
using System.Text.RegularExpressions;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Utilities;

namespace Pesedjet.Client.ViewModels;

public partial class TwoFactorAuthViewModel : ViewModelBase
{
    private const int VerificationCodeLength = 6;
    private const int ExpirationTimeInMinutes = 5;

    private readonly DispatcherTimer _timer;
    private TimeSpan _remainingTime;

    [ObservableProperty]
    private string _verificationCode = string.Empty;

    [ObservableProperty]
    private string _timerDisplay = "05:00";

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _hasError;

    [ObservableProperty]
    private string _infoMessage = string.Empty;

    [ObservableProperty]
    private bool _hasInfoMessage;

    [ObservableProperty]
    private bool _isCodeInvalid;

    [ObservableProperty]
    private bool _isCodeExpired;

    [ObservableProperty]
    private bool _isResendEnabled;

    public event Action? RequestClose;

    public TwoFactorAuthViewModel()
    {
        _remainingTime = TimeSpan.FromMinutes(ExpirationTimeInMinutes);
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += OnTimerTick;
        StartTimer();
    }

    [RelayCommand]
    public void VerifyCode()
    {
        ClearStatusMessages();

        if (IsCodeExpired)
        {
            IsCodeInvalid = true;
            ShowError(LocalizationManager.Instance["TwoFactor.Error.ExpiredCode"]);
            return;
        }

        string trimmedCode = VerificationCode?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(trimmedCode) ||
            trimmedCode.Length != VerificationCodeLength ||
            !NumericCodeRegex().IsMatch(trimmedCode))
        {
            IsCodeInvalid = true;
            ShowError(LocalizationManager.Instance["TwoFactor.Error.InvalidCodeLength"]);
            return;
        }

        // TODO: Request server-side code validation via proxy service.
        // On success, notify parent window and close modal.
        RequestClose?.Invoke();
    }

    [RelayCommand]
    public void ResendCode()
    {
        ClearStatusMessages();

        // TODO: Call backend service to send a new 2FA email code via SMTP.
        ResetTimer();
        ShowInfo(LocalizationManager.Instance["TwoFactor.ResendSuccess"]);
    }

    [RelayCommand]
    public void Cancel()
    {
        StopTimer();
        RequestClose?.Invoke();
    }

    private void StartTimer()
    {
        IsResendEnabled = false;
        IsCodeExpired = false;
        _timer.Start();
    }

    private void StopTimer()
    {
        _timer.Stop();
    }

    private void ResetTimer()
    {
        StopTimer();
        _remainingTime = TimeSpan.FromMinutes(ExpirationTimeInMinutes);
        TimerDisplay = _remainingTime.ToString(@"mm\:ss");
        StartTimer();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        if (_remainingTime > TimeSpan.Zero)
        {
            _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
            TimerDisplay = _remainingTime.ToString(@"mm\:ss");
        }
        else
        {
            StopTimer();
            IsCodeExpired = true;
            IsResendEnabled = true;
            IsCodeInvalid = true;
            ShowError(LocalizationManager.Instance["TwoFactor.Error.ExpiredCode"]);
        }
    }

    private void ClearStatusMessages()
    {
        HasError = false;
        ErrorMessage = string.Empty;
        HasInfoMessage = false;
        InfoMessage = string.Empty;
        IsCodeInvalid = false;
    }

    private void ShowError(string message)
    {
        HasError = true;
        ErrorMessage = message;
    }

    private void ShowInfo(string message)
    {
        HasInfoMessage = true;
        InfoMessage = message;
    }

    [GeneratedRegex(@"^\d+$", RegexOptions.None, matchTimeoutMilliseconds: 250)]
    private static partial Regex NumericCodeRegex();
}