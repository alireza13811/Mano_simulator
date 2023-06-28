﻿using System;
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
        Assembly assembly = new Assembly();

        public MainWindow()
        {
            WindowState = WindowState.Maximized;
            
            InitializeComponent();

            WindowState = WindowState.Maximized;
           

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
            btnRun.Visibility = Visibility.Hidden;
            btnStepover.Visibility = Visibility.Hidden;
            btnStop.Visibility = Visibility.Hidden;

        }
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void btnStepover_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnComile_Click(object sender, RoutedEventArgs e)
        {
            btnRun.Visibility = Visibility.Visible;
            string program = new TextRange(txtProgrammMemmory.Document.ContentStart, txtProgrammMemmory.Document.ContentEnd).Text;
            assembly.microprogram_first_iteration(program);
            assembly.microprogram_second_iteration(program);

            string program2 = new TextRange(txtProgramm.Document.ContentStart, txtProgramm.Document.ContentEnd).Text;
            assembly.first_iteration(program2);
            assembly.second_iteration(program2);

            // update the data grid
            for (int i = 0; i < 2048; i++)
            {
                DataGridData data = new DataGridData();
                data.Address = "0x" + i.ToString("X");
                // convert memory array to string of 16 bits
                data.Value = Convert.ToInt32(assembly.memory[i, 0]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 1]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 2]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 3]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 4]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 5]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 6]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 7]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 8]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 9]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 10]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 11]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 12]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 13]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 14]).ToString() + " " +
                    Convert.ToInt32(assembly.memory[i, 15]).ToString();

                Datamemmory.Items[i] = data;

            }

            // update the microprogram data grid
            for (int i = 0; i < 128; i++)
            {
                DataGridData data = new DataGridData();
                data.Address = "0x" + i.ToString("X");
                // convert memory array to string of 16 bits
                data.Value = assembly.controlMemory[i, 0] + " " +
                    assembly.controlMemory[i, 1] + " " +
                    assembly.controlMemory[i, 2] + " " +
                    assembly.controlMemory[i, 3] + " " +
                    assembly.controlMemory[i, 4] + " " +
                    assembly.controlMemory[i, 5];

                Microprogrammemmory.Items[i] = data;

            }

        }

        private void btnDebug_Click(object sender, RoutedEventArgs e)
        {
            btnStepover.Visibility = Visibility.Visible;
            btnStop.Visibility = Visibility.Visible;
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
