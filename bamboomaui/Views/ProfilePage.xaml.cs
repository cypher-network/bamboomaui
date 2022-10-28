using Bamboomaui.ViewModels;

namespace Bamboomaui.Views;


public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfilePageViewModel profilePageViewModel)
    {
        InitializeComponent();
        BindingContext = profilePageViewModel;
    }
}