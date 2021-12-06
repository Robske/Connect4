namespace ConnectFour
{
    /* State reprentation of the board. */

    class Minmax
    {
        int Strength = 10000;
        Random rnd = new Random();

        /* Check for win for single move */
        public int DefaultWin(Board board, ConnectFour.Player curPlayer)
        {
            Board newBoard = board.Copy();
            List<int> possibleMoves = newBoard.PossibleMoves();

            foreach (int move in possibleMoves)
            {
                newBoard.PlaceMove(curPlayer, move);
                if (newBoard.BoardFinished(curPlayer))
                    return move;

                newBoard = board.Copy();
            }

            return -1;
        }

        /* MiniMax algorithm */
        public float FindMoveValue(Board board, ConnectFour.Player curPlayer, int col)
        {
            int value = 0;

            for (int i = 0; i < Strength; i++)
            {
                Board newBoard = board.Copy();
                /* Make move */
                newBoard.PlaceMove(curPlayer, col);

                /* Calculate value of move */
                ConnectFour.Player winner = PlayGame(newBoard, curPlayer);
                if (winner == curPlayer)
                    value++;
                else if (winner == ConnectFour.Player._)
                    value = 0;
                else
                    value--;
            }

            /* Return strength of move */
            return (value / (float)Strength);
        }

        /* Checks all the next possible moves given a Board. */
        ConnectFour.Player PlayGame(Board b, ConnectFour.Player curPlayer)
        {
            int move = 0;

            /* While moves available and no winner... */
            while (b.PossibleMoves().Count > 0 && b.LookForWinner() == ConnectFour.Player._)
            {
                List<int> possibleMoves = b.PossibleMoves();

                /* Choose random move */
                move = rnd.Next(possibleMoves.Count);
                b.PlaceMove(curPlayer, possibleMoves[move]);

                if (curPlayer == ConnectFour.Player.BLACK)
                    curPlayer = ConnectFour.Player.RED;
                else
                    curPlayer = ConnectFour.Player.BLACK;
            }

            // return who would win the game
            return b.LookForWinner();
        }
    }
}