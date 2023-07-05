using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cup
{
    static class GoodDestroyer
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static Dictionary dictionary = mainWindow.dictionary;
        static Good currentPlant = mainWindow.currentGood;
        static List<Good> plants = mainWindow.goods;
        static TextBlock destroyRarity = mainWindow.DestroyRarity;
        static Image image = mainWindow.DestroyImage;
        public static void ChangeVisibility(Visibility visibility)
        {
            for(int i = 1; i < mainWindow.DestroyParams.Children.Count; i++)
            {
                mainWindow.DestroyParams.Children[i].Visibility = visibility;
            }
        }
        public static void SearchDestroy(string name)
        {
            for(int i = 0; i < plants.Count; i++)
            {
                if (plants[i].Name == name)
                {
                    mainWindow.SelectedPlantIndex = i;
                    currentPlant.SetParamsFromGood(plants[i]);

                    if (string.IsNullOrEmpty(currentPlant.ImagePath))
                    {
                        image.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
                    }
                    else
                    {
                        mainWindow.ChangeSelectedImage(image, new(System.IO.Path.Combine(Environment.CurrentDirectory, "UserPlantImages", currentPlant.ImagePath), UriKind.Absolute));
                    }
                    destroyRarity.Text = currentPlant.Rarity;
                    ChangeVisibility(Visibility.Visible);
                    return;
                }
            }
            if (mainWindow.DestroySearch.Children[0] is TextBlock textBlock)
            {
                textBlock.Foreground = Brushes.Red;
            }
            ChangeVisibility(Visibility.Collapsed);
            mainWindow.SelectedPlantIndex = -1;
        }
        public static void DestroyPlant()
        {
            if(mainWindow.SelectedPlantIndex != -1)
            {
                if (!string.IsNullOrEmpty(currentPlant.ImagePath))
                {
                    string path = System.IO.Path.Combine(Environment.CurrentDirectory, "UserPlantImages", currentPlant.ImagePath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                plants.RemoveAt(mainWindow.SelectedPlantIndex);
                mainWindow.Clear();
                GoodSaver.SavePlantsToJsonFile();
            }
            else if(mainWindow.DestroySearch.Children[0] is TextBlock textBlock)
            {
                textBlock.Foreground = Brushes.Red;
            }
        }
    }
}
