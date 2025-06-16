using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace KorPublicAPI
{
    public partial class Form1 : Form
    {
        private readonly string baseUrl; // URL
        private readonly string serviceKey; // 서비스 키
        private readonly int year; // 발생연도, 사용자 인풋으로 받아올 수 있음.
        private readonly MyGridView view; // 사용자 데이터 그리드 뷰, 더블 버퍼링을 사용하기 위함.

        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true; // 폼 더블 버퍼링을 사용한다.

            WindowState = FormWindowState.Maximized; // 폼을 최대화한다.
            Controls.Add(view = new MyGridView()); // RichTextBox 컨트롤을 추가한다.
            baseUrl = "http://apis.data.go.kr/B552061/jaywalking/getRestJaywalking"; // URL
            serviceKey = "thZTFQZO2a7WfAp0knA%2FH3lJetId1vnZUbrtxtiEDxFA4qJ%2F4JCsJbIXqWDqXFriLeye7SZNYo38xPtEOf1tCQ%3D%3D";
            year = 2015; // 연도, 사용자로 부터 받아올 수 있음.

            RunAsync(); // (1) 버튼 클릭 등의 트리거 이벤트로 시작할 수 있음.
        }

        /// <summary>
        /// (1) 시작 트리거 함수
        /// </summary>
        public async void RunAsync()
        {
            var resData = await GetAsync(); // 비동기로 데이터를 가져온다.
            XDocument? xdoc = XDocument.Parse(resData); // XML 데이터를 파싱한다.
            if(xdoc == null) return; // XML 데이터가 없으면 리턴한다.
            if(xdoc.Root == null) return; // XML 데이터의 루트가 없으면 리턴한다.
            foreach (XName? name in xdoc.Root.DescendantNodes().OfType<XElement>().Select(x => x.Name).Distinct())
            {
                Debug.WriteLine($"{name}");
            }

            XElement? list = xdoc.XPathSelectElement("//items");

            var tmp = list?.Elements().Select(x => x.Element("sido_sgg_nm")).Distinct();
            foreach (var x in tmp)
            {
                Debug.WriteLine(x?.Value);
            }

            IEnumerable<XElement>? elements = xdoc?.Descendants("items").FirstOrDefault()?.Elements(); // XML 데이터에서 items 엘리먼트를 찾아서 가져온다.
            if (elements == null) return;

            List<TrafficAccident> trafficAccidents = []; // 교통사고 리스트를 생성한다.

            foreach (var x in elements)
            {
                TrafficAccident trafficAccident = new() // 교통사고 객체를 생성한다.
                {
                    Location = Convert.ToString(x.Element("sido_sgg_nm")?.Value), // 지역
                    Spot = Convert.ToString(x.Element("spot_nm")?.Value), // 지점
                    Accidents = Convert.ToInt32(x.Element("occrrnc_cnt")?.Value), // 사고 건수
                    Fatalities = Convert.ToInt32(x.Element("dth_dnv_cnt")?.Value), // 사망자
                    SevereInjuries = Convert.ToInt32(x.Element("se_dnv_cnt")?.Value), // 중상자
                    MinorInjuries = Convert.ToInt32(x.Element("sl_dnv_cnt")?.Value) // 경상자

                };

                trafficAccidents.Add(trafficAccident); // 교통사고 리스트에 추가한다.
            }
            view.DataSource = trafficAccidents; // 데이터 소스를 설정한다.

            await Task.CompletedTask; // 비동기 작업이 완료되었음을 알린다.
        }

        /// <summary>
        /// 서비스 키를 포함한 API URI를 가져온다.
        /// </summary>
        /// <param name="siDo"></param>
        /// <param name="guGun"></param>
        /// <param name="formatType"></param>
        /// <param name="numOfRows"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        private string GetApiUri(int siDo, int guGun, string formatType, int numOfRows, int pageNo)
        {
            var model = new ApiUri // API URI 모델을 생성한다.
            {
                ServiceKey = serviceKey,
                SearchYearCd = year.ToString(),
                SiDo = siDo,
                GuGun = guGun,
                Type = formatType,
                NumOfRows = numOfRows,
                PageNo = pageNo
            };

            StringBuilder sb = new(); // 스트링 빌더를 생성한다.

            sb.Append(baseUrl); // URL을 추가한다.

            sb.Append($"?ServiceKey={serviceKey}"); // 서비스 키를 추가한다.

            foreach (var prop in model.GetType().GetProperties())
            {
                JsonPropertyNameAttribute? props = prop.GetCustomAttribute<JsonPropertyNameAttribute>(); // JsonPropertyNameAttribute를 가져온다.
                if (props == null) continue; // JsonPropertyNameAttribute가 없으면 다음으로 넘어간다.
                string name = props.Name; // JsonPropertyNameAttribute의 이름을 가져온다.
                sb.Append($"&{name}={prop.GetValue(model)}"); // 이름과 값을 추가한다.
            }
            return sb.ToString(); // 스트링 빌더를 스트링으로 변환하여 반환한다.
        }

        /// <summary>
        /// 비동기로 API 데이터를 가져온다.
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetAsync()
        {
            var httpClient = new HttpClient(); // HTTP 클라이언트를 생성한다.
            var temp = GetApiUri(11, 320, "xml", 10, 1); // API URI를 가져온다.
            var rs = Uri.TryCreate(temp, UriKind.Absolute, out Uri? uri); // URI를 생성한다.
            if (!rs) return "Uri.TryCreate() failed"; // URI 생성에 실패하면 에러 메시지를 반환한다.
            using HttpResponseMessage response = await httpClient.GetAsync(uri); // HTTP 클라이언트로 API 데이터를 가져온다.
            var readResponse = await response.Content.ReadAsStringAsync(); // API 데이터를 스트링으로 변환한다.
            return readResponse; // API 데이터를 반환한다.
        }
    }

    /// <summary>
    ///  API URI 클래스
    /// </summary>
    public class ApiUri
    {
        public string? ServiceKey { get; set; }

        [JsonPropertyName("searchYearCd")]
        public string? SearchYearCd { get; set; }

        [JsonPropertyName("siDo")]
        public int SiDo { get; set; }

        [JsonPropertyName("guGun")]
        public int GuGun { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("numOfRows")]
        public int NumOfRows { get; set; }

        [JsonPropertyName("pageNo")]
        public int PageNo { get; set; }
    }

    /// <summary>
    /// 교통사고 클래스
    /// </summary>
    public class TrafficAccident
    {
        [DisplayName("발생지역")]
        public string? Location { get; set; }

        [DisplayName("발생지점")]
        public string? Spot { get; set; }

        [DisplayName("사고건수")]
        public int Accidents { get; set; }

        [DisplayName("사망자 수")]
        public int Fatalities { get; set; } // 사망자

        [DisplayName("중상자 수")]
        public int SevereInjuries { get; set; } // 중상자

        [DisplayName("경상자 수")]
        public int MinorInjuries { get; set; } // 경상자
    }

}
