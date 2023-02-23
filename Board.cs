using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Othello
{
    public class Board
    {
        private readonly int r_BoardSize;
        private readonly Cell[,] r_Cells;
        private int m_BlackCount;
        private int m_WhiteCount;


        public Board(int i_BoardSize, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            r_BoardSize = i_BoardSize;
            r_Cells = new Cell[r_BoardSize, r_BoardSize];
            m_BlackCount = 2;
            m_WhiteCount = 2;

            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    r_Cells[i, j] = new Cell(i, j);
                }
            }

            r_Cells[(r_BoardSize / 2) - 1, r_BoardSize / 2 - 1].CurrentColor = Player.eColor.White;
            r_Cells[r_BoardSize / 2, r_BoardSize / 2].CurrentColor = Player.eColor.White;
            r_Cells[r_BoardSize / 2 - 1, r_BoardSize / 2].CurrentColor = Player.eColor.Black;
            r_Cells[r_BoardSize / 2, r_BoardSize / 2 - 1].CurrentColor = Player.eColor.Black;
            DisplayBoard(i_FirstPlayerName, i_SecondPlayerName, i_FirstPlayerName);
        }
        
        public Cell[,] Cells
        {
            get{ return r_Cells; }
        }
        
        public int BlackCount
        {
            get
            {
                return m_BlackCount;

            }
            set
            {
                m_BlackCount = value;
            }
        }
        
        public int WhiteCount
        {
            get
            {
                return m_WhiteCount;
            }
            set
            {
                m_WhiteCount = value;
            }
        }
        
        public int BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }
        
      
        public void UpdatesScore(Player i_MovePlayer, int i_CapturbaleCellsCount)
        {
            if (i_MovePlayer.PlayerColor == Player.eColor.Black)
            {
                m_BlackCount += i_CapturbaleCellsCount + 1;
                m_WhiteCount -= i_CapturbaleCellsCount;
            }
            else
            {
                m_WhiteCount += i_CapturbaleCellsCount + 1;
                m_BlackCount -= i_CapturbaleCellsCount;
            }
        }
        
        public void DisplayBoard(string i_FirstName, string i_SecondName, string i_PlayerToPlay)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (r_BoardSize == 8)
            {
                Console.WriteLine("      A     B     C     D     E     F     G     H   ");
                Console.Write("   ");
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("=================================================");
            }
            else if (r_BoardSize == 6)
            {
                Console.WriteLine("      A     B     C     D     E     F   ");
                Console.Write("   ");
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("=====================================");
            }
            for (int i = 0; i < r_BoardSize; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write(i + 1 + "  ");
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write("|");
                for (int j = 0; j < r_BoardSize; j++)
                {
                    
                    switch (r_Cells[i, j].CurrentColor)
                    {
                        case Player.eColor.None:
                            {
                                Console.Write("     |");
                                break;
                            }
                        case Player.eColor.Black:
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("  O  ");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("|"); ;
                                break;
                            }
                        case Player.eColor.White:
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("  O  ");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("|");
                                break;
                            }
                    
                    }

                }

                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                if (r_BoardSize == 8)
                {
                    Console.Write("   ");
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("=================================================");
                }
                else if (r_BoardSize == 6)
                {
                    Console.Write("   ");
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("=====================================");
                }
                
            }
            Console.ResetColor();
            Console.WriteLine(
                @"                   Score
     {0}(Black):{1}     -     {2}(White):{3}

It's {4} turn:",
                i_FirstName,
                m_BlackCount,
                i_SecondName,
                m_WhiteCount,
                i_PlayerToPlay);
        }

    }
}

