using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class AnyKeyToBegin : MonoBehaviour
{
    private bool _wasPressed = false;

    void Update()
    {
        if(Keyboard.current.anyKey.wasPressedThisFrame && !_wasPressed ||
            Gamepad.current.startButton.wasPressedThisFrame && !_wasPressed)
        {
            _wasPressed = true;
            SceneManager.LoadScene("Main Menu Scene");
        }

    }


}
