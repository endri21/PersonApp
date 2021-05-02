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

        private readonly IPersonRepository _ipersonRepository = new PersonRepository(new PersonEntity.Entity.PersonDbEntities());
        public Dashboard()
        {
            InitializeComponent();
            LoadData();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {

            var result = await _ipersonRepository.AddPersonAsync(new PersonCore.Dto.PersonDto
            {
                Name = txtName.Text,
                LastName = txtLastName.Text,
                
                Birthday = txtBirthday.SelectedDate.Value,
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
            txtBirthday.Text = "";
        }

        private  async void Search_Click(object sender, RoutedEventArgs e)
        {
            var data = await _ipersonRepository.GetPersonByTextAsync(txtSearch.Text);
            PersonDataGrid.ItemsSource = data;
        }
        void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
                PersonDto dataselected =  (PersonDto)row.Item;
            txtName.Text = dataselected.Name;
            txtLastName.Text = dataselected.LastName;
            BtnSave.Visibility = Visibility.Hidden;
            BtnUpdate.Visibility = Visibility.Visible;

            // Some operations with this row
        }

        async void Update_Click(object sender, RoutedEventArgs e)
        {
            var update = await _ipersonRepository.UpdatePersonAsync(new PersonDto
            {
                Name = txtName.Text,
                LastName = txtLastName.Text,
                Id = TabIndex, 
                Birthday = txtBirthday.SelectedDate.Value,
                PhoneNumber = txtPhoneNumber.Text
            });
        }
    }
}
