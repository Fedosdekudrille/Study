using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DnP
{
    public class Dictionary
    {
        public string A { get; set; } = "asdf";
        public enum Language
        {
            Ru,
            Eng,
        }
        public class Word : INotifyPropertyChanged
        {
            static Language chosenLanguage;
            static Word()
            {

            }
            public string Key { get; private set; }
            private string Eng { get; set; }
            private string Ru { get; set; }
            public Word(string key, string eng, string ru)
            {
                Key = key;
                Eng = eng;
                Ru = ru;
                Selected = Eng;
            }
            public void ChangeLanguage(Language language)
            {
                if(language == Language.Eng)
                {
                    Selected = Eng;
                }
                else
                {
                    Selected = Ru;
                }
                OnPropertyChanged("Selected");
            }
            public bool CheckCompilance(string word)
            {
                if(word.Equals(Key) || word.Equals(Eng) || word.Equals(Ru))
                {
                    return true;
                }
                return false;
            }
            public string Selected { get; private set; }

            public event PropertyChangedEventHandler? PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public Dictionary()
        {
            
        }
        public Language ChosenLanguage { get; private set; } = Language.Eng;

        public void ChangeLanguage()
        {
            if(ChosenLanguage == Language.Eng)
            {
                ChosenLanguage = Language.Ru;
            }
            else if(ChosenLanguage == Language.Ru)
            {
                ChosenLanguage = Language.Eng;
            }
            foreach(Word word in Array)
            {
                word.ChangeLanguage(ChosenLanguage);
            }
            foreach(Word word in PlantRarityNames)
            {
                word.ChangeLanguage(ChosenLanguage);
            }
            foreach(Word word in Location)
            {
                word.ChangeLanguage(ChosenLanguage);
            }
        }

        public Word[] Array { get; private set; } = new Word[]
        {
            new("SearchDefault", "Type here to search...", "Отправиться на поиски..."),

            new("PlantGenerate", "Plant generator", "Генератор растений"),
            new("PlantCreate", "Plant creator", "Создатель растений"),
            new("PlantChange", "Plant modyfier", "Модификатор растений"),
            new("PlantDestroy", "Plant destroyer", "Уничтожитель растений"),
            new("PlantScroll", "Plant list", "Список растений"),
            new("PlantDisplay", "Plant", "Растение"),

            new("GenerationRarity", "Rarity", "Редкость"),
            new("GenerationLocation", "Location", "Локация"),
            new("GenerationNumber", "Number", "Количество"),
            new("GenerationExceptions", "Exceptions", "Исключения"),
            new("GenerationName", "Name", "Название"),
            new("Generate", "Generate", "Сгенерировать"),

            new("CreateButton", "Create", "Вырастить"),
            new("ChangeButton", "Change", "Селекция"),
            new("DestroyButton", "Destroy", "Уничтожить"),
            new("ScrollButton", "Scroll", "Свиток"),
            new("GenerateButton", "Generate", "На поиски"),

            new("AddLocationButton", "Add location", "Добавить ареал"),
            new("PlantFeatures", "Features", "Свойства"),
            new("PlantDescription", "Description", "Описание"),

            new("SaveButton", "Save", "Сохранить"),
            new("ClearButton", "Clear", "Очистить"),

            new("BackFromPlant", "To scroll", "К свитку"),
            new("ToChange", "Modify", "Изменить"),
            new("ToDestroy", "Destroy", "Удалить"),

            new("EnterName", "EnterName:", "Введите имя:"),

            new("ChangeLanguage", "Русский", "English"),
            new("ChangeTheme", "Change theme", "Сменить тему"),
        };
        public Word[] PlantRarityNames { get; private set; } = new Word[]
        {
            new("PlantRarityCommon", "Common", "Обычный"),
            new("PlantRarityUncommon", "Uncommon", "Необычный"),
            new("PlantRarityRare", "Rare", "Редкий"),
            new("PlantRarityEpic", "Epic", "Эпический"),
            new("PlantRarityLegendary", "Legendary", "Легендарный"),
        };
        public Word[] Location { get; private set; } = new Word[]
        {
            new("Coast", "Coast", "Побережье"),
            new("Desert", "Desert", "Пустыня"),
            new("Cave", "Cave", "Пещера"),
            new("Heels", "Heels", "Холмы"),
            new("Forest", "Forest", "Лес"),
            new("Ice", "Ice", "Льды"),
            new("Valley", "Valley", "Равнина"),
            new("Mountains", "Mountains", "Горы"),
            new("Swamp", "Swamp", "Болото"),
        };
        public Word this[string key]
        {
            get
            {
                foreach(Word word in Array)
                {
                    if (word.Key == key)
                        return word;
                }
                return null;
            }
        }
    }
}
