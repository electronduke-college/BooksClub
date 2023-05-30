using BooksClub.BooksClubDataSetTableAdapters;
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
    /// Логика взаимодействия для GuestPage.xaml
    /// </summary>
    public partial class GuestPage : Page
    {
        private readonly MainWindow mainWindow;
        private BookTableAdapter booksTable;
        public GuestPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            booksTable = new BookTableAdapter();
            UpdateListView();
        }

        private void UpdateListView()
        {
            var products = booksTable.GetData().ToList();
            products = UpdateImageInBook(products);
            lvBooks.ItemsSource = products;
            //lvBooks.Items.Refresh();
        }

        private List<BooksClubDataSet.BookRow> UpdateImageInBook(List<BooksClubDataSet.BookRow> input)
        {
            tbCount.Text = $"{input.Count} из {input.Count}";
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAA");
            input.ForEach(item =>
            {
                if (item.Image == "")
                {
                    item.Image = $"favicon.ico";
                }

            });

            input.ForEach(item => item.Image = $"../Images/{item.Image}");
            return input;

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AuthPage(mainWindow));
        }
    }
}
