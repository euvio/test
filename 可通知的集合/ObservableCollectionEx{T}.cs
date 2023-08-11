using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.ComponentModel
{
    // 需求
    // 读取数据库数据显示到列表，增删改反应到数据库
    // 增，删，改，所有操作做完之后，点提交按钮再一次性把本次的所有修改反馈到数据库
    // 增的不在删除集合里面的要写入
    // 删的不在增集合里面的要删除
    // 改的不在删除集合且不在增集合里面的要更新

    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        public ObservableCollectionEx()
        {
        }

        public ObservableCollectionEx(List<T> list) : this(list as IEnumerable<T>)
        {
        }

        public ObservableCollectionEx(IEnumerable<T> items) : base(items)
        {
            foreach (var item in items)
            {
                RegisterPropertyChanged(item);
            }
        }

        public new void Add(T item)
        {
            base.Add(item);
            RegisterPropertyChanged(item);
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            RegisterPropertyChanged(item);
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            UnregisterPropertyChanged(item);
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);

            UnregisterPropertyChanged(this[index]);
        }

        private void Inpc_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            SetItem(this.IndexOf((T)sender), (T)sender);
        }

        private void RegisterPropertyChanged(T item)
        {
            if (item is INotifyPropertyChanged inpc)
            {
                inpc.PropertyChanged -= Inpc_PropertyChanged;
                inpc.PropertyChanged += Inpc_PropertyChanged;
            }
        }

        private void UnregisterPropertyChanged(T item)
        {
            if (item is INotifyPropertyChanged inpc)
            {
                inpc.PropertyChanged -= Inpc_PropertyChanged;
            }
        }
    }
}