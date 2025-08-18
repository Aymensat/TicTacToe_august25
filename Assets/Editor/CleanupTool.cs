using UnityEngine;
using UnityEditor;

public class NewEmptyCSharpScript
{
    // This creates a new menu item at the top of the Unity editor
    [MenuItem("My Tools/Delete Child Text Objects")]
    private static void DeleteChildTextObjects()
    {
        // Find all game objects with the "GridButton" tag
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("GridButton");

        foreach (GameObject button in buttons)
        {
            // Look for a child object named exactly "Text"
            Transform textChild = button.transform.Find("Text (TMP)");

            if (textChild != null)
            {
                // Destroy the child object. Use DestroyImmediate for editor scripts.
                Object.DestroyImmediate(textChild.gameObject);
                Debug.Log("Destroyed Text child on button: " + button.name);
            }
            else Debug.Log("text is null ?"); 
        }
    }
}