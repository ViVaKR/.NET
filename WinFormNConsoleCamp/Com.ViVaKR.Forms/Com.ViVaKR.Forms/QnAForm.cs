using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Com.ViVaKR.Forms
{
    public partial class Form1 : Form
    {
        private bool StatusMonitoring { get; set; }

        public Form1()
        {
            InitializeComponent();

            var button = new Button
            {
                Dock = DockStyle.Bottom,
                Text = "Run"
            };
            Controls.Add(button);

            button.Click += (s, e) =>
            {
                // 테스트 시작
                StatusMonitoring = new Random().Next(0, 100) % 2 == 0;

                // 1. 트리거
                _ = Trigger(new Random().Next(0, typeof(CompareProgressStatus).GetEnumNames().Length));
            };
        }

        /// <summary>
        /// 상태바 및 라벨 컨트롤 처리
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private async Task UpdateProgressBarAsync(int value, Color color, CompareProgressStatus status)
        {
            await Task.Run(() =>
            {
                BeginInvoke(new Action(() =>
                {
                    progressBar1.Value = value;
                    labelStatusMessage.BackColor = color;
                    labelStatusMessage.Text = $"Procese Status => {status} ";
                }));

            });
        }

        // 테스트 용 
        private int CalculateProgressBarCurrentValue() => new Random().Next(0, 100);


        private async Task Trigger(int manager)
        {
            // 2.테스트 시작
            await Task.Run(() => RunAsync((CompareProgressStatus)manager));
        }

        /// <summary>
        /// 문의에 대한 답변
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task RunAsync(CompareProgressStatus status)
        {
            while (StatusMonitoring)
            {
                try
                {
                    switch (status)
                    {
                        case CompareProgressStatus.Waiting: await UpdateProgressBarAsync(0, Color.LightSteelBlue, status);break;
                        case CompareProgressStatus.Progressing: await UpdateProgressBarAsync(CalculateProgressBarCurrentValue(), Color.LightYellow, status);break;
                        case CompareProgressStatus.Done: await UpdateProgressBarAsync(100, Color.DarkSeaGreen, status); break;
                        case CompareProgressStatus.Cancel: await UpdateProgressBarAsync(0, Color.Thistle,status); break;
                        case CompareProgressStatus.ProcessCanNotAccessTheFileError:
                        case CompareProgressStatus.IncorrectFileError:
                        case CompareProgressStatus.IncorrectEncodingError:
                        case CompareProgressStatus.NoColumnDataInFileError:
                        case CompareProgressStatus.NoMeasureDataInFileError:
                        case CompareProgressStatus.CalculationError:
                        case CompareProgressStatus.EtcError: await UpdateProgressBarAsync(0, Color.LightCoral, status); break;
                        default: await UpdateProgressBarAsync(0, Color.LightGray,status); break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message + Environment.NewLine);
                    StatusMonitoring = false;
                }
                await Task.Delay(1000);
            }
        }

        enum CompareProgressStatus
        {
            ProcessCanNotAccessTheFileError,
            IncorrectFileError,
            IncorrectEncodingError,
            NoColumnDataInFileError,
            NoMeasureDataInFileError,
            CalculationError,
            EtcError,
            Waiting,
            Progressing,
            Cancel,
            Done
        }
    }
}
