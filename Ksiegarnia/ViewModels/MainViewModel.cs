using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Ksiegarnia.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private object _currentView;
		public object CurrentView
		{
			get => _currentView;
			set { _currentView = value; OnPropertyChanged(); }
		}

		public KsiazkiViewModel KsiazkiVM { get; } = new KsiazkiViewModel();
		public KlienciViewModel KlienciVM { get; } = new KlienciViewModel();
		public WypozyczeniaViewModel WypozyczeniaVM { get; } = new WypozyczeniaViewModel();

		public ICommand NavigateCommand { get; }

		public MainViewModel()
		{
			NavigateCommand = new RelayCommand(Navigate);
			CurrentView = KsiazkiVM; // default view on startup
		}

		private void Navigate(object parameter)
		{
			CurrentView = parameter switch
			{
				"ksiazki" => (object)KsiazkiVM,
				"klienci" => KlienciVM,
				"wypozyczenia" => WypozyczeniaVM,
				_ => CurrentView
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}