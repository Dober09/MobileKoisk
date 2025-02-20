
using MobileKoisk.ViewModel;
using ZXing.Net.Maui;

namespace MobileKoisk.View;

public partial class ScanningPage : ContentPage
{

    
    public ScanningPage(ScanningViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
        
    }

    protected  override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ScanningViewModel vm)
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status == PermissionStatus.Granted)
            {
                vm.ResetScanningState();
                vm.StartScanningCommand.Execute(null);
            }
            else
            {
                await vm.RequestCameraPermissionCommand.ExecuteAsync(null);
            }
        }
    }


    private async void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (BindingContext is ScanningViewModel vm)
        {
            await Task.Delay(100);

            vm.HandleBarcodesDectedCommand.Execute(e);
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is ScanningViewModel vm)
        {
            vm.StopScanningCommand.Execute(null);
        }
    }
 




}