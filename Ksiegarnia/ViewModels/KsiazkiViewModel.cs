
using System.Collections.ObjectModel;
using Ksiegarnia.Models;

namespace Ksiegarnia.ViewModels
{
	public class KsiazkiViewModel
	{
		public ObservableCollection<Ksiazka> Books { get; set; }
		public ObservableCollection<Magazyn> Booksnum { get; set; }
		public ObservableCollection<Ksiazkifull> Booksfull { get; set; }

		public KsiazkiViewModel()
		{
			Books = new ObservableCollection<Ksiazka>();
			Booksnum = new ObservableCollection<Magazyn>();
			Booksfull = new ObservableCollection<Ksiazkifull>();
			LoadData();
		}

		public void LoadData()
		{
			Books.Clear();
			Booksnum.Clear();
			Booksfull.Clear();
			using var db = new AppDbContext();
			foreach (var book in db.Ksiazki)
				Books.Add(book);
			foreach (var booknum in db.Magazyn)
				Booksnum.Add(booknum);
			CombineData();
		}
		private void CombineData()
		{
			var query = from b in Books
						join m in Booksnum
						on b.ID_ksiazki equals m.ID_ksiazki
						select new Ksiazkifull
						{
							Id = b.ID_ksiazki,
							Tytul = b.Nazwa,
							Autor = b.Autor,
							Cena = b.Cena ?? 0m,
							Stan = m.Dostepne,
							Wypozyczone = m.Wypozyczone
						};

			foreach (var item in query)
				Booksfull.Add(item);
		}
	}
}
