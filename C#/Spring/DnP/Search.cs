using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections;
using System.Windows.Data;

namespace DnP
{
    public static class Search
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static Dictionary dictionary = mainWindow.dictionary;
        static Grid plantDisplayBody = mainWindow.PlantDisplayBody;
        static List<Plant> plants = mainWindow.plants;
        static Plant currentPlant = mainWindow.currentPlant;
        static TextBox searchPrompt = mainWindow.searchPrompt;
        static Image image = mainWindow.DisplayImage;
        static WrapPanel locations = mainWindow.DisplayLocations;
        public static bool isEmpty = true;
        public static void SetLocations(WrapPanel locations)
        {
            locations.Children?.Clear();
            for (int i = 0; i < currentPlant.Locations.Count; i++)
            {
                StackPanel stackPanel = new();

                TextBlock locationTextBlock = new TextBlock();
                Binding locationBinding = new Binding($"Location[{mainWindow.FindLocationIndex(currentPlant.Locations[i])}].Selected");
                locationBinding.Source = dictionary;
                locationBinding.Mode = BindingMode.OneWay;
                locationTextBlock.SetBinding(TextBlock.TextProperty, locationBinding);
                stackPanel.Children.Add(locationTextBlock);

                if (i != currentPlant.Locations.Count - 1)
                {
                    TextBlock coma = new();
                    coma.Text = ",";
                    stackPanel.Children.Add(coma);
                }

                locations.Children.Add(stackPanel);
            }
        }
        public static void SearchPlant(string plantName)
        {
            foreach(Plant plant in plants)
            {
                if(plant.Name == plantName)
                {
                    mainWindow.SetMode("Display");

                    currentPlant.SetParamsFromPlant(plant);
                    if (string.IsNullOrEmpty(currentPlant.ImagePath))
                    {
                        image.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
                    }
                    else
                    {
                        mainWindow.ChangeSelectedImage(image, new(System.IO.Path.Combine(Environment.CurrentDirectory, "UserPlantImages", currentPlant.ImagePath), UriKind.Absolute));
                    }

                    Binding binding = new Binding($"PlantRarityNames[{((int?)currentPlant.GetPlantRarity())}].Selected");
                    binding.Source = dictionary;
                    binding.Mode = BindingMode.OneWay;
                    mainWindow.DisplayRarity.SetBinding(TextBlock.TextProperty, binding);
                    mainWindow.DisplayRarity.Foreground = new SolidColorBrush(Plant.ChooseColor((PlantRarity)currentPlant.GetPlantRarity()));

                    SetLocations(locations);

                    searchPrompt.Text = "";
                    return;
                }
            }
            searchPrompt.Foreground = Brushes.Red;
        }
    }
}
