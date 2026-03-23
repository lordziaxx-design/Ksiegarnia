using Ksiegarnia.Models;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Xml.Linq;
namespace Ksiegarnia
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class KsiazkaForm : Window
	{
		private Ksiazkifull _record;
		public bool Saved { get; private set; } = false;

		// Called with null for Add, or an existing record for Edit
		public KsiazkaForm(Ksiazkifull record = null)
		{
			InitializeComponent();
			_record = record;

			if (_record != null) // EDIT mode — pre-fill the form
			{
				nazwa.Text = _record.Tytul;
				autor.Text = _record.Autor;
				cena.Text = _record.Cena.ToString();
				mag.Text = _record.Stan.ToString();
			}

		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			// Basic validation
			if (string.IsNullOrWhiteSpace(nazwa.Text))
			{
				MessageBox.Show("Name is required."); return;
			}
			if (!decimal.TryParse(cena.Text, out decimal price))
			{
				MessageBox.Show("Enter a valid price."); return;
			}

			_record.Tytul = nazwa.Text.Trim();
			_record.Cena = price;
			_record.Stan = int.Parse(mag.Text);

			Saved = true;
			DialogResult = true; // closes the dialog
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		public Ksiazkifull GetRecord() => _record;
	}
}