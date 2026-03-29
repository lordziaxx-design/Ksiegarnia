using Ksiegarnia.Models;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Interop;

namespace Ksiegarnia.Views
{
	public partial class KlienciForm : Window
	{
		private Czytelnik _record;
		public bool Saved { get; private set; } = false;

		// Called with null for Add, or an existing record for Edit
		public KlienciForm(Czytelnik record = null)
		{
			InitializeComponent();
			_record = record;

			if (_record != null) // EDIT mode — pre-fill the form
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
			if (string.IsNullOrWhiteSpace(imie.Text))
			{
				MessageBox.Show("Name is required."); return;
			}
			if (string.IsNullOrWhiteSpace(nazwisko.Text))
			{
				MessageBox.Show("Name is required."); return;
			}
			if (string.IsNullOrWhiteSpace(telefon.Text))
			{
				MessageBox.Show("Enter a valid price."); return;
			}

			_record.Imie = imie.Text.Trim();
			_record.Nazwisko = nazwisko.Text.Trim();
			_record.Email = email.Text.Trim();
			_record.Telefon = telefon.Text.Trim();

			Saved = true;
			DialogResult = true; // closes the dialog
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		public Czytelnik GetRecord() => _record;
	}
}

