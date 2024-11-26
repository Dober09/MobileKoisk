using Camera.MAUI;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace MobileKoisk.View;

public partial class ScanningPage : ContentPage
{

    private bool isCameraInitialized = false;
    private bool hasPermission = false;
    public ScanningPage()
	{
		InitializeComponent();
	}

    private  async void CameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (!hasPermission)
        {
            await CheckAndRequestPermission();
        }

        if (hasPermission && !isCameraInitialized && cameraView?.NumCamerasDetected > 0)
        {

            await InitializeCamera();
        }
    }

    private async void CameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    { 
    }

    private async Task CheckAndRequestPermission()
    {
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();
            }

            hasPermission = status == PermissionStatus.Granted;

            if (!hasPermission)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Permission Required", "Camera permission is requieed for this feature", "OK");
                });
            }

        }
        catch (Exception ex)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await DisplayAlert("Error", $"Permission error: {ex.Message}", "OK");
            });
        }
    }

    private async Task InitializeCamera()
    {
        try
        {
            var camera = cameraView.Cameras.FirstOrDefault();
            if (camera != null)
            {
                cameraView.Camera = camera;
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    try
                    {
                        //await cameraView.StopCameraAsync();
                        await Task.Delay(100);
                        await cameraView.StartCameraAsync();
                        SetupBarcodeScanning();
                        isCameraInitialized = true;
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Camera Error", $"Failed to start camera: {ex.Message}", "OK");
                    }


                });
            }
        }
        catch (Exception ex)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await DisplayAlert("Error",
                    $"Camera initialization error: {ex.Message}", "OK");
            });
        }
    }


    private void SetupBarcodeScanning()
    {

        // Defined which barcode formats to scan form
        var barcodeFormats = BarcodeFormat.EAN_13 | BarcodeFormat.UPC_A |
            BarcodeFormat.CODE_128;


        ;
        cameraView.BarCodeOptions = new BarcodeDecodeOptions
        {
            AutoRotate = true,
            PossibleFormats
            = new List<BarcodeFormat> { barcodeFormats },
            TryHarder = true,
        };

    }

}