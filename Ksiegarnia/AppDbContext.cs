using Ksiegarnia;
using Ksiegarnia.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
				var baseConn = Environment.GetEnvironmentVariable("DB_CONN") ??
							   Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

				if (string.IsNullOrWhiteSpace(baseConn))
				{
					baseConn = "Server=localhost\\SQLEXPRESS;Database=Ksiegarnia;Trusted_Connection=True;TrustServerCertificate=True;";
				}

				var builder = new SqlConnectionStringBuilder(baseConn)
				{
					Encrypt = true,
					PersistSecurityInfo = false,
					ConnectTimeout = 30
				};

				// jeśli używamy LocalDB lub Integrated Security, wyłącz szyfrowanie (LocalDB nie wspiera)
				if (!string.IsNullOrWhiteSpace(builder.DataSource) &&
					(builder.DataSource.StartsWith("(localdb)", StringComparison.OrdinalIgnoreCase) || builder.IntegratedSecurity))
				{
					builder.Encrypt = false;
				}

				optionsBuilder.UseSqlServer(builder.ConnectionString);
			}
		}

	}
}