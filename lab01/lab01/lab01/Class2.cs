using System;
using System.Collections.Generic;
using System.Text;

namespace lab01
{
    internal class Class2 : Class1, Interface1
    {
        private int _property;
        public int Property
        {
            get { return _property; }
            set { _property = value; }
        }
        public void RaiseEvent()
        {
            Event?.Invoke();
        }
        
        //индексатор
        private readonly int[] _indexer = new int[5];
        public int this[int index]
        {
            get { return _indexer[index]; }
            set { _indexer[index] = value; }

        }

        //метод интерфейса
        public void InterfaceMethod()
        {
            Console.WriteLine("Метод Интрефейса");
        }

        //событие
        public event Action Event;

       
    }
}
