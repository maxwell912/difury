using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace difury
{
    public partial class Form2 : Form
    {
        Decimal p = 2;
        int n = 1000;

        List<PointD> startPoints = new List<PointD>()
        {
            new PointD(2, 1),
            new PointD(1, 2),
            new PointD(3, 9),
        };

        decimal f(decimal x, decimal y) => 1 - x * y;
        decimal g(decimal x, decimal y) => p * y * (x - (2 / (y + 1)));

        public Form2()
        {
            InitializeComponent();

            this.numericUpDown1.Value = 13;
            this.numericUpDown1.Increment = (Decimal)0.1;

            this.startPointX.Increment = (Decimal)0.1;
            this.startPointY.Increment = (Decimal)0.1;

            this.chart1.ChartAreas[0].AxisY.Minimum = -1;
            this.chart1.ChartAreas[0].AxisX.Minimum = -1;

            DrawLines(startPoints);
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            Clear();

            var chartArea = this.chart1.ChartAreas[0];

            chartArea.CursorX.SetCursorPixelPosition(new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y), true);
            chartArea.CursorY.SetCursorPixelPosition(new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y), true);

            double pX = chartArea.CursorX.Position;
            double pY = chartArea.CursorY.Position;

            DrawLine(new PointD(pX, pY));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawLines(startPoints);
        }

        private void DrawLines(List<PointD> points)
        {
            Clear();

            foreach (var point in points)
            {
                DrawLine(point);
            }
        }

        private void DrawLine(PointD stPoint)
        {
            var idx = this.chart1.Series.Count;

            if (this.chart1.Series.IsUniqueName(stPoint.ToString()))
                this.chart1.Series.Add(stPoint.ToString());
            else return;

            var ser = this.chart1.Series[idx];

            ser.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            ser.BorderWidth += 1;

            var rk = new Rub_Kutt(f, g);

            foreach (var point in rk.GetPoints(stPoint, p, (Decimal)0.01).Take(n))
            {
                ser.Points.AddXY(point.X, point.Y);
            }
        }

        private void Clear()
        {
            this.chart1.Series.Clear();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            p = this.numericUpDown1.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrawLine(new PointD(this.startPointX.Value, this.startPointY.Value));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var idx = this.chart1.Series.Count - 1;
            if (idx > -1)
                this.chart1.Series.RemoveAt(this.chart1.Series.Count - 1);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            n = (int)this.numericUpDown2.Value;
        }
    }
}