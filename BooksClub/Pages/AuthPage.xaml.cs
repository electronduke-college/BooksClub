using BooksClub.BooksClubDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BooksClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private readonly MainWindow mainWindow;
        private UserTableAdapter usersTable;
        public AuthPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            usersTable = new UserTableAdapter();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = tbPassword.Text;

            var user = usersTable.GetData().Where(item => item.Login == login).Where(item => item.Password == password).FirstOrDefault();
            if( user == null)
            {
                MessageBox.Show("Неправильные данные для входа");
            }
            else
            {
                switch (user.RoleId)
                {
                    case 1:
                        this.NavigationService.Navigate(new AdminPage(mainWindow, user));
                        break;
                    case 2:
                        this.NavigationService.Navigate(new ManagerPage(mainWindow, user));
                        break;
                    case 3:
                        this.NavigationService.Navigate(new ClientPage(mainWindow, user));
                        break;

                }
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new GuestPage(mainWindow));
        }
    }
}
