using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace DnP
{
    public class MyTextBlock : TextBlock
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

        public static readonly DependencyProperty MyTextProperty;
        static MyTextBlock()
        {
            var metadata = new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault);
            metadata.CoerceValueCallback = OnCoerceMyTextValue;
            MyTextProperty =
            DependencyProperty.Register(
                "MyText",
                typeof(string),
                typeof(MyTextBlock),
                metadata,
                IsValidMyText);
        }
        private static bool IsValidMyText(object value)
        {
            if((value as string) == "") return false;
            return true;
        }

        private static object OnCoerceMyTextValue(DependencyObject d, object value)
        {
            string val = (string)value;
            return string.IsNullOrEmpty(val)? "null":val;
        }
        
        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            mainWindow.StP.AddHandler(CustomMouseClickEvent, new RoutedEventHandler(mainWindow.MyTextBlock_CustomMouseClick));
            RoutedEventArgs args = new RoutedEventArgs(CustomMouseClickEvent, this);
            RaiseEvent(args);
        }

        public static readonly RoutedEvent CustomMouseClickEvent = EventManager.RegisterRoutedEvent(
            "CustomMouseClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MyTextBlock));

        public event RoutedEventHandler CustomMouseClick
        {
            add { AddHandler(CustomMouseClickEvent, value); }
            remove { RemoveHandler(CustomMouseClickEvent, value); }
        }
    }
}
