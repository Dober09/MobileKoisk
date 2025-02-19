using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileKoisk.Models;
using MobileKoisk.Services;
using MobileKoisk.View;


namespace MobileKoisk.ViewModel
{
	public class MainViewModel : BaseViewModel
	{
       

        private readonly SalesService _salesService;


        private readonly WishListServices _wishListServices;

        private readonly ProductItemService _productItemService;

		public ObservableCollection <Product> Products { get; set; }

		public ObservableCollection<SaleItem> SalesItems { get; set; }

		public ObservableCollection<Product> FilteredProducts { get; set; } = new ObservableCollection<Product>();
		public MainViewModel(ProductItemService productItemService,
            SalesService  salesService,
            WishListServices wishListServices) {
            
           
		
			_productItemService = productItemService;
			_wishListServices = wishListServices;
			_salesService = salesService;
			Products = new ObservableCollection<Product>();
			SalesItems = new ObservableCollection<SaleItem>(); 



			LoadProductItems();
            CategoryTappedCommand = new Command<string>(OnCategoryTapped);
            AddToWishListCommand = new Command<Product>(AddToWishList);
            //LoadSalesItem();
        }

        private async Task LoadImageAsync(Product product)
        {
            if (string.IsNullOrEmpty(product.image_url))
                return;

            try
            {
                // Add cache buster to force fresh load when needed
                product.image_url = product.image_url + "?cache=" + DateTime.Now.Ticks;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading image for {product.item_description}: {ex.Message}");
                product.image_url = "placeholder_image.png";
            }
        }


        public ICommand AddToWishListCommand { get; }
        public ICommand CategoryTappedCommand { get; }
        private async void LoadProductItems()
		{
			try
			{
				var products = await _productItemService.LoadJsonDataAsync();
				Products.Clear();

				foreach (var product in products)
				{
				System.Diagnostics.Debug.WriteLine($"image --->{product.item_description} && {product.image_url} " );
                    await LoadImageAsync(product);
					Products.Add(product);
				}

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error loading products --->  {ex.Message}");
			}
		}

		private async void LoadSalesItem()
		{
            try
            {
                var saleItems = await _salesService.LoadJsonDataAsync();
				SalesItems.Clear();

                foreach (var sale in saleItems)
                {
                    SalesItems.Add(sale);
                }
                System.Diagnostics.Debug.WriteLine($"Total list of products --->  {Products.Count}");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading products --->  {ex.Message}");
            }

        }

        public async void OnCategoryTapped(string category)
        {
            FilteredProducts.Clear();
            foreach (var product in Products)
            {
                if (product.category == category)
                {
                    FilteredProducts.Add(product);
                }
            }

            NavigateToFilteredProductsPage(category);
        }

        private void NavigateToFilteredProductsPage(string category)
        {
            var productsPage = new ProductListPage(category);
            Application.Current.MainPage.Navigation.PushAsync(productsPage);
        }


        private void AddToWishList(Product product)
        {
            _wishListServices.AddToWishList(product);
            WishlistCounterServirce.SetCount(WishlistCounterServirce.Count + 1);
            System.Diagnostics.Debug.WriteLine($"Product {product.item_description} added to wishlist");
        }

    }
}
