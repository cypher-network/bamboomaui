using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bamboomaui.ViewModels;

public partial class WelcomePageViewModel : ObservableObject
{
    [RelayCommand]
    Task NewWalletButtonClicked()
    {
        return Shell.Current.GoToAsync($"pin?NewWallet={true}", true);
    }
    
    [RelayCommand]
    async Task RestoreWalletButtonClicked()
    {
        await Toast.Make("Password: 1111").Show();
    }
}