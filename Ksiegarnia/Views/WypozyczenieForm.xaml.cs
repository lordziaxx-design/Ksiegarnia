using Ksiegarnia.Models;
using System.Windows;
namespace Ksiegarnia
{
	public partial class WypozyczenieForm : Window
	{
		private Wypozyczenie _record;

		public bool Saved { get; private set; } = false;
		public WypozyczenieForm(Wypozyczenie record = null)
		{
			InitializeComponent();
			_record = record;
			LoadComboBoxes();
			if (_record != null)
			{

				ksiazka.SelectedValue = record.IDks;
				czyt.SelectedValue = record.IDczyt;
				dataod.SelectedDate = record.DataOD;
				datawyp.SelectedDate = record.DataDO;

				if (record.DataOddania == null)
				{
					odd.IsChecked = false;
				}
				else
				{
					odd.IsChecked = true;
				}
			}
			else
			{
				_record = new Wypozyczenie();
			}

		}
		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			if (ksiazka.SelectedItem == null)
			{
				MessageBox.Show("Ksiazka is required."); return;
			}
			if (czyt.SelectedItem == null)
			{
				MessageBox.Show("Name is required."); return;
			}


			_record.IDczyt = (int)czyt.SelectedValue;
			_record.IDks = (int)ksiazka.SelectedValue;
			_record.DataOD = dataod.SelectedDate.Value;
			_record.DataDO = datawyp.SelectedDate.Value;

			int selectedBookId = (int)ksiazka.SelectedValue;
			if (_record.IDks != selectedBookId || _record.IDwyp == 0)
			{
				using var db = new AppDbContext();
				var magazyn = db.Magazyn.Find(selectedBookId);
				if (magazyn == null || magazyn.Dostepne <= 0)
				{
					MessageBox.Show("Brak dostępnych egzemplarzy tej książki.");
					return;
				}
			}

			if (odd.IsChecked == true)
			{
				_record.DataOddania = DateTime.Now;
			}
			else
			{
				_record.DataOddania = null;
			}
		

			Saved = true;
			DialogResult = true; 
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
		private void LoadComboBoxes()
		{
			using var db = new AppDbContext();
			datawyp.SelectedDate = DateTime.Today;
			dataod.SelectedDate = DateTime.Today;
			czyt.ItemsSource = db.Czytelnicy.ToList();
			czyt.DisplayMemberPath = "ImieNazwisko";
			czyt.SelectedValuePath = "ID";

			ksiazka.ItemsSource = db.Ksiazki.ToList(); 
			ksiazka.DisplayMemberPath = "Nazwa";      
			ksiazka.SelectedValuePath = "ID_ksiazki";
		}
		public Wypozyczenie GetRecord() => _record;
	}
}