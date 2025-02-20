using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ZXing.Net.Maui;
using MobileKoisk.Models;
using MobileKoisk.Services;
using MobileKoisk.View;
using CommunityToolkit.Maui.Views;


namespace MobileKoisk.ViewModel
{
    public partial class ScanningViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isManualEntry;

       

        [ObservableProperty]
        private bool showManualEntry;

        [ObservableProperty]
        private string manualBarcode;

        [ObservableProperty]
        private bool isScanning = true;

        [ObservableProperty]
        private string scanResult;

        [ObservableProperty]
        private Product scannedProduct;

        [ObservableProperty]
        private BarcodeReaderOptions readerOptions;

        private readonly BadgeCounterService _badgeCounterService;

        private string lastScannedBarcode;
        private DateTime lastScanTime = DateTime.MinValue;

        private const int SCAN_COOLDOWN_MS = 3000;


        public ObservableCollection<Product> ScannedItems { get; set; }

        private bool isProcessingBarcode = false;

        ProductItemService productItemService;
        // products for scanned items

        public ScanningViewModel(ProductItemService productItemService ,BadgeCounterService badgeCounterService) {
            _badgeCounterService = badgeCounterService;

            this.productItemService = productItemService;
            ReaderOptions = new BarcodeReaderOptions {
                Formats = BarcodeFormat.Ean13,
                AutoRotate = true,
                Multiple = true,
            };
            isScanning = true;

            isManualEntry = false;
        }


        [RelayCommand]
        private void OpenManualEntry()
        {
            IsManualEntry = true;
            IsScanning = false;
            ManualBarcode = string.Empty;

        }



        [RelayCommand]
        private void ReturnToScanner()
        {
            IsManualEntry = false;
            IsScanning = true;
            ManualBarcode = string.Empty;
        }


        [RelayCommand]
        private async Task SearchBarcode()
        {
            if (string.IsNullOrWhiteSpace(ManualBarcode))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please enter a barcode", "OK");
                return;
            }

            try
            {
                var productResults = await productItemService.LoadJsonDataAsync();
                var product = productResults.FirstOrDefault(item => item.barcode.ToString() == ManualBarcode);

                if (product != null)
                {
                    var popup = new ScannedPopup(product ,_badgeCounterService);
                    await App.Current.MainPage.ShowPopupAsync(popup);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Product Not Found",
                        $"No product found with barcode: {ManualBarcode}",
                        "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to search barcode", "OK");
                System.Diagnostics.Debug.WriteLine($"Error in SearchBarcode: {ex}");
            }

        }


        [RelayCommand]
        private async Task HandleBarcodesDected(BarcodeDetectionEventArgs args)
        {

            if (isProcessingBarcode) return;
            try
            {

                isProcessingBarcode = true;

                System.Diagnostics.Debug.WriteLine("Bacode detection triggered");

                var result = args.Results?.FirstOrDefault();
                if (result is null)
                {
                    System.Diagnostics.Debug.WriteLine("No results found");
                    return;
                }

               //Check if this is the same barcode and if enough time has passed
                var currentTime = DateTime.Now;
                if (result.Value == lastScannedBarcode &&
                    (currentTime - lastScanTime).TotalMilliseconds < SCAN_COOLDOWN_MS)
                {
                    return;
                }
                isProcessingBarcode = true;
                lastScannedBarcode = result.Value;
                lastScanTime = currentTime;


                IsScanning = false;
                System.Diagnostics.Debug.WriteLine($"Detected barcode: {result.Value}");
                ScanResult = result.Value;
                var productResults = await productItemService.LoadJsonDataAsync();
                // Check if product exists
                var product = productResults.FirstOrDefault(item => item.barcode.ToString() == ScanResult );


                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    try
                    {
                        if (product != null)
                        {


                            var popup = new ScannedPopup(product, _badgeCounterService);
                            

                             App.Current.MainPage.ShowPopup(popup);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert(
                                "Product Not Found",
                                $"No product found with barcode: {scanResult}",
                                "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Alert error: {ex.Message}");
                    }
                    finally
                    {
                        // Resume scanning
                        IsScanning = true;
                    }
                });
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine($"Error in handleBarcodesDected :{ex}");

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to process barcode", "Ok");
                });
            }finally { isProcessingBarcode = false; }
        }


        public void ResetScanningState()
        {
            isProcessingBarcode = false;
            IsManualEntry = false;
            IsScanning = true;
            ScanResult = string.Empty;
            lastScannedBarcode = string.Empty; 
        }


        [RelayCommand]
        private async Task RequestCameraPermission()
        {
            var status = await Permissions.RequestAsync<Permissions.Camera>();
            if(status != PermissionStatus.Granted)
            {
                await Shell.Current.DisplayAlert(
                    "Permission requered",
                    "Camera permission is requered for scanning barcodes",
                    "ok");
            }
        }


        [RelayCommand]
        public async void StartScanning()
        {
            try { 
                isProcessingBarcode = false;
                await Task.Delay(100); //  small delay
                IsScanning = true;
                System.Diagnostics.Debug.WriteLine("Camera started");

            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Error is StartScanning {ex}");
            }
           
        }

        [RelayCommand]
        public async void StopScanning()
        {
            try
            {
                IsScanning = false;
                System.Diagnostics.Debug.WriteLine("Camera stopped");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in StopScanning: {ex}");
            }
        }

        //Add a method to handle page appearing
        [RelayCommand]
        public async void OnPageAppearing()
        {
            try
            {
                isProcessingBarcode = false;
                IsScanning = true;  // Reset scanning state
                System.Diagnostics.Debug.WriteLine("Camera resumed on page appearing");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in OnPageAppearing: {ex}");
            }
        }

     

      
        [RelayCommand]
        private void ToggleManualEntry()
        {
          
            ShowManualEntry = !ShowManualEntry;
            if (ShowManualEntry) ManualBarcode = string.Empty;
        }


        [RelayCommand]
        private void ConfirmManualEntry()
        {
            if (!string.IsNullOrWhiteSpace(ManualBarcode))
            {
                // Handle the manual barcode entry
                //HandleBarcode(ManualBarcode);
            }
            ShowManualEntry = false;
        }


        [RelayCommand]
        private void CloseManualEntry()
        {
            ShowManualEntry = false;
            ManualBarcode = string.Empty;
        }

    }
}
