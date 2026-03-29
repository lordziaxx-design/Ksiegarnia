using Ksiegarnia.Models;
using Ksiegarnia.ViewModels;
using Ksiegarnia.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace Ksiegarnia.Views
{
    /// <summary>
    /// Logika interakcji dla klasy KsiazkiView.xaml
    /// </summary>
    public partial class KsiazkiView : UserControl
    {
        public KsiazkiView()
        {
            InitializeComponent();
        }

		// ADD
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			
			var form = new KsiazkaForm(null) { Owner = Window.GetWindow(this) };
			if (form.ShowDialog() == true)
			{
				var record = form.GetRecord();
				using var db = new AppDbContext();

				var ksiazka = new Ksiazka
				{
					Nazwa = record.Tytul,
					Autor = record.Autor,
					Cena = record.Cena
				};
				db.Ksiazki.Add(ksiazka);
				db.SaveChanges(); // ← zapisz najpierw żeby dostać Id

				var magazyn = new Magazyn
				{
					ID_ksiazki = ksiazka.ID_ksiazki, // ← teraz Id już istnieje
					Dostepne = record.Stan,
					Wypozyczone = 0
				};
				db.Magazyn.Add(magazyn);
				db.SaveChanges();
				var vm = (KsiazkiViewModel)DataContext;
				vm.LoadData();
			}
		}

		// EDIT
		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			var rekord = (Ksiazkifull)((Button)sender).Tag;
			var form = new KsiazkaForm(rekord) { Owner = Window.GetWindow(this) };
			if (form.ShowDialog() == true)
			{
				var record = form.GetRecord();
				using var db = new AppDbContext();

				var ksiazka = db.Ksiazki.Find(record.Id);
				ksiazka.Nazwa = record.Tytul;
				ksiazka.Autor = record.Autor;
				ksiazka.Cena = record.Cena;

				var magazyn = db.Magazyn.First(m => m.ID_ksiazki == record.Id);
				magazyn.Dostepne = record.Stan;

				db.SaveChanges(); // ← jeden SaveChanges na końcu wystarczy
				var vm = (KsiazkiViewModel)DataContext;
				vm.LoadData();
			}
		}

		// DELETE
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var record = (Ksiazkifull)((Button)sender).Tag;
			var confirm = MessageBox.Show($"Usunąć '{record.Tytul}'?", "Potwierdź", MessageBoxButton.YesNo);
			if (confirm == MessageBoxResult.Yes)
			{
				using var db = new AppDbContext();

				var magazyn = db.Magazyn.FirstOrDefault(m => m.ID_ksiazki == record.Id);
				if (magazyn != null)
					db.Magazyn.Remove(magazyn);
				db.SaveChanges();
				var ksiazka = db.Ksiazki.Find(record.Id);
				if (ksiazka != null)
					db.Ksiazki.Remove(ksiazka);

				db.SaveChanges();
				var vm = (KsiazkiViewModel)DataContext;
				vm.LoadData();
			}
		}
	}
}
