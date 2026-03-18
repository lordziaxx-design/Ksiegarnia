using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksiegarnia.Models
{
	[Table("Ksiazki")]
	public class Ksiazka
	{
		[Key]
		public int ID_ksiazki { get; set; }
		public string Nazwa { get; set; }
		public string Autor { get; set; }
		public decimal Cena { get; set; }
	}
}