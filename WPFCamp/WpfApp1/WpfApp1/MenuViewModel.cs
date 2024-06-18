using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WpfApp1
{
    internal class MenuViewModel : INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private List<string>? itemsList;
        public List<string>? ItemsList
        {
            get => itemsList;
            set
            {
                if(itemsList == null)
                {
                    itemsList = value;
                    Notify("ItemsList");
                }
            }
        }

        public MenuViewModel()
        {
            ItemsList = new List<string>
            {
                "Hello", "World"
            };
        }

        private void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


    }
}
