using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace VivCam
{
    public partial class MainForm : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly UVideo uv;
        private VideoCapture capture;
        private Mat frame;
        private Bitmap image;
        public Bitmap CameraCapture
        {
            get => image;
            set
            {
                image = value;
                NotifyPropertyChanged($"{nameof(CameraCapture)} : {DateTime.Now}");
            }
        }

        private readonly Timer captureTimer;
        private readonly CascadeClassifier faceCascade;
        private bool isRunning;

        private readonly Button button;
        private readonly GroupBox box;
        private readonly string xmlPath;
        private readonly int fps = 30;

        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
            Controls.Add(uv = new UVideo());

            captureTimer = new Timer { Interval = 30 };
            captureTimer.Tick += CaptureTimer_Tick;

            Controls.Add(box = new GroupBox
            {
                Dock = DockStyle.Right,
                Width = 200
            });


            isRunning = false;
            xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", "haarcascade_frontalface_default.xml");
            faceCascade = new CascadeClassifier();

            box.Controls.Add(button = new Button
            {
                Dock = DockStyle.Top,
                Text = "대기중",
                Height = 100,
                Font = new Font("맑은 고딕", 24f, FontStyle.Bold),
                ForeColor = Color.Teal
            });
            button.Click += Button_Click;
            FormClosing += Form1_FormClosing;
        }

        private void CaptureTimer_Tick(object sender, EventArgs e)
        {
            if (!isRunning) captureTimer.Stop();
            ProcessFrame();
        }

        private void ProcessFrame()
        {
            if (capture == null || capture.IsDisposed) return;
            if (!faceCascade.Load(xmlPath)) return;

            capture.Read(frame);
            Rect[] faces = faceCascade.DetectMultiScale(frame);

            foreach (var item in faces)
            {
                Cv2.Rectangle(frame, item, Scalar.Red);
            }

            uv.Image = frame.ToBitmap();
            image = null;
            Cv2.WaitKey(1000 / fps);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;
            btn.Text = (isRunning = !isRunning) ? "캡쳐중" : "대기중";


            frame = new Mat();
            capture = new VideoCapture
            {
                Fps = fps,
                FrameWidth = ClientRectangle.Width - box.Width,
                FrameHeight = ClientRectangle.Height - box.Height
            };

            capture.Open(0);

            if (isRunning) captureTimer.Start();
            else captureTimer.Stop();
        }



        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            capture?.Dispose();
            frame?.Dispose();
            uv?.Dispose();
            image?.Dispose();
            captureTimer?.Dispose();
        }
    }
}
