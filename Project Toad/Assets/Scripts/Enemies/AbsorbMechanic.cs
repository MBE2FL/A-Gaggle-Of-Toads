using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbMechanic : MonoBehaviour
{
    Mechanic _currentMechanic;
    float _totalAbsorbTime;
    float _currAbsorbTime;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentMechanic)
        {
            _currAbsorbTime += Time.deltaTime;

            if (_currAbsorbTime >= _totalAbsorbTime)
            {
                _currAbsorbTime = 0;
                _currentMechanic.use();
                _currentMechanic.PickedUp = false;
                _currentMechanic = null;
            }
        }
    }
}
