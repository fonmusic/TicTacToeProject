namespace TicTacToeLibrary;

public class TicTacToeGame
{
    public required char[] Board { get; set; }
    public required string NextPlayer { get; set; }
    public required string Winner { get; set; }
    public required TicTacToeGameState GameState { get; set; }

    public bool MakeMove(int position)
    {
        if (Board[position] != '\0')
            return false;

        Board[position] = NextPlayer[0];
        NextPlayer = NextPlayer == "X" ? "O" : "X";
        Winner = CheckForWinner();
        GameState = Winner switch
        {
            "X" => TicTacToeGameState.XWin,
            "O" => TicTacToeGameState.OWin,
            _ => GameState == TicTacToeGameState.XMove ? TicTacToeGameState.OMove : TicTacToeGameState.XMove
        };
        if (Board.All(c => c != '\0'))
            GameState = TicTacToeGameState.Draw;
        return true;
    }

    private string CheckForWinner()
    {
        // Check rows
        for (int i = 0; i < 9; i += 3)
        {
            if (Board[i] != '\0' && Board[i] == Board[i + 1] && Board[i] == Board[i + 2])
                return Board[i].ToString();
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (Board[i] != '\0' && Board[i] == Board[i + 3] && Board[i] == Board[i + 6])
                return Board[i].ToString();
        }

        // Check diagonals
        if (Board[0] != '\0' && Board[0] == Board[4] && Board[0] == Board[8])
            return Board[0].ToString();
        if (Board[2] != '\0' && Board[2] == Board[4] && Board[2] == Board[6])
            return Board[2].ToString();

        return string.Empty;
    }

    public void Reset()
    {
        Board = new char[9];
        NextPlayer = "X";
        Winner = string.Empty;
        GameState = TicTacToeGameState.XMove;
    }
}
