# Unity Documentation: C# Scripting

This document covers the basics of writing C# scripts for Unity.

## 1. `MonoBehaviour`

Your C# scripts will almost always inherit from the `UnityEngine.MonoBehaviour` class. This is the base class that allows a script to be attached to a GameObject as a component. It gives you access to a wide range of Unity's functionality and event functions.

```csharp
using UnityEngine;

// By adding ": MonoBehaviour", this script can be attached to GameObjects.
public class MyCustomScript : MonoBehaviour
{
    // ... script logic here ...
}
```

## 2. Common MonoBehaviour Event Functions (Lifecycle Methods)

`MonoBehaviour` provides special functions that Unity calls automatically at specific times. You don't call them yourself. You just declare them in your script, and Unity finds and executes them.

-   **`Awake()`**: Called once when the script instance is being loaded. It's the very first thing that happens, even before `Start()`. Use it for setting up references between scripts and initialization that must happen first.
-   **`Start()`**: Called once on the frame when a script is enabled, just before any of the `Update()` methods are called for the first time. Use it for most initialization tasks.
-   **`Update()`**: Called once every frame. This is where most of your game logic that needs to happen over time will go (e.g., checking for keyboard input, moving an object continuously).
-   **`FixedUpdate()`**: Called at a fixed time interval, independent of the frame rate. Use this for physics calculations (e.g., applying forces to a `Rigidbody`).
-   **`OnEnable()` / `OnDisable()`**: Called when the component is enabled or disabled.
-   **`OnDestroy()`**: Called just before the component is destroyed.

**Example:**

```csharp
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake: Script is loading.");
    }

    void Start()
    {
        Debug.Log("Start: Script is enabled and ready.");
    }

    void Update()
    {
        // This will run every single frame.
        // For example, check if the space bar is pressed.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space bar was pressed!");
        }
    }
}
```

## 3. Accessing Other Components and GameObjects

A script often needs to communicate with other components or GameObjects.

**A. Getting a Component on the *Same* GameObject**

Use `GetComponent<T>()`. This is very common.

```csharp
void Start()
{
    // Let's say this script is on a GameObject that also has a Rigidbody component.
    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.useGravity = false;
    }
}
```

**B. Finding Other GameObjects**

-   **`GameObject.Find("NameOfObject")`**: Searches the entire scene for a GameObject with that name. It's slow; avoid using it in `Update()`.
-   **`GameObject.FindWithTag("Player")`**: Finds the first active GameObject with the specified tag. Faster than finding by name.

**C. Public Fields (The Inspector Method)**

The easiest and most common way to get a reference to another object is to create a public field in your script. This creates a "slot" in the Inspector that you can drag another GameObject or Component into.

```csharp
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This creates a slot in the Inspector.
    // You can drag any GameObject with a PlayerController script on it into this slot.
    public PlayerController player;

    // This creates a slot for any GameObject.
    public GameObject enemyPrefab;

    void Start()
    {
        // Now you can access the player directly.
        if (player != null)
        {
            // ... do something with the player ...
        }
    }
}
```
This is the preferred method as it's fast, explicit, and doesn't rely on slow search functions.

## 4. `Debug.Log()`

The most essential tool for debugging. It prints a message to the Unity Console window. Use it liberally to understand the flow of your code and inspect the values of variables.

`Debug.Log("Hello World");`
`Debug.LogWarning("This is a warning.");`
`Debug.LogError("This is an error.");`
