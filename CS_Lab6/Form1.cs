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
            graphLine_color = Color.Blue;
            graphLine_pen = new Pen(graphLine_color, 3);
            graph_brush = new SolidBrush(graphLine_colors);
            graph_fontSmall = new Font("Arial", 10, FontStyle.Bold);
            graph_fontLarge = new Font("Arial", 14, FontStyle.Bold);
            graph_brush = new SolidBrush(graphLine_colors);

            //Graph bar chart (Rectangle)
            barChart_rectangles = new Rectangle[years.Length];
            barChart_color = Color.Red;
            barChart_pen = new Pen(barChart_color, 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Title Subtitle and Table
            DisplayTitle();       //to display title and subtitle on the middle of the top
            DisplayTableData();   //to display the years and revenue data on the right side
            DisplayTableLines();  //to display table horizontal lines
            DisplayRectangle();   //to display table boundry

            //Graph
            FillGraphDataArray();       //to fill the years and revenue in an array of points as x,y values
            GraphText();                //to dispaly years and revenue words on the (x and y) axes
            DisplayBarChartRectangle(); //to display the graph as a bar chart 
            DisplayGraphLine();         //display all the lines of the graph x-axis and y-axis
            DrawGraph();                //connect the graph points from the points array
        }
        public void DisplayTitle()
        {
            //Display Title and Subtitle (company and revenue)
            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            var title_X_Axis = (this.Width - g.MeasureString(title, title_font).Width)/2;
            var subitle_X_Axis = (this.Width - g.MeasureString(subtitle, subtitle_font).Width) / 2;

            title_pen = new PointF(title_X_Axis, 20);
            subtitle_pen = new PointF(subitle_X_Axis, 60);
            
            g.DrawString(title, title_font, title_brush, title_pen);
            g.DrawString(subtitle, subtitle_font, subtitle_brush, subtitle_pen);
        }

        public void DisplayTableData()
        {
            //Display data in table (Year and revenue)
            Graphics g = this.CreateGraphics();
            var X_axisForYears = 1090;
            var X_axisForRevenue = X_axisForYears + 130;
            int h = 150;

            table_point1 = new PointF(X_axisForYears, h);
            table_point2 = new PointF(X_axisForRevenue - 20, h);
            g.DrawString(yearStr, table_font, table_brush, table_point1);
            g.DrawString(revenueStr, table_font, table_brush, table_point2);

            for (int i = 0; i < years.Length; i++)
            {
                h += 50;
                table_point1 = new PointF(X_axisForYears, h);
                table_point2 = new PointF(X_axisForRevenue, h);

                string strYear = years[i].ToString();
                g.DrawString(strYear, table_font, table_brush, table_point1);

                string strRevenue = revenue[i].ToString();
                g.DrawString(strRevenue, table_font, table_brush, table_point2);
            }
        }
        public void DisplayTableLines()
        {//tableLineStart
            //Display table lines
            Graphics g = this.CreateGraphics();
            var startHorizontalLine = 1050;
            var endtHorizontalLine = startHorizontalLine + 250;

            int h = 140;
            for (int i = 0; i < years.Length+1; i++)
            {
                h += 50;
                table_point1 = new PointF(startHorizontalLine, h);
                table_point2 = new PointF(endtHorizontalLine, h);
                g.DrawLine(tableLine_pen, table_point1, table_point2);
            }
        }

        public new void DisplayRectangle()
        {
            //Display table frame (rectangle)
            Graphics g = CreateGraphics();
            var p1_tableLine = 1050;
            table_height = 50 * (years.Length + 1);
            rectangle1 = new Rectangle(p1_tableLine,140, 250, table_height);
            rectangle2 = new Rectangle(p1_tableLine, 140, 125, table_height);
            g.DrawRectangle(rectangle_pen, rectangle1);
            g.DrawRectangle(rectangle_pen, rectangle2);
        }

        public void DisplayGraphLine()
        {
            //Draw graph horizontal and verticle long lines
            Graphics g = this.CreateGraphics();
            graph_point1 = new PointF(200, 640);
            graph_point2 = new PointF(200, 200);
            graph_point3 = new PointF(640, 640);
            g.DrawLine(tableLongLine_pen, graph_point1, graph_point2);
            g.DrawLine(tableLongLine_pen, graph_point1, graph_point3);

            int origin_Y = 640;
            int revenueValues = 0;
            for (int i = 0; i < (years.Length); i++)
            {
                origin_Y -= 40;
                revenueValues += 35;
                //Vertical small lines
                table_point1 = new PointF(195, origin_Y);
                table_point2 = new PointF(205, origin_Y);
                g.DrawLine(tableLine_pen, table_point1, table_point2);

                //Horizontal small lines
                table_point1 = new PointF(origin_Y, 635);
                table_point2 = new PointF(origin_Y, 645);
                g.DrawLine(tableLine_pen, table_point1, table_point2);

                int revenue_Y = origin_Y - 10;
                int Year_X = origin_Y - 15;
                //Horizontal & Vertical (years and revenue) words
                subTitleStr_point1 = new PointF(160, revenue_Y);
                subTitleStr_point2 = new PointF(Year_X, 655);
                revGraph = revenueValues.ToString();
                yearGraph = years[years.Length-1-i].ToString();
                g.DrawString(revGraph, graph_fontSmall, graph_brush, subTitleStr_point1);
                g.DrawString(yearGraph, graph_fontSmall, graph_brush, subTitleStr_point2);
            }
        }

        public void FillGraphDataArray()
        {
            int x_axis = 0;
            //fill the graph array with points
            for (int i = 0; i < years.Length; i++)
            {
                x_axis += 40;
                GraphData_pointArray[i].X = 200 + x_axis;
                GraphData_pointArray[i].Y = 600 - (int)(revenue[i]);
            }
        }

        public void DrawGraph()
        {
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
                startPoint[i].X = GraphData_pointArray[i].X - 15;
                startPoint[i].Y = GraphData_pointArray[i].Y;
                int barHeight = 640 - startPoint[i].Y;

                HatchStyle barHatch = HatchStyle.ForwardDiagonal;
                Brush graph2ndBrush = new HatchBrush(barHatch, Color.Aqua, Color.Red);
                barChart_rectangles[i] = new Rectangle(startPoint[i].X, startPoint[i].Y, 30, barHeight);
                g.DrawRectangle(barChart_pen, barChart_rectangles[i]);
                g.FillRectangle(graph2ndBrush, barChart_rectangles[i]);
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //Change the graph line colors win pressing (shift + B) or (shift + G) or (shift + R)
            switch ((int)e.KeyChar)
            {
                case 2: //B char
                    graphLine_color = Color.Blue;
                    break;
                case 7: //G char
                    graphLine_color = Color.Green;
                    break;
                case 18: //R char
                    graphLine_color = Color.Red;
                    break;
            }
            Pen graph2ndColor = new Pen(graphLine_color, 3);
            graphLine_pen = graph2ndColor;
            Invalidate();
        }
    }
}
