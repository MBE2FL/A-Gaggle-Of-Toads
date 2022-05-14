using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mechanic : MonoBehaviour
{
    bool _pickedUp = false;


    public bool PickedUp
    {
        get
        {
            return _pickedUp;
        }
        set
        {
            _pickedUp = value;
            onPickedUpDown();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void use();

    void onPickedUpDown()
    {
        // Turn renderer and collisions off.
        if (_pickedUp)
        {

        }
        // Turn renderer and collisions on.
        else
        {

        }
    }
}
