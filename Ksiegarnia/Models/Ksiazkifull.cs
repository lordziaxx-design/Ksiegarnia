namespace Ksiegarnia.Models
{
	public class Ksiazkifull
	{
		public int Id { get; set; }
		public string Tytul { get; set; }
		public string Autor { get; set; }

		public decimal? Cena { get; set; }

		public int Stan { get; set; }
		public int Wypozyczone { get; set; }
	}
}
