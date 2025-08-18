# Tic-Tac-Toe: A Developer's Journey

This document chronicles the development process of a Tic-Tac-Toe game in Unity, focusing on the key concepts and learning milestones.

## 1. Core Game Logic
The foundation of the project was establishing the core game rules in C#.
- **Game State:** A 2D list (`List<List<int>>`) was used to represent the 3x3 grid logically.
- **Turn Management:** A `char currentPlayer` variable tracks whose turn it is ('X' or 'O').
- **Win Detection:** The `CheckGameState()` method was implemented to check for wins across rows, columns, and diagonals after every move. It also handles logic for a "Draw" or "Pending" state.

## 2. UI and Player Interaction
With the logic in place, the focus shifted to the user interface.
- **Grid:** A series of UI `Button` components were used to create the interactive grid.
- **Visual Feedback:** The `OnClick` event for each button was wired to the `OnCellClicked()` method in `GameLogic.cs`. Sprites for 'X' and 'O' were assigned to the button's `Image` component to provide visual feedback for each move.
- **Input Handling:** The `interactable` property of buttons was used to prevent clicking on an already-occupied cell.

## 3. Advanced Visuals: The Win Line
To make wins more impactful, a `LineRenderer` component was added to draw a line through the winning cells. This involved several key learning steps:
- **Component References:** Learned the difference between creating a `public` variable to assign in the Inspector versus using `GetComponent<T>()` in code.
- **World Space Canvas:** Discovered that for a `LineRenderer` (a world object) to appear correctly on top of a UI `Canvas`, the canvas's Render Mode had to be set to `World Space`.
- **Coordinate Translation:** Wrote the `GetWinningPoints()` helper function to translate logical grid coordinates (e.g., row 1, column 2) into the `Vector3` world positions of the corresponding UI buttons, which was then fed to the `LineRenderer`.

## 4. Game Loop and State Management
A playable game needs a proper game loop.
- **Resetting the Board:** A `GameReset()` function was created to handle resetting the game state without restarting the application. This involved clearing the logical grid, clearing the button sprites, and re-enabling all buttons.
- **Score Keeping:** A separate `Score.cs` script was created to manage the display of the score.
- **Architectural Refactor:** Initially, the `Score.cs` script used its `Update()` method to constantly check the `GameLogic` for the current score. This was refactored into a more efficient, event-driven pattern. The `GameLogic` now directly calls a public method in `Score.cs` to update the display *only when the score actually changes*. This demonstrates a key software design principle: **Separation of Concerns**.

## 5. Final Polish & Deployment
The final phase focused on polishing the user experience and preparing the project for distribution.
- **UI Polish:** The `CanvasScaler` was configured to `Scale With Screen Size` to ensure the UI looks good on different resolutions. The `Button` component's `Normal Color` was adjusted to pure white to prevent it from tinting the X/O sprites. Non-interactive background panels had their `Raycast Target` property disabled for optimization.
- **Version Control:** The project was prepared for Git by creating a standard Unity `.gitignore` file to exclude temporary and machine-specific folders like `Library` and `Temp`.
- **Building the Game:** Learned the difference between a GitHub Release (a snapshot of source code) and a playable Build (an executable for players), and how to create a build using Unity's `File > Build Settings`.

This project served as a comprehensive introduction to fundamental Unity concepts, from scripting and UI design to architecture and deployment.
