using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // This loads the given scene and removes the previous scene.
    public void LoadLevelSingle(string levelPath)
    {
        LoadLevel(levelPath, LoadSceneMode.Single);
    }
    // This loads the previous scene ontop of the given scene.
    public void LoadLevelAdditive(string levelPath)
    {
        LoadLevel(levelPath, LoadSceneMode.Additive);
    }
    // This loads the GUI scene ontop of a given scene.
    public void LoadLevelGUI(string levelPath)
    {
        LoadIntoLevelGUI(levelPath);
    }
    // This loads the scene with the loadscenemode provided.
    private void LoadLevel(string levelPath, LoadSceneMode mode)
    {
        SceneManager.LoadSceneAsync(levelPath, mode);
    }
    // This loads the given scene with the gui ontop.
    private void LoadIntoLevelGUI(string levelPath)
    {
        SceneManager.LoadSceneAsync(levelPath, LoadSceneMode.Single);
        SceneManager.LoadSceneAsync("Assets/Scenes/GUI.Unity", LoadSceneMode.Additive);
    }

}
