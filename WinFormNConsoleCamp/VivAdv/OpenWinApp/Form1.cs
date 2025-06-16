using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Collections.Specialized;
namespace OpenWinApp
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized; // 일단 폼을 션하게 최대화한다.

            Panel panel = new() { Dock = DockStyle.Fill }; // 패널을 생성하고 폼에 추가한다.
            Controls.Add(panel);

            TextBox textBox = new(){Dock = DockStyle.Top }; // 텍스트 박스를 생성하고 패널에 추가한다.
            panel.Controls.Add(textBox);

            ListBox listBox = new(){ Dock = DockStyle.Fill }; // 리스트 박스를 생성하고 패널에 추가한다.
            panel.Controls.Add(listBox);

            foreach (var item in GetAllControl(this, typeof(Control))) // 모든 컨트롤을 가져온다.
            {
                var t = item.GetType();
                string name = t.Name;

                // 문의에 대한 답변
                var allEvents = t.GetEvents().Select(x => $"{name}\t\t{x.Name}").ToList();
                listBox.Items.AddRange([.. allEvents]); // 리스트 박스에 모든 컨트롤 이름과 모든 이벤트 이름을 추가한다.
            }

            EventWatcher(); // (+원뫄 띵) 컬렉션 변경 이벤트를 감지한다.
        }

        /// <summary>
        /// 재귀 함수를 사용하여 모든 컨트롤을 가져온다.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<Control> GetAllControl(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>(); // 컨트롤을 캐스팅한다.
            return controls.SelectMany(ctrl => GetAllControl(ctrl, type)).Concat(controls); // 컨트롤을 선택하고 컨트롤을 연결한다.
        }

        /// <summary>
        /// (+원뫄 띵) 컬렉션 변경 이벤트를 감지한다.
        /// </summary>
        public void EventWatcher()
        {
            ObservableCollection<int> myList = [];

            myList.CollectionChanged += (s, e) =>
            {

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        Debug.WriteLine($"Added - {s?.GetType().Name}");
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        Debug.WriteLine($"Removed - {s?.GetType().Name}");
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        Debug.WriteLine($"Replace - {s?.GetType().Name}");
                        break;
                    case NotifyCollectionChangedAction.Move:
                        Debug.WriteLine($"Move - {s?.GetType().Name}");
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        Debug.WriteLine($"Reset - {s?.GetType().Name}");
                        break;
                    default:
                        break;
                }
            };

            myList.Add(1);
            myList.Remove(1);
        }
    }
}
