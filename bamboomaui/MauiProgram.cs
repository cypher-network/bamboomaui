using Bamboomaui.Core;
using Bamboomaui.ViewModels;
using Bamboomaui.Views;
using CommunityToolkit.Maui;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace Bamboomaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FASolid");
                fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FABrands");
                fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FARegular");
            });

        builder.Services.AddSingleton(typeof(IFingerprint), CrossFingerprint.Current);
        builder.Services.AddSingleton<IWallet, Wallet>();
        builder.Services.AddSingleton<LoginPage, LoginPageViewModel>();
        builder.Services.AddTransient<WelcomePage, WelcomePageViewModel>();
        builder.Services.AddTransient<NameWalletPage, NameWalletViewModel>();
        builder.Services.AddTransient<ImportantPage, ImportantPageViewModel>();
        builder.Services.AddTransient<ShowWalletNameSeedPage, ShowWalletNameSeedPageModel>();
        builder.Services.AddSingleton<ProfilePage, ProfilePageViewModel>();
        return builder.Build();
    }
}