namespace TicTacToeLibrary;

public class TicTacToe
{
    public int Id { get; set; }
    public TicTacToeDescription Description { get; set; } = new();
    public bool MakeMove(int position)
    {
        if (Description.Board[position] != ' ')
            return false;

        Description.Board = Description.Board[..position] + Description.NextPlayer[0] + Description.Board[(position + 1)..];
        Description.NextPlayer = Description.NextPlayer == "X" ? "O" : "X";
        Description.Winner = CheckForWinner();
        Description.GameState = Description.Winner switch
        {
            "X" => TicTacToeGameState.XWin,
            "O" => TicTacToeGameState.OWin,
            _ => Description.GameState == TicTacToeGameState.XMove ? TicTacToeGameState.OMove : TicTacToeGameState.XMove
        };
        if (Description.Board.All(c => c != ' '))
            Description.GameState = TicTacToeGameState.Draw;
        return true;
    }

    private string CheckForWinner()
    {
        // Check rows
        for (int i = 0; i < 9; i += 3)
        {
            if (Description.Board[i] != ' ' && Description.Board[i] == Description.Board[i + 1] && Description.Board[i] == Description.Board[i + 2])
                return Description.Board[i].ToString();
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (Description.Board[i] != ' ' && Description.Board[i] == Description.Board[i + 3] && Description.Board[i] == Description.Board[i + 6])
                return Description.Board[i].ToString();
        }

        // Check diagonals
        if (Description.Board[0] != ' ' && Description.Board[0] == Description.Board[4] && Description.Board[0] == Description.Board[8])
            return Description.Board[0].ToString();
        if (Description.Board[2] != ' ' && Description.Board[2] == Description.Board[4] && Description.Board[2] == Description.Board[6])
            return Description.Board[2].ToString();
        return string.Empty;
    }
}
