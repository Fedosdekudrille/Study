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
    public class MyImage : Image
    {
        static MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            mainWindow.Logo.AddHandler(CustomMouseClickEvent, new RoutedEventHandler(mainWindow.MyTextBlock_CustomMouseClick));
            this.AddHandler(CustomMouseClickEvent, new RoutedEventHandler(mainWindow.MyTextBlock_CustomMouseClick));
            RoutedEventArgs args = new RoutedEventArgs(CustomMouseClickEvent, this);
            RaiseEvent(args);
        }

        public static readonly RoutedEvent CustomMouseClickEvent = EventManager.RegisterRoutedEvent(
            "TunnelMouseClick",
            RoutingStrategy.Tunnel,
            typeof(RoutedEventHandler),
            typeof(MyTextBlock));

        public event RoutedEventHandler CustomMouseClick
        {
            add { AddHandler(CustomMouseClickEvent, value); }
            remove { RemoveHandler(CustomMouseClickEvent, value); }
        }
    }
}
