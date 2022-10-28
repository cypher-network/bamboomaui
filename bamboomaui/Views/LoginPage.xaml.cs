using Bamboomaui.ViewModels;

namespace Bamboomaui.Views;


public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel loginPageViewModel)
    {
        InitializeComponent();
        BindingContext = loginPageViewModel;
    }
}