using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinSampleCollectionView.ViewModel
{
    public class CatViewModel : MvvmHelpers.BaseViewModel
    {
        private HttpClient httpClient;

        private bool initialLoading;

        public CatViewModel()
        {
            this.httpClient = new HttpClient();
            this.Cats = new ObservableCollection<Model.Cat>();
            this.GetCatsCommand = new Command(async () => await GetCatsAsync());

            //this.InitialLoading = true;
            _ = this.GetCatsAsync();
        }

        public ObservableCollection<Model.Cat> Cats { get; set; }

        public Command GetCatsCommand { get; }

        public bool InitialLoading
        {
            get => this.initialLoading;
            set
            {
                this.SetProperty(ref this.initialLoading, value);
            }
        }

        public async Task GetCatsAsync()
        {
            if (this.IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                var json = await this.httpClient.GetStringAsync(
                    "https://raw.githubusercontent.com/m1ksoftware/app-sample-xamarin-collection-view/master/XamarinSampleCollectionView/data.json");

                var cats = Model.Cat.FromJson(json);

                Cats.Clear();

                foreach (var cat in cats)
                {
                    Cats.Add(cat);
                }

                this.InitialLoading = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get the list of cats: {ex.Message}");

                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }        
    }
}

