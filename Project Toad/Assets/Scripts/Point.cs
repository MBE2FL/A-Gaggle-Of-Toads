using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Point : MonoBehaviour
{
    public enum PointType
    {
        NONE,
        START,
        CHECK,
        END
    }

    public PointType type = PointType.NONE;


    void Start()
    {
        tag = "Point";
    }
}
