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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private readonly MainWindow mainWindow;
        private BooksClubDataSet.UserRow user;
        private BookTableAdapter booksTable;
        public AdminPage(MainWindow mainWindow, BooksClubDataSet.UserRow user)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.user = user;
            booksTable = new BookTableAdapter();
            UpdateListView();
            tbName.Text = $"{user.Surname} {user.Name}";
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

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new AddBookPage(mainWindow));
        }

        private void miDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            BooksClubDataSet.BookRow? book = (sender as MenuItem).DataContext as BooksClubDataSet.BookRow;
            var orders = new OrderProductTableAdapter();
            var bookInOrder = orders.GetData().ToList().FirstOrDefault(order => order.BookId == book!.Id);

            if(bookInOrder == null)
            {
                booksTable.DeleteQuery(book!.Id);
            }
            else
            {
                MessageBox.Show("Данный товар нельзя удалить, так как он находится в заказе");
            }
            UpdateListView();
        }

        private void miEdutBook_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
