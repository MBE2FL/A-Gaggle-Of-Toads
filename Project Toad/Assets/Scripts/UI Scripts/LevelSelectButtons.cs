using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButtons : MonoBehaviour
{
    public void OnClick_LevelOne()
    {
        SceneManager.LoadScene("Level One");
    }

    public void OnClick_LevelTwo()
    {
        SceneManager.LoadScene("Level Two");
    }

    public void OnClick_LevelThree()
    {
        SceneManager.LoadScene("Level Three");
    }

    public void OnClick_LevelFour()
    {
        SceneManager.LoadScene("Level Four");
    }

    public void OnClick_BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }

}
