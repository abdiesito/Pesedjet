using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Models;

namespace Pesedjet.Client.ViewModels;

public partial class LeaderboardViewModel : ViewModelBase
{
    private const string CurrentUserGametag = "HorusMaster99";

    [ObservableProperty]
    private bool _isEmpty;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private bool _showFriendsOnly;

    public ObservableCollection<LeaderboardEntryItem> TopRankings { get; } = new();

    public LeaderboardViewModel()
    {
        LoadLeaderboardData();
    }

    [RelayCommand]
    public void GoBack()
    {
        // TODO: Navigate back to GUI-MenuPrincipal via navigation manager or main viewmodel.
    }

    [RelayCommand]
    public void RefreshLeaderboard()
    {
        LoadLeaderboardData();
    }

    [RelayCommand]
    public void ToggleFilter(string filterType)
    {
        ShowFriendsOnly = filterType == "Friends";
        LoadLeaderboardData();
    }

    [RelayCommand]
    public void ViewPlayerProfile(LeaderboardEntryItem? entry)
    {
        if (entry == null)
        {
            return;
        }

        // TODO: Open public profile modal or navigate to player detail view.
    }

    private void LoadLeaderboardData()
    {
        IsLoading = true;
        TopRankings.Clear();

        List<LeaderboardEntryItem> rawEntries = GetSampleEntries();
        
        IEnumerable<LeaderboardEntryItem> filteredEntries = rawEntries;
        if (ShowFriendsOnly)
        {
            filteredEntries = rawEntries.Where(e => e.IsFriend || e.IsCurrentPlayer);
        }
        
        List<LeaderboardEntryItem> sortedEntries = filteredEntries
            .OrderByDescending(e => e.VictoriesCount)
            .ThenByDescending(e => e.AmuletsCount)
            .ToList();

        int currentRank = 1;
        foreach (LeaderboardEntryItem entry in sortedEntries)
        {
            entry.Position = currentRank;
            entry.IsCurrentPlayer = (entry.Gametag == CurrentUserGametag);
            TopRankings.Add(entry);
            currentRank++;
        }

        IsEmpty = TopRankings.Count == 0;
        IsLoading = false;
    }

    private static List<LeaderboardEntryItem> GetSampleEntries()
    {
        return new List<LeaderboardEntryItem>
        {
            new LeaderboardEntryItem 
            { 
                Gametag = "OsirisKing", 
                ProfilePictureUrl = "avares/avatar1.png",
                VictoriesCount = 45, 
                AmuletsCount = 120, 
                IsFriend = true 
            },
            new LeaderboardEntryItem 
            { 
                Gametag = "HorusMaster99", 
                ProfilePictureUrl = "avares/avatar2.png",
                VictoriesCount = 28, 
                AmuletsCount = 84, 
                IsFriend = false 
            },
            new LeaderboardEntryItem 
            { 
                Gametag = "AnubisGuard", 
                ProfilePictureUrl = "avares/avatar3.png",
                VictoriesCount = 28, 
                AmuletsCount = 76, 
                IsFriend = true 
            },
            new LeaderboardEntryItem 
            { 
                Gametag = "IsisSpell", 
                ProfilePictureUrl = "avares/avatar4.png",
                VictoriesCount = 19, 
                AmuletsCount = 52, 
                IsFriend = false 
            },
            new LeaderboardEntryItem 
            { 
                Gametag = "RaPharaoh", 
                ProfilePictureUrl = "avares/avatar5.png",
                VictoriesCount = 12, 
                AmuletsCount = 31, 
                IsFriend = true 
            }
        };
    }
}