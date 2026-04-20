using Ksiegarnia.Models;
using System.Windows;

namespace Ksiegarnia.Views
{
	public partial class KlienciForm : Window
	{
		private Czytelnik _record;
		public bool Saved { get; private set; } = false;
		public KlienciForm(Czytelnik record = null)
		{
			InitializeComponent();
			_record = record;

			if (_record != null) 
			{
				imie.Text = _record.Imie;
				nazwisko.Text = _record.Nazwisko;
				email.Text = _record.Email;
				telefon.Text = _record.Telefon;
			}
			else
			{
				_record = new Czytelnik();
			}

		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			// Basic validation
			if (string.IsNullOrWhiteSpace(imie.Text) || !imie.Text.All(char.IsLetter))
			{
				MessageBox.Show("Name is required."); return;
			}
			if (string.IsNullOrWhiteSpace(nazwisko.Text) || !nazwisko.Text.All(char.IsLetter))
			{
				MessageBox.Show("Last name is required."); return;
			}
			if (string.IsNullOrWhiteSpace(telefon.Text) || !telefon.Text.All(char.IsDigit))
			{
				MessageBox.Show("Enter a valid phone number."); return;
			}

			_record.Imie = imie.Text.Trim();
			_record.Nazwisko = nazwisko.Text.Trim();
			_record.Email = email.Text.Trim();
			_record.Telefon = telefon.Text.Trim();

			Saved = true;
			DialogResult = true;
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		public Czytelnik GetRecord() => _record;
	}
}

