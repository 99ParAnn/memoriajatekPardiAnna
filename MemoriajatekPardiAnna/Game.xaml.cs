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
using System.Windows.Shapes;

namespace MemoriajatekPardiAnna
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        Button[,] tileButtons;
        int[,] tileValues;
        List<Button> upturned = new List<Button>();
        int PairsSoFar = 0;
        int NumberOfPairs;
        //unsure if i need the tiles button matrix
        //i do need it, if i want to turn the tiles back down without them forgetting their value
        public Game(int rowNumber, int columnNumber)
        {
            NumberOfPairs = rowNumber * columnNumber / 2;
            InitializeComponent();
            GenerateGrid(rowNumber, columnNumber);
            ShuffleTiles();
        }
        public void GenerateGrid(int rowNumber, int columnNumber)
        {
            for (int i = 0; i < rowNumber; i++)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < columnNumber; i++)
            {
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            GenerateTiles(rowNumber, columnNumber);
        }
        public void GenerateTiles(int rowNumber, int columnNumber)
        {
            tileButtons = new Button[rowNumber, columnNumber];
            tileValues = new int[rowNumber, columnNumber];
            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < columnNumber; j++)
                {
                    Button tile = new Button();
                    tile.Click += Button_Click;

                    tile.FontSize = 70;
                    tile.Content = ":)";
                    Grid.SetRow(tile, i);
                    Grid.SetColumn(tile, j);
                    mainGrid.Children.Add(tile);
                    tileButtons[i, j] = tile;

                }
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button tile = sender as Button;
            switch (upturned.Count)
            {
                case 0:

                    if (!IsUpturned(tile))
                    {
                        UpturnTile(tile);
                        upturned.Add(tile);
                    }
                    break;
                case 1:
                    if (!IsUpturned(tile))
                    {
                        UpturnTile(tile);
                        upturned.Add(tile);
                        if (CheckIfTileEqual(tile, upturned[0]))
                        {
                            PairsSoFar++;
                            upturned.Clear();
                            if (PairsSoFar == NumberOfPairs)
                            {
                                Win();
                            }
                        }

                    }
                    break;
                case 2:
                    foreach (Button tileTurned in upturned)
                    {
                        DownturnTile(tileTurned);
                    }
                    upturned.Clear();
                    break;
                default:
                    break;
            }
        }


        private bool IsUpturned(Button tile)
        {
            return tile.Content != ":)" && tile.Content != ":(";
        }

        private void Win()
        {
            foreach(Button tile in mainGrid.Children)
            {
                tile.Content = "*o*";
                tile.Click += Exit;
            }


        }

        private void UpturnTile(Button tile)
        {
            int rowNumber = Grid.GetRow(tile);
            int columnNumber = Grid.GetColumn(tile);
            tile.Content = $"{tileValues[rowNumber, columnNumber]}";

        }

        private void DownturnTile(Button tile)
        {

            tile.Content = ":(";

        }
        public bool CheckIfTileEqual(Button tile1, Button tile2)
        {
            int firstValue = tileValues[Grid.GetRow(tile1), Grid.GetColumn(tile1)];
            int secondValue = tileValues[Grid.GetRow(tile2), Grid.GetColumn(tile2)];
            return firstValue == secondValue;
        }
        public void ShuffleTiles() //only works if the tileValues matrix exists
        {
            int howManyDifferent = tileValues.GetLength(0) * tileValues.GetLength(1) / 2;
            List<int> values = new List<int>();
            for (int i = 1; i <= howManyDifferent; i++)
            {
                values.Add(i);
                values.Add(i);
            }
            Random rnd = new Random();
            for (int i = 0; i < tileValues.GetLength(0); i++)
            {
                for (int j = 0; j < tileValues.GetLength(1); j++)
                {
                    int chosen = rnd.Next(0, values.Count);
                    tileValues[i, j] = values[chosen];
                    values.Remove(values[chosen]);
                }
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
