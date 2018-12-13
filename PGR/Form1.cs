using PGRForms.Database;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using PGRForms.Measurement;
using System.Linq;
using PGRForms.Utils;

namespace PGRForms
{
    public partial class Form1 : Form
    {
        private FirebaseConnection _database = new FirebaseConnection();

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(12, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(881, 477);
            this.tabControl1.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(905, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSessionToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // loadSessionToolStripMenuItem
            // 
            this.loadSessionToolStripMenuItem.Name = "loadSessionToolStripMenuItem";
            this.loadSessionToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.loadSessionToolStripMenuItem.Text = "Load Session";
            this.loadSessionToolStripMenuItem.Click += new System.EventHandler(this.loadSessionToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(905, 520);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            // if we want to get session names:
            var sessionsNames = _database.GetAllSessionsMeas().Keys;
        }

        private void UpdateTab(string sessionName)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    UpdateDataGrid($"{sessionName}");
                    UpdateChart(sessionName);
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

            // last measurement list view
            tab.Controls.Add(CreateListView($"{sessionName}Last", new Point(6, 50), new Size(197, 200)));
            // avg measurement list view
            tab.Controls.Add(CreateListView($"{sessionName}Avg", new Point(6, 300), new Size(197, 120)));

            tab.Controls.Add(CreateComboBox());


            tab.Controls.Add(CreateChart(sessionName));

            return tab;
        }

        private ComboBox CreateComboBox()
        {
            var xd = new List<AvgParam>
            {
                AvgParam.AsuLevel,
                AvgParam.CQI,
                AvgParam.RSRP,
                AvgParam.RSRQ,
                AvgParam.SNR
            };

            var comboBox = new ComboBox
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(397, 54),
                Name = "comboBox1",
                Size = new System.Drawing.Size(121, 21),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            xd.ForEach(x => comboBox.Items.Add(x));
            comboBox.SelectedIndex = UIConsts.DefaultComboBoxIndex;
            return comboBox;
        }

        private ListViewDoubleBuffered CreateListView(string name, Point location, Size size)
        {
            var list = new ListViewDoubleBuffered()
            {
                Location = location, //new System.Drawing.Point(6, 12),
                Name = $"listView{name}",
                Size = size, // new System.Drawing.Size(197, 264),
                TabIndex = 2,
                UseCompatibleStateImageBehavior = false,
                View = System.Windows.Forms.View.Details
            };
            list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            new ColumnHeader(){Text = UIConsts.ColumnHeaderText1,Width = 106},
            new ColumnHeader(){Text = UIConsts.ColumnHeaderText2,Width = 81}});

            return list;
        }

        private Chart CreateChart(string sessionName)
        {
            var chart = new Chart()
            {
                Location = new System.Drawing.Point(300, 180),
                Name = $"chart{sessionName}"
            };
            chart.Size = new Size(400, 200);
            chart.TabIndex = 0;

            // title
            var title = new Title()
            {
                Alignment = ContentAlignment.TopCenter,
                Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold)
            };

            // area
            var chartArea = new ChartArea()
            {
                Name = $"chartArea{sessionName}",
            };            
            chartArea.AxisX.LabelStyle.Enabled = false;
            chartArea.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisX.ArrowStyle = AxisArrowStyle.Triangle;
            chartArea.AxisY.ArrowStyle = AxisArrowStyle.Triangle;
            chartArea.AxisX.TitleFont = new Font(FontFamily.GenericSansSerif, 12);
            chartArea.AxisY.TitleFont = new Font(FontFamily.GenericSansSerif, 10);

            // series
            var series = new Series()
            {
                BorderWidth = 2,
                ChartArea = $"chartArea{sessionName}",
                ChartType = SeriesChartType.Line,
                Color = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))),
                Name = $"series{sessionName}",
                //Color = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))))
            };


            chart.Titles.Add(title);
            chart.ChartAreas.Add(chartArea);
            chart.Series.Add(series);

            return chart;
        }
        

        private void loadSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopupForm popup = new PopupForm
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                var sessionName = popup.GetSesionName();
                if (String.IsNullOrEmpty(sessionName))
                {
                    MessageBox.Show("Please provide session name!");
                    return;
                }

                var sessionData = _database.GetSingleSessionMeas(sessionName);
                if (sessionData == null)
                {
                    MessageBox.Show($"Could not find any data for session name: {sessionName}!");
                    return;
                }

                tabControl1.Controls.Add(CreateNewTab(sessionName));
                UpdateTab(sessionName);
            }
            else if (dialogresult == DialogResult.Cancel)
            {
                Console.WriteLine("You clicked either Cancel or X button in the top right corner");
            }
            popup.Dispose();
        }
    }

    
    public class CustomChart
    {
        public AvgParam Name { get; set; }
        public string TitleOX { get; set; }
        public string TitleOY { get; set; }
    }

    public static class UIConsts
    {
        public const string ColumnHeaderText1 = "Parametr";
        public const string ColumnHeaderText2 = "Wartość";
        public const int DefaultComboBoxIndex = 4;
    }

}
