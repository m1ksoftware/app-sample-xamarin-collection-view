using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSampleCollectionView.ViewModel;

namespace XamarinSampleCollectionView
{
    public partial class MainPage : ContentPage
    {
        private CatViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this.viewModel = new CatViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            
        }
    }
}

