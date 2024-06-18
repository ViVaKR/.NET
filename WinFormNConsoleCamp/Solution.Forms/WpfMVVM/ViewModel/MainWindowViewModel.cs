
using System.Collections.ObjectModel;
using System.Diagnostics;
using WpfMVVM.Model;
using WpfMVVM.MVVM;

namespace WpfMVVM.ViewModel
{

    /// <summary>
    /// M(Model)
    /// V(View) -> MainWindow
    /// VM(View Model) -> MainWindowViewModel
    /// 
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// (옵~'저러불) 관찰 대상 컬렉셜(데이터 베이스) 선언
        /// 실제는 데이터 베이스로 부터 인스턴스화 됨.
        /// </summary>
        public ObservableCollection<Item>? Items { get; set; }

        public ObservableCollection<Customer>? Customers { get; set; }

        /// 그리드에서 선택된 행 레코드.
        /// 수정 삭제 등의 목적으로
        /// 속성이 변경되는 순간 포착 
        /// 쌍방향 프로퍼티 및 관련 필드
        /// </summary>
        private Item? selectedItem;
        public Item? SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private Customer? selectedCustomer;
        public Customer? SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        public RelayCommand<Item> AddCommand => new(x => AddItem());
        public RelayCommand<Customer> AddCustomerCommand => new(x => AddCustomer());
        public RelayCommand<Item> SaveCommand => new(x => Save(), x => CanSave());
        public RelayCommand<Item> SaveCustomerCommand => new(x => SaveCustomer(), x=>true);
        public RelayCommand<Item> DeleteCommand => new(x => DeleteItem(), y => SelectedItem != null);
        public RelayCommand<Item> DeleteCustomerCommand => new(x => DeleteItem(), y => SelectedItem != null);

        public MainWindowViewModel()
        {
            Items = [];
            Customers =
            [
                new Customer { Id = 1, Name = "Viv"},
                new Customer { Id = 2, Name = "Hello"},
                new Customer { Id = 3, Name = "World"},
            ];
        }

        /// <summary>
        /// (C)reate
        /// 신규 데이터 추가 하기.
        /// 시뮬레이터
        /// </summary>
        private void AddItem()
        {
            Items ??= [];

            int id = Items.Count + 1;

            Items?.Add(new Item
            {
                Id = id,
                Name = $"Item_{id + 100}",
                SerialNumber = $"S_{new Random().Next(100, 999)}",
                Quantity = new Random().Next(1, 100)
            });
        }

        private void AddCustomer()
        {
            Customers ??= [];

            int id = Customers.Count + 1;

            Customers?.Add(new Customer
            {
                Id = id,
                Name = $"Item_{id + 100}",
            });
        }



        /// <summary>
        /// (U)pdate
        /// 수정 저장 및 
        /// 확인 출력 테스트..
        /// </summary>
        private void Save()
        {
            if (Items != null)
                foreach (var item in Items)
                    Debug.WriteLine(
                        $"{item.Id}, " +
                        $"{item.Name}, " +
                        $"{item.SerialNumber}, " +
                        $"{item.Quantity}, ");
        }

        private void SaveCustomer()
        {
            if (Customers != null)
                foreach (var item in Customers)
                    Debug.WriteLine(
                        $"{item.Id}, " +
                        $"{item.Name}");
        }

        private bool CanSave() => Items != null;

        /// <summary>
        /// (D)elete 
        /// </summary>
        private void DeleteItem()
        {
            if (SelectedItem != null)
                _ = (Items?.Remove(item: SelectedItem));
        }
    }
}
