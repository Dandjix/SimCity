#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoadAttribute]
public static class DefaultSceneLoader
{
    static DefaultSceneLoader()
    {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    static void LoadDefaultScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            ClearConsole();
            EditorSceneManager.LoadScene(1);
        }
    }

    private static void ClearConsole()
    {
        // This will use reflection to call the internal method to clear the console.
        Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        System.Type type = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo method = type.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        method.Invoke(null, null);
    }
}
#endif
