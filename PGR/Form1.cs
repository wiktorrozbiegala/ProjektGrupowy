using PGRForms.Database;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PGRForms
{
    public partial class Form1 : Form
    {         
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(881, 496);
            this.tabControl1.TabIndex = 3;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(905, 520);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FirebaseConnection database = new FirebaseConnection();            

            var sessionsNames = database.GetAllSessionsMeas().Keys;

            foreach (var session in sessionsNames)
            {
                this.tabControl1.Controls.Add(CreateNewTab(session));
                UpdateTab(session);
            }
            
        }

        private void UpdateTab(string sessionName)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    UpdateDataGrid(sessionName);
                    UpdateChart(sessionName, Param.SNR);
                    UpdateChart(sessionName, Param.SignalStrengthdBm);
                    Thread.Sleep(1000);
                }
            }).Start();
        }

        private TabPage CreateNewTab(string sessionName)
        {
            var tab = new TabPage()
            {
                Location = new System.Drawing.Point(4, 22),
                Name = $"tab{sessionName}",
                Padding = new System.Windows.Forms.Padding(3),
                Size = new System.Drawing.Size(873, 470),
                //TabIndex = 0,
                Text = sessionName,
                UseVisualStyleBackColor = true,
            };
            tab.Controls.Add(CreateListView(sessionName));
            tab.Controls.AddRange(CreateCharts(sessionName, new List<CustomChart>()
            {
                new CustomChart { Name = Param.SNR, TitleOX = "[t]", TitleOY = "[dB]" },
                new CustomChart { Name = Param.SignalStrengthdBm, TitleOX = "[t]", TitleOY = "[dBm]" }
            }).ToArray());

            return tab;
        }

        private ListViewDoubleBuffered CreateListView(string name)
        {
            var list = new ListViewDoubleBuffered()
            {
                Location = new System.Drawing.Point(6, 12),
                Name = $"listView{name}",
                Size = new System.Drawing.Size(197, 264),
                TabIndex = 2,
                UseCompatibleStateImageBehavior = false,
                View = System.Windows.Forms.View.Details
            };
            list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            new ColumnHeader(){Text = UIConsts.ColumnHeaderText1,Width = 106},
            new ColumnHeader(){Text = UIConsts.ColumnHeaderText2,Width = 81}});

            return list;
        }

        private List<Chart> CreateCharts(string sessionName, List<CustomChart> charts)
        {
            var ret = new List<Chart>();
            for (int i = 0; i < charts.Count; i++)
            {

                var chartArea = new ChartArea()
                {
                    Name = $"chartArea{charts[i].Name}{sessionName}"
                };
                chartArea.AxisX.Title = charts[i].TitleOX;
                chartArea.AxisY.Title = charts[i].TitleOY;


                var chart = new Chart()
                {
                    Location = new System.Drawing.Point(497, 220 * i + 10),
                    Name = $"chart{charts[i].Name}{sessionName}"
                };

                chart.ChartAreas.Add(chartArea);

                var series = new Series()
                {
                    BorderWidth = 3,
                    ChartArea = $"chartArea{charts[i].Name}{sessionName}",
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                    Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))),
                    Name = $"series{charts[i].Name}{sessionName}"
                };

                chart.Series.Add(series);
                chart.Size = new System.Drawing.Size(313, 215);
                chart.TabIndex = 0;

                var title = new Title()
                {
                    Name = $"title{charts[i].Name}{sessionName}",
                    Text = charts[i].Name.ToString()
                };
                chart.Titles.Add(title);

                ret.Add(chart);
            }

            return ret;
        }
    }

    public class CustomChart
    {
        public Param Name { get; set; }
        public string TitleOX { get; set; }
        public string TitleOY { get; set; }
    }

    public static class UIConsts
    {
        public const string ColumnHeaderText1 = "Parametr";
        public const string ColumnHeaderText2 = "Wartość";
    }
}
