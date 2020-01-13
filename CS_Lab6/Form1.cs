using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Lab6
{
    public partial class Form1 : Form
    {
        //Company (Title)
        string title;
        Font title_font;
        Brush title_brush;
        PointF title_pen;
        Color title_color;

        //subtitle (revenue)
        string subtitle;
        Font subtitle_font;
        Brush subtitle_brush;
        PointF subtitle_pen;
        Color subtitle_color;
        Point hh;

        //Table data
        string yearStr;
        string revenueStr;
        int[] years;
        int[] revenue;
        Font table_font;
        Brush table_brush;
        PointF table_point1;
        PointF table_point2;
        Color table_color;

        //Table Lines
        Pen tableLine_pen;
        Pen tableLongLine_pen;
        Color tableLine_color;

        //Table Rectangle
        Pen rectangle_pen;
        Color rectangle_color;
        Rectangle rectangle1;
        Rectangle rectangle2;
        int table_height;

        //Graph Lines
        PointF graph_point1;
        PointF graph_point2;
        PointF graph_point3;
        Pen graphLines_pen;
        Pen graphLine_pen;
        Color graphLine_colors;
        Color graphLine_color;

        //Graph points
        Point[] GraphData_pointArray;
        PointF subTitleStr_point1;
        PointF subTitleStr_point2;
        string yearGraph;
        string revGraph;
        Brush graph_brush;
        Font graph_fontSmall;
        Font graph_fontLarge;

        //Graph bar chart (Rectangle)
        Pen barChart_pen;
        Color barChart_color;
        Rectangle[] barChart_rectangles;

        public Form1()
        {
            InitializeComponent();

            //Title (company)
            title = "ABC Company";
            title_font = new Font("Arial", 20, FontStyle.Underline);
            title_color = Color.Blue;
            title_brush = new SolidBrush(title_color);

            //subtitle (revenue)
            subtitle = "Annual Revenue";
            subtitle_font = new Font("Arial", 16, FontStyle.Underline);
            subtitle_color = Color.DarkBlue;
            subtitle_brush = new SolidBrush(subtitle_color);

            //Table Data
            yearStr = "Year";
            revenueStr = "Revenue";
            years = new int[]{ 1988,1989, 1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997};
            revenue = new int[] { 150, 170, 180, 175, 200, 250, 210, 240, 280, 140 };
            table_font = new Font("Arial", 12, FontStyle.Bold);
            table_color = Color.DarkGray;
            table_brush = new SolidBrush(subtitle_color);

            //Table Lines
            tableLine_color = Color.Black;
            tableLine_pen = new Pen(tableLine_color, 2);
            tableLongLine_pen = new Pen(tableLine_color, 4);
            tableLongLine_pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            //Table Rectangle
            rectangle_color = Color.Black;
            rectangle_pen = new Pen(rectangle_color, 2);

            //Gragh points array
            GraphData_pointArray = new Point[years.Length];
            graphLine_colors = Color.Blue;
            graphLines_pen = new Pen(graphLine_colors, 2);
            graphLine_color = Color.FromArgb(34, 65, 86);
            graphLine_pen = new Pen(graphLine_color, 3);
            graph_brush = new SolidBrush(graphLine_colors);
            graph_fontSmall = new Font("Arial", 10, FontStyle.Regular);
            graph_fontLarge = new Font("Arial", 14, FontStyle.Bold);
            graph_brush = new SolidBrush(graphLine_colors);


            //Graph bar chart (Rectangle)
            barChart_rectangles = new Rectangle[years.Length];
            barChart_color = Color.Blue;
            barChart_pen = new Pen(barChart_color, 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DisplayText();
            DisplayTableData();
            DisplayLine();
            DisplayRectangle();
            DisplayGLine();
            DrawGraph();
            GraphText();
            DisplayBarChartRectangle();
            DisplayGLine();
            DrawGraph();
        }
        public void DisplayText()
        {
            //Display Title and Subtitle (company and revenue)
            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            var screen_comp = (this.Width - g.MeasureString(title, title_font).Width)/2;
            var screen_rev = (this.Width - g.MeasureString(subtitle, subtitle_font).Width) / 2;

            title_pen = new PointF(screen_comp, 20);
            subtitle_pen = new PointF(screen_rev, 60);
            
            g.DrawString(title, title_font, title_brush, title_pen);
            g.DrawString(subtitle, subtitle_font, subtitle_brush, subtitle_pen);
            
            
        }

        public void DisplayTableData()
        {
            //Display data in table (Year and revenue)
            Graphics g = this.CreateGraphics();
            var screen_table1 = 1090;
            var screen_table2 = screen_table1 + 130;
            int h = 150;

            table_point1 = new PointF(screen_table1, h);
            table_point2 = new PointF(screen_table2 - 20, h);
            g.DrawString(yearStr, table_font, table_brush, table_point1);
            g.DrawString(revenueStr, table_font, table_brush, table_point2);

            for (int i = 0; i < years.Length; i++)
            {
                h += 50;
                table_point1 = new PointF(screen_table1, h);
                table_point2 = new PointF(screen_table2, h);

                string strYear = years[i].ToString();
                g.DrawString(strYear, table_font, table_brush, table_point1);

                string strRevenue = revenue[i].ToString();
                g.DrawString(strRevenue, table_font, table_brush, table_point2);
            }
        }
        public void DisplayLine()
        {
            //Display table lines
            Graphics ge = this.CreateGraphics();
            var p1_tableLine = 1050;
            var p2_tableLine = p1_tableLine + 250;

            int h = 140;
            for (int i = 0; i < years.Length+1; i++)
            {
                h += 50;
                table_point1 = new PointF(p1_tableLine, h);
                table_point2 = new PointF(p2_tableLine, h);
                ge.DrawLine(tableLine_pen, table_point1, table_point2);
            }
            table_height = 50 * (years.Length + 1);
        }

        public new void DisplayRectangle()
        {
            //Display table frame (rectangle)
            Graphics g = CreateGraphics();
            var p1_tableLine = 1050;
            rectangle1 = new Rectangle(p1_tableLine,140, 250, table_height);
            rectangle2 = new Rectangle(p1_tableLine, 140, 125, table_height);
            g.DrawRectangle(rectangle_pen, rectangle1);
            g.DrawRectangle(rectangle_pen, rectangle2);
        }

        public void DisplayGLine()
        {
            //Draw graph horizontal and verticle long lines
            Graphics g = this.CreateGraphics();
            graph_point1 = new PointF(200, 640);
            graph_point2 = new PointF(200, 200);
            graph_point3 = new PointF(640, 640);
            g.DrawLine(tableLongLine_pen, graph_point1, graph_point2);
            g.DrawLine(tableLongLine_pen, graph_point1, graph_point3);


            int h = 640;
            int revNum = 0;
            //int v = 40;
            for (int i = 0; i < (years.Length); i++)
            {
                h -= 40;
                revNum += 35;
                //Vertical small lines
                table_point1 = new PointF(195, h);
                table_point2 = new PointF(205, h);
                g.DrawLine(tableLine_pen, table_point1, table_point2);

                //Horizontal small lines
                table_point1 = new PointF(h, 635);
                table_point2 = new PointF(h, 645);
                g.DrawLine(tableLine_pen, table_point1, table_point2);

                int m = h - 10;
                int n = h - 15;
                //Horizontal Vertical years and revenue
                subTitleStr_point1 = new PointF(160, m);
                subTitleStr_point2 = new PointF(n, 655);
                revGraph = revNum.ToString();
                yearGraph = years[years.Length-1-i].ToString();
                g.DrawString(revGraph, graph_fontSmall, graph_brush, subTitleStr_point1);
                g.DrawString(yearGraph, graph_fontSmall, graph_brush, subTitleStr_point2);
            }
        }
        public void DrawGraph()
        {
            int xAxis = 0;
            //fill the graph array with points
            for (int i = 0; i < years.Length; i++)
            {
                xAxis += 40;
                GraphData_pointArray[i].X = 200 + xAxis;
                GraphData_pointArray[i].Y = 600 - (int)(revenue[i]);
            }

            //draw the graph connected points
            Graphics g = this.CreateGraphics();
            g.DrawLines(graphLine_pen, GraphData_pointArray);
        }

        public void GraphText()
        {
            Graphics g = this.CreateGraphics();
            g.DrawString(revenueStr, graph_fontLarge, graph_brush, 160, 170);
            g.DrawString(yearStr, graph_fontLarge, graph_brush, 660, 630);
        }

        public void DisplayBarChartRectangle()
        {
            //Display barchart frame (rectangle)
            Graphics g = CreateGraphics();
            Point[] startPoint = new Point[years.Length];


            for (int i = 0; i < years.Length; i++)
            {
                startPoint[i].X = GraphData_pointArray[i].X - 20;
                startPoint[i].Y = GraphData_pointArray[i].Y;
                int barHeight = 640 - startPoint[i].Y;
                int check = i % 2;
                Color ChangeColor = Color.Beige;

                switch (check)
                {
                    case (0):
                        ChangeColor = Color.FromArgb(242, 108, 82);
                        break;
                    case (1):
                        ChangeColor = Color.FromArgb(0, 110, 185);
                        break;
                }

                HatchStyle barHatch = HatchStyle.BackwardDiagonal;
                Brush br = new HatchBrush(barHatch, ChangeColor, ChangeColor);
                barChart_rectangles[i] = new Rectangle(startPoint[i].X, startPoint[i].Y, 35, barHeight);
                //g.DrawRectangle(barChart_pen, barChart_rectangles[i]);
                g.FillRectangle(br, barChart_rectangles[i]);

            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //Change the graph line colors win pressing (shift + B) or (shift + G) or (shift + R)
            switch ((int)e.KeyChar)
            {
                case 2: //Blue
                    graphLine_color = Color.Blue;
                    break;
                case 7: //Green
                    graphLine_color = Color.Green;
                    break;
                case 18: //Red
                    graphLine_color = Color.Red;
                    break;
            }
            Pen gColor = new Pen(graphLine_color, 3);
            graphLine_pen = gColor;
            Invalidate();
        }
    }
}
