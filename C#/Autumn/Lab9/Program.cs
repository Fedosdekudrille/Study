using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Lab9
{
    enum GameTypes
    {
        Monopoly,
        Pocker,
        GameOfThrones,
        DnD,
    }
    class Game : IEnumerable
    {
        string[] players;
        GameTypes type;
        public Game(int playersAmount)
        {
            Random random = new Random();
            type = (GameTypes)random.Next(0, Enum.GetNames(typeof(GameTypes)).Length);
            players = new string[playersAmount];
            for(int i = 0; i < playersAmount; i++)
            {
                players[i] = "Игрок " + (i + 1);
            }
        }
        public Game(GameTypes type, params string[] players)
        {
            this.players = players;
            this.type = type;
        }
        public IEnumerator GetEnumerator() => players.GetEnumerator();
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new(4);
            foreach (var player in game)
            {
                Console.WriteLine(player);
            }
            BlockingCollection<int> ints = new BlockingCollection<int>(10);
            Console.WriteLine(ints.Count);
            try
            {
                for (int i = 0; i < ints.BoundedCapacity; i++)
                {
                    ints.Add(i);
                    if (i == 7)
                    {
                        ints.CompleteAdding();
                    }
                }
                Console.WriteLine(ints.Count);
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine("А вот нечего было! Коллекция то закрыта...");
            }
            Console.WriteLine(ints.Sum());
            Console.WriteLine(ints.Take());
            foreach (int i in ints)
            {
                Console.Write($"{i}  ");
            }
            Console.WriteLine();
            BlockingCollection<char> chars = new(10);
            Console.WriteLine((char)chars.BoundedCapacity + 65);
            for (char ch = 'a'; ch <= 'd'; ch++)
            {
                Console.WriteLine(ch);
                chars.Add(ch);
            }
            foreach (char ch in chars)
            {
                Console.Write(ch);
            }
            Console.WriteLine();
            ObservableCollection<int> Observe = new ObservableCollection<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12,
            };
            Observe.CollectionChanged += (object? sender, NotifyCollectionChangedEventArgs e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        if (e.NewItems?[0] is int newEl)
                            Console.WriteLine($"Добавлен новый элемент: {newEl}");
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        if (e.OldItems?[0] is int oldEl)
                            Console.WriteLine($"Удален элемент: {oldEl}");
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        if ((e.NewItems?[0] is int replacingEl) && (e.OldItems?[0] is int replacedEl))
                            Console.WriteLine($"Элемент {replacedEl} заменен элементом {replacingEl}");
                        break;
                }
            };
            Observe.Add(3);
            Observe.RemoveAt(10);
            Observe[6] = 1;
        }
    }
}