using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace DnP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Themes
        {
            Light,
            Dark,
        }
        Themes currentTheme = Themes.Dark;
        public Dictionary? dictionary;
        public Plant? currentPlant;
        public List<Plant> plants = new();
        public GenerationParams generationParams;
        int PreviousCaretIndex = -1;
        public int SelectedPlantIndex = -1;
        public string CurrentModeChoise = "Generate";
        public MainWindow()
        {
            var uri = new Uri("DarkTheme.xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            InitializeComponent();
            StreamResourceInfo sri = Application.GetResourceStream(
            new Uri("Source/cursor.ani", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;

            if (Resources["PlantInstance"] is Plant pl)
                currentPlant = pl;

            if (Resources["DictionaryInstance"] is Dictionary dict)
                dictionary = dict;

            if (Resources["GenerationInstance"] is GenerationParams gen)
            {
                generationParams = gen;
            }

            if (PlantLocations.Children[0] is Grid grid)
            {
                if (grid.Children[0] is Border border)
                {
                    if (border.Child is ComboBox comboBox)
                    {
                        for (int i = 0; i < dictionary.Location.Length; i++)
                        {
                            ComboBoxItem comboBoxItem = new ComboBoxItem();

                            Binding binding = new Binding($"Location[{i}].Selected");
                            binding.Source = dictionary;
                            binding.Mode = BindingMode.OneWay;
                            comboBoxItem.SetBinding(ContentProperty, binding);
                            comboBox.Items.Add(comboBoxItem);
                            ComboBoxItem generationComboBoxItem = new();
                            generationComboBoxItem.SetBinding(ContentProperty, binding);
                            GenerationLocation.Items.Add(generationComboBoxItem);
                        }
                    }
                }
            }


            PlantSaver.LoadPlantsFromJsonFile();
        }
        private void SearchFocus(object sender, MouseButtonEventArgs e)
        {
            if (Search.isEmpty)
                searchPrompt.CaretIndex = 0;
            searchPrompt.Focus();
        }

        private void SearchButtonClick(object sender, MouseButtonEventArgs e)
        {
            if (!Search.isEmpty)
            {
                Search.SearchPlant(searchPrompt.Text);
            }
            else
            {
                SearchFocus(sender, e);
            }
        }

        private void SearchKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    if (searchPrompt.Text.Length > 0)
                        Search.SearchPlant(searchPrompt.Text);
                    e.Handled = true;
                    break;
                case Key.Right:
                    if (Search.isEmpty)
                    {
                        e.Handled = true;
                    }
                    break;
                case Key.Delete:
                    if (Search.isEmpty)
                    {
                        e.Handled = true;
                    }
                    break;
                case Key.Space:
                    if(Search.isEmpty)
                        e.Handled = true;
                    break;
                case Key.V:
                    if(Keyboard.Modifiers== ModifierKeys.Control)
                    {
                        WriteIntoEmptySearch(Clipboard.GetText());
                        e.Handled= true;
                    }
                    break;
                        
                default:
                    if(!Search.isEmpty)
                        searchPrompt.Foreground = Brushes.White;
                    break;
            }
        }
        private void WriteIntoEmptySearch(string text)
        {
            Search.isEmpty = false;
            searchPrompt.Text = text;
            searchPrompt.Foreground = Brushes.White;
            searchPrompt.CaretIndex = searchPrompt.Text.Length;
        }
        private void SearchTextEnter(object sender, TextCompositionEventArgs e)
        {
            if (Search.isEmpty)
            {
                WriteIntoEmptySearch(e.Text);
                e.Handled = true;

            }
        }

        private void searchPrompt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchPrompt.Text.Length == 0)
            {
                searchPrompt.Foreground = new SolidColorBrush(Color.FromRgb(133, 142, 173));
                Binding binding = new Binding("[SearchDefault].Selected");
                binding.Source = dictionary;
                binding.Mode = BindingMode.OneWay;
                searchPrompt.SetBinding(TextBox.TextProperty, binding);
                Search.isEmpty = true;
            }
        }

        private void SearchClick(object sender, MouseButtonEventArgs e)
        {
            if (Search.isEmpty)
            {
                searchPrompt.Focus();
                searchPrompt.CaretIndex = 0;
                e.Handled = true;
            }
        }
        private void ChangeVisibility(UIElement element)
        {
            if (element.Visibility == Visibility.Visible)
            {
                element.Visibility = Visibility.Collapsed;
            }
            else
            {
                element.Visibility = Visibility.Visible;
            }
        }
        private void MenuClick(object sender, MouseButtonEventArgs e)
        {
            ChangeVisibility(Menu);
        }

        private void ChangeMode(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border chosenBorder)
            {
                SetMode(chosenBorder.Name);
            }

        }
        public void SetMode(string choise)
        {
            if (CurrentModeChoise == choise.Replace("Button", ""))
                return;
            Clear();
            string preModeChoise = choise;
            string previousMode = CurrentModeChoise;
            if (choise.Equals("Display"))
                preModeChoise = "ScrollButton";
            if (CurrentModeChoise.Equals("Display"))
                previousMode = "Scroll";
            Border currentChoiseButton = (Border)ModeButtons.FindName(previousMode + "Button");
            currentChoiseButton.Visibility = Visibility.Visible;
            ((Border)ModeButtons.FindName(preModeChoise)).Visibility = Visibility.Collapsed;
            if (Body.FindName($"Plant{CurrentModeChoise}Body") is Grid collapsingGrid)
            {
                collapsingGrid.Visibility = Visibility.Collapsed;
            }


            CurrentModeChoise = choise.Replace("Button", "");


            int i = 0;
            foreach (Border border in ModeButtons.Children)
            {
                if (border.Visibility == Visibility.Visible)
                {
                    border.SetValue(Grid.ColumnProperty, i++);
                }
            }
            if (Body.Children[0] is TextBlock textBlock)
            {
                Binding binding = new Binding($"[{"Plant" + CurrentModeChoise}].Selected");
                binding.Source = dictionary;
                binding.Mode = BindingMode.OneWay;
                textBlock.SetBinding(TextBlock.TextProperty, binding);
            }
            if (Body.FindName($"Plant{CurrentModeChoise}Body") is Grid visualisatingGrid)
            {
                visualisatingGrid.Visibility = Visibility.Visible;
            }
            
            if(CurrentModeChoise == "Scroll")
            {
                PlantScroller.SetPlants();
            }
        }
        private void SelectExceptionsIndex()
        {
            if (GenerationExceptions.CaretIndex == PreviousCaretIndex)
                return;
            if (GenerationExceptions.CaretIndex == 0)
            {
                GenerationExceptions.CaretIndex = 1;
            }
            else if (GenerationExceptions.Text.Length == GenerationExceptions.CaretIndex)
            {
                GenerationExceptions.CaretIndex = GenerationExceptions.Text.Length - 1;
            }

        }

        private void GenerationParamClick(object sender, MouseButtonEventArgs e)
        {
            Grid? grid;
            if (sender is Grid send)
                grid = send;
            else
                grid = FindParentOfType<Grid>((DependencyObject)sender);
            if (grid != null)
            {
                if (VisualTreeHelper.GetChild(grid, 1) is TextBox textBox)
                {
                    textBox.Focus();
                    if (textBox.Name == "GenerationExceptions")
                    {
                        if (GenerationExceptions.Text.Length == 0)
                            GenerationExceptions.Text = "\"\"";
                        SelectExceptionsIndex();
                        e.Handled = true;
                    }
                    if (e.Source.GetType() != typeof(TextBox))
                        e.Handled = true;
                }
                TextBlockUpAnimation(grid);
            }
        }
        private void TextBlockUpAnimation(Grid grid)
        {
            if (VisualTreeHelper.GetChild(grid, 0) is TextBlock textBlock)
            {
                if (textBlock.Margin.Top < 25)
                {
                    return;
                }
                ThicknessAnimation marginAnimation = new();
                marginAnimation.Duration = TimeSpan.FromMilliseconds(100);
                marginAnimation.From = new Thickness(0, 25, 10, 0);
                marginAnimation.To = new Thickness(0, 0, 10, 0);

                DoubleAnimation fontAnimation = new();
                fontAnimation.Duration = marginAnimation.Duration;
                fontAnimation.From = 20;
                fontAnimation.To = 12;

                Storyboard storyboard = new();
                storyboard.Children.Add(marginAnimation);
                storyboard.Children.Add(fontAnimation);
                Storyboard.SetTarget(marginAnimation, textBlock);
                Storyboard.SetTargetProperty(marginAnimation, new PropertyPath(MarginProperty));
                Storyboard.SetTarget(fontAnimation, textBlock);
                Storyboard.SetTargetProperty(fontAnimation, new PropertyPath(TextBlock.FontSizeProperty));
                storyboard.Begin();
            }
        }
        private T? FindParentOfType<T>(DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent == null) return null;
            var parentOfType = parent as T;
            return parentOfType ?? FindParentOfType<T>(parent);
        }
        public static T? FindChildOfType<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T tChild)
                {
                    return tChild;
                }
                else
                {
                    T foundChild = FindChildOfType<T>(child);
                    if (foundChild != null) return foundChild;
                }
            }

            return null;
        }
        private void ReturnPlantFocus(int margin, int fontSize, object sender, RoutedEventArgs e = null)
        {
            Grid? grid;
            if (sender is Grid send)
                grid = send;
            else
                grid = FindParentOfType<Grid>((DependencyObject)sender);
            if (grid != null & grid.Name != "ChangePlantName")
            {
                if (VisualTreeHelper.GetChild(grid, 0) is TextBlock textBlock)
                {
                    if (e?.Source is TextBox textBox)
                    {
                        if (textBox.Name == "GenerationExceptions" & textBox.Text == "\"\"")
                        {
                            textBox.Text = "";
                        }
                        else if (textBox.Text.Length > 0)
                            return;
                    }
                    ThicknessAnimation marginAnimation = new();
                    marginAnimation.Duration = TimeSpan.FromMilliseconds(100);
                    marginAnimation.To = new Thickness(0, margin, 10, 0);
                    marginAnimation.From = new Thickness(0, 0, 10, 0);

                    DoubleAnimation fontAnimation = new();
                    fontAnimation.Duration = marginAnimation.Duration;
                    fontAnimation.To = fontSize;
                    fontAnimation.From = 12;

                    Storyboard storyboard = new();
                    storyboard.Children.Add(marginAnimation);
                    storyboard.Children.Add(fontAnimation);
                    Storyboard.SetTarget(marginAnimation, textBlock);
                    Storyboard.SetTargetProperty(marginAnimation, new PropertyPath(MarginProperty));
                    Storyboard.SetTarget(fontAnimation, textBlock);
                    Storyboard.SetTargetProperty(fontAnimation, new PropertyPath(TextBlock.FontSizeProperty));
                    storyboard.Begin();
                }
            }
        }
        private void TextBoxLostFocus(object sender, RoutedEventArgs e = null)
        {
            ReturnPlantFocus(25, 20, sender, e);
        }
        private void DestroySearchLostFocus(object sender, RoutedEventArgs e = null)
        {
            ReturnPlantFocus(25, 35, sender, e);
        }
        private void GenerationExceptionsKeyDown(object sender, KeyEventArgs e)
        {
            int caretIndex = GenerationExceptions.CaretIndex;
            switch (e.Key)
            {
                case Key.Space:
                    if (GenerationExceptions.Text[caretIndex - 1] == ' ')
                    {
                        GenerationExceptions.Text = GenerationExceptions.Text.Remove(caretIndex - 1, 1);
                        caretIndex = GenerationExceptions.Text.IndexOf('\"', caretIndex - 1) + 1;
                        GenerationExceptions.Text = GenerationExceptions.Text.Insert(caretIndex, "  \"\"");
                        GenerationExceptions.CaretIndex = caretIndex + 3;
                        e.Handled = true;
                    }
                    break;
                case Key.Delete:
                    if (GenerationExceptions.Text[caretIndex] == '\"')
                    {
                        e.Handled = true;
                        if (GenerationExceptions.Text.Length > caretIndex + 1)
                        {
                            if (GenerationExceptions.Text[caretIndex - 1] == '\"')
                            {
                                GenerationExceptions.Text = GenerationExceptions.Text.Remove(caretIndex - 1, 4);
                                GenerationExceptions.CaretIndex = caretIndex;
                            }
                            else
                            {
                                if (GenerationExceptions.Text[caretIndex + 4] == '\"')
                                {
                                    GenerationExceptions.Text = GenerationExceptions.Text.Remove(caretIndex + 1, 4);
                                    GenerationExceptions.CaretIndex = caretIndex;
                                }
                                else
                                {
                                    GenerationExceptions.Text = GenerationExceptions.Text.Remove(caretIndex + 4, 1);
                                    GenerationExceptions.CaretIndex = caretIndex + 4;
                                }
                            }
                        }
                    }
                    break;
                case Key.Back:
                    if (GenerationExceptions.Text[caretIndex - 1] == '\"')
                    {
                        e.Handled = true;
                        if (caretIndex != 1)
                        {
                            if (GenerationExceptions.Text[caretIndex] == '\"')
                            {
                                GenerationExceptions.Text = GenerationExceptions.Text.Remove(caretIndex - 3, 4);
                                GenerationExceptions.CaretIndex = caretIndex - 4;
                            }
                            else
                            {
                                if (GenerationExceptions.Text[caretIndex - 5] == '\"')
                                {
                                    GenerationExceptions.Text = GenerationExceptions.Text.Remove(caretIndex - 5, 4);
                                    GenerationExceptions.CaretIndex = caretIndex - 8;
                                }
                                else
                                {
                                    GenerationExceptions.Text = GenerationExceptions.Text.Remove(caretIndex - 5, 1);
                                    GenerationExceptions.CaretIndex = caretIndex - 5;
                                }
                            }
                        }
                    }
                    break;
                case Key.Left:
                    if (GenerationExceptions.Text[caretIndex - 1] == '\"')
                    {
                        e.Handled = true;
                        if (caretIndex != 1)
                        {
                            GenerationExceptions.CaretIndex = caretIndex - 4;
                        }
                    }
                    break;
                case Key.Right:
                    if (GenerationExceptions.Text[caretIndex] == '\"')
                    {
                        e.Handled = true;
                        if (caretIndex != GenerationExceptions.Text.Length - 1)
                        {
                            GenerationExceptions.CaretIndex = caretIndex + 4;
                        }
                    }
                    break;
                case Key.Up:
                    e.Handled = true;
                    break;
                case Key.Down:
                    e.Handled = true;
                    break;
            }
        }
        private void ChangeGridParams(in Grid grid, bool isWide)
        {
            if (grid.Children[1] is Grid createGrid)
            {
                if (isWide)
                {
                    createGrid.SetValue(Grid.RowProperty, 0);
                    createGrid.SetValue(Grid.ColumnProperty, 1);
                    grid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                }
                else
                {

                    createGrid.SetValue(Grid.RowProperty, 1);
                    createGrid.SetValue(Grid.ColumnProperty, 0);
                    grid.ColumnDefinitions[1].Width = new GridLength(0);
                }
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth < 900)
            {
                PlantGenerateBody.ColumnDefinitions[0].Width = new GridLength(0);
                Logo.Children[1].Visibility = Visibility.Collapsed;
                Logo.Children[2].Visibility = Visibility.Visible;
                ModeButtons.Height = 100;
                foreach (var button in ModeButtons.Children)
                {
                    if (button is Border border)
                        if (border.Child is Grid grid)
                        {
                            if (grid.Children[0] is Image image)
                                image.Visibility = Visibility.Collapsed;
                            if (grid.Children[1] is TextBlock textBlock)
                                textBlock.FontSize = 20;
                        }
                }
                ChangeGridParams(CreateParams, false);
                ChangeGridParams(ChangeParams, false);
                ChangeGridParams(DisplayCommonInfo, false);
            }
            else
            {
                PlantGenerateBody.ColumnDefinitions[0].Width = new GridLength(0.8, GridUnitType.Star);
                Logo.Children[2].Visibility = Visibility.Collapsed;
                Logo.Children[1].Visibility = Visibility.Visible;
                ModeButtons.Height = 160;
                foreach (var button in ModeButtons.Children)
                {
                    if (button is Border border)
                        if (border.Child is Grid grid)
                        {
                            if (grid.Children[0] is Image image)
                                image.Visibility = Visibility.Visible;
                            if (grid.Children[1] is TextBlock textBlock)
                                textBlock.FontSize = 28;
                        }
                }
                ChangeGridParams(CreateParams, true);
                ChangeGridParams(ChangeParams, true);
                ChangeGridParams(DisplayCommonInfo, true);
            }
        }
        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }
        public bool IsImageFileInUse(string imagePath)
        {
            try
            {
                using (var stream = File.Open(imagePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false;
                }
            }
            catch (IOException)
            {
                return true;
            }
        }
        public void ChangeSelectedImage(Image image, Uri uri)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bi.UriSource = uri;
            bi.EndInit();
            image.Source = bi;
            
        }
        private void OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string file = files[0];
                    if (System.IO.Path.GetExtension(file) == ".png" || System.IO.Path.GetExtension(file) == ".jpg")
                    {
                        if (CurrentModeChoise != "Create" && CurrentModeChoise != "Change")
                            return;
                        currentPlant.ImagePath = file;

                        Grid grid = Body.FindName($"Plant{CurrentModeChoise}Body") as Grid;
                        if (grid.Children[0] is Grid childGrid)
                        {
                            if (childGrid.Children[0] is Image image)
                            {
                                if (CurrentModeChoise.Equals("Change"))
                                {
                                    if (childGrid.Children[2] is Rectangle rectangle)
                                        if (rectangle.Visibility != Visibility.Visible)
                                        {
                                            ChangeSelectedImage(image, new(file));
                                        }
                                }
                                else
                                {
                                    ChangeSelectedImage(image, new(file));
                                }
                            }
                        }
                    }
                }
            }
            e.Handled = true;
        }
        private void GenerateFreeLocations()
        {
            foreach (var child in PlantLocations.Children)
            {
                if (child is Grid grid)
                {
                    if (grid.Children[0] is Border border)
                    {
                        if (border.Child is ComboBox comboBox)
                        {
                            for (int i = 0; i < dictionary.Location.Length; i++)
                            {
                                ComboBoxItem comboBoxItem = new ComboBoxItem();

                                Binding binding = new Binding($"Location[{i}].Selected");
                                binding.Source = dictionary;
                                binding.Mode = BindingMode.OneWay;
                                comboBoxItem.SetBinding(ContentProperty, binding);
                                comboBox.Items.Add(comboBoxItem);
                            }
                        }
                    }
                }
            }
        }
        private Grid CreatePlantLocation(int selectedIndex = -1)
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.Margin = new Thickness(0, 10, 0, 0);

            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition() { Width = GridLength.Auto };
            grid.ColumnDefinitions.Add(col1);
            grid.ColumnDefinitions.Add(col2);

            Border border = new Border();
            border.CornerRadius = new CornerRadius(10);
            border.Padding = new Thickness(4);
            border.Cursor = Cursors.Hand;


            ComboBox comboBox = new ComboBox();
            SetFreeLocations(comboBox, selectedIndex);
            if(selectedIndex != -1)
            {
                comboBox.SelectedIndex = selectedIndex;
            }

            border.Child = comboBox;
            Grid.SetColumn(border, 0);
            grid.Children.Add(border);

            Grid imageGrid = new Grid();
            imageGrid.Background = Brushes.Transparent;
            imageGrid.Cursor = Cursors.Hand;
            imageGrid.Margin = new Thickness(20, 0, 10, 0);
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition());
            imageGrid.Width = 20;
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("Source/Images/minus.png", UriKind.Relative));
            Grid.SetColumn(imageGrid, 1);
            imageGrid.Children.Add(image);
            grid.Children.Add(imageGrid);

            imageGrid.MouseDown += RemovePlantLocation;

            return grid;
        }
        private ComboBox GetPlantLocationChoise(Grid grid)
        {
            if (grid.Children[0] is Border border)
            {
                if (border.Child is ComboBox comboBox)
                {
                    return comboBox;
                }
            }
            return null;
        }
        private StackPanel ChoosePlantMode()
        {
            if (CurrentModeChoise.Equals("Create"))
            {
                return PlantLocations;
            }
            else
            {
                return ChangePlantLocations;
            }
        }
        private void AddPlantLocation(object sender = null, MouseButtonEventArgs e = null)
        {
            StackPanel stackPanel = ChoosePlantMode();

            if (stackPanel.Children.Count - 1 == dictionary?.Location.Length)
                return;
            if (stackPanel.Children[PlantLocations.Children.Count - 2] is Grid grid)
            {
                ComboBox comboBox = GetPlantLocationChoise(grid);
                if (comboBox.SelectedIndex != -1)
                {
                    stackPanel.Children.Insert(stackPanel.Children.Count - 1, CreatePlantLocation());
                }
            }
        }
        private void RemovePlantLocation(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (Grid)((Grid)sender).Parent;
            if (grid.Children[0] is Border border)
            {
                if (border.Child is ComboBox comboBox)
                {
                    StackPanel stackPanel;
                    if(CurrentModeChoise == "Create")
                    {
                        stackPanel = PlantLocations;
                    }
                    else
                    {
                        stackPanel = ChangePlantLocations;
                    }
                    string selectedValue = comboBox.SelectedValue?.ToString();
                    if (selectedValue != null)
                    {
                        int selectedIndex = FindLocationIndex(selectedValue);
                        locationIndexes.Remove(selectedIndex);
                        stackPanel.Children.Remove(grid);
                        PlantLocationChanged();
                    }
                    else
                    {
                        stackPanel.Children.Remove(grid);
                    }
                }
            }
        }

        private void NumericEnterCheck(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
        public int FindLocationIndex(string location)
        {
            for (int i = 0; i < dictionary.Location.Length; i++)
            {
                if (dictionary.Location[i].CheckCompilance(location))
                    return i;
            }
            return -1;
        }
        private int SetFreeLocations(ComboBox comboBox, int selectedIndex = -1)
        {
            comboBox.Items.Clear();
            int k = 0, result = -1;
            for (int i = 0; i < dictionary.Location.Length; i++)
            {
                if (i == selectedIndex)
                    result = k;
                if (locationIndexes.Contains(i) & i != selectedIndex)
                        continue;
                k++;
                ComboBoxItem comboBoxItem = new ComboBoxItem();

                Binding binding = new Binding($"Location[{i}].Selected");
                binding.Source = dictionary;
                binding.Mode = BindingMode.OneWay;
                comboBoxItem.SetBinding(ContentProperty, binding);
                comboBoxItem.VerticalContentAlignment= VerticalAlignment.Center;
                comboBoxItem.HorizontalContentAlignment= HorizontalAlignment.Left;
                comboBox.Items.Add(comboBoxItem);
            }
            return result;
        }
        bool isUserEvent = true;
        public List<int> locationIndexes = new();
        private void PlantLocationChanged(object sender = null, SelectionChangedEventArgs e = null)
        {
            if (!isUserEvent)
                return;
            isUserEvent = false;
            locationIndexes.Clear();
            StackPanel stackPanel = ChoosePlantMode();
            if (stackPanel.Parent is Grid gr)
            {
                if (gr.Children[0] is TextBlock textBlock)
                {
                    textBlock.Foreground = Brushes.White;
                }
            }
            foreach (var locationContainer in stackPanel.Children)
            {
                if (locationContainer is Grid grid)
                {
                    if (grid.Children[0] is Border border)
                    {
                        if (border.Child is ComboBox comboBox)
                        {
                            if (comboBox.SelectedValue != null)
                            {
                                string selectedValue = comboBox.SelectedValue.ToString();
                                int selectedIndex = FindLocationIndex(selectedValue);
                                if (!locationIndexes.Contains(selectedIndex))
                                    locationIndexes.Add(selectedIndex);
                            }
                        }
                    }
                }
            }
            foreach (var locationContainer in stackPanel.Children)
            {
                if (locationContainer is Grid grid)
                {
                    if (grid.Children[0] is Border border)
                    {
                        if (border.Child is ComboBox comboBox)
                        {
                            if (comboBox.SelectedValue != null)
                            {
                                string selectedValue = comboBox.SelectedValue.ToString();
                                int selectedIndex = FindLocationIndex(selectedValue);
                                if (selectedIndex == -1)
                                    continue;
                                comboBox.SelectedIndex = SetFreeLocations(comboBox, selectedIndex);
                            }
                            else
                            {
                                SetFreeLocations(comboBox);
                            }
                        }
                    }
                }
            }
            isUserEvent = true;
        }

        private void RarityEnter(object sender, TextCompositionEventArgs e)
        {
            NumericEnterCheck(sender, e);
            if (e.Handled == false)
                if (sender is TextBox textBox)
                {
                    int rarity = int.Parse(textBox.Text + e.Text);
                    if(sender is MyTextBox myTextBox)
                    {
                        if(rarity < myTextBox.MinValue || rarity > myTextBox.MaxValue)
                            e.Handled = true;
                    }
                    else if (rarity < 1 || rarity > 5)
                        e.Handled = true;
                }
        }
        private void SavePlant(object sender, MouseButtonEventArgs e)
        {
            PlantSaver.SaveLocations();
            ChangeImage.Source = null;
            ChangeImage.UpdateLayout();
            if (PlantSaver.SavePlant(SelectedPlantIndex))
            {
                Clear();
                //if (sender is Border border)
                //{
                //    LinearGradientBrush myBrush = border.Background as LinearGradientBrush;
                //    var colorAnimation = new DoubleAnimationUsingKeyFrames
                //    {
                //        KeyFrames =
                //        {
                //            new LinearDoubleKeyFrame(1, KeyTime.FromPercent(0)),
                //            new LinearDoubleKeyFrame(10, KeyTime.FromPercent(0.5)),
                //            new LinearDoubleKeyFrame(1, KeyTime.FromPercent(1))
                //        },
                //        Duration = TimeSpan.FromSeconds(1),
                //    };

                //    Storyboard.SetTarget(colorAnimation, myBrush.GradientStops[1]);
                //    Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("Offset"));


                //    myBrush.BeginAnimation(GradientStop.OffsetProperty, colorAnimation);
                //}
            }

        }
        private void ProhibitMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }
        private void ClearPlantParams(Grid plantParams)
        {
            for (int n = 0; n < plantParams.Children.Count; n++)
            {
                if (plantParams.Children[n] is Grid paramColumn)
                {
                    for (int k = 0; k < paramColumn.Children.Count; k++)
                    {
                        if (paramColumn.Children[k] is Grid paramGrid)
                        {
                            if (paramGrid.Children[0] is TextBlock textBlock)
                            {
                                textBlock.Foreground = Brushes.White;
                            }
                            if (paramGrid.Children[1] is TextBox textBox)
                            {
                                if (!string.IsNullOrEmpty(textBox.Text))
                                    textBox.Focus();

                                if ((k != 0 | n != 0) | CurrentModeChoise != "Change")
                                    textBox.Clear();
                            }
                            else if (paramGrid.Children[1] is StackPanel)
                            {
                                StackPanel stackPanel = ChoosePlantMode();
                                if (stackPanel.Children[0] is Grid grid)
                                {
                                    ComboBox comboBox = GetPlantLocationChoise(grid);
                                    comboBox.SelectedIndex = -1;
                                }
                                for (int i = stackPanel.Children.Count - 2; i > 0; i--)
                                {
                                    stackPanel.Children.RemoveAt(i);
                                }
                                PlantSaver.SaveLocations();
                            }
                        }
                    }
                }
            }
        }
        public void Clear()
        {
            switch (CurrentModeChoise)
            {
                case "Generate":
                    foreach (var param in GenerationParams.Children)
                    {
                        if (param is Grid generationParam)
                        {
                            if (generationParam.Children[1] is TextBox generationText)
                            {
                                generationText.Text = "";
                                TextBoxLostFocus(generationText);
                            }
                            if (generationParam.Children[0] is TextBlock textBlock)
                            {
                                textBlock.Foreground = Brushes.White;
                            }
                        }
                    }
                    GenerationLocation.SelectedIndex = -1;
                    GenerationResults.Children.Clear();
                    NoGenerationImage.Visibility = Visibility.Visible;
                    break;
                case "Create":
                    CreateImage.Source = new BitmapImage(new Uri("Source/Images/PlantGirl.png", UriKind.Relative));
                    locationIndexes.Clear();
                    ClearPlantParams(CreateParams);
                    break;
                case "Change":
                    if (ChangePlantName.Children[1] is TextBox textBox)
                    {
                        textBox.Text = string.Empty;
                        CheckPlantFind(textBox);
                        if (ChangePlantName.Children[0] is TextBlock textBlock)
                        {
                            textBlock.Foreground = Brushes.White;
                        }
                    }
                    SelectedPlantIndex = -1;
                    break;
                case "Display":
                    DisplayImage.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
                    DisplayImage.UpdateLayout();
                    currentPlant.ClearParams();
                    break;
                case "Destroy":
                    DestroyImage.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
                    DestroyImage.UpdateLayout();
                    currentPlant.ClearParams();
                    PlantDestroyer.ChangeVisibility(Visibility.Collapsed);
                    SelectedPlantIndex = -1;
                    break;
            }
        }
        private void ClearButtonClick(object sender, MouseButtonEventArgs e)
        {
            Clear();
        }

        private void Generate(object sender, MouseButtonEventArgs e) => Generation.Generate();

        private void GenerationToWhite(object sender, RoutedEventArgs e)
        {
            if (GenerateLocation.Children[0] is TextBlock textBlock)
            {
                textBlock.Foreground = Brushes.White;
            }
        }

        private void GnerationTextToWhite(object sender, TextChangedEventArgs e)
        {
            if (FindParentOfType<Grid>((DependencyObject)sender).Children[0] is TextBlock textBlock)
            {
                textBlock.Foreground = Brushes.White;
            }
        }
        public int FindPlantIndexByName(string name)
        {
            for(int i = 0; i < plants.Count; i++)
            {
                if (name.Equals(plants[i].Name))
                    return i;
            }
            return -1;
        }
        private void AddPlantChangeLocation(object sender, MouseButtonEventArgs e)
        {
            if (PlantLocations.Children.Count - 1 == dictionary?.Location.Length)
                return;
            if (PlantLocations.Children[PlantLocations.Children.Count - 2] is Grid grid)
            {
                ComboBox comboBox = GetPlantLocationChoise(grid);
                if (comboBox.SelectedIndex != -1)
                {
                    PlantLocations.Children.Insert(PlantLocations.Children.Count - 1, CreatePlantLocation());
                }
            }
        }
        private void AddChangePlantLocation(object sender, MouseButtonEventArgs e)
        {
            if (ChangePlantLocations.Children.Count - 1 == dictionary?.Location.Length)
                return;
            if (ChangePlantLocations.Children[ChangePlantLocations.Children.Count - 2] is Grid grid)
            {
                ComboBox comboBox = GetPlantLocationChoise(grid);
                if (comboBox.SelectedIndex != -1)
                {
                    ChangePlantLocations.Children.Insert(ChangePlantLocations.Children.Count - 1, CreatePlantLocation());
                }
            }
        }
        private void SetCheckPlant(int plantIndex)
        {
            currentPlant.SetParamsFromPlant(plants[plantIndex]);
            for (int i = 0; i < ChangeParams.Children.Count; i++)
            {
                if (ChangeParams.Children[i] is Grid paramColumn)
                {
                    for (int k = 0; k < paramColumn.Children.Count; k++)
                    {
                        if ((k != 1 | i != 0) && paramColumn.Children[k] is Grid paramGrid)
                        {
                            TextBlockUpAnimation(paramGrid);
                        }
                    }
                }
            }
            SelectedPlantIndex = plantIndex;
            ChangePlantLocations.Children.RemoveRange(1, ChangePlantLocations.Children.Count - 2);
            locationIndexes.Clear();
            ChoiseButtonCover.Visibility = Visibility.Collapsed;
            ChoiseCover.Visibility = Visibility.Collapsed;
            ImageCover.Visibility = Visibility.Collapsed;
            if (string.IsNullOrEmpty(currentPlant.ImagePath))
            {
                ChangeImage.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
            }
            else
            {
                ChangeSelectedImage(ChangeImage, new(System.IO.Path.Combine(Environment.CurrentDirectory, "UserPlantImages", currentPlant.ImagePath), UriKind.Absolute));
            }
            for (int i = 0; i < currentPlant.Locations.Count; i++)
            {
                int index = FindLocationIndex(currentPlant.Locations[i]);
                if (i != 0)
                    AddPlantLocation();
                if (ChangePlantLocations.Children[ChangePlantLocations.Children.Count - 2] is Grid location)
                    if (location.Children[0] is Border border)
                        if (border.Child is ComboBox comboBox)
                        {
                            comboBox.SelectedIndex = SetFreeLocations(comboBox, index);
                        }
            }
        }
        private void CheckPlantFind(object sender)
        {
            if (sender is TextBox t)
            {
                if (string.IsNullOrEmpty(t.Text))
                    t.Text = string.Empty;
            }
            int plantIndex = FindPlantIndexByName(currentPlant.Name);
            if (plantIndex != -1)
            {
                SetCheckPlant(plantIndex);
            }
            else
            {
                string name = currentPlant.Name;
                ClearPlantParams(ChangeParams);
                currentPlant.ClearParams();
                currentPlant.Name = name;
                if (ChangePlantName.Children[1] is TextBox textName)
                    textName.Focus();
                SelectedPlantIndex = -1;
                locationIndexes.Clear();
                Uri uri = new("Source/Images/PlantGirl.png", UriKind.Relative);
                BitmapImage bitmapImage = new BitmapImage(uri);
                ChangeImage.Source = bitmapImage;
                if (sender is TextBox textBox)
                {
                    textBox.CaretIndex = textBox.Text.Length;
                    if (VisualTreeHelper.GetChild(VisualTreeHelper.GetParent(textBox), 0) is TextBlock textBlock)
                        textBlock.Foreground = Brushes.Red;
                }
                ChoiseButtonCover.Visibility = Visibility.Visible;
                ChoiseCover.Visibility = Visibility.Visible;
                ImageCover.Visibility = Visibility.Visible;
            }
            //MessageBox.Show(IsImageFileInUse("C:\\Study\\C#\\Spring\\DnP\\bin\\Debug\\net7.0-windows\\UserPlantImages\\aaaa.png").ToString());

        }

        private void NameConfirm(object sender, KeyEventArgs e)
        {
            if(FindParentOfType<Grid>((DependencyObject)sender).Children[0] is TextBlock textBlock)
            {
                textBlock.Foreground = Brushes.White;
            }
            if(e.Key== Key.Enter)
            {
                CheckPlantFind(sender);
            }
        }

        private void BackToScroll(object sender, MouseButtonEventArgs e)
        {
            SetMode("ScrollButton");
        }

        private void ToChange(object sender, MouseButtonEventArgs e)
        {
            string name = currentPlant.Name;
            SetMode("ChangeButton");
            SetCheckPlant(FindPlantIndexByName(name));
        }

        private void ToDestroy(object sender, MouseButtonEventArgs e)
        {
            string name = currentPlant.Name;
            SetMode("DestroyButton");
            PlantDestroyer.SearchDestroy(name);
            TextBlockUpAnimation(DestroySearch);
        }
        private void DestroySearchKeyDown(object sender, KeyEventArgs e)
        {
            if (DestroySearch.Children[0] is TextBlock textBlock)
            {
                textBlock.Foreground = Brushes.White;
            }
            switch (e.Key)
            {
                case Key.Enter:
                    PlantDestroyer.SearchDestroy(DestroyNameSearch.Text);
                    break;
            }
        }

        private void DestroyButtonClick(object sender, MouseButtonEventArgs e)
        {
            PlantDestroyer.DestroyPlant();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        public void PlantClick(object sender, MouseButtonEventArgs e)
        {
            SetMode("Display");
            if(sender is Border border)
            {
                if (border.Child is Grid grid)
                    if (grid.Children[1] is TextBlock textBlock)
                    {
                        Search.SearchPlant(textBlock.Text);
                    }
            }
            else if(sender is Grid grid)
                if (grid.Children[1] is TextBlock textBlock)
                {
                    Search.SearchPlant(textBlock.Text);
                }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            ChangeVisibility(Menu);
        }
        public static RoutedCommand routedCommand = new RoutedCommand();

        private void ChangeLanguage(object sender, MouseButtonEventArgs e)
        {
            dictionary.ChangeLanguage();
        }

        private void ChangeTheme(object sender, MouseButtonEventArgs e)
        {
            if(currentTheme == Themes.Dark)
            {
                currentTheme = Themes.Light;
            }
            else
            {
                currentTheme = Themes.Dark;
            }
            Application.Current.Resources.MergedDictionaries.Clear();
            var uri = new Uri(currentTheme.ToString() + "Theme.xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void MyTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is MyTextBlock textBlock)
            {
                textBlock.MyText = new Random().Next().ToString();
            }
        }
        public void MyTextBlock_CustomMouseClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.GetType().ToString());
        }
    }
}