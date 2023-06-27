﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mano_simulator
{
    internal class Assembly
    {
        // Main
        bool[,] memory = new bool[2096, 16];
        bool[] AR = new bool[11];
        bool[] PC = new bool[11];
        bool[] DR = new bool[16];
        bool[] AC = new bool[16];

        IDictionary<string, bool[]> symbolTable = new Dictionary<string, bool[]>();

        // Microprogram
        bool[,] controlMemory = new bool[128, 20];
        bool[] CAR = new bool[7];
        bool[] SBR = new bool[7];
        bool[] F1 = new bool[3];

        public bool[,] Memory { get => memory; set => memory = value; }
        public bool[] AR1 { get => AR; set => AR = value; }
        public bool[] PC1 { get => PC; set => PC = value; }
        public bool[] DR1 { get => DR; set => DR = value; }
        public bool[] AC1 { get => AC; set => AC = value; }
        public bool[,] ControlMemory { get => controlMemory; set => controlMemory = value; }
        public bool[] CAR1 { get => CAR; set => CAR = value; }
        public bool[] SBR1 { get => SBR; set => SBR = value; }

        public Assembly()
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
                for (int j = 0; j < 20; j++)
                {
                    ControlMemory[i, j] = false;
                }
            }
            for (int i = 0; i < 7; i++)
            {
                CAR[i] = false;
                SBR[i] = false;
            }
        }

        public void LoadMemory(string[] lines)
        {
            int i = 0;
            foreach (string line in lines)
            {
                string[] words = line.Split(' ');
                for (int j = 0; j < 16; j++)
                {
                    if (words[1][j] == '1')
                    {
                        Memory[i, j] = true;
                    }
                    else
                    {
                        Memory[i, j] = false;
                    }
                }
                i++;
            }
        }

        public void LoadControlMemory(string[] lines)
        {
            int i = 0;
            foreach (string line in lines)
            {
                string[] words = line.Split(' ');
                for (int j = 0; j < 20; j++)
                {
                    if (words[1][j] == '1')
                    {
                        ControlMemory[i, j] = true;
                    }
                    else
                    {
                        ControlMemory[i, j] = false;
                    }
                }
                i++;
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

        public void LoadPC(string[] words)
        {
            for (int i = 0; i < 11; i++)
            {
                if (words[1][i] == '1')
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

        public void LoadCAR(string[] words)
        {
            for (int i = 0; i < 7; i++)
            {
                if (words[1][i] == '1')
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
            int carry = 0;
            for (int i = 15; i >= 0; i--)
            {
                if (AC[i] && DR[i])
                {
                    if (carry == 1)
                    {
                        AC[i] = false;
                    }
                    else
                    {
                        AC[i] = true;
                    }
                    carry = 0;
                }
                else if (AC[i] || DR[i])
                {
                    if (carry == 1)
                    {
                        AC[i] = true;
                        carry = 0;
                    }
                    else
                    {
                        AC[i] = false;
                        carry = 1;
                    }
                }
                else
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
            }
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
                AR[i] = DR[i];
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

        public void READ()
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
                DR[i] = Memory[address, i];
            }
        }

        public void OR()
        {

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

        public void JUMP()
        {
            // Jump to the address in the AR
            for (int i = 0; i < 11; i++)
            {
                PC[i] = AR[i];
            }
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


    }



}
