
using System.ComponentModel;
using Bamboomaui.Core;
using Bamboomaui.Extensions;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace Bamboomaui.ViewModels;

[QueryProperty("NewWallet", "NewWallet")]
public partial class LoginPageViewModel: ObservableObject
{
    private const string DefaultPinText = "Enter your PIN";
    private const string NewWalletPropertyName = "NewWallet";
    private const string PinTextObservablePropertyName = "PinText";

    private readonly IWallet _wallet;
    
    [ObservableProperty] private bool _newWallet = false;
    [ObservableProperty] private string _pin = string.Empty;
    [ObservableProperty] private string _pinText = DefaultPinText;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="wallet"></param>
    public LoginPageViewModel(IWallet wallet)
    {
        _wallet = wallet;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (e.PropertyName == NewWalletPropertyName)
        {
            _pinText = _newWallet ? "Setup your PIN" : DefaultPinText;
            OnPropertyChanged(PinTextObservablePropertyName);
        }
        
        base.OnPropertyChanged(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="cancellationToken"></param>
    [RelayCommand]
    public async Task KeyboardButtonClicked(string parameter, CancellationToken cancellationToken)
    {
        _pin += parameter;
        switch (_pin.Length)
        {
            case < 4:
                break;
            default:
                if (await _wallet.CreateWallet(_pin.ToSecureString(), "Default"))
                {
                    await Toast.Make("Your PIN has been set up successfully").Show(cancellationToken);
                    await Shell.Current.GoToAsync("namewallet", true);
                }
                break;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    [RelayCommand]
    private async Task ForgotPasswordButtonClicked()
    {
        await Toast.Make("Password: 1111").Show();
    }
    
    /// <summary>
    /// 
    /// </summary>
    [RelayCommand]
    private void DeleteButtonClicked()
    {
        if (_pin.Length > 0)
        {
            _pin = _pin[..^1];
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    [RelayCommand]
    private async Task BiometryAuthClicked(CancellationToken cancellationToken)
    {
        if (!await BiometryAuth(cancellationToken))
        {
            await Toast.Make("Biometric authentication is not supported").Show(cancellationToken);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [RelayCommand]
    private async Task<bool> BiometryAuth(CancellationToken cancellationToken)
    {
        var request = new AuthenticationRequestConfiguration("Prove you have fingers!", "Because without it you can't have access");
        var result = await CrossFingerprint.Current.AuthenticateAsync(request, cancellationToken);
        if (result.Authenticated)
        {
            await Toast.Make("Password: 1111").Show();
        }

        return result.Authenticated;
    }
}