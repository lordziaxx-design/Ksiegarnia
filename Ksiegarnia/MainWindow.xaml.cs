using System.Text;
using System.Windows;
using Microsoft.Data.SqlClient;
namespace Ksiegarnia
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
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