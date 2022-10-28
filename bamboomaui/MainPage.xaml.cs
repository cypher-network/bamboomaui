using Bamboomaui.Crypto;
using Plugin.Fingerprint.Abstractions;

namespace Bamboomaui;

public partial class MainPage : ContentPage
{
    private readonly IFingerprint _fingerprint;
    
    int count = 0;

    public MainPage(IFingerprint fingerprint)
    {
        _fingerprint = fingerprint;
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;
        CounterBtn.Text = $"{count}";
        SemanticScreenReader.Announce(CounterBtn.Text);
        
        var request = new AuthenticationRequestConfiguration("Prove you have fingers!", "Because without it you can't have access");
        // using DI
        var result = await _fingerprint.AuthenticateAsync(request);
        // using static implementation
        //var result = await CrossFingerprint.Current.AuthenticateAsync(request);

        if (result.Authenticated)
        {
            await DisplayAlert("Authenticated!", "Access granted", "Cool beans");
        }
        else
        {
            await DisplayAlert("Not authenticated!", "Access denied", "aww");
        }
    }
}