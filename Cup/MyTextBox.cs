using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Cup;

namespace Cup
{
    public class MyDependencyTextBox : TextBox
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public static readonly DependencyProperty MaxValueProperty;

        public static readonly DependencyProperty MinValueProperty;
        static MyDependencyTextBox() 
        {
            var maxMeta = new FrameworkPropertyMetadata();
            maxMeta.CoerceValueCallback = CoerceMaxValue;
            MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(MyTextBox), maxMeta, OnValidateMaxValue);

            var minMeta = new FrameworkPropertyMetadata();
            minMeta.CoerceValueCallback = CoerceMinValue;
            MinValueProperty = DependencyProperty.Register("MinValue", typeof(int), typeof(MyTextBox), minMeta, OnValidateMinValue);
        }
        private static bool OnValidateMaxValue(object value)
        {
            int currentValue = (int)value;
            if (currentValue <= 10000) // если текущее значение от нуля и выше
                return true;
            return false;
        }

        private static bool OnValidateMinValue(object value)
        {
            int currentValue = (int)value;
            if (currentValue >= 0) // если текущее значение от нуля и выше
                return true;
            return false;
        }

        private static object CoerceMaxValue(DependencyObject obj, object value)
        {
            int intValue = (int)value;
            if (intValue < ((MyDependencyTextBox)obj).MinValue)
            {
                return ((MyDependencyTextBox)obj).MinValue;
            }
            return intValue;
        }

        private static object CoerceMinValue(DependencyObject obj, object value)
        {
            int intValue = (int)value;
            if (intValue > ((MyDependencyTextBox)obj).MaxValue)
            {
                return ((MyDependencyTextBox)obj).MaxValue;
            }
            return intValue;
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            mainWindow.Grr.AddHandler(CustomMouseClickEvent, new RoutedEventHandler(mainWindow.MyTextBlock_CustomMouseClick));
            RoutedEventArgs args = new RoutedEventArgs(CustomMouseClickEvent, this);
            RaiseEvent(args);
        }

        public static readonly RoutedEvent CustomMouseClickEvent = EventManager.RegisterRoutedEvent(
            "DirectlMouseClick",
            RoutingStrategy.Direct,
            typeof(RoutedEventHandler),
            typeof(MyTextBlock));

        public event RoutedEventHandler CustomMouseClick
        {
            add { AddHandler(CustomMouseClickEvent, value); }
            remove { RemoveHandler(CustomMouseClickEvent, value); }
        }
    }
}
