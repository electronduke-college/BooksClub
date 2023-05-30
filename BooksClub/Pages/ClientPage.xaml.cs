using BooksClub.BooksClubDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        private readonly MainWindow mainWindow;
        private BooksClubDataSet.UserRow user;
        List<BooksClubDataSet.BookRow> booksCart;
        private BookTableAdapter booksTable;
        public ClientPage(MainWindow mainWindow, BooksClubDataSet.UserRow user)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.user = user;
            booksCart = new List<BooksClubDataSet.BookRow>();
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

        private List<BooksClubDataSet.BookRow>  UpdateImageInBook(List<BooksClubDataSet.BookRow> input)
        {
            tbCount.Text = $"{input.Count} из {input.Count}";
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAA");
            input.ForEach(item => 
            { 
                if(item.Image == "")
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

        private void miAddToCart_Click(object sender, RoutedEventArgs e)
        {
            if(user.RoleId == 3)
            {
                booksCart.Add((sender as MenuItem).DataContext as BooksClubDataSet.BookRow);
                btnOrder.Visibility = Visibility;
            }
            
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            if(booksCart != null)
            {
                OrderDialog orderDialog = new OrderDialog(booksCart, user);
                if (orderDialog.ShowDialog().Equals(true))
                {
                    Console.WriteLine('O');
                }
            }
        }
    }
}
