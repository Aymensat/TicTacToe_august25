# Unity Documentation: Game Logic and State Management

This document provides C# and Unity concepts for managing the state of your game.

## 1. What is Game State?

Game State is all the information that describes the current situation of your game. For Tic-Tac-Toe, the state includes:

- The contents of the 3x3 board.
- Whose turn it is (Player X or Player O).
- Whether the game is ongoing, has been won, or is a draw.

You need a central place to store and manage this information. A script called `GameManager` or `GameLogic` on a dedicated GameObject is a perfect place for this.

## 2. Storing the Board State: Arrays

A 2D array is the perfect data structure to represent a Tic-Tac-Toe board in code.

**Declaration:**

In C#, you declare a 2D array with `[ , ]`.

```csharp
// An integer array to store the state of the board.
// We can use 0 for empty, 1 for Player X, and 2 for Player O.
int[,] boardState = new int[3, 3];
```

**Accessing and Modifying:**

You access elements with `[row, column]`.

```csharp
// Set the top-left cell to Player X (value 1)
boardState[0, 0] = 1;

// Check the value of the center cell
int centerCell = boardState[1, 1];

// Loop through the entire board to print its state
for (int row = 0; row < 3; row++)
{
    for (int col = 0; col < 3; col++)
    {
        Debug.Log("Cell (" + row + "," + col + ") has value: " + boardState[row, col]);
    }
}
```

## 3. Managing Turns and Player Identity: Enums

Using numbers like `1` for Player X and `2` for Player O works, but it's not very readable. This is a perfect use case for an **`enum`** (enumeration). An enum is a custom type that has a fixed set of named constants.

**Declaration:**

```csharp
public enum Player
{
    None, // Represents an empty cell
    X,
    O
}
```

**Usage:**

Now you can create variables of this `Player` type. This makes your code much more readable and less error-prone.

```csharp
// Variable to track the current player
Player currentPlayer;

// A board state using the Player enum instead of ints
Player[,] board = new Player[3, 3];

void Start()
{
    // X always goes first
    currentPlayer = Player.X;

    // Set the center cell to be owned by Player O
    board[1, 1] = Player.O;
}

void SwitchPlayerTurn()
{
    if (currentPlayer == Player.X)
    {
        currentPlayer = Player.O;
    }
    else
    {
        currentPlayer = Player.X;
    }

    // A more advanced way to write the above:
    // currentPlayer = (currentPlayer == Player.X) ? Player.O : Player.X;
}
```

## 4. Structuring Your Game Logic Script

Here is a conceptual structure for your `GameLogic.cs` script. This is not a complete solution, just a guide to how you might organize the variables and functions you will need to write.

```csharp
using UnityEngine;
// You might need this for UI elements like TextMeshPro
// using TMPro;

public class GameLogic : MonoBehaviour
{
    // --- STATE VARIABLES ---
    // Use an enum for the game's overall status
    public enum GameState { InProgress, Won, Draw }

    private Player currentPlayer;
    private Player[,] board;
    private GameState currentGameState;
    private int moveCount;


    // --- SCENE REFERENCES ---
    // Create public fields to link to your UI elements in the Inspector
    // public TextMeshProUGUI statusText;
    // public Button[] cells; // Maybe an array of all your button components


    // --- LIFECYCLE METHODS ---
    void Start()
    {
        // This is where you would call a function to start a new game
        // NewGame();
    }


    // --- PUBLIC METHODS (called by buttons) ---
    public void OnCellClicked(int cellIndex)
    {
        // 1. Check if the move is valid (is the cell empty? is the game still in progress?).
        // 2. Calculate row and col from cellIndex.
        // 3. Update your 'board' array with the currentPlayer.
        // 4. Visually update the clicked cell (e.g., change the button's text to "X" or "O").
        // 5. Increment moveCount.
        // 6. Call a function to check if the game has been won or is a draw.
        // 7. If the game is still in progress, switch the current player.
    }


    // --- PRIVATE HELPER METHODS (your core logic) ---

    // private void NewGame() { ... reset all state variables for a fresh game ... }
    // private void UpdateBoardVisuals(int row, int col) { ... }
    // private GameState CheckForWinner(int lastMoveRow, int lastMoveCol) { ... return GameState.Won or GameState.InProgress ... }
    // private void HandleGameOver(GameState outcome) { ... display winner/draw message ... }
    // private void SwitchPlayerTurn() { ... }
}
```
