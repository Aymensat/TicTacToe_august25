# Unity Documentation: Core Concepts

This document covers the absolute fundamentals of the Unity editor and engine.

## 1. The Unity Project Structure

When you create a Unity project, several folders are made. The two most important for you are:

-   **`Assets` Folder**: This is where you put everything that will be in your game. Your C# scripts, scenes, images (sprites), 3D models, audio files, etc. All your work happens here.
-   **`ProjectSettings` Folder**: This contains configuration files for your project, such as physics settings, graphics quality, input bindings, and more. You will occasionally modify things here through the Unity Editor's menus (`Edit -> Project Settings`).
-   **`Packages` Folder**: This contains the extra features or libraries you've added to your project via the Unity Package Manager (e.g., TextMeshPro, the new Input System).

## 2. GameObjects and Components (The Core Idea of Unity)

Everything in a Unity scene is a **GameObject**. Think of a GameObject as an empty container. By itself, it does nothing. You give GameObjects functionality by attaching **Components** to them.

-   **GameObject**: A container in your scene. It has a name, a tag, a layer, and most importantly, a `Transform` component.
-   **Component**: A piece of functionality. Examples include:
    -   **`Transform`**: Every GameObject has one. It defines the GameObject's Position, Rotation, and Scale in the game world.
    -   **`MeshRenderer`**: Renders a 3D mesh.
    -   **`SpriteRenderer`**: Renders a 2D sprite.
    -   **`Button`**: A UI component that makes a GameObject clickable.
    -   **`AudioSource`**: A component to play sounds.
    -   **Your C# Scripts**: When you create a C# script and attach it to a GameObject, it becomes a component of that object.

**Key Takeaway**: The "entity-component system" is Unity's core design. You build complex behaviors by creating GameObjects and attaching various combinations of components to them.

## 3. The `Transform` Component

This is the most fundamental component. You will interact with it constantly from your scripts.

**Common Properties:**

-   `transform.position`: The world-space position of the object (a `Vector3`).
-   `transform.rotation`: The world-space rotation of the object (a `Quaternion`).
-   `transform.localScale`: The local-scale of the object relative to its parent (a `Vector3`).

**Example in C#:**

```csharp
// To get a reference to the Transform component of the GameObject this script is on:
Transform myTransform = this.transform;

// To move the object 5 units to the right:
myTransform.position = new Vector3(5, 0, 0);
```

## 4. Prefabs

A **Prefab** is a reusable GameObject that you save in your `Assets` folder. It's a template. If you want to create many copies of the same object (like enemies, bullets, or even Tic-Tac-Toe pieces), you create one perfect version of it in the scene, then drag it from the Hierarchy into the `Assets` folder to create a Prefab.

You can then "instantiate" (create copies of) this Prefab at runtime using your scripts. If you modify the main Prefab asset, all instances of that Prefab in your scene will be updated.
