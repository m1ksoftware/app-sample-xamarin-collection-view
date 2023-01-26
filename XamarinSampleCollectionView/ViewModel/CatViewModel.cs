using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinSampleCollectionView.ViewModel
{
    public class CatViewModel : BaseViewModel
    {
        private HttpClient httpClient;

        public CatViewModel()
        {
            this.GetCatsCommand = new Command(async () => await GetCatsAsync());
        }

        public HttpClient CustomHttpClient => httpClient ?? (httpClient = new HttpClient());

        public ObservableCollection<Model.Cat> Cats { get; }

        public Command GetCatsCommand { get; }

        public async Task GetCatsAsync()
        {
            if (this.IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                var json = await CustomHttpClient.GetStringAsync("https://montemagno.com/monkeys.json");
                var cats = Cat.FromJson(json);

                Cats.Clear();

                foreach (var cat in cats)
                {
                    Cats.Add(cat);
                }
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

