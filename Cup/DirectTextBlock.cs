using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace Cup
{
    public class DirectTextBlock : TextBlock
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            mainWindow.StP.AddHandler(CustomMouseClickEvent, new RoutedEventHandler(mainWindow.MyTextBlock_CustomMouseClick));
            RoutedEventArgs args = new RoutedEventArgs(CustomMouseClickEvent, this);
            RaiseEvent(args);
        }
        public static readonly RoutedEvent CustomMouseClickEvent = EventManager.RegisterRoutedEvent(
            "DirectMouseClick",
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
