using Ksiegarnia;
using Ksiegarnia.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace Ksiegarnia
{
	public class AppDbContext : DbContext
	{
		// Each DbSet = one table
		public DbSet<Ksiazka> Ksiazki { get; set; }
		public DbSet<Czytelnik> Czytelnicy { get; set; }
		public DbSet<Wypozyczenie> Wypozyczenia { get; set; }
		public DbSet<Magazyn> Magazyn { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				IConfigurationRoot configuration = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("settings.json").Build();

				var connectionString = configuration.GetConnectionString("DefaultConnection");
				optionsBuilder.UseSqlServer(connectionString);
			}
		}

	}
}