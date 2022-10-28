using Bamboomaui.Core;
using Bamboomaui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bamboomaui.ViewModels;

public partial class ShowWalletNameSeedPageModel : ObservableObject
{
    [ObservableProperty] private string _walletName;
    [ObservableProperty] private string _seed;
    
    public ShowWalletNameSeedPageModel(IWallet wallet)
    {
        _walletName = wallet.Session.Name.FromSecureString();
        _seed = wallet.Session.Seed.FromSecureString();
    }
    
    [RelayCommand]
    async Task NextButtonClicked()
    {
        await Shell.Current.GoToAsync("home", true);
    }
}