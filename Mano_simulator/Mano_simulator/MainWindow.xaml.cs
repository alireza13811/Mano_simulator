using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mano_simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowState = WindowState.Maximized;
            
            InitializeComponent();
            // create a for loop to add rows to data grid
            for (int i = 0; i < 2048; i++)
            {
                DataGridData data = new DataGridData();
                data.Address = "0x"+i.ToString("X");
                data.Value = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0";
                Datamemmory.Items.Add(data);
                
            }
            for (int i = 0; i < 128; i++)
            {
                DataGridData data = new DataGridData();
                data.Address = "0x" + i.ToString("X");
                data.Value = "";
                Microprogrammemmory.Items.Add(data);

            }

        }
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {

            Datamemmory.Items.Add("nima");
        }

    }

    // create a classs that will hold the data for the data grid
    public class DataGridData
    {
        public string Address { get; set; }
        public string Value { get; set; }
        public DataGridData()
        {

        }


    }
}
