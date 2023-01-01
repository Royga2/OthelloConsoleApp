﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    public class Cell
    {
        private readonly int r_Row;
        private readonly int r_Col;
        private eColor m_CurrentColor;


        public Cell(int i_Row, int i_Col)
        {
            r_Row = i_Row;
            r_Col = i_Col;
        }

        public int Row
        {
            get
            {
                return r_Row;
            }
        }
       
        public int Col
        {
            get
            {
                return r_Col;
            }
        }
      
        public eColor CurrentColor
        {
            get{ return m_CurrentColor; }
            set{ m_CurrentColor = value; }
        }
       
        public override bool Equals(object i_Obj)
        {
            if(i_Obj is Cell other)
            {
                return r_Row == other.Row && r_Col == other.Col;
            }

            return false;
        }
       
        public override int GetHashCode()
        {
            return 8 * r_Row + r_Col;
        }
    }
}