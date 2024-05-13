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

namespace MemoriajatekPardiAnna
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ListBoxItem)RowNum.SelectedItem!;
            var num_str = selectedItem.Content.ToString()!;
            int columnNumber = Int32.Parse(num_str);
            selectedItem = (ListBoxItem)ColNum.SelectedItem!;
            num_str = selectedItem.Content.ToString()!;
            int rowNumber = Int32.Parse(num_str);


            if (columnNumber % 2 == 1 && rowNumber % 2 == 1)
            {
                Warning.Content = "Páratlan számú kártyával nem lehet memóriát játszani";
            }
            else
            {
                Game game = new Game(columnNumber,rowNumber);

                game.Show();
            }
        }
    }
}