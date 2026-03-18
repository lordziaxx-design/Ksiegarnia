using Ksiegarnia;
using System.Collections.ObjectModel;
using System.Linq;
using Ksiegarnia.Models;

namespace Ksiegarnia.ViewModels
{
	// A helper class just for display — combines data from 3 tables
	public class WypozyczenieDisplay
	{
		public int IDwyp { get; set; }
		public string Ksiazka { get; set; }
		public string Czytelnik { get; set; }
		public string DataOD { get; set; }
		public string DataDO { get; set; }
		public string DataOddania { get; set; }
	}

	public class WypozyczeniaViewModel
	{
		public ObservableCollection<WypozyczenieDisplay> Rentals { get; set; }

		public WypozyczeniaViewModel()
		{
			Rentals = new ObservableCollection<WypozyczenieDisplay>();
			LoadData();
		}

		private void LoadData()
		{
			using var db = new AppDbContext();

			var query = from w in db.Wypozyczenia
						join k in db.Ksiazki on w.IDks equals k.ID_ksiazki
						join c in db.Czytelnicy on w.IDczyt equals c.ID
						select new WypozyczenieDisplay
						{
							IDwyp = w.IDwyp,
							Ksiazka = k.Nazwa,
							Czytelnik = c.Imie + " " + c.Nazwisko,
							DataOD = w.DataOD.ToShortDateString(),
							DataDO = w.DataDO.ToShortDateString(),
							DataOddania = w.DataOddania.HasValue
											? w.DataOddania.Value.ToShortDateString()
											: "Nie oddano"
						};

			foreach (var item in query)
				Rentals.Add(item);
		}
	}
}