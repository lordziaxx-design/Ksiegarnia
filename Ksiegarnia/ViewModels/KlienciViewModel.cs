using Ksiegarnia;
using Ksiegarnia.Models;
using System.Collections.ObjectModel;
using Ksiegarnia.Models;

namespace Ksiegarnia.ViewModels
{
	public class KlienciViewModel
	{
		public ObservableCollection<Czytelnik> Clients { get; set; }

		public KlienciViewModel()
		{
			Clients = new ObservableCollection<Czytelnik>();
			LoadData();
		}

		public void LoadData()
		{
			Clients.Clear();
			using var db = new AppDbContext();
			foreach (var client in db.Czytelnicy)
				Clients.Add(client);
		}
	}
}