using PersonCore.Interfaces;
using PersonCore.Dto;
using PersonCore.Repositories;
using PersonEntity.Entity;
using System.Windows;

namespace PersonApp.Windows
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {


        private readonly IUserRepository _userRepository;//= new UserRepository(new PersonDbEntities());

        public Register(IUserRepository user)
        {
            InitializeComponent();
            this._userRepository = user;
        }

        private async void Sign_Click(object sender, RoutedEventArgs e)
        {
            var result = await _userRepository.AddUserAsync(new UserRegisterRequest()
            {
                Username = Username.Text,
                Email = Email.Text,
                Name = Name.Text,
                Address = Address.Text,
                Password = Password.Password.ToString()
            });

            MessageBox.Show(result.ErrorMessage);

        }

   

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            //MainWindow main = new MainWindow(
            //    new LoginProviderRepository(_userRepository,new PersonDbEntities()));
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
