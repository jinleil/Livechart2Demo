using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows;

namespace LiveChart2Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string dateFormat = "HH:mm:ss";

        public MainWindow()
        {
            InitializeComponent();

            DateTimePoint[] dateTimePoints = new DateTimePoint[200];
            var times = GetDateTimes();
            for (int i = 0; i < dateTimePoints.Length; i++)
            {
                dateTimePoints[i] = new DateTimePoint(times[i], (double)i);
            }

            SeriesCollection = new ISeries[] {
                new LineSeries<DateTimePoint>
                {
                   //设置鼠标放在数据点上 提示文本格式
                   TooltipLabelFormatter = (chartPoint) =>$"{new DateTime((long) chartPoint.SecondaryValue).ToString(dateFormat)}, {chartPoint.PrimaryValue}",

                   Values = dateTimePoints,
                }
            };

            this.DataContext = this;
        }

        public ISeries[] SeriesCollection { get; set; }

        public Axis[] XAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "X Axis",
                    Labeler = value => new DateTime((long) value).ToString(dateFormat),
                    LabelsRotation = -60,
                    TextSize=15,

                    // when using a date time type, let the library know your unit
                    UnitWidth = TimeSpan.FromMinutes(1).Ticks,

                    MinStep = TimeSpan.FromSeconds(1).Ticks
                }
            };

        public Axis[] YAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "Y Axis",
                    NamePaint = new SolidColorPaint(SKColors.Red),

                    LabelsPaint = new SolidColorPaint(SKColors.Green),
                    TextSize = 15,
                }
            };

        //测试数据
        public List<DateTime> GetDateTimes()
        {
            DateTime startTime = DateTime.Now.Date;
            List<DateTime> timePoints = new List<DateTime>();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    DateTime time = startTime.AddMinutes(j * (24 * 60 / 1000) + i * 24 * 60);
                    timePoints.Add(time);
                }
            }
            return timePoints;
        }
    }
}