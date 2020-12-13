using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CCLForWPF;

namespace WPFDemoForCCLForWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class BarChartValue
        {
            public double YValue { get; set; }
            public string XLabel { get; set; }
            
            public BarChartValue(string xlabel, double yvalue)
            {
                YValue = yvalue;
                XLabel = xlabel;
            } 
        }

        public MainWindow()
        {
            InitializeComponent();
            BarChart bc = new BarChart();
            bc.Width = 500;
            bc.Height = 400;
            bc.Title = "Test Title";
            bc.TitleFontSize = 20;
            bc.TitleFontBrush = Brushes.Black;
            bc.DynamicallyCalculatedYAxisLength = true;
            bc.DynamicallyCalculatedMaximumDataValue = false;
            bc.DynamicallyCalculatedMinimumDataValue = false;
            bc.DynamicBarColors = true;
            bc.XLabelsPropertyName = "XLabel";
            bc.YValuesPropertyName = "YValue";
            bc.NumberOfMarksY = 10;
            bc.WidthOfMarksY = 5;
            bc.MinDataValue = 0;
            bc.MaxDataValue = 100;
            bc.FullMarksLines = true;
            bc.BarWidth = 100;
            bc.Data = new List<object>() { new BarChartValue("Cow", 23.5), new BarChartValue("Dog", 53.8) };
            Canvas canvas = (Canvas)bc.DrawBarChart(Globals.ChartType.Canvas);
            myGrid.Children.Add(canvas);
            Canvas.SetLeft(canvas, 250);
            Canvas.SetTop(canvas, 200);
        }
    }
}
