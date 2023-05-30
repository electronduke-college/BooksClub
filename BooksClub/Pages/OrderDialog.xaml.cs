using BooksClub.BooksClubDataSetTableAdapters;
using System;
using System.CodeDom;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BooksClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderDialog.xaml
    /// </summary>
    public partial class OrderDialog : Window
    {
        private List<BooksClubDataSet.BookRow> books;
        private OrderTableAdapter ordersTable;
        private PickUpPointTableAdapter pointTable;
        private OrderProductTableAdapter orderProductsTable;
        private BooksClubDataSet.UserRow user;

        decimal oldPrice = 0;
        decimal newPrice = 0;

        public OrderDialog(List<BooksClubDataSet.BookRow> books, BooksClubDataSet.UserRow user)
        {
            InitializeComponent();
            this.books = books;
            this.user = user;
            ordersTable = new OrderTableAdapter();
            pointTable = new PickUpPointTableAdapter();
            orderProductsTable = new OrderProductTableAdapter();
            lvBooks.ItemsSource = books;
            cbPoint.ItemsSource = pointTable.GetData().ToList();
            cbPoint.DisplayMemberPath = "Address";
            cbPoint.SelectedValuePath = "Id";
            UpdatePrice();
        }

        private void UpdatePrice() 
        {
            oldPrice = 0;
            newPrice = 0;
            books.ForEach(book => oldPrice += book.Price);
            books.ForEach(book => 
            {
                if(book.DiscountAmount != 0)
                {
                    newPrice += book.Price - (book.Price / 100 * book.DiscountAmount);
                }
                else
                {
                    newPrice += book.Price;
                }
            });
            tbNewPrice.Text = newPrice.ToString();
            tbOldPrice.Text = oldPrice.ToString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            books.Remove(books.First(book => book == (sender as Button).DataContext as BooksClubDataSet.BookRow));
            lvBooks.ItemsSource = books;
            lvBooks.Items.Refresh();
            UpdatePrice();
            if (books.Count <= 0)
            {
                btnMakeOrder.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnMakeOrder.Visibility = Visibility.Visible;
            }

        }

        private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            if(books.Count > 0)
            {
                if(cbPoint.SelectedValue != null)
                {
                    DateTime now = DateTime.Now;
                    Random rnd = new Random();
                    int code = rnd.Next(100, 999);
                    ordersTable.InsertQuery(now, now.AddDays(4), code, user.Id, int.Parse(cbPoint.SelectedValue.ToString()));


                    books.ForEach
                        (
                            item => orderProductsTable.InsertQuery(OrderId: ordersTable.GetData().Last().Id, BookId: item.Id)
                        ) ;

                    MessageBox.Show($"Ваш заказ успешно оформлен!\nДата оформления заказа: {now}\nПримерная дата доставки: {now.AddDays(4)}\nВаш код для получения: {code}");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Сначала нужно выбрать пунктвыдачи");
                }
                
            }
        }

        private void cbPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
