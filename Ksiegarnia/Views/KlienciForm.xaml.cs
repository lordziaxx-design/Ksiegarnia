using Ksiegarnia.Models;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace Ksiegarnia.Views
{
	public partial class KlienciForm : Window
	{
		public KlienciForm()
		{
			InitializeComponent();
			string connectionString = "Server=localhost\\SQLEXPRESS;Database=Ksiegarnia;Trusted_Connection=True;TrustServerCertificate=True;";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					MessageBox.Show("Connected!");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
	}
}
