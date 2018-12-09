using PGRForms.Database;
using PGRForms.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                var temp = connection.GetSingleSessionMeas(sessionName).LastOrDefault().ToParams();
                var tempList = new List<ListViewItem>();
                foreach (var item in temp)
                {
                    tempList.Add(new ListViewItem(new string[] { item.Param, item.Value }));
                }
                var temppp = tempList.ToArray();
                var control = (ListView)this.Controls.Find($"listView{sessionName}", true).FirstOrDefault();
                jebacTo = ()=> {
                    control.BeginUpdate();
                    control.Items.Clear();
                    control.Items.AddRange(temppp);
                    control.EndUpdate();
                };
                control.Invoke(jebacTo);
            }
        }

        public void UpdateChart(string sessionName, Param chartType)
        {
            if (this.IsHandleCreated)
            {
                var temp = connection.GetSingleSessionMeas(sessionName);
                var data = new DataCollector(temp).RetrieveData(chartType);
                var control = (Chart)this.Controls.Find($"chart{chartType}{sessionName}", true).FirstOrDefault();
                jebacTo = () => 
                {
                    control.Series[0].Points.DataBindXY(Enumerable.Range(1, data.Count).ToList(), data);
                };
                control.Invoke(jebacTo);
            }
        }

        //public void Init()
        //{
        //    connection.SetAction(UpdateDataGrid, "-LB8eNjME3_jibkajhcw", FirebaseAction.OnChange);
        //}
        private ObservableCollection<X> data => connection.GetSingleSessionMeas("-LB8eNjME3_jibkajhcw").LastOrDefault().ToParams();
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

