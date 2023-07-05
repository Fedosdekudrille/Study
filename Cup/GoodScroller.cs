using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Data;

namespace Cup
{
    internal static class GoodScroller
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static Dictionary dictionary = mainWindow.dictionary;
        static Good currentPlant = mainWindow.currentGood;
        static List<Good> searchedGoods = mainWindow.searchedGoods;
        static StackPanel scrollDisplay = mainWindow.ScrollDisplay;
        enum SortMode
        {
            Rarity,
            Types,
        }
        static SortMode sortMode = SortMode.Types;
        private static StackPanel SetPlantCathegory(string cathegory)
        {
            StackPanel stackPanel = new StackPanel();
            TextBlock textBlock = new TextBlock();
            textBlock.Margin = new Thickness(30, 0, 0, 0);
            textBlock.FontSize = 30;
            textBlock.Text = cathegory;
            stackPanel.Children.Add(textBlock);
            Rectangle rectangle = new Rectangle();
            stackPanel.Children.Add(rectangle);

            WrapPanel wrapPanel = new WrapPanel();
            stackPanel.Children.Add(wrapPanel);
            return stackPanel;
        }
        private static Border GetPlant(string name, string price, string? imageSource = null)
        {
            Border border = new Border();
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            Image image = new Image();
            if (string.IsNullOrEmpty(imageSource))
            {
                image.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
            }
            else
            {
                mainWindow.ChangeSelectedImage(image, new(System.IO.Path.Combine(Environment.CurrentDirectory, "UserPlantImages", imageSource), UriKind.Absolute));
            }
            grid.Children.Add(image);
            TextBlock plantName = new TextBlock();
            plantName.Style = (Style)mainWindow.Scroll.Resources["PlantName"];
            plantName.Text = name;
            grid.Children.Add(plantName);
            TextBlock plantParams = new TextBlock();
            plantParams.Style = (Style)mainWindow.Scroll.Resources["PlantParams"];
            plantParams.FontSize = 18;

            plantParams.Text = price;

            Grid.SetRow(plantParams, 1);
            grid.Children.Add(plantParams);
            border.Child = grid;

            return border;
        }
        public static void SetSortMode(int index)
        {
            sortMode = (SortMode)index;
            if(mainWindow.CurrentModeChoise == "Scroll")
            {
                SetPlants(mainWindow.goods);
            }
        }
        private static void SortPlants(List<Good> goods)
        {
            switch (sortMode)
            {
                case SortMode.Types:
                    goods.OrderBy(p => p.Name);
                    break;
                case SortMode.Rarity:
                    goods.OrderBy(p => p.Rarity);
                    break;
            }
        }
        public static void SetPlants(List<Good> goods)
        {
            SortPlants(goods);
            scrollDisplay.Children.Clear();
            for(int i = 0; i < goods.Count; i++)
            {
                if (i == 0)
                {
                    switch (sortMode)
                    {
                        case SortMode.Types:
                            scrollDisplay.Children.Add(SetPlantCathegory(goods[0].Type));
                            break;
                        case SortMode.Rarity:
                            scrollDisplay.Children.Add(SetPlantCathegory(goods[0].Rarity));
                            break;
                    }
                }
                else
                {
                    switch (sortMode)
                    {
                        case SortMode.Types:
                            if (goods[i].Type != goods[i - 1].Type)
                                scrollDisplay.Children.Add(SetPlantCathegory(goods[i].Type));
                            break;
                        case SortMode.Rarity:
                            scrollDisplay.Children.Add(SetPlantCathegory(goods[i].Rarity));
                            break;
                    }
                }
                if(scrollDisplay.Children[scrollDisplay.Children.Count - 1] is StackPanel stackPanel)
                    if (stackPanel.Children[2] is WrapPanel wrapPanel)
                    {
                        wrapPanel.Children.Add(GetPlant(goods[i].Name, goods[i].Rarity, goods[i].ImagePath));
                    }
            }
        }
    }
}
