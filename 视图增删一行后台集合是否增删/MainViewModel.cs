using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace 视图增删一行后台集合是否增删
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            ObservableProducts = new ObservableCollectionEx<Product>(Repository.Instance);

            ObservableProducts.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    var d = args.NewItems; // null
                    var a = args.NewStartingIndex; // 5
                    var b = args.OldStartingIndex; // 0
                    var c = args.OldItems; //
                }
            };
        }

        public ObservableCollectionEx<Product> ObservableProducts { get; }
    }



    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        private MethodInfo _InsertItem;
        private MethodInfo _RemoveItem;
        public ObservableCollectionEx()
        {
            _InsertItem = this.GetType().BaseType.BaseType.GetMethod("InsertItem", BindingFlags.NonPublic | BindingFlags.Instance);
            _RemoveItem = this.GetType().BaseType.BaseType.GetMethod("RemoveItem", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public ObservableCollectionEx(IEnumerable<T> collection) : base(collection)
        {

            _InsertItem = this.GetType().BaseType.BaseType.GetMethod("InsertItem", BindingFlags.NonPublic | BindingFlags.Instance);
            _RemoveItem = this.GetType().BaseType.BaseType.GetMethod("RemoveItem", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public ObservableCollectionEx(List<T> list) : base(list)
        {

            _InsertItem = this.GetType().BaseType.BaseType.GetMethod("InsertItem", BindingFlags.NonPublic | BindingFlags.Instance);
            _RemoveItem = this.GetType().BaseType.BaseType.GetMethod("RemoveItem", BindingFlags.NonPublic | BindingFlags.Instance);
        }


        public void AddRange(IEnumerable<T> items)
        {
            InsertRange(this.Count, items);
        }

        public void InsertRange(int index, IEnumerable<T> items)
        {
            CheckReentrancy();
            int newStartingIndex = index;
            int index2 = index;
            object[] args = new object[2];
            foreach (var item in items)
            {
                args[0] = index2;
                args[1] = item;
                _InsertItem.Invoke(this, args);

                index2++;
            }

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items.ToList(), newStartingIndex));
        }

        public void RemoveRange(int start, int count)
        {
            if (start < 0 || start >= count || start + count > this.Count)
            {
                throw new ArgumentException($"Invalid arguments");
            }

            CheckReentrancy();
            var removedItems = new List<T>();
            object[] args = new object[1] { start };
            for (int i = 0; i < count; i++)
            {
                T removedItem = this[start];
                removedItems.Add(removedItem);
                _RemoveItem.Invoke(this, args);
            }
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems, start));
        }
    }



    public class Repository : List<Product>
    {
        private Repository()
        {
            Add(new Product() { Name = "牛奶" });
            Add(new Product() { Name = "香蕉" });
            Add(new Product() { Name = "可乐" });
            Add(new Product() { Name = "菠萝" });
            Add(new Product() { Name = "荔枝" });
        }

        public static Repository Instance { get; } = new Repository();
    }
}