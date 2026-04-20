using Ksiegarnia.Models;
using Ksiegarnia.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace Ksiegarnia.Views
{
    public partial class WypozyczeniaView : UserControl
    {
        public WypozyczeniaView()
        {
            InitializeComponent();
        }
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{

			var form = new WypozyczenieForm(null) { Owner = Window.GetWindow(this) };
			if (form.ShowDialog() == true)
			{
				var record = form.GetRecord();
				using var db = new AppDbContext();

				var wyp = new Wypozyczenie
				{
					IDks = record.IDks,
					IDczyt = record.IDczyt,
					DataOD = record.DataOD,
					DataDO = record.DataDO,
					DataOddania = record.DataOddania
				};
				db.Wypozyczenia.Add(wyp);
				var ksi = db.Magazyn.Find(wyp.IDks);
				if (wyp.DataOddania == null)
				{
					ksi.Dostepne--;
					ksi.Wypozyczone++;
				}
				
				db.SaveChanges();

				var vm = (WypozyczeniaViewModel)DataContext;
				vm.LoadData();
			}
		}
		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			var rek = (WypozyczenieDisplay)((Button)sender).Tag;

			Wypozyczenie rekord;
			using (var db = new AppDbContext())
				rekord = db.Wypozyczenia.Find(rek.IDwyp);

			var form = new WypozyczenieForm(rekord) { Owner = Window.GetWindow(this) };
			if (form.ShowDialog() == true)
			{
				var record = form.GetRecord();
				using var db = new AppDbContext();

				var wyp = db.Wypozyczenia.Find(record.IDwyp);
				var ksi = db.Magazyn.Find(wyp.IDks);

				if (wyp.DataOddania != record.DataOddania)
				{
					if (wyp.DataOddania == null)
					{
						ksi.Dostepne++;
						ksi.Wypozyczone--;
					}
					else
					{
						ksi.Dostepne--;
						ksi.Wypozyczone++;
					}
				}

				wyp.IDks = record.IDks;
				wyp.IDczyt = record.IDczyt;
				wyp.DataOD = record.DataOD;
				wyp.DataDO = record.DataDO;
				wyp.DataOddania = record.DataOddania;

				db.SaveChanges();
				var vm = (WypozyczeniaViewModel)DataContext;
				vm.LoadData();
			}
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var record = (WypozyczenieDisplay)((Button)sender).Tag;
			var confirm = MessageBox.Show($"Usunąć ?", "Potwierdź", MessageBoxButton.YesNo);
			if (confirm == MessageBoxResult.Yes)
			{
				using var db = new AppDbContext();

				var wyp = db.Wypozyczenia.FirstOrDefault(m => m.IDwyp == record.IDwyp);
				if (wyp != null)
				{
					var ksi = db.Magazyn.Find(wyp.IDks);
				
				
					if (wyp.DataOddania != null)
					{
						ksi.Dostepne++;
						ksi.Wypozyczone--;
					}
					db.Wypozyczenia.Remove(wyp);
				}
					
				db.SaveChanges();
				var vm = (WypozyczeniaViewModel)DataContext;
				vm.LoadData();
			}
		}
	}
}
