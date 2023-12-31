﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        int line = 1;
        int line1 = 0;
        string main_code = "";
        string microprogram_code = "";
        
        int ORG;
        bool HLT = false;
        int address = 0;
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
            btnDebug.Visibility = Visibility.Hidden;
            txtDebug.Visibility = Visibility.Hidden;
        }
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            btnStop.Visibility = Visibility.Visible;
            try
            {
                assembly.run(main_code, "");
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            update_data_grid();
            update_registers();
        }

        private void update_registers()
        {
            lblACvalue.Content = booleanArrayToString(assembly.AC);
            lblARvalue.Content = booleanArrayToString(assembly.AR);
            lblCARvalue.Content = booleanArrayToString(assembly.CAR);
            lblDRvalue.Content = booleanArrayToString(assembly.DR);
            lblSBRvalue.Content = booleanArrayToString(assembly.SBR);
            lblPCvalue.Content = booleanArrayToString(assembly.PC);
        }
        private string booleanArrayToString(bool[] array)
        {
            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i])
                {
                    result += "1";
                }
                else
                {
                    result += "0";
                }
            }
            return result;
        }
        private void btnStepover_Click(object sender, RoutedEventArgs e)
        {
            int state=0;
            try
            {
                state = assembly.execute_line_of_code(ref address, microprogram_code);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnDebug.Visibility = Visibility.Hidden;
                btnStepover.Visibility = Visibility.Hidden;
                txtDebug.Visibility = Visibility.Hidden;
                btnRun.Visibility = Visibility.Hidden;
            }
            if (main_code.Split('\n')[line1].Split(' ')[0].Trim() == "HLT")
            {
                HLT = true;
            }
            if(!HLT)
                highlightOneLine(state);

            highlightOneLineMic(address+1);

            update_registers();
            update_data_grid();
        }

        private void highlightOneLine(int state)
        {
            string[] code = main_code.Split("\n");
            int start = 0;
            int end = 0;
            int start1 = 0;
            int end1 = 0;
            for (int i = 0; i < line1; i++)
            {
                if (i < line1 - 1)
                {
                    start1 = start1 + code[i].Length + 3;
                }
                else
                    end1 = start1 + code[i].Length + 1;
            }

            for (int i = 0; i < line; i++)
            {
                if (i < line - 1)
                {
                    start = start + code[i].Length + 3;
                }
                else
                    end = start + code[i].Length + 1;
            }
            HighlightRow1(start, end);
            HighlightRow2(start1, end1);
            line+=state;
            line1+=state;
        }

        private void highlightOneLineMic(int line)
        {
            string[] code =microprogram_code.Split("\n");
            int start = 0;
            int end = 0;
            int start1 = 0;
            int end1 = microprogram_code.Length;

            for (int i = 0; i < line; i++)
            {
                if (i < line - 1)
                {
                    start = start + code[i].Length + 3;
                }
                else
                    end = start + code[i].Length + 1;
            }
            HighlightRow4(start1, end1);
            HighlightRow3(start, end);
        }

        private void update_data_grid()
        {
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
        private void btnComile_Click(object sender, RoutedEventArgs e)
        {
            assembly.initializations();
            btnRun.Visibility = Visibility.Visible;
            btnDebug.Visibility = Visibility.Visible;
            txtDebug.Visibility = Visibility.Visible;
            microprogram_code = new TextRange(txtProgrammMemmory.Document.ContentStart, txtProgrammMemmory.Document.ContentEnd).Text;
            try
            {
                assembly.microprogram_first_iteration(microprogram_code);
                assembly.microprogram_second_iteration(microprogram_code);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            main_code = new TextRange(txtProgramm.Document.ContentStart, txtProgramm.Document.ContentEnd).Text;

            try
            {
                assembly.first_iteration(main_code);
                assembly.second_iteration(main_code);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            // update the data grid
            update_data_grid();

        }
        private void HighlightRow1(int startOffset, int endOffset)
        {
            // Get the current selection from the RichTextBox
            TextSelection selection = txtProgramm.Selection;

            // Set the start and end positions for highlighting
            TextPointer start = txtProgramm.Document.ContentStart.GetPositionAtOffset(startOffset);
            TextPointer end = txtProgramm.Document.ContentStart.GetPositionAtOffset(endOffset);

            // Create a TextRange from the start and end positions
            TextRange range = new TextRange(start, end);

            // Apply formatting to the TextRange
            range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
        }
        private void HighlightRow2(int startOffset, int endOffset)
        {
            // Get the current selection from the RichTextBox
            TextSelection selection = txtProgramm.Selection;

            // Set the start and end positions for highlighting
            TextPointer start = txtProgramm.Document.ContentStart.GetPositionAtOffset(startOffset);
            TextPointer end = txtProgramm.Document.ContentStart.GetPositionAtOffset(endOffset);

            // Create a TextRange from the start and end positions
            TextRange range = new TextRange(start, end);

            // Apply formatting to the TextRange
            range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.White);
        }
        private void HighlightRow3(int startOffset, int endOffset)
        {
            // Get the current selection from the RichTextBox
            TextSelection selection = txtProgrammMemmory.Selection;

            // Set the start and end positions for highlighting
            TextPointer start = txtProgrammMemmory.Document.ContentStart.GetPositionAtOffset(startOffset);
            TextPointer end = txtProgrammMemmory.Document.ContentStart.GetPositionAtOffset(endOffset);

            // Create a TextRange from the start and end positions
            TextRange range = new TextRange(start, end);

            // Apply formatting to the TextRange
            range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
        }
        private void HighlightRow4(int startOffset, int endOffset)
        {
            // Get the current selection from the RichTextBox
            TextSelection selection = txtProgrammMemmory.Selection;

            // Set the start and end positions for highlighting
            TextPointer start = txtProgrammMemmory.Document.ContentStart.GetPositionAtOffset(startOffset);
            TextPointer end = txtProgrammMemmory.Document.ContentStart.GetPositionAtOffset(endOffset);

            // Create a TextRange from the start and end positions
            TextRange range = new TextRange(start, end);

            // Apply formatting to the TextRange
            range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.White);
        }
        private void btnDebug_Click(object sender, RoutedEventArgs e)
        {
            microprogram_code = new TextRange(txtProgrammMemmory.Document.ContentStart, txtProgrammMemmory.Document.ContentEnd).Text;
            main_code = new TextRange(txtProgramm.Document.ContentStart, txtProgramm.Document.ContentEnd).Text;

            btnStepover.Visibility = Visibility.Visible;
            btnStop.Visibility = Visibility.Visible;
            try
            {
                ORG = assembly.process(main_code);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            int line2 = 1;
            try
            {
                line2 = Convert.ToInt32(txtLine.Text);
            }catch(Exception ex)
            {
                line2 = 1;
            }
            
            line = line2;
            line1 = line2-1;
            int state = 0;
            for (int i = 0; i < line1; i++)
            {
                try
                {
                    state = assembly.execute_line_of_code(ref address, microprogram_code);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
            highlightOneLine(state);
            update_data_grid();
            update_registers();

        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnDebug.Visibility = Visibility.Hidden;
            btnRun.Visibility = Visibility.Hidden;
            btnStepover.Visibility = Visibility.Hidden;
            txtDebug.Visibility = Visibility.Hidden;
            btnStop.Visibility = Visibility.Hidden;
            line = 1;
            line1 = 0;
            HighlightRow2(0, main_code.Length);
            HighlightRow4(0, microprogram_code.Length+30);
            microprogram_code = "";
            main_code = "";
            HLT = false;
            address = 0;
            assembly.initializations();
            update_data_grid();
            update_registers();
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
