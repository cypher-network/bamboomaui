using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bamboomaui.ViewModels;

public partial class ImportantPageViewModel: ObservableObject
{
    public string ImportantText { get; } = "On the next page you will see a series of 24 words. " +
                                           "This is your unique and private seed and it is the ONLY way to recover your wallet in case of loss or damage to the device. " +
                                           "Its YOUR responsibility to write down and store it in a safe place outside of the Bamboo Wallet app.";

    [RelayCommand]
    async Task NextButtonClicked()
    {
        await Shell.Current.GoToAsync("seedwalletname", true);
    }
}