﻿using PGRForms.Database;
using PGRForms.Measurement;
using PGRForms.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PGRForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void UpdateDataGrid(string sessionName)
        {
            if (this.IsHandleCreated)
            {
                var sessionAllMeasurements = connection.GetSingleSessionMeas(sessionName);
                var sessionLastMeasurement = sessionAllMeasurements.LastOrDefault().Value.ToParams();
                
                var sessionLastListViewData = new List<ListViewItem>();
                foreach (var item in sessionLastMeasurement)
                {
                    sessionLastListViewData.Add(new ListViewItem(new string[] { item.Param, item.Value }));
                }                

                // update list view with last measurement
                var control = (ListView)this.Controls.Find($"listView{sessionName}Last", true).FirstOrDefault();
                jebacTo = ()=> {
                    control.BeginUpdate();
                    control.Items.Clear();
                    control.Items.AddRange(sessionLastListViewData.ToArray());
                    control.EndUpdate();
                };
                control.Invoke(jebacTo);


                var listOfAllMeasurements = new List<BaseMeasurement>(sessionAllMeasurements.Values);
                var sessionAvgMeasurement = listOfAllMeasurements.CalculateAvg().ToParams();

                sessionLastListViewData = new List<ListViewItem>();
                foreach (var item in sessionAvgMeasurement)
                {
                    sessionLastListViewData.Add(new ListViewItem(new string[] { item.Param, item.Value }));
                }

                // update list view with avg measurements
                control = (ListView)this.Controls.Find($"listView{sessionName}Avg", true).FirstOrDefault();
                jebacTo = () => {
                    control.BeginUpdate();
                    control.Items.Clear();
                    control.Items.AddRange(sessionLastListViewData.ToArray());
                    control.EndUpdate();
                };
                control.Invoke(jebacTo);

            }
        }

        public void UpdateChart(string sessionName)
        {
            if (this.IsHandleCreated)
            {
                // get current param to display
                AvgParam selectedParam;
                var stringFromComboBox = "";

                jebacTo = () =>
                {
                    stringFromComboBox = this.Controls.Find("comboBox1", true).FirstOrDefault().Text;
                };
                this.Invoke(jebacTo);

                Enum.TryParse(stringFromComboBox, out selectedParam);


                // get data for current param
                var temp = connection.GetSingleSessionMeas(sessionName).Select(x => x.Value).ToList();
                var data = new DataCollector(temp).RetrieveData(selectedParam);

                var chart = CustomChartCreator.Create(selectedParam);

                // try to find chart and update its data
                var control = (Chart)this.Controls.Find($"chart{sessionName}", true).FirstOrDefault();
                jebacTo = () => 
                {
                    control.Titles[0].Name = selectedParam.ToString();
                    control.ChartAreas[0].AxisX.Title = chart.TitleOX;
                    control.ChartAreas[0].AxisY.Title = chart.TitleOY;
                    control.Series[0].Points.DataBindXY(Enumerable.Range(1, data.Count).ToList(), data);
                };
                control.Invoke(jebacTo);
            }
        }
        //private void Chart_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Point mousePoint = new Point(e.X, e.Y);
        //    foreach (var chart in charts...)
        //    {

        //    }
        //    chart.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
        //    chart.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);
        //}

        //public void Init()
        //{
        //    connection.SetAction(UpdateDataGrid, "-LB8eNjME3_jibkajhcw", FirebaseAction.OnChange);
        //}
        private FirebaseConnection connection = new FirebaseConnection();

        public delegate void myDel();
        public myDel jebacTo;
        private TabControl tabControl1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem loadSessionToolStripMenuItem;

        [ToolboxItem(true)]
        [ToolboxBitmap(typeof(ListView))]
        public class ListViewDoubleBuffered : ListView
        {
            public ListViewDoubleBuffered()
            {
                this.DoubleBuffered = true;
            }
        }
    }
}

