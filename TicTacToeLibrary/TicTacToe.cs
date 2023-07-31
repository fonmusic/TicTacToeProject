namespace TicTacToeLibrary;

public class TicTacToe
{
    public bool MakeMove(int position, TicTacToeDescription description)
    {
        if (description.Board[position] != ' ')
            return false;

        description.Board = description.Board[..position] + description.NextPlayer[0] + description.Board[(position + 1)..];
        description.NextPlayer = description.NextPlayer == "X" ? "O" : "X";
        description.Winner = CheckForWinner(description);
        description.GameState = description.Winner switch
        {
            "X" => TicTacToeGameState.XWin,
            "O" => TicTacToeGameState.OWin,
            _ => description.GameState == TicTacToeGameState.XMove ? TicTacToeGameState.OMove : TicTacToeGameState.XMove
        };
        if (description.Board.All(c => c != ' '))
            description.GameState = TicTacToeGameState.Draw;
        return true;
    }

    private string CheckForWinner(TicTacToeDescription description)
    {
        // Check rows
        for (int i = 0; i < 9; i += 3)
        {
            if (description.Board[i] != ' ' && description.Board[i] == description.Board[i + 1] && description.Board[i] == description.Board[i + 2])
                return description.Board[i].ToString();
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (description.Board[i] != ' ' && description.Board[i] == description.Board[i + 3] && description.Board[i] == description.Board[i + 6])
                return description.Board[i].ToString();
        }

        // Check diagonals
        if (description.Board[0] != ' ' && description.Board[0] == description.Board[4] && description.Board[0] == description.Board[8])
            return description.Board[0].ToString();
        if (description.Board[2] != ' ' && description.Board[2] == description.Board[4] && description.Board[2] == description.Board[6])
            return description.Board[2].ToString();
        return string.Empty;
    }
}
