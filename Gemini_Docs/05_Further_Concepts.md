# Unity Documentation: Further Concepts

This document covers concepts that go slightly beyond the immediate needs of Tic-Tac-Toe but will be highly relevant for a more complex game like Chess.

## 1. Instantiating GameObjects from Prefabs

In Tic-Tac-Toe, your 9 cells are static; they are always there. In Chess, you will want to create the pieces dynamically and place them on the board. This is done by creating **Prefabs**.

A Prefab is a "master copy" of a GameObject that lives in your `Assets` folder. You can create new instances of this Prefab at any time using `Instantiate()`.

**Workflow:**

1.  **Create a Prefab**:
    -   Design your GameObject in the scene exactly how you want it (e.g., a "Pawn" GameObject with a `SpriteRenderer` component and a script for its logic).
    -   Drag the configured GameObject from the Hierarchy window into your `Assets` folder. This creates the Prefab asset.
    -   You can now delete the original GameObject from the scene.

2.  **Instantiate from Code**:
    -   In your game logic script, create a public field to hold a reference to the Prefab.
    -   Drag the Prefab from your `Assets` folder into this slot in the Inspector.
    -   Call `Instantiate()` to create a new copy.

**Example:**

```csharp
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // Drag your "Pawn" prefab here in the Inspector
    public GameObject pawnPrefab;

    // Drag your "Rook" prefab here
    public GameObject rookPrefab;

    void Start()
    {
        // Create a new pawn at position (0, 0, 0) with no rotation.
        GameObject newPawn = Instantiate(pawnPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Create a new rook at position (4, 0, 0).
        GameObject newRook = Instantiate(rookPrefab);
        newRook.transform.position = new Vector3(4, 0, 0);
    }
}
```
This is the fundamental technique for creating any dynamic object in a game, such as spawning enemies, firing projectiles, or setting up a chess board.

## 2. The New Input System

While the `OnClick` event is perfect for UI buttons, for a game like chess you'll want more direct control over mouse clicks on the board itself. The legacy `Input.GetMouseButtonDown(0)` works, but the modern approach is to use the **Input System Package**.

**Core Concepts:**

-   **Input Actions**: You create an "Input Actions" asset that defines *what* the player can do, separate from *how* they do it. For example, you define an action called "Select Piece".
-   **Binding**: You then *bind* that "Select Piece" action to a specific input, like the "Left Mouse Button". You could easily add another binding for "Gamepad A Button" without changing any code.
-   **PlayerInput Component**: You add a `PlayerInput` component to your player object, link it to your Input Actions asset, and tell it how to notify your scripts.
-   **Events**: You can have the `PlayerInput` component call functions on your scripts directly (similar to `OnClick`), for example `OnSelectPiece(InputValue value)`.

This system is more complex to set up initially but is far more flexible and powerful for non-UI input handling. You would use it to detect which square on the chessboard a player clicked.

## 3. Coroutines

A coroutine is like a function that has the ability to pause its execution and return control to Unity, but then continue where it left off on a later frame.

They are perfect for creating sequences of events or actions that happen over time without freezing the entire game (which is what `Thread.Sleep()` would do).

**Use Cases:**

-   Fading a screen to black over 2 seconds.
-   Making an AI wait for 1 second before making its move.
-   Animating a piece moving from one square to another smoothly.

**Syntax:**

A coroutine is a method that returns `IEnumerator` and uses `yield return` statements.

```csharp
using System.Collections;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public void MakeMoveAfterDelay()
    {
        // Start the coroutine.
        StartCoroutine(MakeMoveRoutine());
    }

    private IEnumerator MakeMoveRoutine()
    {
        Debug.Log("AI is thinking...");

        // Pause execution for 1.5 seconds.
        yield return new WaitForSeconds(1.5f);

        Debug.Log("AI makes its move!");

        // You could add more steps here, with more delays.
        yield return new WaitForSeconds(0.5f);

        Debug.Log("AI move animation finished.");
    }
}
```
