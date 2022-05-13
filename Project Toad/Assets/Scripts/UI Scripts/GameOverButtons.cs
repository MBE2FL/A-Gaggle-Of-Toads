using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void OnClick_TryAgain()
    {
        //load whatever scene the players were last in
        //SceneManager.LoadScene("Level Select Scene");
    }

    public void OnClick_GiveUp()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }
}