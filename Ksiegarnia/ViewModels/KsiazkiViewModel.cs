using Ksiegarnia;
using System.Collections.ObjectModel;
using Ksiegarnia.Models;

namespace Ksiegarnia.ViewModels
{
	public class KsiazkiViewModel
	{
		public ObservableCollection<Ksiazka> Books { get; set; }

		public KsiazkiViewModel()
		{
			Books = new ObservableCollection<Ksiazka>();
			LoadData();
		}

		private void LoadData()
		{
			using var db = new AppDbContext();
			foreach (var book in db.Ksiazki)
				Books.Add(book);
		}
	}
}