using PGRForms.Database;
using PGRForms.Utils;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static PGRForms.Database.FirebaseConnection;

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

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;

        public void UpdateDataGrid()
        {
            if (Application.OpenForms.Count != 0)
            {
                SafeClear();
                foreach (var item in data)
                {
                    SafeAdd(item);
                }
            }
        }

        public void FillDataGrid()
        {
            foreach (var item in data)
            {
                listView1.Items.Add(new ListViewItem(new string[] { item.Param, item.Value }));
            }

        }

        public void SafeClear()
        {
            jebacTo = listView1.Items.Clear;
            listView1.Invoke(jebacTo);
        }

        public void SafeAdd(X item)
        {
            jebacTo = () => listView1.Items.Add(new ListViewItem(new string[] { item.Param, item.Value }));
            listView1.Invoke(jebacTo);
        }

        public void Init()
        {
            //connection.SetAction(UpdateDataGrid, "-LB8eNjME3_jibkajhcw", FirebaseAction.OnChange);
        }

        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ObservableCollection<X> data => connection.GetSingleSessionMeas("-LB8eNjME3_jibkajhcw").LastOrDefault().ToParams();
        private FirebaseConnection connection = new FirebaseConnection();

        public delegate void myDel();
        public myDel jebacTo;
    }
}

