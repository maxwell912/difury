using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace difury
{
    public partial class Form2 : Form
    {
        Decimal p = 2;

        public Form2()
        {
            InitializeComponent();

            this.numericUpDown1.Increment = (Decimal)0.1;
            this.numericUpDown1.Value = 2;
            this.numericUpDown1.Maximum = (Decimal)12.9;

            DrawLines();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            foreach (var series in this.chart1.Series)
                series.Points.Clear();

            var chartArea = this.chart1.ChartAreas[0];

            chartArea.CursorX.SetCursorPixelPosition(new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y), true);
            chartArea.CursorY.SetCursorPixelPosition(new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y), true);

            double pX = chartArea.CursorX.Position;
            double pY = chartArea.CursorY.Position;

            DrawLine((decimal)pX, (decimal)pY, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawLines();
        }

        private void DrawLines()
        {
            foreach (var series in this.chart1.Series)
                series.Points.Clear();

            var startPoints = new List<(Decimal, Decimal)>() { (1, 2), (2, 1), (0, 1), (1, 0) };
            var idx = 0;

            foreach (var stPoint in startPoints)
            {
                DrawLine(stPoint.Item1, stPoint.Item2, idx);
                idx++;
            }
        }

        private void DrawLine(decimal x, decimal y, int serIdx)
        {
            var count = 0;
            var rk = new Rub_Kutt();

            foreach (var point in rk.GetPoints(x, y, p, (Decimal)0.1))
            {
                this.chart1.Series[serIdx].Points.AddXY(point.X, point.Y);
                count++;
                if (count > 100) break;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            p = this.numericUpDown1.Value;
        }
    }
}