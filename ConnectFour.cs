namespace ConnectFour
{

    class ConnectFour
    {

        enum ConnectFourState { NONE, BLACKWIN, REDWIN, TIE };
        public enum Player { _, BLACK, RED }
        public Player currentPlayer = Player.BLACK;

        Board board;
        ConnectFourState winner = ConnectFourState.NONE;

        public ConnectFour()
        {
            this.board = new Board(6, 7);
        }

        /* Game running function */
        public void Run()
        {
            Console.WriteLine("Welcome, lets play a game.\n");

            /* Game loop */
            while (winner == ConnectFourState.NONE)
            {
                board.DisplayBoard();
                if (currentPlayer == Player.BLACK)
                    while (!RequestMove(currentPlayer)) { }
                else
                {
                    Console.WriteLine("Bot making move...");
                    Minmax m = new Minmax();
                    /* Check for a quick win */
                    int quickWin = m.DefaultWin(board, currentPlayer);

                    /* Check prevent opponent win */
                    currentPlayer = board.SwitchTurn(currentPlayer);
                    int preventLose = m.DefaultWin(board, currentPlayer);
                    currentPlayer = board.SwitchTurn(currentPlayer);

                    if (quickWin != -1)
                        board.PlaceMove(currentPlayer, quickWin);
                    else if (preventLose != -1)
                        board.PlaceMove(currentPlayer, preventLose);
                    else
                    {
                        List<int> possibleMoves = board.PossibleMoves();
                        int bMove = possibleMoves[0];
                        float bMoveValue = 0;
                        float tMoveValue = 0;

                        /* Call minmax function for all possible moves */
                        for (int i = 0; i < possibleMoves.Count - 1; i++)
                        {
                            tMoveValue = m.FindMoveValue(board, currentPlayer, possibleMoves[i]);

                            if (tMoveValue > bMoveValue)
                            {
                                bMove = possibleMoves[i];
                                bMoveValue = tMoveValue;
                            }
                        }
                        board.PlaceMove(currentPlayer, bMove);
                    }
                }


                /* To Add: Check if moves available, else TIE */
                if (board.BoardFinished(currentPlayer)) /* Player won */
                {
                    board.DisplayBoard();
                    if (currentPlayer == Player.BLACK)
                    {
                        winner = ConnectFourState.BLACKWIN;
                    }
                    else
                        winner = ConnectFourState.REDWIN;
                }

                /* Switch player and start loop over */
                currentPlayer = board.SwitchTurn(currentPlayer);
            }

            /* End game, show congrats */
            currentPlayer = board.SwitchTurn(currentPlayer);
            Console.WriteLine("Congratulations " + currentPlayer + ", you won!");
        }


        /* Request move from human player */
        public bool RequestMove(Player p)
        {
            Console.WriteLine("Current player: " + currentPlayer);
            Console.Write("Choose 1-" + board.board.GetLength(0) + " and press enter: ");

            try
            {
                int input = Int32.Parse(Console.ReadLine());
                if (input > 0 && input <= board.board.GetLength(1) && board.PlaceMove(p, input - 1))
                    return true;
                else
                    throw new Exception("");
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                Console.WriteLine("\nNo valid input, try again.");
                return false;
            }
        }
    }
}