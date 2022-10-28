using Bamboomaui.ViewModels;
using NBitcoin;

namespace Bamboomaui.Views;


public partial class NameWalletPage : ContentPage
{
    public NameWalletPage(NameWalletViewModel nameWalletViewModel)
    {
        InitializeComponent();
        BindingContext = nameWalletViewModel;
        languagePicker.ItemsSource = Enum.GetValues(typeof(Language))
            .Cast<Language>()
            .Select(v => v.ToString())
            .ToList();
    }
}