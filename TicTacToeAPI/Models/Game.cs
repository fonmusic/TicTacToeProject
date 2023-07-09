using TicTacToeAPI.Dtos;
namespace TicTacToeAPI.Models;
public class Game
{
    public int Id { get; set; }
    private const int _boardSize = 3;
    private readonly char[,] _board = new char[_boardSize, _boardSize];
    private char _currentPlayer;

    public Game()
    {
        // Initialize the game board with empty cells
        for (var i = 0; i < _boardSize; i++)
        {
            for (var j = 0; j < _boardSize; j++)
            {
                _board[i, j] = ' ';
            }
        }

        // Set the starting player
        _currentPlayer = 'X';
    }

    public char CurrentPlayer => _currentPlayer;

    public char[,] Board => _board;

    public bool MakeMove(int row, int column)
    {
        if (_board[row, column] != ' ')
        {
            // The cell is already occupied
            return false;
        }

        _board[row, column] = _currentPlayer;

        // Switch to the other player
        _currentPlayer = _currentPlayer == 'X' ? 'O' : 'X';

        return true;
    }

    public char CheckWinner()
    {
        // Check rows
        for (var i = 0; i < _boardSize; i++)
        {
            if (_board[i, 0] != ' ' && _board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2])
            {
                return _board[i, 0];
            }
        }

        // Check columns
        for (var j = 0; j < _boardSize; j++)
        {
            if (_board[0, j] != ' ' && _board[0, j] == _board[1, j] && _board[1, j] == _board[2, j])
            {
                return _board[0, j];
            }
        }

        // Check diagonals
        if (_board[0, 0] != ' ' && _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2])
        {
            return _board[0, 0];
        }

        if (_board[2, 0] != ' ' && _board[2, 0] == _board[1, 1] && _board[1, 1] == _board[0, 2])
        {
            return _board[2, 0];
        }

        // No winner yet
        return ' ';
    }

    public bool IsDraw()
    {
        for (var i = 0; i < _boardSize; i++)
        {
            for (var j = 0; j < _boardSize; j++)
            {
                if (_board[i, j] == ' ')
                {
                    // There is an empty cell, so the game is not a draw yet
                    return false;
                }
            }
        }
        // The board is full, so the game is a draw
        return true;
    }

    public GameDto ToDto()
    {
        var dto = new GameDto();
        var cells = new List<char>();

        for (var i = 0; i < _boardSize; i++)
        {
            for (var j = 0; j < _boardSize; j++)
            {
                cells.Add(_board[i, j]);
            }
        }

        dto.Cells = cells.ToArray();

        if (IsDraw())
            dto.State = GameState.Draw;
        else
        {
            var winner = CheckWinner();
            if (winner == 'X')
                dto.State = GameState.XWin;
            else if (winner == 'O')
                dto.State = GameState.OWin;
            else
                dto.State = _currentPlayer == 'X' ? GameState.XMove : GameState.OMove;

        }

        dto.Id = Id;

        return dto;

    }
}
