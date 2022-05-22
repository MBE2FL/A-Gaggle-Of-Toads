using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraMoveState
{
    protected CameraController _camController;

    public virtual void onEnter(CameraController camController)
    {
        _camController = camController;
    }

    public abstract CameraMoveState onUpdate();

    public virtual void onExit()
    {

    }
}
