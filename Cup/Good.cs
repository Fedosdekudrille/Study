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

namespace Cup
{
    public class Good : INotifyPropertyChanged
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

        private float? rarity;
        public string Rarity
        {
            get => rarity == null ? "" : rarity.ToString();
            set
            {
                float numPrice;
                if(float.TryParse(value, out numPrice))
                {
                    if (rarity != numPrice)
                    {
                        rarity = numPrice;
                    }
                }
                else
                {
                    rarity = null;
                }
                OnPropertyChanged("Rarity");
            }
        }

        private string type;
        public string Type
        {
            get => type;
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
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
        public string ImagePath { get; set; }
        public Good() { }
        public Good(string name, int price, string features, string description, string type, string imagePath)
        {
            Name = name;
            rarity = price;
            Features = features;
            Description = description;
            Type = type;
            ImagePath = imagePath;
        }
        public Good(Good plant)
        {
            Name = plant.Name;
            Rarity = plant.Rarity;
            Features = plant.Features;
            Description = plant.Description;
            Type = plant.Type;
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
        public void SetParamsFromGood(Good plant)
        {
            Name = plant.Name;
            Rarity = plant.Rarity;
            Features = plant.Features;
            Description = plant.Description;
            Type = plant.Type;
            ImagePath = plant.ImagePath;
        }
        public void ClearParams()
        {
            Name = "";
            Rarity = "";
            Features = "";
            Description = "";
            Type = "";
            ImagePath = null;
        }
    }
    public static class GoodSaver
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static Dictionary dictionary = mainWindow.dictionary;
        static List<Good> plants;
        static Good currentPlant = mainWindow.currentGood;
        static List<int> locationIndexes = mainWindow.locationIndexes;
        public static bool SavePlant(int plantIndex = -1)
        {
            if (IsValid(currentPlant) & !HasNameRepeat(currentPlant.Name, plantIndex))
            {
                SavePicture(plantIndex);
                if (plantIndex == -1)
                {
                    plants.Add(new Good(currentPlant));
                }
                else
                {
                    plants[plantIndex] = new Good(currentPlant);
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
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                        mainWindow.goods = JsonSerializer.Deserialize<List<Good>>(fs, jsonSerializerOptions);
                }
                catch
                {
                    mainWindow.goods = new();
                }
                plants = mainWindow.goods;
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
        static public bool IsValid(Good plant)
        {
            if (!string.IsNullOrEmpty(plant.Name) && !string.IsNullOrEmpty(plant.Rarity) && !string.IsNullOrEmpty(plant.Features) && !string.IsNullOrEmpty(plant.Description) && !string.IsNullOrEmpty(plant.Type))
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
        private static List<string> FindNotValid(Good plant)
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

            if (string.IsNullOrEmpty(plant.Type))
            {
                AlertTextBlock("Type");
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
