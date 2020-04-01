using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace REST_GoogleBooks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Suchen(object sender, RoutedEventArgs e)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={suchTb.Text}";

            var http = new HttpClient();

            var json = await http.GetStringAsync(url);

          //  var result = JsonConvert.DeserializeObject<BooksResult>(json);
          //  myGrid.ItemsSource = result.items.Select(x => x.volumeInfo).ToList();


            var result2 = System.Text.Json.JsonSerializer.Deserialize<BooksResult>(json); //neu ab core 3.0
            myGrid.ItemsSource = result2.items.Select(x => x.volumeInfo).ToList();

        }
    }
}
