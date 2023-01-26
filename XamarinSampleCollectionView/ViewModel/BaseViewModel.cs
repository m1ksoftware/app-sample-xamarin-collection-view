namespace XamarinSampleCollectionView.ViewModel
{
	public class BaseViewModel : INotifyPropertyChanged
    {
		private bool isBusy;        

        public BaseViewModel()
		{
		}

		public bool IsBusy
		{
            get => isBusy;
            set
            {
                if (isBusy == value)
                    return;
                isBusy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public bool IsNotBusy => !IsBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

