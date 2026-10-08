using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Utilities;

namespace Pesedjet.Client.ViewModels;

public partial class JoinMatchViewModel : ViewModelBase
{
    private const int RoomCodeLength = 6;

    [ObservableProperty]
    private string _roomCode = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _hasError;

    public ObservableCollection<PublicRoomItem> PublicRooms { get; } = new();

    public JoinMatchViewModel()
    {
        LoadSampleRooms();
    }

    [RelayCommand]
    public void JoinMatch()
    {
        ClearError();

        string code = RoomCode?.Trim().ToUpperInvariant() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(code) || code.Length != RoomCodeLength)
        {
            HasError = true;
            ErrorMessage = LocalizationManager.Instance["JoinMatch.Error.InvalidCodeFormat"];
            return;
        }

        // TODO: Request room validation and connection to the server via network service.
    }

    [RelayCommand]
    public void JoinPublicRoom(PublicRoomItem? room)
    {
        if (room == null)
        {
            return;
        }

        RoomCode = room.RoomCode;
        JoinMatch();
    }

    private void LoadSampleRooms()
    {
        PublicRooms.Clear();
        PublicRooms.Add(new PublicRoomItem
        {
            RoomCode = "ANUB99",
            HostUsername = "OsirisPlayer",
            CurrentPlayers = 3,
            MaxPlayers = 4,
            GameType = "Normal"
        });
        PublicRooms.Add(new PublicRoomItem
        {
            RoomCode = "RA7701",
            HostUsername = "HorusMaster",
            CurrentPlayers = 2,
            MaxPlayers = 6,
            GameType = "Rápida"
        });
        PublicRooms.Add(new PublicRoomItem
        {
            RoomCode = "SETH05",
            HostUsername = "AnubisGuard",
            CurrentPlayers = 1,
            MaxPlayers = 2,
            GameType = "Normal"
        });
    }

    private void ClearError()
    {
        HasError = false;
        ErrorMessage = string.Empty;
    }
}