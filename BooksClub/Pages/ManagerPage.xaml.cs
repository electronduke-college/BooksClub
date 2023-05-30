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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BooksClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        private readonly MainWindow mainWindow;
        private BooksClubDataSet.UserRow user;
        public ManagerPage(MainWindow mainWindow, BooksClubDataSet.UserRow user)
        {
            InitializeComponent();
            this.mainWindow = mainWindow; 
            this.user = user;
        }
    }
}
