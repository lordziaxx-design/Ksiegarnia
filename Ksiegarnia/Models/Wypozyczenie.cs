using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksiegarnia.Models
{
	[Table("Wypozyczenia")]
	public class Wypozyczenie
	{
		[Key]
		public int IDwyp { get; set; }
		public int IDks { get; set; }
		public int IDczyt { get; set; }
		public DateTime DataOD { get; set; }
		public DateTime DataDO { get; set; }
		public DateTime? DataOddania { get; set; } // nullable — can be empty
	}
}