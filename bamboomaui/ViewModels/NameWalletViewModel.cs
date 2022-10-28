using Bamboomaui.Core;
using Bamboomaui.Extensions;
using Bamboomaui.Helpers;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NBitcoin;

namespace Bamboomaui.ViewModels;

public partial class NameWalletViewModel: ObservableObject
{
    private readonly IWallet _wallet;
    
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _language;

    public NameWalletViewModel(IWallet wallet)
    {
        _wallet = wallet;
    }
    
    [RelayCommand]
    async Task NextButtonClicked()
    {
        try
        {
            var seed = _wallet.CreateSeed(WordCount.TwentyFour, Enum.Parse<Language>(_language));
            var mmnemonic = string.Join(" ", seed);

            _wallet.Session = new Session(_name.ToSecureString(), _wallet.Session.Passphrase, mmnemonic.ToSecureString());
            
            var taskResult = await _wallet.CreateWalletSeed(mmnemonic.ToSecureString(), _wallet.Session.Passphrase,
               _wallet.Session.Name.FromSecureString());
            if (taskResult.Success)
            {
                var defaultWalletPath = Util.WalletPath("Default");
                if (File.Exists(defaultWalletPath))
                {
                    File.Delete(defaultWalletPath);
                }

                await Shell.Current.GoToAsync("important", true);
            }
            else
            {
                await Toast.Make(taskResult.Exception.Message).Show();
            }
        }
        catch (FileLoadException)
        {
        }
        catch (UnauthorizedAccessException)
        {
            await Toast.Make("Something went wrong! File permission exception").Show();
        }
        catch (Exception e)
        {
            await Toast.Make("Something went wrong!").Show();
        }
    }
} 