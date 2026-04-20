using Ksiegarnia.Models;
using Ksiegarnia.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Ksiegarnia.Views
{

    public partial class KlienciView : UserControl
    {
        public KlienciView()
        {
            InitializeComponent();
        }

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{

			var form = new KlienciForm(null) { Owner = Window.GetWindow(this) };
			if (form.ShowDialog() == true)
			{
				var record = form.GetRecord();
				using var db = new AppDbContext();

				var czytelnik = new Czytelnik
				{
					Imie = record.Imie,
					Nazwisko = record.Nazwisko,
					Email = record.Email,
					Telefon = record.Telefon
				};
				db.Czytelnicy.Add(czytelnik);
				db.SaveChanges(); 
				var vm = (KlienciViewModel)DataContext;
				vm.LoadData();
			}
		}

		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			var rekord = (Czytelnik)((Button)sender).Tag;
			var form = new KlienciForm(rekord) { Owner = Window.GetWindow(this) };
			if (form.ShowDialog() == true)
			{
				var record = form.GetRecord();
				using var db = new AppDbContext();

				var czytelnik = db.Czytelnicy.Find(record.ID);
				czytelnik.Imie = record.Imie;
				czytelnik.Nazwisko = record.Nazwisko;
				czytelnik.Email = record.Email;
				czytelnik.Telefon = record.Telefon;

				db.SaveChanges(); 
				var vm = (KlienciViewModel)DataContext;
				vm.LoadData();
			}
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var record = (Czytelnik)((Button)sender).Tag;
			var confirm = MessageBox.Show($"Usunąć '{record.Nazwisko}'?", "Potwierdź", MessageBoxButton.YesNo);
			if (confirm == MessageBoxResult.Yes)
			{
				using var db = new AppDbContext();

				var czytelnik = db.Czytelnicy.FirstOrDefault(m => m.ID == record.ID);
				if (czytelnik != null)
					db.Czytelnicy.Remove(czytelnik);
				db.SaveChanges();
				var vm = (KlienciViewModel)DataContext;
				vm.LoadData();
			}
		}
	}
}
