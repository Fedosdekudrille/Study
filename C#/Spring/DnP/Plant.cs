using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Xml.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.IO;
using System.Numerics;
using System.Collections;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace DnP
{
    public enum PlantRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    public class Plant : INotifyPropertyChanged
    {
        MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private PlantRarity? rarity;
        public string Rarity
        {
            get => rarity == null ? "" : ((int)rarity + 1).ToString();
            set
            {
                int numRarity;
                if(int.TryParse(value, out numRarity))
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
                OnPropertyChanged("Rarity");
            }
        }
        public PlantRarity? GetPlantRarity()
        {
            return rarity;
        }
        private string features;
        public string Features
        {
            get => features;
            set
            {
                if (features != value)
                {
                    features = value;
                    OnPropertyChanged("Features");
                }
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        public List<string> Locations { get; set; } = new();
        public string ImagePath { get; set; }
        public Plant() { }
        public Plant(string name, PlantRarity plantRarity, string features, string description, List<string> locations, string imagePath)
        {
            Name = name;
            Rarity = ((int)plantRarity).ToString();
            Features = features;
            Description = description;
            Locations = new(locations);
            ImagePath = imagePath;
        }
        public Plant(Plant plant)
        {
            Name = plant.Name;
            Rarity = plant.Rarity;
            Features = plant.Features;
            Description = plant.Description;
            Locations = new(plant.Locations);
            ImagePath = plant.ImagePath;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                if(mainWindow.FindName("Plant" + prop) is Grid grid)
                {
                    if (grid.Children[0] is TextBlock textBlock)
                    {
                        textBlock.Foreground = Brushes.White;
                    }
                }
            }
        }
        public void SetParamsFromPlant(Plant plant)
        {
            Name = plant.Name;
            Rarity = plant.Rarity;
            Features = plant.Features;
            Description = plant.Description;
            Locations = new(plant.Locations);
            ImagePath = plant.ImagePath;
        }
        public void ClearParams()
        {
            Name = "";
            Rarity = "";
            Features = "";
            Description = "";
            Locations = new();
            ImagePath = null;
        }
        static public Color ChooseColor(PlantRarity plantRarity)
        {
            Color color;
            switch (plantRarity)
            {
                case PlantRarity.Common:
                    color = Color.FromRgb(182, 182, 182);
                    break;
                case PlantRarity.Uncommon:
                    color = Color.FromRgb(80, 209, 47);
                    break;
                case PlantRarity.Rare:
                    color = Color.FromRgb(27, 36, 255);
                    break;
                case PlantRarity.Epic:
                    color = Color.FromRgb(134, 4, 180);
                    break;
                case PlantRarity.Legendary:
                    color = Color.FromRgb(180, 162, 3);
                    break;
            }
            return color;
        }
    }
    public static class PlantSaver
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static Dictionary dictionary = mainWindow.dictionary;
        static List<Plant> plants;
        static Plant currentPlant = mainWindow.currentPlant;
        static List<int> locationIndexes = mainWindow.locationIndexes;
        public static bool SavePlant(int plantIndex = -1)
        {
            if (IsValid(currentPlant) & !HasNameRepeat(currentPlant.Name, plantIndex))
            {
                SavePicture(plantIndex);
                if (plantIndex == -1)
                {
                    plants.Add(new Plant(currentPlant));
                }
                else
                {
                    plants[plantIndex] = new Plant(currentPlant);
                }
                SavePlantsToJsonFile();
                return true;
            }
            return false;
        }
        private static bool HasNameRepeat(string name, int index)
        {
            for(int i = 0; i < plants.Count; i++)
            {
                if (plants[i].Name == name && i != index)
                {
                    AlertTextBlock("Name");
                    return true;
                }
            }
            return false;
        }
        public static void SaveLocations()
        {
            currentPlant.Locations.Clear();
            foreach (int index in locationIndexes)
            {
                currentPlant.Locations.Add(dictionary.Location[index].Key);
            }
        }
        public static void SavePicture(int plantIndex = -1)
        {
            if (!string.IsNullOrEmpty(currentPlant.ImagePath))
            {
                if (plantIndex == -1)
                    File.Copy(currentPlant.ImagePath, Path.Combine(Environment.CurrentDirectory, "UserPlantImages", currentPlant.Name) + Path.GetExtension(currentPlant.ImagePath), true);
                else if (plants[plantIndex].ImagePath != currentPlant.ImagePath | plants[plantIndex].Name != currentPlant.Name)
                {
                    string oldPath;
                    if (currentPlant.ImagePath.Contains('\\'))
                        oldPath = currentPlant.ImagePath;
                    else
                        oldPath = Path.Combine(Environment.CurrentDirectory, "UserPlantImages", plants[plantIndex].ImagePath);

                    string newPath = Path.Combine(Environment.CurrentDirectory, "UserPlantImages", currentPlant.Name) + Path.GetExtension(currentPlant.ImagePath);
                    if (File.Exists(newPath))
                    {
                        mainWindow.ChangeImage.Source = null;
                        mainWindow.ChangeImage.UpdateLayout();
                    }
                    File.Copy(oldPath, newPath, true);
                    if (!string.IsNullOrEmpty(plants[plantIndex].ImagePath) && !plants[plantIndex].ImagePath.Equals(newPath) && File.Exists(Path.Combine(Environment.CurrentDirectory, "UserPlantImages", plants[plantIndex].ImagePath)))
                    {
                        //mainWindow.ChangeImage.Source = null;
                        //mainWindow.ChangeImage.UpdateLayout();
                        File.Delete(Path.Combine(Environment.CurrentDirectory, "UserPlantImages", plants[plantIndex].ImagePath));
                    }
                }
                currentPlant.ImagePath = currentPlant.Name + Path.GetExtension(currentPlant.ImagePath);
            }
        }
        static JsonSerializerOptions jsonSerializerOptions = new()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All),
            WriteIndented = true,
        };
        public static void LoadPlantsFromJsonFile()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"SavedPlants\plants.json");

            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    mainWindow.plants = JsonSerializer.Deserialize<List<Plant>>(fs, jsonSerializerOptions);
                plants = mainWindow.plants;
            }
        }
        public static async void SavePlantsToJsonFile()
        {
             string filePath = Path.Combine(Environment.CurrentDirectory, @"SavedPlants\plants.json");

            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "SavedPlants")))
            {

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    await JsonSerializer.SerializeAsync(fs, plants, jsonSerializerOptions);
            }
        }
        static public bool IsValid(Plant plant)
        {
            if (!string.IsNullOrEmpty(plant.Name) && !string.IsNullOrEmpty(plant.Rarity) && !string.IsNullOrEmpty(plant.Features) && !string.IsNullOrEmpty(plant.Description) && plant.Locations.Count >= 0)
                return true;
            FindNotValid(plant);
            return false;
        }
        private static void AlertTextBlock(string prop)
        {
            string name = "";
            switch (mainWindow.CurrentModeChoise)
            {
                case "Create":
                    name = "Plant";
                    break;
                case "Change":
                    name = "ChangePlant";
                    break;
            }
            Grid grid = mainWindow.FindName(name + prop) as Grid;
            if (grid.Children[0] is TextBlock textBlock)
            {
                textBlock.Foreground = Brushes.Red;
            }
        }
        private static List<string> FindNotValid(Plant plant)
        {
            List<string> notValidProperties = new List<string>();

            if (string.IsNullOrEmpty(plant.Name))
            {
                AlertTextBlock("Name");
            }

            if (string.IsNullOrEmpty(plant.Rarity))
            {
                AlertTextBlock("Rarity");
            }

            if (string.IsNullOrEmpty(plant.Features))
            {
                AlertTextBlock("Features");
            }

            if (string.IsNullOrEmpty(plant.Description))
            {
                AlertTextBlock("Description");
            }

            if (plant.Locations.Count == 0)
            {
                AlertTextBlock("Location");
            }

            return notValidProperties;
        }
        private static List<TextBlock> FindTextBlocks(UIElement element, string[] content)
        {
            var textBlocks = new List<TextBlock>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);

                if (child is TextBlock textBlock && content.Contains(textBlock.Text))
                {
                    textBlocks.Add(textBlock);
                }
                else
                {
                    var textBlocksInChildren = FindTextBlocks(child as UIElement, content);
                    textBlocks.AddRange(textBlocksInChildren);
                }
            }

            return textBlocks;
        }
    }
}
