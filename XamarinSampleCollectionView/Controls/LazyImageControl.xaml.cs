using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinSampleCollectionView.ViewModel;

namespace XamarinSampleCollectionView.Controls
{
    public partial class LazyImageControl : ContentView
    {
        public static BindableProperty CurrentViewModelProperty = BindableProperty.Create(
            "CurrentViewModel",
            typeof(CatViewModel),
            typeof(LazyImageControl),
            null,
            BindingMode.OneWay);

        public LazyImageControl()
        {
            InitializeComponent();         
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.BindingContext is Model.Cat cat)
            {
                if (cat.ImageSource == null)
                {
                    this.image.IsVisible = false;
                    this.activityIndicator.IsVisible = true;

                    Task.Run(async () =>
                    {
                        try
                        {
                            var originalId = cat.Id;

                            ImageSource imageSource = ImageSource.FromUri(new Uri(cat.ImageUrl));

                            if (imageSource != null)
                            {
                                // Random delay added for testing purpose,
                                // to see and simulate the loading status in
                                // CollectionView
                                Random rand = new Random();
                                int number = rand.Next(2, 4);
                                await Task.Delay(number * 1000);

                                // To avoid to reload the
                                // images again on CollectionView scroll,
                                // we need to update the object in
                                // collection view
                                var catInCollection = this.CurrentViewModel.Cats.FirstOrDefault(c => c.Id == originalId);
                                catInCollection.ImageSource = imageSource;

                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    if (this.image != null && this.activityIndicator != null)
                                    {
                                        Debug.WriteLine($"Original cat ID {originalId}. Loaded image for cat id {cat.Id}");
                                        this.image.Source = imageSource;
                                        this.activityIndicator.IsVisible = false;
                                        this.image.IsVisible = true;
                                    }
                                });
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("Failed to load the image: {0}", e.Message);
                        }
                    });
                }
                else
                {
                    this.image.Source = cat.ImageSource;
                    this.activityIndicator.IsVisible = false;
                    this.image.IsVisible = true;                    
                }
            }
        }       

        public CatViewModel CurrentViewModel
        {
            get => (CatViewModel)this.GetValue(CurrentViewModelProperty);
            set
            {
                this.SetValue(CurrentViewModelProperty, value);
            }
        }
    }
}