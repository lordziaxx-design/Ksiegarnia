using Ksiegarnia.Models;
using Ksiegarnia.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ksiegarnia.Views
{
    /// <summary>
    /// Logika interakcji dla klasy WypozyczeniaView.xaml
    /// </summary>
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
				
				db.SaveChanges(); // ← zapisz najpierw żeby dostać Id

				var vm = (WypozyczeniaViewModel)DataContext;
				vm.LoadData();
			}
		}
		// EDIT
		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			var rek = (WypozyczenieDisplay)((Button)sender).Tag;
			using var db = new AppDbContext();
			Wypozyczenie rekord = db.Wypozyczenia.Find(rek.IDwyp);
			var form = new WypozyczenieForm(rekord) { Owner = Window.GetWindow(this) };
			if (form.ShowDialog() == true)
			{
				var record = form.GetRecord();

				var wyp = db.Wypozyczenia.Find(record.IDwyp);
				wyp.IDks = record.IDks;
				wyp.IDczyt = record.IDczyt;
				wyp.DataOD = record.DataOD;
				wyp.DataDO = record.DataDO;
				var ksi = db.Magazyn.Find(wyp.IDks);
				if (wyp.DataOddania != record.DataOddania)
				{
					if(wyp.DataOddania == null)
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
				wyp.DataOddania = record.DataOddania;
				

				db.SaveChanges(); // ← jeden SaveChanges na końcu wystarczy
				var vm = (WypozyczeniaViewModel)DataContext;
				vm.LoadData();
			}
		}

		// DELETE
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var record = (WypozyczenieDisplay)((Button)sender).Tag;
			var confirm = MessageBox.Show($"Usunąć ?", "Potwierdź", MessageBoxButton.YesNo);
			if (confirm == MessageBoxResult.Yes)
			{
				using var db = new AppDbContext();

				var wyp = db.Wypozyczenia.FirstOrDefault(m => m.IDwyp == record.IDwyp);

				var ksi = db.Magazyn.Find(wyp.IDks);
				
				if (wyp != null)
				{
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
