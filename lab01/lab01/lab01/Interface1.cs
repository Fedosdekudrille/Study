using System;

namespace lab01
{
    internal interface Interface1
    {
            int Property 
            { get; set; }
            void InterfaceMethod();
            public event Action Event;
            int this[int index] 
            { get; set; }
        }
    }
