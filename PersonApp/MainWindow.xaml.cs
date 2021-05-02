using PersonCore.Interfaces;
using PersonCore.Models;
using PersonCore.Repositories;
using System.Windows;
using PersonEntity.Entity;
using PersonApp.Windows;

namespace PersonApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



       // private readonly ILoginProviderRepository _loginProviderRepository;// = new LoginProviderRepository(_userRepository, new PersonDbEntities());


         private readonly ILoginProviderRepository _loginProviderRepository = new LoginProviderRepository(new UserRepository(new PersonDbEntities()), new PersonDbEntities());

        //public MainWindow(ILoginProviderRepository userRepository)
        //{
        //    //this.Show();
        //    InitializeComponent();
        //    this._loginProviderRepository = userRepository;

        //}

        public MainWindow()
        {


            InitializeComponent();

        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var result = await _loginProviderRepository.LogInUserAsync(new LogInRequestModel()
            {
                UserName = Username.Text,
                Password = Password.Password.ToString()

            });
            if (result.UserExist && result.PasswordCorrect)
            {
                //ridrejtimin ne faqen kryesore 

                this.Hide();
                Dashboard dashboard = new Dashboard(new  PersonRepository(new PersonDbEntities()));
                
                dashboard.Show();
            }
            else
            {

                MessageBox.Show(result.ErrorMessage);
                this.Show();
            }
        
            
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            
            Register register = new Register(new UserRepository(new PersonDbEntities()));

            register.Show();
           

        }
    }
}
