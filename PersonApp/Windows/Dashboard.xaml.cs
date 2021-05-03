using PersonCore.Dto;
using PersonCore.Interfaces;
using PersonCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PersonApp.Windows
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {


        private readonly IPersonRepository _ipersonRepository;
        public Dashboard(IPersonRepository person)
        {
            InitializeComponent();
            this._ipersonRepository = person;
            LoadData();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            var error = Validation();

            if (error)
            {
                var result = await _ipersonRepository.AddPersonAsync(new PersonCore.Dto.PersonDto
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    Employed = chbEmployed.IsChecked.HasValue == true ? (byte?)10 : (byte?)20,
                    Gender = rbMale.IsChecked.HasValue ? "M" : "F",
                    CivilStatus = GetComboValue(dpCivileStatus),
                    Birthday = dtBirthday.SelectedDate.Value,
                    BirthPlace = txtBirthPlace.Text,
                    PhoneNumber = txtPhoneNumber.Text
                });

                MessageBox.Show(result.ErrorMessage);
                ClearFields();
                LoadData();
            }


        }

        private async void LoadData()
        {
            var data = await _ipersonRepository.GetAllPersonsAsync();
            PersonDataGrid.ItemsSource = data;
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtLastName.Text = "";
            txtPhoneNumber.Text = "";
            dtBirthday.Text = "";
            txtBirthPlace.Text = "";
            chbEmployed.IsChecked = false;
            dpCivileStatus.SelectedItem = ((ComboBoxItem)dpCivileStatus.Items[0]);

            rbFemale.IsChecked = false;
            rbMale.IsChecked = false;
            BtnSave.Visibility = Visibility.Visible;
            BtnUpdate.Visibility = Visibility.Hidden;
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            var data = await _ipersonRepository.SearchPersonByTextAsync(txtSearch.Text);
            PersonDataGrid.ItemsSource = data;
        }


        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            PersonDto data = (PersonDto)row.Item;

            txtName.Text = data.Name;
            txtLastName.Text = data?.LastName;
            txtBirthPlace.Text = data?.BirthPlace?.ToString();
            txtPhoneNumber.Text = data?.PhoneNumber;

            int i = IndexOfCombo(data.CivilStatus);

            BtnSave.Visibility = Visibility.Hidden;
            BtnUpdate.Visibility = Visibility.Visible;
            hiddenId.Text = data.Id.ToString();
            dtBirthday.SelectedDate = data.Birthday.Value.Date;
            chbEmployed.IsChecked = data.Employed == 10 ? true : false;
            rbFemale.IsChecked = data.Gender == "F" ? true : false;
            rbMale.IsChecked = data.Gender == "M" ? true : false;
            dpCivileStatus.SelectedItem = ((ComboBoxItem)dpCivileStatus.Items[i]); ;


        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            var error = Validation();
            int.TryParse(hiddenId.Text, out int id);
            if (error)
            {
                var update = await _ipersonRepository.UpdatePersonAsync(new PersonDto
                {
                    Id = id,
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    Employed = chbEmployed.IsChecked.HasValue == true ? (byte?)10 : (byte?)20,
                    Gender = rbMale.IsChecked.HasValue ? "M" : "F",
                    CivilStatus = GetComboValue(dpCivileStatus),
                    Birthday = dtBirthday.SelectedDate.Value,
                    BirthPlace = txtBirthPlace.Text,
                    PhoneNumber = txtPhoneNumber.Text
                });
                MessageBox.Show(update.ErrorMessage);
                ClearFields();
                LoadData();
            }

        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(hiddenId.Text, out int id);

            var deleted = await _ipersonRepository.DeletePersonAsync(id);
            if (deleted)
            {
                MessageBox.Show("Personi u fshi me sukses");
            }
            else
            {
                MessageBox.Show("Ka ndodhur nje gabim .");
            }
            LoadData();

        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        bool Validation()
        {

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Ju lutem plotesoni emrin!");
                return false;
            }
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Ju lutem plotesoni mbiemri!");
                return false;
            }
            if (string.IsNullOrEmpty(dtBirthday.Text))
            {
                MessageBox.Show("Ju lutem plotesoni Datelindjen!");
                return false;
            }
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                MessageBox.Show("Ju lutem plotesoni numri e telefonit!");
                return false;
            }
            if (string.IsNullOrEmpty(txtBirthPlace.Text))
            {
                MessageBox.Show("Ju lutem plotesoni vendlindjen!");
                return false;
            }
            if (!rbMale.IsChecked.Value && !rbFemale.IsChecked.Value)
            {
                MessageBox.Show("Ju lutem plotesoni Gjinine!");
                return false;
            }
            if (dpCivileStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Ju lutem plotesoni Statusin Civil!");
                return false;
            }
            return true;


        }


        int IndexOfCombo(string s)
        {
            if (s.Contains("I martuar"))
                return 1;
            if (s.Contains("Beqar"))
                return 2;
            if (s.Contains("I divorcuar"))
                return 3;
            return 0;
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            BtnUpdate.Visibility = Visibility.Hidden;
            BtnSave.Visibility = Visibility.Visible;


        }
        string GetComboValue(ComboBox combo)
        {
            ComboBoxItem typeItem = (ComboBoxItem)combo.SelectedItem;
            return typeItem.Content.ToString();
        }
    }
}
