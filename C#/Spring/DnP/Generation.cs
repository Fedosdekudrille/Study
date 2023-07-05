using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Windows.Input;

namespace DnP
{
    public class GenerationParams
    {
        public GenerationParams() { }
        private int? num;
        public string? Num
        {
            get => num == null ? "" : num.ToString();
            set
            {
                int num;
                if (int.TryParse(value, out num))
                {
                    if (this.num != num)
                    {
                        this.num = num;
                    }
                }
                else
                {
                    this.num = null;
                }
            }
        }
        public int? GetGenerationNum()
        {
            return num;
        }
        private PlantRarity? rarity;
        public string Rarity
        {
            get => rarity == null ? "" : ((int)rarity + 1).ToString();
            set
            {
                int numRarity;
                if (int.TryParse(value, out numRarity))
                {
                    if (rarity != (PlantRarity)(numRarity - 1))
                    {
                        rarity = (PlantRarity)(numRarity - 1);
                    }
                }
                else
                {
                    rarity = null;
                }
            }
        }
        public int Location { get; set; }
        public string? Exceptions { get; set; }
        private void PunishHeretics(PropertyInfo prop)
        {
            if (Generation.mainWindow.GenerationParams.FindName("Generate" + prop.Name) is Grid grid)
            {
                if (grid.Children[0] is TextBlock textBlock)
                {
                    textBlock.Foreground = Brushes.Red;
                }
            }
        }
        public bool CheckValidity()
        {
            PropertyInfo[] props = typeof(GenerationParams).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            bool isValid = true;
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == "Exceptions")
                    continue;
                if(prop.GetValue(this) is string str)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        PunishHeretics(prop);
                        isValid = false;
                    }
                }
                else if(prop.GetValue(this) is int location)
                {
                    if(location < 0)
                    {
                        PunishHeretics(prop);
                        isValid = false;
                    }
                }
            }
            return isValid;
        }
    }
    static class Generation
    {
        public static MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static GenerationParams generationParams = mainWindow.generationParams;
        static StackPanel generationResults = mainWindow.GenerationResults;
        static Dictionary dictionary = mainWindow.dictionary;
        static List<Plant> plants = mainWindow.plants;
        public static Grid MakePlant(string name, PlantRarity plantRarity, int num, string? imageSource = null)
        {
            Grid grid = new Grid();
            grid.MouseDown += mainWindow.PlantClick;
            grid.Background = Brushes.Transparent;
            grid.Cursor = Cursors.Hand;

            ColumnDefinition column1 = new ColumnDefinition();
            column1.Width = GridLength.Auto;
            ColumnDefinition column2 = new ColumnDefinition();
            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);

            Border border = new Border();

            SolidColorBrush rarityColor = new SolidColorBrush(Plant.ChooseColor(plantRarity));
            border.BorderBrush = rarityColor;
            Image image = new Image();

            if (string.IsNullOrEmpty(imageSource))
            {
                image.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
            }
            else
            {
                mainWindow.ChangeSelectedImage(image, new(System.IO.Path.Combine(Environment.CurrentDirectory, "UserPlantImages", imageSource), UriKind.Absolute));
            }

            border.Child = image;
            grid.Children.Add(border);

            TextBlock plantName = new TextBlock();
            plantName.Style = (Style)generationResults.Resources["PlantName"];
            plantName.Text = name;
            grid.Children.Add(plantName);

            Grid plantParams = new Grid();
            plantParams.Style = (Style)generationResults.Resources["PlantParams"];
            ColumnDefinition column3 = new ColumnDefinition();
            column3.Width = GridLength.Auto;
            ColumnDefinition column4 = new ColumnDefinition();
            ColumnDefinition column5 = new ColumnDefinition();
            plantParams.ColumnDefinitions.Add(column3);
            plantParams.ColumnDefinitions.Add(column4);
            plantParams.ColumnDefinitions.Add(column5);

            TextBlock rarityBlock = new TextBlock();
            rarityBlock.FontSize = 18;
            rarityBlock.Foreground = rarityColor;

            Binding binding = new Binding($"PlantRarityNames[{(int)plantRarity}].Selected");
            binding.Source = dictionary;
            binding.Mode = BindingMode.OneWay;
            rarityBlock.SetBinding(TextBlock.TextProperty, binding);
            plantParams.Children.Add(rarityBlock);

            TextBlock xTextBlock = new TextBlock();
            xTextBlock.Style = (Style)generationResults.Resources["X"];
            xTextBlock.Text = "x";
            Grid.SetColumn(xTextBlock, 1);
            plantParams.Children.Add(xTextBlock);

            TextBlock numberTextBlock = new TextBlock();
            numberTextBlock.FontSize = 35;
            numberTextBlock.Text = num.ToString();
            Grid.SetColumn(numberTextBlock, 2);
            plantParams.Children.Add(numberTextBlock);

            grid.Children.Add(plantParams);
            Grid.SetRow(plantParams, 1);

            return grid;
        }
        private static int FindSelectionIndex(List<(int index, int num)> plantNums, int plantIndex)
        {
            for (int i = 0; i < plantNums.Count; i++)
            {
                if (plantNums[i].index == plantIndex)
                {
                    return i;
                }
            }
            return -1;
        }
        public static void Generate()
        {
            if (generationParams.CheckValidity())
            {
                generationResults.Children.Clear();
                int leftPlantsNum = (int)generationParams.GetGenerationNum();
                string[] exceptions = generationParams.Exceptions.Split("\"").Where(s => s != "  ").ToArray();
                List<Plant> suitiblePlants = new();
                foreach(Plant plant in plants)
                {
                    if(plant.Rarity.Equals(generationParams.Rarity) & plant.Locations.Contains(dictionary.Location[generationParams.Location].Key) & !exceptions.Contains(plant.Name))
                    {
                        suitiblePlants.Add(plant);
                    }
                }
                if(suitiblePlants.Count > 0)
                {
                    mainWindow.NoGenerationImage.Visibility = Visibility.Collapsed;
                    Random random = new();
                    List<(int index, int num)> plantNums = new();
                    while (leftPlantsNum > 0)
                    {
                        int plantCount = random.Next(1, leftPlantsNum / 10 + 1);
                        int plantIndex = random.Next(suitiblePlants.Count);
                        int selectionIndex = FindSelectionIndex(plantNums, plantIndex);
                        if (selectionIndex == -1)
                        {
                            plantNums.Add((plantIndex, plantCount));
                        }
                        else
                        {
                            plantNums[selectionIndex] = (plantIndex, plantNums[selectionIndex].num + plantCount);
                        }
                        leftPlantsNum -= plantCount;
                    }

                    for (int i = 0; i < plantNums.Count; i++)
                    {
                        Plant plant = suitiblePlants[plantNums[i].index];
                        generationResults.Children.Add(MakePlant(plant.Name, (PlantRarity)plant.GetPlantRarity(), plantNums[i].num, plant.ImagePath));
                        if(i < plantNums.Count - 1)
                            generationResults.Children.Add(new System.Windows.Shapes.Rectangle());
                    }
                }
                else
                {
                    mainWindow.NoGenerationImage.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
