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
    bool _isZoomingOut = false;
    [SerializeField]
    float _minZoomFactor;
    [SerializeField]
    float _maxZoomFactor;
    [SerializeField]
    float _camSpeed = 65.0f;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(_playerOne && _playerTwo, "CameraController: Start: Player one and/or two not assigned!");
        _cam = GetComponent<Camera>();
        Debug.Assert(_cam, "CameraController: Start: Camera not assigned!");
        _targetPos.z = transform.position.z;

        _playerOneBoundary = _playerOne.GetComponent<Boundary>();
        Debug.Assert(_playerOneBoundary, "CameraController: Start: Player one's boundary not assigned!");
        _playerTwoBoundary = _playerTwo.GetComponent<Boundary>();
        Debug.Assert(_playerTwoBoundary, "CameraController: Start: Player two's boundary not assigned!");
    }

    // Update is called once per frame
    void Update()
    {
        // Keep camera in the middle of both players.
        Vector2 medianPosition = (_playerOne.position + _playerTwo.position) * 0.5f;
        _targetPos = new Vector3(medianPosition.x, medianPosition.y, _targetPos.z);


        // Zoom out when players are at screen boundary.
        if (_playerOneBoundary.isAtScreenBoundary(new Vector2(1.0f, 1.0f)) ||
            _playerTwoBoundary.isAtScreenBoundary(new Vector2(1.0f, 1.0f)) ||
            transform.position.z > (_minZoomFactor + 0.05f))
        {
            _targetPos = new Vector3(_targetPos.x, _targetPos.y, transform.position.z - _zoomSpeed);
            _targetPos.z = Mathf.Max(_targetPos.z, _maxZoomFactor);
            _isZoomingOut = true;
        }
        // Both players are onscreen, but the camera is still zoomed out.
        else if (!Mathf.Approximately(transform.position.z, _minZoomFactor) &&
                transform.position.z < _minZoomFactor)
        {
            // Wait for the camera to finish zooming out bofore zooming in.
            if (!_isZoomingOut)
            {
                _targetPos = new Vector3(_targetPos.x, _targetPos.y, transform.position.z + _zoomSpeed);
                _targetPos.z = Mathf.Min(_targetPos.z, _minZoomFactor);
            }
            else
            {
                _isZoomingOut = !Mathf.Approximately(transform.position.z, _targetPos.z);
            }
        }
        // Make sure zoom is properly reset.
        else
        {
            _isZoomingOut = false;
        }


        // Smoothly move the camera to it's target position.
        transform.position = Vector3.SmoothDamp(transform.position, _targetPos, ref _targetPosVel, Time.deltaTime * _camSpeed);
    }
}
