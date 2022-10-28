using Bamboomaui.ViewModels;

namespace Bamboomaui.Views;


public partial class ImportantPage : ContentPage
{
    public ImportantPage(ImportantPageViewModel importantPageViewModel)
    {
        InitializeComponent();
        BindingContext = importantPageViewModel;
    }
}