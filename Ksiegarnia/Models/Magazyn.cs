using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksiegarnia.Models
{
	[Table("Magazyn")]
	public class Magazyn
	{
		[Key]
		public int ID { get; set; }
		public int ID_ksiazki { get; set; }
		public int Dostepne { get; set; }
		public int Wypozyczone { get; set; }
	}
}