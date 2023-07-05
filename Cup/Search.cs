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

namespace Cup
{
    internal static class Search
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static List<Good> searchedGoods = mainWindow.searchedGoods;
        static Dictionary dictionary = mainWindow.dictionary;
        static Grid plantDisplayBody = mainWindow.PlantDisplayBody;
        static List<Good> plants = mainWindow.goods;
        static Good currentPlant = mainWindow.currentGood;
        static TextBox searchPrompt = mainWindow.searchPrompt;
        static Image image = mainWindow.DisplayImage;
        public static bool isEmpty = true;
        public static void SearchGood(string plantName)
        {
            foreach(Good plant in plants)
            {
                if(plant.Name == plantName)
                {
                    mainWindow.SetMode("Display");

                    currentPlant.SetParamsFromGood(plant);
                    if (string.IsNullOrEmpty(currentPlant.ImagePath))
                    {
                        image.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
                    }
                    else
                    {
                        mainWindow.ChangeSelectedImage(image, new(System.IO.Path.Combine(Environment.CurrentDirectory, "UserPlantImages", currentPlant.ImagePath), UriKind.Absolute));
                    }

                    mainWindow.DisplayRarity.Text = currentPlant.Rarity;

                    searchPrompt.Text = "";
                    return;
                }
            }
            searchPrompt.Foreground = Brushes.Red;
        }
        public static void SearchGoods(string plantName)
        {
            searchedGoods.Clear();
            int MaxPrice = int.MaxValue;
            int MinPrice = int.MinValue;
            int max;
            int min;
            if(int.TryParse(mainWindow.MaxP.Text, out max))
            {
                MaxPrice = max;
            }
            if (int.TryParse(mainWindow.MinP.Text, out min))
            {
                MinPrice = min;
            }
            if(MinPrice > MaxPrice)
            {
                int p = MinPrice;
                MinPrice = MaxPrice;
                MaxPrice = p;
            }
            foreach (Good plant in plants)
            {
                if (plant.Name.Contains(plantName))
                {
                    int price = int.Parse(plant.Rarity);
                    
                    if (MinPrice < price && MaxPrice > price)
                    searchedGoods.Add(plant);
                }
            }
            if(searchedGoods.Count > 0)
            {
                mainWindow.SetMode("Scroll");
                GoodScroller.SetPlants(searchedGoods);
            }
            else
            {
                searchPrompt.Foreground = Brushes.Red;
            }
        }
    }
}
