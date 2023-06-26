using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mano_simulator
{
    internal class Assembly
    {
        // Main
        bool[,] memory = new bool[2096,16];
        bool[] AR = new bool[11];
        bool[] PC = new bool[11];
        bool[] DR = new bool[16];
        bool[] AC = new bool[16];

        IDictionary<string, bool[]> symbolTable = new Dictionary<string, bool[]>();

        // Microprogram
        bool[,] controlMemory = new bool[128,20];
        bool[] CAR = new bool[7];
        bool[] SBR = new bool[7];

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



    }

    

}
