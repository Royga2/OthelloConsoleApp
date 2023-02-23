using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    public class Player
    {
        public enum eColor
        {
            None = 0,
            Black,
            White
        }

        private readonly string r_PlayerName;
        private readonly eColor r_PlayerColor;
        private readonly bool r_IsComputer;


        public Player(string i_Name)
        {
            r_PlayerName = i_Name;
            r_PlayerColor = eColor.Black;
            r_IsComputer = false;
        }

        public Player(string i_Name, eColor i_PlayerColor)
        {

            r_PlayerName = i_Name;
            r_PlayerColor = i_PlayerColor;
            r_IsComputer = false;
        }
       
        public Player(string i_Name, eColor i_OponentColor, bool i_IsComputer)
        {

            r_PlayerName = i_IsComputer == true ? "CPU" : i_Name;
            r_PlayerColor = i_OponentColor == eColor.Black ? eColor.White : eColor.Black;
            r_IsComputer = i_IsComputer;
        }
        
        public bool IsComputer
        {
            get {return r_IsComputer; }
        }
        
        public eColor PlayerColor
        {
            get { return r_PlayerColor; }
        }
        
        public string PlayerName
        {
            get { return r_PlayerName; }
        }
    }
}
