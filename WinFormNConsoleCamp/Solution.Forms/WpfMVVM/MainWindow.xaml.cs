
using System.Windows;
using WpfMVVM.ViewModel;

namespace WpfMVVM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 상호작용할 뷰 모델 인스턴스 구성.
            MainWindowViewModel viewModel = new();

            DataContext = viewModel;

            /*

                    [ (M-V-VM) Model-View-ViewModel]

                    View --> ViewModel --> Model
                    Model -> ViewModel -> View

                    데이터베이스를 업데이트하고 데이터베이스에서 뷰로 푸시해야 하는 경우가 될 수 있습니다.
            
                    ObservableCollections<T>
                    ListBox, GridView, 기타 ItemsControl을 지원하는 모델 또는 뷰 모델 이 대상이됨

                    --> (1) 사용자가 뷰를 업데이트하면 
                            - 데이터바인딩이 변경 사항을 ViewModel에 푸시하고 
                    ---> (2) 현재 모델 데이터가 업데이트 되면
                            - 데이터바인딩이 변경 사항을 뷰에 푸시합니다.
                    (요약)
                    - x:Name을 사용하여 컨트롤 이름을 지정할 필요가 거의 없다면 재대로 구현된 것으로 판단할 수 있음.


                    - 다음만 포함해야 합니다.
                     * ViewModel을 인스턴스화하기 위한 기본 연결(그리고 필요한 서비스를 주입할 수도 있음)
                     * 단순히 공용 ViewModel 메서드에 위임하는 명령 Execute/CanExecut 쌍에 대한 명령 바인딩 및 스캐폴딩.

                    => 코드를 최대한 깔끔하게 유지하려고 노력하세요.
            */

        }
    }
}