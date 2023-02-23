using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    public class Cell
    {
        private readonly int r_Row;
        private readonly int r_Col;
        private Player.eColor m_CurrentColor;


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
      
        public Player.eColor CurrentColor
        {
            get{ return m_CurrentColor; }
            set{ m_CurrentColor = value; }
        }
       
        public override bool Equals(object i_Obj)
        {
            bool isEquals = false;

            if (i_Obj is Cell other)
            {
                isEquals = r_Row == other.Row && r_Col == other.Col;
            }

            return isEquals;
        }

        public override int GetHashCode()
        {
            return 8 * r_Row + r_Col;
        }
    }
}
