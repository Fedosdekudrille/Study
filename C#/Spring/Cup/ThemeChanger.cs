using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Cup
{
    public class ThemeChanger
    {
        public enum Theme
        {
            Light,
            Dark,
        }
        public class ElColor : INotifyPropertyChanged
        {
            static Theme chosenTheme;
            static ElColor()
            {

            }
            public string Key { get; private set; }
            private SolidColorBrush Light { get; set; }
            private SolidColorBrush Dark { get; set; }
            public ElColor(string key, Color light, Color dark)
            {
                Key = key;
                Light = new(light);
                Dark = new(dark);
                Selected = Light;
            }
            public void ChangeTheme(Theme theme)
            {
                if (theme == Theme.Light)
                {
                    Selected = Light;
                }
                else
                {
                    Selected = Dark;
                }
                OnPropertyChanged("Selected");
            }
            public bool CheckCompilance(string word)
            {
                if (word.Equals(Key) || word.Equals(Light) || word.Equals(Dark))
                {
                    return true;
                }
                return false;
            }
            public SolidColorBrush Selected { get; private set; }

            public event PropertyChangedEventHandler? PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public ThemeChanger()
        {

        }
        public Theme ChosenTheme { get; private set; } = Theme.Light;

        public void ChangeTheme()
        {
            if (ChosenTheme == Theme.Light)
            {
                ChosenTheme = Theme.Dark;
            }
            else if (ChosenTheme == Theme.Dark)
            {
                ChosenTheme = Theme.Light;
            }
            foreach (ElColor word in Array)
            {
                word.ChangeTheme(ChosenTheme);
            }
        }

        public ElColor[] Array { get; private set; } = new ElColor[]
        {
            new("Header", Color.FromRgb(0, 7, 172), Color.FromRgb(0, 3, 71)),
            new("Theme", Color.FromRgb(201,183,24), Color.FromRgb(9, 39, 198)),
            new("Search", Color.FromRgb(14,114,135), Color.FromRgb(69, 8, 107)),
            new("Menu", Color.FromRgb(127, 252, 29), Color.FromRgb(22, 108, 0)),
            new("Background", Color.FromRgb(235, 177, 29), Color.FromRgb(151, 100, 0)),
            new("Card", Color.FromRgb(169, 169, 169), Color.FromRgb(25, 46, 51)),
        };
        public ElColor this[string key]
        {
            get
            {
                foreach (ElColor word in Array)
                {
                    if (word.Key == key)
                        return word;
                }
                return null;
            }
        }

    }
}
