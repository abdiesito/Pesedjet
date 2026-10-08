using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Pesedjet.Client.ViewModels;

public partial class MyProfileViewModel : ViewModelBase
{
    private const double PercentageMultiplier = 100.0;

    [ObservableProperty]
    private string _fullName = string.Empty;

    [ObservableProperty]
    private string _gametag = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? _birthDate;

    [ObservableProperty]
    private int _playerLevel;

    [ObservableProperty]
    private string _rankName = string.Empty;

    [ObservableProperty]
    private int _matchesPlayed;

    [ObservableProperty]
    private int _matchesWon;

    public string WinRateDisplay
    {
        get
        {
            if (MatchesPlayed == 0)
            {
                return "0%";
            }

            double rate = (double)MatchesWon / MatchesPlayed * PercentageMultiplier;
            return $"{rate:F1}%";
        }
    }

    public string FormattedBirthDate => BirthDate?.ToString("dd/MM/yyyy") ?? "N/A";

    public MyProfileViewModel()
    {
        LoadProfileData();
    }

    [RelayCommand]
    public void EditProfile()
    {
        // TODO: Launch edit profile view or modal dialog.
    }

    private void LoadProfileData()
    {
        FullName = "Abdiel Pérez Mar";
        Gametag = "MartesMaster99";
        Email = "abdiel@example.com";
        BirthDate = new DateTimeOffset(2006, 9, 27, 0, 0, 0, TimeSpan.Zero);
        PlayerLevel = 15;
        RankName = "Faraón de Plata";
        MatchesPlayed = 42;
        MatchesWon = 28;
    }
}