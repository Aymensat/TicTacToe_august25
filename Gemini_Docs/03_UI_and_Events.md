# Unity Documentation: UI and Events

This document covers the basics of creating UI and handling user input through UI events, which is the core of your Tic-Tac-Toe game.

## 1. The Canvas

All UI elements in Unity must live inside a **Canvas**. The Canvas is a GameObject with a Canvas component that acts as the root for all your UI.

-   **Render Mode**: The most important setting on the Canvas.
    -   **Screen Space - Overlay**: This is the default. The UI is drawn on top of everything else in the scene. It's perfect for menus, health bars, etc.
    -   **Screen Space - Camera**: The UI is rendered by a specific camera. This allows you to have 3D objects appear in front of the UI.
    -   **World Space**: The Canvas behaves like any other object in the 3D world. Useful for interactive screens on a computer terminal inside your game, for example.

-   **Canvas Scaler**: A component on the Canvas that controls how your UI scales with different screen sizes. A common setting is `Scale With Screen Size`, which helps your UI look consistent across different resolutions.

## 2. RectTransform

Instead of the standard `Transform` component, all UI GameObjects have a **`RectTransform`**. It inherits from `Transform` but adds concepts needed for 2D layout, such as:

-   **Anchors**: The most important concept for responsive UI. Anchors define how the UI element's corners are attached to the parent `RectTransform`. If you anchor a health bar to the top-right corner, it will stay in the top-right corner even if the screen size changes.
-   **Pivot**: The point around which the `RectTransform` rotates and scales. It's represented as a normalized coordinate (0,0 is bottom-left, 1,1 is top-right).
-   **Width / Height**: The size of the UI element.

## 3. Common UI Components

-   **Panel**: A simple, flat-colored rectangle. Often used as a background container for other UI elements.
-   **Image**: Displays a 2D `Sprite`.
-   **Raw Image**: Displays any `Texture`.
-   **Text - TextMeshPro**: The modern, powerful text rendering solution in Unity. Always prefer this over the legacy "Text" component.
-   **Button**: The most important interactive element. It's a combination of an `Image` and a `Button` script.

## 4. The `Button` Component and `OnClick` Event

This is the "how-to" for making buttons do things.

The `Button` component has a special section called **`On Click ()`**. This is a list of actions to perform when the button is clicked. Each action is a call to a `public` function on a script.

**How to wire it up (The Static Parameter Method):**

This method allows you to call a public function that accepts a simple parameter (`int`, `string`, `bool`, `float`, or a Unity Object like `GameObject`).

1.  **The Script**: Write a `public` function in your script that takes one parameter.

    ```csharp
    // In a script like GameLogic.cs
    public void MyFunction(int someValue)
    {
        Debug.Log("Button was clicked! The value is: " + someValue);
    }

    public void AnotherFunction(string message)
    {
        Debug.Log("The message is: " + message);
    }
    ```

2.  **The Editor**:
    a. Select your `Button` in the scene.
    b. In the `On Click ()` panel, click the `+` to add a new slot.
    c. Drag the GameObject that has your script onto the `None (Object)` field.
    d. Click the `No Function` dropdown. At the top, switch from the `Dynamic` list to the **`Static`** list.
    e. Find your script name (`GameLogic`) and then select your function from the list (e.g., `MyFunction (int)`).
    f. **A new field will appear.** This is where you type the value that will be sent to the function when this specific button is clicked.

This technique is how you can have all 9 of your Tic-Tac-Toe cells call the *same* `OnCellClicked(int cellIndex)` function, but each one passes in a different index (0 through 8).

## 5. Layout Groups

To automatically arrange UI elements, you can use Layout Group components. You add the component to the *parent* GameObject.

-   **Grid Layout Group**: Arranges its children in a grid. Perfect for a Tic-Tac-Toe board, an inventory screen, or a level select menu. You can control the cell size, spacing, and constraints.
-   **Horizontal Layout Group**: Arranges its children in a single horizontal row.
-   **Vertical Layout Group**: Arranges its children in a single vertical column.
