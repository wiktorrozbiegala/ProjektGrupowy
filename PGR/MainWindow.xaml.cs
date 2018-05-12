using PGR.Database;
using PGR.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PGR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<X> MyList
        {
            get { return new FirebaseConnection().GetSingleSessionMeas("-LB8e_hc2EbEWa3uDr3d").LastOrDefault().ToParams(); }
            set { }
        }

        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = this;
        }
        
        
    }
}
