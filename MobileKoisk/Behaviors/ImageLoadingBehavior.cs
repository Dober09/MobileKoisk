using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.Behaviors
{
    public class ImageLoadingBehavior : Behavior<Image>
    {
        private Image _image;
        private readonly IDispatcher _dispatcher;
        private Grid _parentGrid;
        private ActivityIndicator _loadingIndicator;

        public ImageLoadingBehavior()
        {
            _dispatcher = Application.Current.Dispatcher;
        }

        protected override void OnAttachedTo(Image bindable)
        {
            base.OnAttachedTo(bindable);
            _image = bindable;

            // Find the parent Grid and ActivityIndicator
            _parentGrid = _image.Parent as Grid;
            if (_parentGrid != null)
            {
                _loadingIndicator = _parentGrid.Children.OfType<ActivityIndicator>().FirstOrDefault();
            }

            // Subscribe to source property changes
            _image.PropertyChanged += OnImagePropertyChanged;
        }

        protected override void OnDetachingFrom(Image bindable)
        {
            base.OnDetachingFrom(bindable);
            _image.PropertyChanged -= OnImagePropertyChanged;
            _image = null;
            _parentGrid = null;
            _loadingIndicator = null;
        }

        private void OnImagePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Image.SourceProperty.PropertyName)
            {
                HandleImageLoading();
            }
        }

        private async void HandleImageLoading()
        {
            if (_image.Source == null || _loadingIndicator == null)
                return;

            try
            {
                _loadingIndicator.IsVisible = true;
                _loadingIndicator.IsRunning = true;
                _image.Opacity = 0;

                // Give time for the image to load
                await Task.Delay(100);

                await _dispatcher.DispatchAsync(async () =>
                {
                    await _image.FadeTo(1, 200);
                    _loadingIndicator.IsVisible = false;
                    _loadingIndicator.IsRunning = false;
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading image: {ex.Message}");
                _loadingIndicator.IsVisible = false;
                _loadingIndicator.IsRunning = false;
                _image.Source = "placeholder_image.png"; // Make sure to add this to your resources
            }
        }
    }
}

