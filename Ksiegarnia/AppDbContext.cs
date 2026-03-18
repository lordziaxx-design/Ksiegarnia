using Ksiegarnia.Models;
using Microsoft.EntityFrameworkCore;
using Ksiegarnia;

namespace Ksiegarnia
{
	public class AppDbContext : DbContext
	{
		// Each DbSet = one table
		public DbSet<Ksiazka> Ksiazki { get; set; }
		public DbSet<Czytelnik> Czytelnicy { get; set; }
		public DbSet<Wypozyczenie> Wypozyczenia { get; set; }
		public DbSet<Magazyn> Magazyn { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer(
				@"Server=.\SQLEXPRESS;Database=Ksiegarnia;Trusted_Connection=True;TrustServerCertificate=True;"
			);
		}
	}
}