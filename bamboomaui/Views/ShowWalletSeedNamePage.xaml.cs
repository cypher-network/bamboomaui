using Bamboomaui.ViewModels;

namespace Bamboomaui.Views;


public partial class ShowWalletNameSeedPage : ContentPage
{
    public ShowWalletNameSeedPage(ShowWalletNameSeedPageModel showWalletNameSeedPageModel)
    {
        InitializeComponent();
        BindingContext = showWalletNameSeedPageModel;
    }
}