using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void OnClick_StartGame()
    {
        SceneManager.LoadScene("Player Join Scene");
    }

    public void OnClick_LevelSelect()
    {
        SceneManager.LoadScene("Level Select Scene");
    }

    public void OnClick_Options()
    {
        //pops up/ pops down the options
    }

    public void OnClick_Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
