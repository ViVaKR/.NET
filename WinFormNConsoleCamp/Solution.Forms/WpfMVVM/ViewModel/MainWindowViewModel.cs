
using System.Collections.ObjectModel;
using WpfMVVM.Model;
using WpfMVVM.MVVM;

namespace WpfMVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Item>? Items { get; set; }


        private Item? selectedItem;

        public Item? SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public RelayCommand AddCommand => new(execute => AddItem());

        public RelayCommand DeleteCommand => new(execute => DeleteItem(), canExecute => SelectedItem != null);

        public RelayCommand SaveCommand => new(execute => Save(), canExecute => CanSave());

        public MainWindowViewModel()
        {
            Items = [];
        }

        private void AddItem()
        {
            Items?.Add(new Item
            {
                Name = "NEW ITEM",
                SerialNumber = "XXXXX",
                Quantity = 0
            });
        }

        private void DeleteItem()
        {
            _ = (Items?.Remove(SelectedItem));
        }

        private void Save()
        {
            // Save to file or database
        }

        private bool CanSave()
        {
            // 데이터베이스 연결 상태등 권한 확인등등..
            return true;
        }



    }
}
