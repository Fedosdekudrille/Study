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

namespace DnP
{
    internal static class PlantScroller
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static Dictionary dictionary = mainWindow.dictionary;
        static Plant currentPlant = mainWindow.currentPlant;
        static List<Plant> plants = mainWindow.plants;
        static StackPanel scrollDisplay = mainWindow.ScrollDisplay;
        enum SortMode
        {
            Letters,
            Rarity,
        }
        static SortMode sortMode = SortMode.Letters;
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
        private static Border GetPlant(string name, PlantRarity plantRarity, string? imageSource = null)
        {
            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(Plant.ChooseColor(plantRarity));
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
            plantParams.Foreground = new SolidColorBrush(Plant.ChooseColor(plantRarity));

            Binding binding = new Binding($"PlantRarityNames[{(int)plantRarity}].Selected");
            binding.Source = dictionary;
            binding.Mode = BindingMode.OneWay;
            plantParams.SetBinding(TextBlock.TextProperty, binding);

            Grid.SetRow(plantParams, 1);
            grid.Children.Add(plantParams);
            border.Child = grid;

            return border;
        }
        public static void SetSortMode(int index)
        {
            sortMode = (SortMode)index;
        }
        private static void SortPlants()
        {
            switch (sortMode)
            {
                case SortMode.Letters:
                    plants.OrderBy(p => p.Name);
                    break;
                case SortMode.Rarity:
                    plants.OrderBy(p => p.Rarity);
                    break;
            }
        }
        public static void SetPlants()
        {
            SortPlants();
            scrollDisplay.Children.Clear();
            for(int i = 0; i < plants.Count; i++)
            {
                if (i == 0)
                {
                    switch (sortMode)
                    {
                        case SortMode.Letters:
                            scrollDisplay.Children.Add(SetPlantCathegory(plants[0].Name[0].ToString()));
                            break;
                        case SortMode.Rarity:
                            scrollDisplay.Children.Add(SetPlantCathegory(plants[0].Rarity));
                            break;
                    }
                }
                else
                {
                    switch (sortMode)
                    {
                        case SortMode.Letters:
                            if (plants[i].Name[0] != plants[i - 1].Name[0])
                                scrollDisplay.Children.Add(SetPlantCathegory(plants[i].Name[0].ToString()));
                            break;
                        case SortMode.Rarity:
                            scrollDisplay.Children.Add(SetPlantCathegory(plants[i].Rarity));
                            break;
                    }
                }
                if(scrollDisplay.Children[scrollDisplay.Children.Count - 1] is StackPanel stackPanel)
                    if (stackPanel.Children[2] is WrapPanel wrapPanel)
                    {
                        wrapPanel.Children.Add(GetPlant(plants[i].Name, (PlantRarity)plants[i].GetPlantRarity(), plants[i].ImagePath));
                    }
            }
        }
    }
}
