using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform _playerOne;
    [SerializeField]
    Transform _playerTwo;
    Boundary _playerOneBoundary;
    Boundary _playerTwoBoundary;
    Camera _cam;
    Vector3 _targetPos;
    Vector3 _targetPosVel;
    [SerializeField]
    float _zoomSpeed = 1.0f;
    [SerializeField]
    float _currZoomLevel;
    [SerializeField]
    float _minZoomLevel;
    [SerializeField]
    float _maxZoomLevel;
    [SerializeField]
    float _camSpeed = 65.0f;

    CameraMoveState _currentMoveState;
    static CameraMoveIdleState _camMoveIdleState = new CameraMoveIdleState();
    static CameraMoveZoomOutState _camMoveZoomOutState = new CameraMoveZoomOutState();
    static CameraMoveZoomInState _camMoveZoomInState = new CameraMoveZoomInState();



    public Transform PlayerOne
    {
        get
        {
            return _playerOne;
        }
    }

    public Transform PlayerTwo
    {
        get
        {
            return _playerTwo;
        }
    }

    public Boundary PlayerOneBoundary
    {
        get
        {
            return _playerOneBoundary;
        }
    }

    public Boundary PlayerTwoBoundary
    {
        get
        {
            return _playerTwoBoundary;
        }
    }

    public Camera Camera
    {
        get
        {
            return _cam;
        }
    }

    public Transform CamTransform
    {
        get
        {
            return transform;
        }
    }

    public Vector3 TargetPos
    {
        get
        {
            return _targetPos;
        }
        set
        {
            _targetPos = value;
        }
    }

    public float ZoomSpeed
    {
        get
        {
            return _zoomSpeed;
        }
    }

    public float CurrZoomLevel
    {
        get
        {
            return _currZoomLevel;
        }
        set
        {
            _currZoomLevel = value;
        }
    }

    public float MinZoomLevel
    {
        get
        {
            return _minZoomLevel;
        }
    }

    public float MaxZoomLevel
    {
        get
        {
            return _maxZoomLevel;
        }
    }

    static public CameraMoveIdleState CamMoveIdleState
    {
        get
        {
            return _camMoveIdleState;
        }
    }

    static public CameraMoveZoomOutState CamMoveZoomOutState
    {
        get
        {
            return _camMoveZoomOutState;
        }
    }

    static public CameraMoveZoomInState CamMoveZoomInState
    {
        get
        {
            return _camMoveZoomInState;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(_playerOne && _playerTwo, "CameraController: Start: Player one and/or two not assigned!");
        _cam = GetComponent<Camera>();
        Debug.Assert(_cam, "CameraController: Start: Camera not assigned!");
        //_targetPos.z = transform.position.z;
        _currZoomLevel = _minZoomLevel;

        _playerOneBoundary = _playerOne.GetComponentInParent<Boundary>();
        Debug.Assert(_playerOneBoundary, "CameraController: Start: Player one's boundary not assigned!");
        _playerTwoBoundary = _playerTwo.GetComponentInParent<Boundary>();
        Debug.Assert(_playerTwoBoundary, "CameraController: Start: Player two's boundary not assigned!");


        _currentMoveState = _camMoveIdleState;
        _currentMoveState.onEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        CameraMoveState newMoveState = _currentMoveState.onUpdate();
        if (newMoveState != null)
        {
            _currentMoveState.onExit();
            _currentMoveState = newMoveState;
            _currentMoveState.onEnter(this);
        }


        // Smoothly move the camera to it's target position.
        transform.position = Vector3.SmoothDamp(transform.position, _targetPos, ref _targetPosVel, Time.deltaTime * _camSpeed);
    }

    //void OldUpdate()
    //{
    //    // Keep camera in the middle of both players.
    //    Vector3 medianPosition = (_playerOne.position + _playerTwo.position) * 0.5f;
    //    Vector3 rayDir = (transform.position - medianPosition).normalized;
    //    _targetPos = medianPosition + (rayDir * _currZoomLevel);

    //    Vector2 medianViewPosition = _cam.WorldToViewportPoint(medianPosition);
    //    Vector2 medianDiff = medianViewPosition - new Vector2(0.5f, 0.5f);
    //    _targetPos += medianDiff.x * transform.right;
    //    _targetPos += medianDiff.y * transform.up;

    //    Debug.DrawRay(medianPosition, rayDir, Color.red);
    //    Ray midCamRay = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
    //    Debug.DrawRay(midCamRay.origin, midCamRay.direction, Color.blue);

    //    //bool arePlayersOverScreenBoundary = _playerOneBoundary.isOverScreenBoundary(new Vector2(1.0f, 1.0f)) ||
    //    //                                    _playerTwoBoundary.isOverScreenBoundary(new Vector2(1.0f, 1.0f));

    //    bool arePlayersInScreenBoundary = _playerOneBoundary.isInScreenBoundary(new Vector2(1.0f, 1.0f)) ||
    //                                _playerTwoBoundary.isInScreenBoundary(new Vector2(1.0f, 1.0f));

    //    // Zoom out when players are at screen boundary.
    //    //if (arePlayersOverScreenBoundary ||
    //    //    _currZoomLevel < (_minZoomLevel - 0.05f))
    //    if (!arePlayersInScreenBoundary ||
    //        _currZoomLevel < (_minZoomLevel - 0.01f))
    //    {
    //        _currZoomLevel += _zoomSpeed;
    //        _currZoomLevel = Mathf.Min(_currZoomLevel, _maxZoomLevel);
    //        _isZoomingOut = true;
    //        _canZoomIn = false;
    //    }
    //    // Both players are onscreen, but the camera is still zoomed out.
    //    else if ((_currZoomLevel > _minZoomLevel) && _canZoomIn)
    //    {
    //        if (_playerOneBoundary.isInScreenBoundary(new Vector2(5.0f, 5.0f)) ||
    //            _playerTwoBoundary.isInScreenBoundary(new Vector2(5.0f, 5.0f)))
    //        {
    //            _currZoomLevel = Mathf.Max(_currZoomLevel, _minZoomLevel);
    //            _currZoomLevel -= _zoomSpeed;
    //        }
    //        else
    //        {
    //            _canZoomIn = false;
    //        }
    //    }


    //    // Smoothly move the camera to it's target position.
    //    transform.position = Vector3.SmoothDamp(transform.position, _targetPos, ref _targetPosVel, Time.deltaTime * _camSpeed);
    //}
}
