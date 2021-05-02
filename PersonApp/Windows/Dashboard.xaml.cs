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

        // private readonly IPersonRepository _ipersonRepository = new PersonRepository(new PersonEntity.Entity.PersonDbEntities());
        private readonly IPersonRepository _ipersonRepository;
        public Dashboard(IPersonRepository person)
        {
            InitializeComponent();
            this._ipersonRepository = person;
            LoadData();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {

            var result = await _ipersonRepository.AddPersonAsync(new PersonCore.Dto.PersonDto
            {
                Name = txtName.Text,
                LastName = txtLastName.Text,
                Employed = chbEmployed.IsChecked.HasValue == true ? (byte?)10 : (byte?)20,
                Gender = rbMale.IsChecked.HasValue ? "M" : "F",
                CivilStatus = dpCivileStatus.SelectedItem.ToString(),
                Birthday = dtBirthday.SelectedDate.Value,
                PhoneNumber = txtPhoneNumber.Text
            });
            MessageBox.Show(result.ErrorMessage);
            ClearFields();
            LoadData();
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
            chbEmployed.IsChecked = false;
            dpCivileStatus.SelectedItem = -1;
            BtnSave.Visibility = Visibility.Visible;
            BtnUpdate.Visibility = Visibility.Hidden;
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            var data = await _ipersonRepository.GetPersonByTextAsync(txtSearch.Text);
            PersonDataGrid.ItemsSource = data;
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            PersonDto dataselected = (PersonDto)row.Item;

            txtName.Text = dataselected.Name;
            txtLastName.Text = dataselected.LastName;
            BtnSave.Visibility = Visibility.Hidden;
            BtnUpdate.Visibility = Visibility.Visible;
            hiddenId.Text = dataselected.Id.ToString();


            // Some operations with this row
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {

            var update = await _ipersonRepository.UpdatePersonAsync(new PersonDto
            {
                Name = txtName.Text,
                LastName = txtLastName.Text,
                Id = TabIndex,
                Birthday = dtBirthday.SelectedDate.Value,
                PhoneNumber = txtPhoneNumber.Text
            });
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

    }
}
