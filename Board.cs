using System.Collections.Generic;
namespace ConnectFour
{
    /* State reprentation of the board. */
    public enum BoardState { _, X, O };

    class Board
    {
        public BoardState[,] board;

        public Board(int h, int w)
        {
            this.board = this.InitEmptyBoard(h, w);
        }

        /* Init empty board */
        private BoardState[,] InitEmptyBoard(int h, int w)
        {
            BoardState[,] board = new BoardState[h, w];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                    board[i, j] = BoardState._;
            }

            return board;
        }

        /* Displays current board state */
        public void DisplayBoard()
        {
            Console.Write("\n");
            for (int i = 0; i < this.board.GetLength(1); i++)
                Console.Write(i + 1 + " ");

            Console.WriteLine();

            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                for (int j = 0; j < this.board.GetLength(1); j++)
                    Console.Write(board[i, j] + " ");

                Console.WriteLine();
            }

            for (int i = 0; i < this.board.GetLength(1); i++)
                Console.Write(i + 1 + " ");

            Console.Write("\n");
        }

        /* Places move on board */
        public bool PlaceMove(ConnectFour.Player p, int row)
        {
            if (!ValidateMove(row))
                return false;

            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                if (i + 1 == this.board.GetLength(0) || board[i + 1, row] != BoardState._)
                {
                    if (p == ConnectFour.Player.BLACK)
                        board[i, row] = BoardState.X;
                    else
                        board[i, row] = BoardState.O;

                    break;
                }
            }

            return true;
        }

        /* Switches player turn */
        public ConnectFour.Player SwitchTurn(ConnectFour.Player currentPlayer)
        {
            if (currentPlayer == ConnectFour.Player.BLACK)
                return ConnectFour.Player.RED;
            else
                return ConnectFour.Player.BLACK;
        }

        /* Validates move */
        private bool ValidateMove(int row)
        {
            for (int i = 0; i < this.board.GetLength(0); i++)
                if (board[i, row] == BoardState._)
                    return true;

            return false;
        }

        /* Minimum amount posible moves */
        public Int32 MinAmountPossibleMoves()
        {
            int count = 0;
            for (int i = 0; i < this.board.GetLength(1); i++)
                if (board[0, i] == BoardState._)
                    count++;
            return count;
        }

        /* Possible moves */
        public List<Int32> PossibleMoves()
        {
            List<int> possibleMoves = new List<int>();
            for (int i = 0; i < this.board.GetLength(1); i++)
                if (board[0, i] == BoardState._)
                    possibleMoves.Add(i);
            return possibleMoves;
        }

        /* Check board finish */
        public bool BoardFinished(ConnectFour.Player p)
        {
            BoardState b = BoardState._;
            if (p == ConnectFour.Player.BLACK)
                b = BoardState.X;
            else
                b = BoardState.O;

            /* Horizontal wins */
            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                for (int j = 0; j < this.board.GetLength(1) - 3; j++)
                {
                    if (board[i, j] == b &&
                        board[i, j + 1] == b &&
                        board[i, j + 2] == b &&
                        board[i, j + 3] == b)
                        return true;
                }
            }

            /* Vertical wins */
            for (int i = 0; i < this.board.GetLength(1); i++)
            {
                for (int j = 0; j < this.board.GetLength(0) - 3; j++)
                {
                    if (board[j, i] == b &&
                        board[j + 1, i] == b &&
                        board[j + 2, i] == b &&
                        board[j + 3, i] == b)
                        return true;
                }
            }

            /* Downwards diagnal wins */
            for (int i = 0; i < this.board.GetLength(0) - 3; i++)
            {
                for (int j = 0; j < this.board.GetLength(1) - 3; j++)
                {
                    if (board[i, j] == b &&
                        board[i + 1, j + 1] == b &&
                        board[i + 2, j + 2] == b &&
                        board[i + 3, j + 3] == b)
                        return true;
                }
            }

            /* Upwards diagnal wins */
            for (int i = this.board.GetLength(0) - 1; i > 3; i--)
            {
                for (int j = 0; j < this.board.GetLength(1) - 3; j++)
                {
                    if (board[i, j] == b &&
                        board[i - 1, j + 1] == b &&
                        board[i - 2, j + 2] == b &&
                        board[i - 3, j + 3] == b)
                        return true;
                }
            }

            return false;
        }

        public ConnectFour.Player LookForWinner()
        {
            foreach (ConnectFour.Player _p in Enum.GetValues(typeof(ConnectFour.Player)))
            {
                if (_p != ConnectFour.Player._)
                    if (BoardFinished(_p))
                        return _p;
            }

            return ConnectFour.Player._;
        }

        public Board Copy()
        {
            int h = this.board.GetLength(0);
            int w = this.board.GetLength(1);
            Board newBoard = new Board(h, w);
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    newBoard.board[i, j] = this.board[i, j];

            return newBoard;
        }
    }
}