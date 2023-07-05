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

namespace Cup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ThemeChanger themeChanger;
        public Dictionary? dictionary;
        public Good? currentGood;
        public List<Good> goods = new();
        public int SelectedPlantIndex = -1;
        public string CurrentModeChoise = "Main";
        public List<Good> searchedGoods = new();
        public MainWindow()
        {
            InitializeComponent();
            StreamResourceInfo sri = Application.GetResourceStream(
            new Uri("Source/cursor.ani", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;

            if (Resources["PlantInstance"] is Good pl)
                currentGood = pl;

            if (Resources["DictionaryInstance"] is Dictionary dict)
                dictionary = dict;

            if (Resources["ThemeChangerInstance"] is ThemeChanger th)
                themeChanger = th;

            GoodSaver.LoadPlantsFromJsonFile();
            AddMainGoods();
        }
        private void AddMainGoods()
        {
            PopularGoods.Children.Clear();
            Good good;
            if(goods.Count > 0)
            {
                good = goods[0];
                PopularGoods.Children.Add(CreateMainGood(good.Name, good.Type, good.Rarity, 0, true, good.ImagePath));
                if (goods.Count > 1)
                {
                    WrapPanel wrapPanel = new();
                    Grid.SetRow(wrapPanel, 1);
                    for(int i = 1; i < 3; i++)
                    {
                        if (goods.Count > i)
                        {
                            good = goods[i];
                            wrapPanel.Children.Add(CreateMainGood(good.Name, good.Type, good.Rarity, (i - 1) * 3, false, good.ImagePath));
                        }   
                    }
                    PopularGoods.Children.Add(wrapPanel);
                }
            }
        }
        private Border CreateMainGood(string name, string type, string price, int gridColumn = 0, bool isMain = false, string imageSource = "")
        {
            Border border = new Border();
            border.MouseDown += PlantClick;
            if(isMain)
                Grid.SetColumnSpan(border, 3);
            Grid.SetColumn(border, gridColumn);
            int gridRow = 1;
            if (isMain)
                gridRow = 0;
            Grid.SetRow(border, gridRow);
            if (isMain)
            {
                Style borderStyle = new Style(typeof(Border));
                borderStyle.Setters.Add(new Setter(Border.BackgroundProperty, new Binding("[Card].Selected") { Source = themeChanger }));
                borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(20)));
                borderStyle.Setters.Add(new Setter(Border.PaddingProperty, new Thickness(30)));
                borderStyle.Setters.Add(new Setter(Border.MarginProperty, new Thickness(0, 0, 0, 10)));
                borderStyle.Setters.Add(new Setter(Border.HeightProperty, 250d));
                border.Resources.Add(typeof(Border), borderStyle);

                Style nameStyle = new Style(typeof(TextBlock));
                nameStyle.Setters.Add(new Setter(TextBlock.FontSizeProperty, 40d));
                nameStyle.Setters.Add(new Setter(TextBlock.FontWeightProperty, FontWeights.UltraBold));
                border.Resources.Add("Name", nameStyle);

                Style stackPanelStyle = new Style(typeof(StackPanel));
                stackPanelStyle.Setters.Add(new Setter(StackPanel.OrientationProperty, Orientation.Horizontal));
                border.Resources.Add(typeof(StackPanel), stackPanelStyle);
            }
            else
            {
                Style borderStyle = new Style(typeof(Border));
                borderStyle.Setters.Add(new Setter(Border.BackgroundProperty, new Binding("[Card].Selected") { Source = themeChanger }));
                borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(20)));
                borderStyle.Setters.Add(new Setter(Border.PaddingProperty, new Thickness(30)));
                borderStyle.Setters.Add(new Setter(Border.MarginProperty, new Thickness(0, 0, 0, 10)));
                borderStyle.Setters.Add(new Setter(Border.HeightProperty, 250d));
                border.Resources.Add(typeof(Border), borderStyle);

                Style nameStyle = new Style(typeof(TextBlock));
                nameStyle.Setters.Add(new Setter(TextBlock.FontSizeProperty, 40d));
                nameStyle.Setters.Add(new Setter(TextBlock.FontWeightProperty, FontWeights.UltraBold));
                border.Resources.Add("Name", nameStyle);

                Style stackPanelStyle = new Style(typeof(StackPanel));
                stackPanelStyle.Setters.Add(new Setter(StackPanel.OrientationProperty, Orientation.Horizontal));
                border.Resources.Add(typeof(StackPanel), stackPanelStyle);
            }

            Grid grid = new();
            border.Child = grid;

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            grid.RowDefinitions.Add(new RowDefinition());

            TextBlock nameTextBlock = new TextBlock();
            nameTextBlock.TextAlignment = TextAlignment.Center;
            nameTextBlock.Style = (Style)border.Resources["Name"];
            nameTextBlock.Text = name;
            Grid.SetRow(nameTextBlock, 0);
            grid.Children.Add(nameTextBlock);

            WrapPanel wrapPanel = new WrapPanel();
            Grid.SetRow(wrapPanel, 2);
            grid.Children.Add(wrapPanel);

            TextBlock dishTextBlock = new TextBlock();
            dishTextBlock.Text = type;
            dishTextBlock.Margin = new Thickness(0, 0, 20, 0);
            Grid.SetRow(dishTextBlock, 1);
            wrapPanel.Children.Add(dishTextBlock);

            StackPanel priceStackPanel = new StackPanel();
            wrapPanel.Children.Add(priceStackPanel);

            TextBlock priceTextBlock1 = new TextBlock();
            priceTextBlock1.SetBinding(TextBlock.TextProperty, new Binding("[Price].Selected") { Source = dictionary });
            priceStackPanel.Children.Add(priceTextBlock1);

            TextBlock priceTextBlock2 = new TextBlock();
            priceTextBlock2.Text = price;
            priceStackPanel.Children.Add(priceTextBlock2);

            Image image = new Image();
            
            if (string.IsNullOrEmpty(imageSource))
            {
                image.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
            }
            else
            {
                ChangeSelectedImage(image, new(System.IO.Path.Combine(Environment.CurrentDirectory, "UserPlantImages", imageSource), UriKind.Absolute));
            }
            Grid.SetColumn(image, 1);
            Grid.SetRowSpan(image, 3);
            grid.Children.Add(image);

            return border;
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
                Search.SearchGood(searchPrompt.Text);
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
                        Search.SearchGoods(searchPrompt.Text);
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
            RarityEnter(sender, e);
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

        private void MenuClick(object sender, MouseButtonEventArgs e)
        {
            dictionary.ChangeLanguage();
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
            //string preModeChoise = choise;
            //string previousMode = CurrentModeChoise;
            //if (choise.Equals("Display"))
            //    preModeChoise = "ScrollButton";
            //if (CurrentModeChoise.Equals("Display"))
            //    previousMode = "Scroll";
            //Border currentChoiseButton = (Border)ModeButtons.FindName(previousMode + "Button");
            //currentChoiseButton.Visibility = Visibility.Visible;
            //((Border)ModeButtons.FindName(preModeChoise)).Visibility = Visibility.Collapsed;
            if (Body.FindName($"Plant{CurrentModeChoise}Body") is Grid collapsingGrid)
            {
                collapsingGrid.Visibility = Visibility.Collapsed;
            }


            CurrentModeChoise = choise.Replace("Button", "");


            //int i = 0;
            //foreach (Border border in ModeButtons.Children)
            //{
            //    if (border.Visibility == Visibility.Visible)
            //    {
            //        border.SetValue(Grid.ColumnProperty, i++);
            //    }
            //}
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

            if (CurrentModeChoise == "Scroll")
            {
                GoodScroller.SetPlants(goods);
            }
            else if(CurrentModeChoise == "Main")
            {
                AddMainGoods();
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
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth < 900)
            {
                Logo.Children[1].Visibility = Visibility.Collapsed;
                Logo.Children[2].Visibility = Visibility.Visible;
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
            }
            else
            {
                Logo.Children[2].Visibility = Visibility.Collapsed;
                Logo.Children[1].Visibility = Visibility.Visible;
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
                        currentGood.ImagePath = file;

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

        private void NumericEnterCheck(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
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

        private void RarityEnter(object sender, TextCompositionEventArgs e)
        {
            NumericEnterCheck(sender, e);
            if (e.Handled == false)
                if (sender is TextBox textBox)
                {
                    int rarity = int.Parse(textBox.Text + e.Text);
                    if (sender is MyDependencyTextBox myTextBox)
                    {
                        if (rarity < myTextBox.MinValue || rarity > myTextBox.MaxValue)
                            e.Handled = true;
                    }
                    else if (rarity < 1 || rarity > 5)
                        e.Handled = true;
                }
        }
        private void SavePlant(object sender, MouseButtonEventArgs e)
        {
            ChangeImage.Source = null;
            ChangeImage.UpdateLayout();
            if (GoodSaver.SavePlant(SelectedPlantIndex))
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
                        }
                    }
                }
            }
        }
        public void Clear()
        {
            switch (CurrentModeChoise)
            {
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
                    currentGood.ClearParams();
                    break;
                case "Destroy":
                    DestroyImage.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
                    DestroyImage.UpdateLayout();
                    currentGood.ClearParams();
                    GoodDestroyer.ChangeVisibility(Visibility.Collapsed);
                    SelectedPlantIndex = -1;
                    break;
            }
        }
        private void ClearButtonClick(object sender, MouseButtonEventArgs e)
        {
            Clear();
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
            for(int i = 0; i < goods.Count; i++)
            {
                if (name.Equals(goods[i].Name))
                    return i;
            }
            return -1;
        
        }
        private void SetCheckPlant(int plantIndex)
        {
            currentGood.SetParamsFromGood(goods[plantIndex]);
            for (int i = 0; i < ChangeParams.Children.Count; i++)
            {
                if (ChangeParams.Children[i] is Grid paramColumn)
                {
                    for (int k = 0; k < paramColumn.Children.Count; k++)
                    {
                        if (paramColumn.Children[k] is Grid paramGrid)
                        {
                            TextBlockUpAnimation(paramGrid);
                        }
                    }
                }
            }
            SelectedPlantIndex = plantIndex;
            locationIndexes.Clear();
            ChoiseButtonCover.Visibility = Visibility.Collapsed;
            ChoiseCover.Visibility = Visibility.Collapsed;
            ImageCover.Visibility = Visibility.Collapsed;
            if (string.IsNullOrEmpty(currentGood.ImagePath))
            {
                ChangeImage.Source = new BitmapImage(new("Source/Images/PlantGirl.png", UriKind.Relative));
            }
            else
            {
                ChangeSelectedImage(ChangeImage, new(System.IO.Path.Combine(Environment.CurrentDirectory, "UserPlantImages", currentGood.ImagePath), UriKind.Absolute));
            }
        }
        private void CheckPlantFind(object sender)
        {
            if (sender is TextBox t)
            {
                if (string.IsNullOrEmpty(t.Text))
                    t.Text = string.Empty;
            }
            int plantIndex = FindPlantIndexByName(currentGood.Name);
            if (plantIndex != -1)
            {
                SetCheckPlant(plantIndex);
            }
            else
            {
                string name = currentGood.Name;
                ClearPlantParams(ChangeParams);
                currentGood.ClearParams();
                currentGood.Name = name;
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
            //MessageBox.Show(IsImageFileInUse("C:\\Study\\C#\\Spring\\Cup\\bin\\Debug\\net7.0-windows\\UserPlantImages\\aaaa.png").ToString());

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
            string name = currentGood.Name;
            SetMode("ChangeButton");
            SetCheckPlant(FindPlantIndexByName(name));
        }

        private void ToDestroy(object sender, MouseButtonEventArgs e)
        {
            string name = currentGood.Name;
            SetMode("DestroyButton");
            GoodDestroyer.SearchDestroy(name);
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
                    GoodDestroyer.SearchDestroy(DestroyNameSearch.Text);
                    break;
            }
        }

        private void DestroyButtonClick(object sender, MouseButtonEventArgs e)
        {
            GoodDestroyer.DestroyPlant();
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
                        Search.SearchGood(textBlock.Text);
                    }
                    else if (grid.Children[0] is TextBlock textBlock2)
                    {
                        Search.SearchGood(textBlock2.Text);
                    }
            }
            else if(sender is Grid grid)
                if (grid.Children[1] is TextBlock textBlock)
                {
                    Search.SearchGood(textBlock.Text);
                }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dictionary.ChangeLanguage();
        }
        public static RoutedCommand routedCommand = new RoutedCommand();

        private void ChangeTheme(object sender, MouseButtonEventArgs e)
        {
            themeChanger.ChangeTheme();
            if((int)themeChanger.ChosenTheme == 1)
                Resources["MainBrush"] = new SolidColorBrush(Color.FromRgb(151, 100, 0));
            else
            {
                Resources["MainBrush"] = new SolidColorBrush(Color.FromRgb(235, 177, 29));
            }
        }

        private void Select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox combo)
                GoodScroller.SetSortMode(combo.SelectedIndex);
        }
        private void MyTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is MyTextBlock textBlock)
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