using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    public class GameManager
    {
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private Board m_GameBoard;
        private Player m_CurrentPlayer;
        private Dictionary<Cell, List<Cell>> m_PlayerLegalMove;
        private bool m_GameOver;
        private string m_Winner; 


        public GameManager()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Blue;

            Console.WriteLine(@"Hello and welcome to our Othello console game! :)
");
            Console.ResetColor();
            r_Player1 = new Player(setPlayerName());
            Console.Clear();
            Console.WriteLine(
                @"Thank you {0}!

Do you want to play against the computer?
press 0 for PvP game or 1 to play against the Machine",
                r_Player1.PlayerName);
            bool isComputer = inputGameMode();
            Console.Clear();
            r_Player2 = new Player(setPlayerName(), r_Player1.PlayerColor, isComputer);
            m_GameBoard = new Board(GetValidBoardSize(), r_Player1.PlayerName, r_Player2.PlayerName);
            m_CurrentPlayer = r_Player1.PlayerColor == Player.eColor.Black ? r_Player1 : r_Player2;
            m_PlayerLegalMove = findLegalMoves(m_CurrentPlayer);
            PlayGame();
        }

        public bool GameOver
        {
            get {return m_GameOver; }
            set {m_GameOver = value; }
        }
       
        public string Winner
        {
            get { return m_Winner; }
            set { m_Winner = value; }
        }

        public void PlayGame()
        {
            bool wantToPlay = true;
            while (wantToPlay)
            {
                while(m_GameOver == false)
                {

                    if (CurrentPlayer.IsComputer == false)
                    {
                        string playerInputMove = MoveInput();
                        if(playerInputMove[0] == 'Q')
                        {
                            break;
                        }

                        int[] userInput = getUserInputInInts(playerInputMove);
                        Cell userCoicheCell = new Cell(userInput[0], userInput[1]);
                        MakeMove(userCoicheCell);
                    }
                    else
                    {
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Random randomMove = new Random();
                        ICollection<Cell> keys = m_PlayerLegalMove.Keys;
                        Cell randomKey = keys.ElementAt(randomMove.Next(keys.Count));
                        MakeMove(randomKey);
                    }
                }
                m_Winner = getWinnerPlayerName();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(@"Game Over...
The final score is:
{0}(Black): {1} - {2}(White): {3}

AND THE WINNER IS... *** {4} *** !!!
", r_Player1.PlayerName, m_GameBoard.BlackCount, r_Player2.PlayerName, m_GameBoard.WhiteCount, m_Winner);
                Console.ResetColor();
                Console.WriteLine(
                    @"Do you want a REMATCH?
press 0 to end session or 1 to play again");
                bool playRematch = inputGameMode();
                if(playRematch == false)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    
                    Console.WriteLine(@"Thank you for playing!
Have a good day,
Bye-Bye :)");
                    Console.ResetColor();
                    wantToPlay = false;
                }
                else
                {
                    Console.Clear();
                    int currentBoardSize = m_GameBoard.BoardSize;
                    m_GameBoard = new Board(currentBoardSize, r_Player1.PlayerName, r_Player2.PlayerName);
                    m_GameOver = false;
                    m_CurrentPlayer = r_Player1.PlayerColor == Player.eColor.Black ? r_Player1 : r_Player2;
                    m_PlayerLegalMove = findLegalMoves(m_CurrentPlayer);
                }
            }
        }
        
        public bool MakeMove(Cell i_Cell)
        {
            if (!m_PlayerLegalMove.ContainsKey((i_Cell)))
            {
                Console.WriteLine("This is invalid move, please try again:");
                return false;
            }

            Player movePlayer = m_CurrentPlayer;
            List<Cell> capturbaleCells = m_PlayerLegalMove[i_Cell];
            m_GameBoard.Cells[i_Cell.Row, i_Cell.Col].CurrentColor = CurrentPlayer.PlayerColor;
            flipDiscs(capturbaleCells);
            m_GameBoard.UpdatesScore(movePlayer, capturbaleCells.Count);
            Console.Clear();
            passTurn();
            m_GameBoard.DisplayBoard(r_Player1.PlayerName,r_Player2.PlayerName, m_CurrentPlayer.PlayerName);
            return true;
        }

        private bool inputGameMode()
        {
            bool inputMode = false;
            int userInput;
            string inputString = Console.ReadLine();
            while(!int.TryParse(inputString, out userInput) || (userInput != 0) && (userInput != 1))
            {
                Console.WriteLine("Invalid input, please try again, press 0 or 1 :");
                inputString = Console.ReadLine();
            }

            if(userInput != 0)
            {
                inputMode = true;
            }

            return inputMode;
        }

        private int[] getUserInputInInts(string i_UserInput)
        {
            int[] intArray = new int[2];
            intArray[0] = int.Parse(i_UserInput[0].ToString()) - 1;
            intArray[1] = Convert.ToInt32(i_UserInput[1]) - 'A';
            return intArray;
        }
       
        public Player CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; }
        }

        public Dictionary<Cell, List<Cell>> PlayerLegalMove
        {
            get { return m_PlayerLegalMove; }
            set { m_PlayerLegalMove = value; }
        }
      
        private List<Cell> capturbaleCellsInline(Cell i_Cell, Player i_Player, int i_RowOffSet, int i_ColOffSet)
        {
            List<Cell> capturbaleCells = new List<Cell>();
            int row = i_Cell.Row + i_RowOffSet;
            int col = i_Cell.Col + i_ColOffSet;
            bool capturableCellsFound = false;

            while (isInsideBoard(row, col) && m_GameBoard.Cells[row, col].CurrentColor != Player.eColor.None)
            {
                if (m_GameBoard.Cells[row, col].CurrentColor == OpponentPlayerColor(i_Player.PlayerColor))
                {
                    capturbaleCells.Add(new Cell(row, col));
                    row += i_RowOffSet;
                    col += i_ColOffSet;
                }
                else
                {
                    capturableCellsFound = true;
                    break;
                }
            }

            return capturableCellsFound ? capturbaleCells : new List<Cell>();
        }
        
        public static Player.eColor OpponentPlayerColor(Player.eColor i_PlayerColor)
        {
            switch (i_PlayerColor)
            {
                case Player.eColor.White:
                    {
                        return Player.eColor.Black;
                    }
                case Player.eColor.Black:
                    {
                        return Player.eColor.White;
                    }
            }

            return Player.eColor.None;
        }
        
        private List<Cell> capturbaleCells(Cell i_Cell, Player i_Player)
        {
            List<Cell> capturbaleCells = new List<Cell>();
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for(int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    if(rowOffset == 0 && colOffset == 0)
                    {
                        continue;
                    }

                    capturbaleCells.AddRange(capturbaleCellsInline(i_Cell, i_Player, rowOffset, colOffset));
                }
            }

            return capturbaleCells;
        }

        private bool isMoveLegal(Player i_Player, Cell i_Cell, out List<Cell> o_CapturbaleCells)
        {
            if(m_GameBoard.Cells[i_Cell.Row, i_Cell.Col].CurrentColor != Player.eColor.None)
            {
                o_CapturbaleCells = null;
                return false;
            }

            o_CapturbaleCells = this.capturbaleCells(i_Cell, i_Player);
            return o_CapturbaleCells.Count > 0;
        }
       
        private bool isInsideBoard(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < m_GameBoard.BoardSize && i_Col >= 0 && i_Col < m_GameBoard.BoardSize;
        }

        private void switchPlayer()
        {
            m_CurrentPlayer = m_CurrentPlayer == r_Player1 ? r_Player2 : r_Player1;
            m_PlayerLegalMove = findLegalMoves(CurrentPlayer);
        }

        private string getWinnerPlayerName()
        {
            string winnerName = "NO ONE :(";
            if (m_GameBoard.BlackCount > m_GameBoard.WhiteCount)
            {
                winnerName = r_Player1.PlayerColor == Player.eColor.Black ? r_Player1.PlayerName : r_Player2.PlayerName;
            }

            if(m_GameBoard.WhiteCount > m_GameBoard.BlackCount)
            {
                winnerName = r_Player1.PlayerColor == Player.eColor.White ? r_Player1.PlayerName : r_Player2.PlayerName;
            }

            return  winnerName;

        }

        private void passTurn()
        {
            switchPlayer();
            if(m_PlayerLegalMove.Count > 0)
            {
                return;
            }

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"{0} you don't have any legal move your turn is skipped", m_CurrentPlayer.PlayerName);
            Console.ResetColor();
            switchPlayer();

            if (m_PlayerLegalMove.Count == 0)
            {
                m_GameOver = true;
            }
        }

        private void flipDiscs(List<Cell> i_Cells)
        {
            foreach (Cell cell in i_Cells)
            {
                m_GameBoard.Cells[cell.Row, cell.Col].CurrentColor = m_CurrentPlayer.PlayerColor;
            }
        }
        
        private Dictionary<Cell, List<Cell>> findLegalMoves(Player i_Player)
        {
            Dictionary<Cell, List<Cell>> legalMoves = new Dictionary<Cell, List<Cell>>();
            for(int row = 0; row < m_GameBoard.BoardSize; row++)
            {
                for(int col = 0; col < m_GameBoard.BoardSize; col++)
                {
                    Cell cellToAdd = new Cell(row, col);

                    if(isMoveLegal(i_Player, cellToAdd, out List<Cell> capturbaleCells))
                    {
                        legalMoves[cellToAdd] = capturbaleCells;
                    }
                }
            }

            return legalMoves;
        }
        
        public bool IsValidSyntaxMove(string i_MoveString)
        {
            bool isValid = true;
            if (i_MoveString.Length == 0)
            {
                isValid = false;
            }
            else if ((i_MoveString[0] == 'Q' || i_MoveString[0] == 'q') && i_MoveString.Length == 1)
            {
                m_GameOver = true;
            }
            else if (i_MoveString.Length != 2)
            {
                isValid = false;
            }
            else
            {
                int gameBoardSize = m_GameBoard.BoardSize;
                char charLetter = char.ToUpper(i_MoveString[1]);
                char charNumber = i_MoveString[0];

                if (!char.IsLetter(charLetter))
                {
                    isValid = false;
                }

                if (!char.IsNumber(charNumber))
                {
                    isValid = false;
                }

                if (charLetter < 'A' || charLetter > 'A' + gameBoardSize)
                {
                    isValid = false;
                }

                if (charNumber < '0' || charNumber > (char)gameBoardSize + '0')
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        public string MoveInput()
        {
            Console.WriteLine(@"
Please enter your move (Number for Row and then Letter for Col):");
            string playerMove = (Console.ReadLine());

            while (IsValidSyntaxMove(playerMove) == false)
            {
                Console.WriteLine("This is invalid syntax, please try again:");
                playerMove = Console.ReadLine();
                
            }
            playerMove = playerMove.ToUpper();

            return playerMove;
        }

        public int GetValidBoardSize()
        {
            int boardSizeInput;
            Console.WriteLine(@"Please choose the board size:
press 6 for 6x6 or 8 for 8x8 :");
            string inputString = Console.ReadLine();
            while (!int.TryParse(inputString, out boardSizeInput) || (boardSizeInput != 6) && (boardSizeInput != 8))
            {
                Console.WriteLine("Invalid input, please try again.... press 6 for 6x6 or 8 for 8x8 :");
                inputString = Console.ReadLine();
            }
            Console.Clear();
            return boardSizeInput;
        }

        private string setPlayerName()
        {

            Console.WriteLine("Please enter your name: ");
            string playerName = Console.ReadLine();

            while (playerName.Length <= 0 || playerName.Length > 30)
            {
                Console.WriteLine(@"Error: name must be at list one characters and no longer than 30 characters,
Please try again");
                playerName = Console.ReadLine();
            }
            Console.Clear();
            return playerName;
        }
    }
}
