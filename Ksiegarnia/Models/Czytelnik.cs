using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksiegarnia.Models
{
	[Table("Czytelnik")]
	public class Czytelnik
	{
		[Key]
		public int ID { get; set; }
		public string Imie { get; set; }
		public string Nazwisko { get; set; }

		[Column("e-mail")]
		public string Email { get; set; }

		[Column("nr.tel")]
		public string Telefon { get; set; }

		[NotMapped] 
		public string ImieNazwisko => $"{ID}.{Imie.Trim()} {Nazwisko.Trim()}";
	}
}