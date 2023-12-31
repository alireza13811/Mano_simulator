﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Mano_simulator
{
    internal class Assembly
    {
        // Main
        public bool[,] memory = new bool[2096, 16];
        public bool[] AR = new bool[11];
        public bool[] PC = new bool[11];
        public bool[] DR = new bool[16];
        public bool[] AC = new bool[16];

        IDictionary<string, int> labelAddressTable = new Dictionary<string, int>();

        // Microprogram
        public string[,] controlMemory = new string[128, 6];
        public bool[] CAR = new bool[7];
        public bool[] SBR = new bool[7];

        IDictionary<string, int> microprogramLabels = new Dictionary<string, int>();
        

        public bool[,] Memory { get => memory; set => memory = value; }
        public bool[] AR1 { get => AR; set => AR = value; }
        public bool[] PC1 { get => PC; set => PC = value; }
        public bool[] DR1 { get => DR; set => DR = value; }
        public bool[] AC1 { get => AC; set => AC = value; }
        public string[,] ControlMemory { get => controlMemory; set => controlMemory = value; }
        public bool[] CAR1 { get => CAR; set => CAR = value; }
        public bool[] SBR1 { get => SBR; set => SBR = value; }

        public Assembly()
        {
            initializations();
        }

        public void initializations()
        {
            // Main
            for (int i = 0; i < 2096; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Memory[i, j] = false;
                }
            }
            for (int i = 0; i < 11; i++)
            {
                AR[i] = false;
                PC[i] = false;
            }
            for (int i = 0; i < 16; i++)
            {
                DR[i] = false;
                AC[i] = false;
            }

            // Microprogram
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    ControlMemory[i, j] = "";
                }
            }
            for (int i = 0; i < 7; i++)
            {
                CAR[i] = false;
                SBR[i] = false;
            }
            microprogramLabels.Clear();
            labelAddressTable.Clear();
        }

        public void LoadMemory(int LC, string line)
        {
            for (int i = 0; i < 16; i++)
            {
                if (line[i] == '1')
                {
                    Memory[LC, i] = true;
                }
                else
                {
                    Memory[LC, i] = false;
                }
            }
        }

        public void LoadAR(string[] words)
        {
            for (int i = 0; i < 11; i++)
            {
                if (words[1][i] == '1')
                {
                    AR[i] = true;
                }
                else
                {
                    AR[i] = false;
                }
            }
        }

        public void LoadPC(string words)
        {
            for (int i = 0; i < 11; i++)
            {
                if (words[1] == '1')
                {
                    PC[i] = true;
                }
                else
                {
                    PC[i] = false;
                }
            }
        }

        public void LoadDR(string[] words)
        {
            for (int i = 0; i < 16; i++)
            {
                if (words[1][i] == '1')
                {
                    DR[i] = true;
                }
                else
                {
                    DR[i] = false;
                }
            }
        }

        public void LoadAC(string[] words)
        {
            for (int i = 0; i < 16; i++)
            {
                if (words[1][i] == '1')
                {
                    AC[i] = true;
                }
                else
                {
                    AC[i] = false;
                }
            }
        }

        public void LoadCAR(string words)
        {
            for (int i = 0; i < 7; i++)
            {
                if (words[i] == '1')
                {
                    CAR[i] = true;
                }
                else
                {
                    CAR[i] = false;
                }
            }
        }

        public void LoadSBR(string[] words)
        {
            for (int i = 0; i < 7; i++)
            {
                if (words[1][i] == '1')
                {
                    SBR[i] = true;
                }
                else
                {
                    SBR[i] = false;
                }
            }
        }

        // Microprogram Instructions
        public void ADD()
        {
            int carry = 0;
            for (int i = 15; i >= 0; i--)
            {
                if (AC[i] && DR[i])
                {
                    if (carry == 1)
                    {
                        AC[i] = true;
                    }
                    else
                    {
                        AC[i] = false;
                    }
                    carry = 1;
                }
                else if (AC[i] || DR[i])
                {
                    if (carry == 1)
                    {
                        AC[i] = false;
                        carry = 1;
                    }
                    else
                    {
                        AC[i] = true;
                        carry = 0;
                    }
                }
                else
                {
                    if (carry == 1)
                    {
                        AC[i] = true;
                        carry = 0;
                    }
                    else
                    {
                        AC[i] = false;
                        carry = 0;
                    }
                }
            }
        }

        public void SUB()
        {
            for (int i = 0; i < 16; i++)
            {
                if (DR[i] == true)
                {
                    DR[i] = false;
                }
                else
                {
                    DR[i] = true;
                }
            }
            INCDR();
            ADD();
        }

        public void CLRAC()
        {
            for (int i = 0; i < 16; i++)
            {
                AC[i] = false;
            }
        }

        public void INCAC()
        {
            int carry = 1;
            for (int i = 15; i >= 0; i--)
            {
                if (AC[i])
                {
                    if (carry == 1)
                    {
                        AC[i] = false;
                        carry = 1;
                    }
                    else
                    {
                        AC[i] = true;
                        carry = 0;
                    }
                }
                else
                {
                    if (carry == 1)
                    {
                        AC[i] = true;
                        carry = 0;
                    }
                    else
                    {
                        AC[i] = false;
                        carry = 0;
                    }
                }
            }
        }

        public void DRTAC()
        {
            for (int i = 0; i < 16; i++)
            {
                AC[i] = DR[i];
            }
        }

        public void DRTAR()
        {
            for (int i = 0; i < 11; i++)
            {
                AR[i] = DR[i+5];
            }
        }

        public void PCTAR()
        {
            for (int i = 0; i < 11; i++)
            {
                AR[i] = PC[i];
            }
        }

        public void WRITE()
        {
            int address = 0;
            for (int i = 0; i < 11; i++)
            {
                if (AR[i])
                {
                    address += (int)Math.Pow(2, 10 - i);
                }
            }
            for (int i = 0; i < 16; i++)
            {
                Memory[address, i] = DR[i];
            }

        }

        public bool READ()
        {
            int address = 0;
            bool flag = true;
            for (int i = 0; i < 11; i++)
            {
                if (AR[i])
                {
                    address += (int)Math.Pow(2, 10 - i);
                }
            }
            for (int i = 0; i < 16; i++)
            {
                DR[i] = Memory[address, i];
                if (!Memory[address, i])
                {
                    flag = false;
                }
            }
            return flag;
        }

        public void OR()
        {
            for (int i = 0; i < 16; i++)
            {
                if (AC[i] || DR[i])
                {
                    AC[i] = true;
                }
                else
                {
                    AC[i] = false;
                }
            }
        }

        public void AND()
        {
            // AND the contents of the AC and DR and store the result in the AC
            for (int i = 0; i < 16; i++)
            {
                if (AC[i] && DR[i])
                {
                    AC[i] = true;
                }
                else
                {
                    AC[i] = false;
                }
            }
        }

        public void COM()
        {
            // NOT the contents of the AC and store the result in the AC
            for (int i = 0; i < 16; i++)
            {
                if (AC[i])
                {
                    AC[i] = false;
                }
                else
                {
                    AC[i] = true;
                }
            }
        }

        public void JUMP(string word)
        {
            int address = microprogramLabels[word];
            string binary = Convert.ToString(address, 2);
            while (binary.Length < 4)
            {
                binary = "0" + binary;
            }
            LoadCAR(binary);
        }

        public void ACTDR()
        {
            // Transfer the contents of the AC to the DR
            for (int i = 0; i < 16; i++)
            {
                DR[i] = AC[i];
            }
        }

        public void PCTDR()
        {
            // Transfer the contents of the PC to the DR
            for (int i = 0; i < 11; i++)
            {
                DR[i] = PC[i];
            }
        }

        public void XOR()
        {
            // XOR the contents of the AC and DR and store the result in the AC
            for (int i = 0; i < 16; i++)
            {
                if (AC[i] == DR[i])
                {
                    AC[i] = false;
                }
                else
                {
                    AC[i] = true;
                }
            }
        }

        public void SHL()
        {
            // Shift the contents of the AC left by one bit
            bool temp = AC[0];
            for (int i = 0; i < 15; i++)
            {
                AC[i] = AC[i + 1];
            }
            AC[15] = temp;
        }

        public void SHR()
        {
            // Shift the contents of the AC right by one bit
            bool temp = AC[15];
            for (int i = 15; i > 0; i--)
            {
                AC[i] = AC[i - 1];
            }
            AC[0] = temp;
        }

        public void INCPC()
        {
            // Increment the contents of the PC by one
            int carry = 1;
            for (int i = 10; i >= 0; i--)
            {
                if (PC[i])
                {
                    if (carry == 1)
                    {
                        PC[i] = false;
                        carry = 1;
                    }
                    else
                    {
                        PC[i] = true;
                        carry = 0;
                    }
                }
                else
                {
                    if (carry == 1)
                    {
                        PC[i] = true;
                        carry = 0;
                    }
                    else
                    {
                        PC[i] = false;
                        carry = 0;
                    }
                }
            }
        }

        public void ARTPC()
        {
            // Transfer the contents of the AR to the PC
            for (int i = 0; i < 11; i++)
            {
                PC[i] = AR[i];
            }
        }

        public void CALL()
        {
            // Increase the contents of the CAR by one
            int carry = 1;
            for (int i = 10; i >= 0; i--)
            {
                if (CAR[i])
                {
                    if (carry == 1)
                    {
                        CAR[i] = false;
                        carry = 1;
                    }
                    else
                    {
                        CAR[i] = true;
                        carry = 0;
                    }
                }
                else
                {
                    if (carry == 1)
                    {
                        CAR[i] = true;
                        carry = 0;
                    }
                    else
                    {
                        CAR[i] = false;
                        carry = 0;
                    }
                }
            }

            // transfer 

            for (int i = 0; i < 7; i++)
            {

            }

        }

        public void NOP()
        {
            // Do nothing
        }

        public void INCDR()
        {
            // Increment the contents of the DR by one
            int carry = 1;
            for (int i = 15; i >= 0; i--)
            {
                if (DR[i])
                {
                    if (carry == 1)
                    {
                        DR[i] = false;
                        carry = 1;
                    }
                    else
                    {
                        DR[i] = true;
                        carry = 0;
                    }
                }
                else
                {
                    if (carry == 1)
                    {
                        DR[i] = true;
                        carry = 0;
                    }
                    else
                    {
                        DR[i] = false;
                        carry = 0;
                    }
                }
            }
        }

        public void INCCAR()
        {
            // Increment the contents of the CAR by one
            int carry = 1;
            for (int i = 6; i >= 0; i--)
            {
                if (CAR[i])
                {
                    if (carry == 1)
                    {
                        CAR[i] = false;
                        carry = 1;
                    }
                    else
                    {
                        CAR[i] = true;
                        carry = 0;
                    }
                }
                else
                {
                    if (carry == 1)
                    {
                        CAR[i] = true;
                        carry = 0;
                    }
                    else
                    {
                        CAR[i] = false;
                        carry = 0;
                    }
                }
            }
        }

        public void CARTSBR()
        {
            // Transfer the contents of the CAR to the SBR
            for (int i = 0; i < 7; i++)
            {
                SBR[i] = CAR[i];
            }
        }

        public void SBRTCAR()
        {
            // Transfer the contents of the SBR to the CAR
            for (int i = 0; i < 7; i++)
            {
                CAR[i] = SBR[i];
            }
        }

        public void MAP()
        {
            for (int i = 1; i < 5; i++)
            {
                CAR[i] = DR[i];
            }
            CAR[0] = false;
            CAR[5] = false;
            CAR[6] = false;
        }

        // functions to compile the assembly code
        // first iteration to create label address table
        public void first_iteration(string code)
        {
            string[] linesOfCode = code.Split('\n');
            int LC = 0;
            string symbol;
            string[] symbols;
            foreach(string lineOfCode in linesOfCode)
            {
                symbol = lineOfCode.Split(' ')[0].Trim();
                if(symbol == "ORG")
                {
                    LC = int.Parse(lineOfCode.Split(' ')[1]);
                    continue;
                }
                else if(symbol.Length>2 && symbol.Substring(0,3) == "END")
                {
                    break;
                }
                symbols = lineOfCode.Split(',');
                if (symbols.Length > 1)
                {
                    symbol = symbols[0];
                    if (labelAddressTable.ContainsKey(symbol))
                    {
                        Console.WriteLine("Error: Duplicate label");
                    }
                    else
                    {
                        labelAddressTable.Add(symbol, LC);
                    }
                }
                LC++;
            }
        }

        // second iteration to compile code to binary using label address table and store in memory
        public void second_iteration(string code)
        {
            string[] lines = code.Split('\n');
            int LC = 0, address;
            string symbol, word="", binary, value;
            string[] instructions, symbols;
            
            foreach(string lineOfCode in lines)
            {
                symbol = lineOfCode.Split(' ')[0].Trim();
                if(symbol == "")
                {
                    throw new Exception("Error: Invalid instruction");
                }
                if(symbol == "ORG")
                {
                    LC = int.Parse(lineOfCode.Split(' ')[1]);
                    continue;
                }
                else if(symbol == "HEX" || symbol == "DEC" || symbol == "BIN")
                {
                    storing_label_in_memory(LC, lineOfCode.Trim());
                    LC++;
                    continue;
                }
                else if(symbol == "HLT")
                {
                    LoadMemory(LC, "1111111111111111");
                    LC++;
                    continue;
                }
                else if(symbol.Length > 2 && symbol.Substring(0, 3) == "END")
                {
                    break;
                }

                symbols = lineOfCode.Split(',');
                if(symbols.Length > 1)
                {
                    symbol = symbols[0];
                    if (labelAddressTable.ContainsKey(symbol))
                    {
                        value = lineOfCode.Split(',')[1].Trim();
                        storing_label_in_memory(LC, value);   
                    }
                    else
                    {
                        Console.WriteLine("Error: Label not found");
                    }
                    LC++;
                    continue;
                }
                word = "";
                instructions = lineOfCode.Trim().Split(" ");
                if(instructions.Length == 3 && instructions[2] == "I") 
                {
                    word += "1";

                }
                else
                {
                    word += "0";
                }
                address = microprogramLabels[instructions[0]];
                address /= 4;
                binary = Convert.ToString(address, 2);
                while (binary.Length < 4)
                {
                    binary = "0" + binary;
                }
                word += binary;

                address = labelAddressTable[instructions[1]];
                binary = Convert.ToString(address, 2);
                while (binary.Length < 11)
                {
                    binary = "0" + binary;
                }
                word += binary;

                LoadMemory(LC, word);

                LC++;
            }
        }

        public void storing_label_in_memory(int LC, string value)
        {
            string type = value.Split(' ')[0];
            string operand = value.Split(' ')[1];
            if (type == "DEC")
            {
                int dec = int.Parse(operand);
                string binary = Convert.ToString(dec, 2);
                while (binary.Length < 16)
                {
                    binary = "0" + binary;
                }
                for (int i = 0; i < 16; i++)
                {
                    if (binary[i] == '0')
                    {
                        memory[LC, i] = false;
                    }
                    else
                    {
                        memory[LC, i] = true;
                    }
                }
            }
            else if (type == "HEX")
            {
                int dec = int.Parse(operand, System.Globalization.NumberStyles.HexNumber);
                string binary = Convert.ToString(dec, 2);
                while (binary.Length < 16)
                {
                    binary = "0" + binary;
                }
                for (int i = 0; i < 16; i++)
                {
                    if (binary[i] == '0')
                    {
                        memory[LC, i] = false;
                    }
                    else
                    {
                        memory[LC, i] = true;
                    }
                }
            }
            else if (type == "BIN")
            {
                string binary = operand;
                while (binary.Length < 16)
                {
                    binary = "0" + binary;
                }
                for (int i = 0; i < 16; i++)
                {
                    if (binary[i] == '0')
                    {
                        memory[LC, i] = false;
                    }
                    else
                    {
                        memory[LC, i] = true;
                    }
                }
            }
            else if (type == "CHAR")
            {
                char[] charArray = operand.ToCharArray();
                for (int i = 0; i < 16; i++)
                {
                    if (charArray[i] == '0')
                    {
                        memory[LC, i] = false;
                    }
                    else
                    {
                        memory[LC, i] = true;
                    }
                }
            }
        }

        /*public void assembly_compile()*/


        // functions to compile the microprogram code
        public void microprogram_first_iteration(string code)
        {
            string[] lines = code.Split('\n');
            int LC = 0;
            string symbol;
            string[] symbols;
            foreach(string line in lines)
            {
                symbol = line.Split(' ')[0];
                if(symbol == "ORG")
                {
                    LC = int.Parse(line.Split(' ')[1]);
                    continue;
                }
                else if(symbol.Length > 2 && symbol.Substring(0, 3) == "END")
                {
                    break;
                }

                symbols = line.Split(':');
                if(symbols.Length > 1)
                {
                    if (labelAddressTable.ContainsKey(symbols[0]))
                    {
                        Console.WriteLine("Error: Duplicate label");
                    }
                    else
                    {
                        microprogramLabels.Add(symbols[0], LC);
                    }
                }
                LC++;
            }
        }

        public void microprogram_second_iteration(string code)
        {
            string[] lines = code.Split('\n');
            int LC = 0;
            string[] symbols, microinstructions, operations;

            foreach(string line in lines)
            {
                symbols = line.Split(':');
                if(symbols.Length > 1)
                {
                    LC = microprogramLabels[symbols[0]];
                    symbols = symbols.Skip(1).ToArray();
                }
                if (symbols[0].Split(' ').Length > 2)
                {
                    microinstructions = symbols[0].Split(' ');
                    operations = microinstructions[0].Split(',');
                    microinstructions = microinstructions.Skip(1).ToArray();
                    for(int i = 0; i < operations.Length; i++)
                    {
                        controlMemory[LC, i] = operations[i].Trim();
                    }
                    for(int i = 0; i <  microinstructions.Length - 3; i++)
                    {
                        controlMemory[LC, operations.Length+i] = microinstructions[0].Trim();
                        microinstructions = microinstructions.Skip(1).ToArray();
                    }

                    for(int i = 3; i < microinstructions.Length + 3; i++)
                    {
                        controlMemory[LC, i] = microinstructions[i - 3].Trim();
                    }

                }
                LC++;
            }
        }

        public int process(string code)
        {
            string[] lines = code.Split('\n');
            string pc = "";
            foreach (string line in lines)
            {
                if (line.Split(' ')[0] == "ORG")
                {
                    pc = line.Split(' ')[1];
                }
            }
            int dec = int.Parse(pc, System.Globalization.NumberStyles.HexNumber);
            string binary = Convert.ToString(dec, 2);
            while (binary.Length < 11)
            {
                binary = "0" + binary;
            }
            LoadPC(binary);

            int address = microprogramLabels["FETCH"];
            // convert address to binary string
            binary = Convert.ToString(address, 2);
            while (binary.Length < 4)
            {
                binary = "0" + binary;
            }
            LoadCAR(binary);
            return dec;
        }
        public void run(string code, string micro_code)
        {
            process(code);
            int state = 0, address = 0;
            while (state != 2)
            {
                state = execute_line_of_code(ref address, "");
            }
        }

        public int execute_line_of_code(ref int address, string micro_code)
        {
            int address2 = 0;
            int state = 0;
            for (int i = 0; i < 7; i++)
            {
                address2 += (int)Math.Pow(2, 6 - i) * Convert.ToInt32(CAR[i]);
            }

            string[] microinstructions = new string[6];
            for (int i = 0; i < 6; i++)
            {
                microinstructions[i] = controlMemory[address2, i]; 
            }
            string symbol = microprogramLabels.FirstOrDefault(x => x.Value == address2).Key;
            string[] lines = micro_code.Split('\n');
            int counter = 0;
            bool flag1 = false;

            foreach (string line in lines)
            {
                if (line.Split(':')[0] == symbol)
                {
                    address = counter;
                    flag1 = true;
                    break;
                }
                counter++;
            }
            if (!flag1)
            {
                address++;
            }

            string operation;
            for (int i = 0; i < 3; i++)
            {
                operation = microinstructions[i];
                if (operation == "" || operation == null)
                    continue;
                switch (operation)
                {
                    case "NOP":
                        NOP();
                        break;
                    case "CLRAC":
                        CLRAC();
                        break;
                    case "INCAC":
                        INCAC();
                        break;
                    case "DRTAC":
                        DRTAC();
                        break;
                    case "DRTAR":
                        DRTAR();
                        break;
                    case "PCTAR":
                        PCTAR();
                        break;
                    case "WRITE":
                        WRITE();
                        break;
                    case "READ":
                        state = READ() ?  2 : 0;
                        break;
                    case "SUB":
                        SUB();
                        break;
                    case "OR":
                        OR();
                        break;
                    case "AND":
                        AND();
                        break;
                    case "ACTDR":
                        ACTDR();
                        break;
                    case "INCDR":
                        INCDR();
                        break;
                    case "PCTDR":
                        PCTDR();
                        break;
                    case "XOR":
                        XOR();
                        break;
                    case "COM":
                        COM();
                        break;
                    case "SHL":
                        SHL();
                        break;
                    case "SHR":
                        SHR();
                        break;
                    case "INCPC":
                        INCPC();
                        break;
                    case "ARTPC":
                        ARTPC();
                        break;
                    case "ADD":
                        ADD();
                        break;
                    default:
                        throw new ArgumentException("Invalid operation");

                }

            }

            string condition = microinstructions[3];
            bool flag = true;
            switch (condition)
            {
                case "I":
                    flag = DR[0];
                    break;
                case "Z":
                    flag = IsACZero();
                    break;
                case "S":
                    flag = AC[0];
                    break;

            }

            if (flag)
            {
                string BR = microinstructions[4];
                string dest = microinstructions[5];
                switch (BR)
                {
                    case "JMP":
                        {
                            if(dest == "NEXT")
                            {
                                INCCAR();
                            }
                            else
                            {
                                if (dest == "FETCH")
                                    state = 1;
                                JUMP(dest);
                            }
                            break;
                        }
                    case "CALL":
                        {
                            INCCAR();
                            CARTSBR();
                            JUMP(dest);
                            break;
                        }
                    case "RET":
                        {
                            SBRTCAR();
                            break;
                        }
                    case "MAP":
                        {
                            MAP();
                            break;
                        }
                    default:
                        throw new ArgumentException("Invalid branch");

                }
            }
            else
            {
                INCCAR();
            }

            return state;
        }

        public bool IsACZero()
        {
            bool flag = true;
            for (int i = 0; i < 16; i++)
            {
                if (AC[i])
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

    }



}
