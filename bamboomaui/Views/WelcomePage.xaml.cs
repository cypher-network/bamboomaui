using Bamboomaui.ViewModels;

namespace Bamboomaui.Views;

public partial class WelcomePage : ContentPage
{
    public WelcomePage(WelcomePageViewModel welcomePageViewModel)
    {
        InitializeComponent();
        BindingContext = welcomePageViewModel;
    }
}