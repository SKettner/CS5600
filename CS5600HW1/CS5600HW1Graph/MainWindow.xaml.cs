using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows;
using System.Windows.Media;

namespace CS5600HW1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Define the series for the chart
            var series = new LineSeries
            {
                Title = "f(x) = -12 - 21x + 18x² - 2.75x³",
                Values = new ChartValues<double>(),
                Fill = Brushes.Transparent // This removes the fill under the line
            };

            // Calculate f(x) for a range of x values and add to the series
            for (double x = 2.17; x <= 2.25; x += .0001)
            {
                double y = CalculateFunction(x);
                series.Values.Add(y);
            }

            // Add the series to the chart
            cartesianChart.Series = new SeriesCollection { series };

            // Configure the axes
            cartesianChart.AxisX.Add(new Axis
            {
                Title = "X",
                Labels = null
            });

            cartesianChart.AxisY.Add(new Axis
            {
                Title = "f(x)",
                LabelFormatter = value => value.ToString("N")
            });
        }

        // Method to calculate f(x)
        private double CalculateFunction(double x)
        {
            return -12 - 21 * x + 18 * Math.Pow(x, 2) - 2.75 * Math.Pow(x, 3);
        }
    }
}