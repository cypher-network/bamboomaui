using Bamboomaui.Views;

namespace Bamboomaui;

public partial class AppShell : Shell
{
    public Dictionary<string, Type> Routes { get; } = new();
    
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    /// <summary>
    /// 
    /// </summary>
    private void RegisterRoutes()
    {
        Routes.Add("pin", typeof(LoginPage));
        Routes.Add("namewallet", typeof(NameWalletPage));
        Routes.Add("important", typeof(ImportantPage));
        Routes.Add("seedwalletname", typeof(ShowWalletNameSeedPage));
        foreach (var route in Routes)
        {
            Routing.RegisterRoute(route.Key, route.Value);
        }
    }
}