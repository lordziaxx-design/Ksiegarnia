using Ksiegarnia.Models;
using System.Windows;
namespace Ksiegarnia
{

	public partial class KsiazkaForm : Window
	{
		private Ksiazkifull _record;
		public bool Saved { get; private set; } = false;

		public KsiazkaForm(Ksiazkifull record = null)
		{
			InitializeComponent();
			_record = record;

			if (_record != null)
			{
				nazwa.Text = _record.Tytul;
				autor.Text = _record.Autor;
				cena.Text = _record.Cena.ToString();
				mag.Text = _record.Stan.ToString();
			}
			else
			{
				_record = new Ksiazkifull(); 
			}

		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			// Basic validation
			if (string.IsNullOrWhiteSpace(nazwa.Text) || !nazwa.Text.All(char.IsLetter)	)
			{
				MessageBox.Show("Name is required."); return;
			}
			if (string.IsNullOrWhiteSpace(autor.Text) || !autor.Text.All(char.IsLetter))
			{
				MessageBox.Show("Author is required."); return;
			}
			if (!decimal.TryParse(cena.Text, out decimal price))
			{
				MessageBox.Show("Enter a valid price."); return;
			}

			_record.Tytul = nazwa.Text.Trim();
			_record.Autor = autor.Text.Trim();
			_record.Cena = price;
			_record.Stan = int.Parse(mag.Text);

			Saved = true;
			DialogResult = true; 
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		public Ksiazkifull GetRecord() => _record;
	}
}