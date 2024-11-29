using Camera.MAUI;
using Camera.MAUI.ZXingHelper;
using MobileKoisk.Models;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace MobileKoisk.View;

public partial class ScanningPage : ContentPage
{

    private bool isCameraInitialized = false;
    private bool hasPermission = false;
    private ScannedItem currentScandedItem;
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

    private async void CameraView_BarcodeDetected(object sender, BarcodeEventArgs args)
    {
        // stop further procwssing if we're already handling a scan
        if (currentScandedItem == null)
            return;


        try
        {
            //process the scanned barcode
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                //Validate and process the barcode
                if (!string.IsNullOrEmpty(args.Result.ToString()))
                {
                    // stop camera to prevent multiple scans
                    await cameraView.StopCameraAsync();
                }
            });
        }
        catch (Exception ex) {
            await DisplayAlert("Scan Error", $"Error processing barcode: {ex.Message}", "OK");
        }
    }


    private async Task ProcessScannedBarcode(string barcodeText, BarcodeFormat format)
    {
        try
        {

            // Here you would typically:
            // 1. Look up the product in your database
            // 2. Create a ScannedItem object
            currentScandedItem = new ScannedItem
            {
                Barcode = barcodeText,
                ProductName = "aQuelle",
                Price = 19.99m,
                ScannedAt = DateTime.Now,
                ProductCategory = "unknown"

            };

            //show confimation dialog
            bool result = await DisplayAlert(
                 "Item Scanned",
                 $"Barcode: {barcodeText}\nProduct: {currentScandedItem.ProductName}\nPrice: {currentScandedItem.Price:C}",
                 "Add to Cart",
                 "Cancel"
             );

            if (result)
            {

                // add to cart or ptovedd the item
            }

            //reset for the next scan
            currentScandedItem = null;

            //restart the camera
            await cameraView.StartCameraAsync();


        }
        catch (Exception ex) {
            await DisplayAlert("Processing Error", $"Error: {ex.Message}", "OK");
        }

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