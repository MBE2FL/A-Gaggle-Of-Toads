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
    float _currZoomLevel;
    [SerializeField]
    float _minZoomLevel;
    [SerializeField]
    float _maxZoomLevel;
    [SerializeField]
    float _camSpeed = 65.0f;
    bool _shouldZoomIn = false;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(_playerOne && _playerTwo, "CameraController: Start: Player one and/or two not assigned!");
        _cam = GetComponent<Camera>();
        Debug.Assert(_cam, "CameraController: Start: Camera not assigned!");
        //_targetPos.z = transform.position.z;
        _currZoomLevel = _minZoomLevel;

        _playerOneBoundary = _playerOne.GetComponentInChildren<Boundary>();
        Debug.Assert(_playerOneBoundary, "CameraController: Start: Player one's boundary not assigned!");
        _playerTwoBoundary = _playerTwo.GetComponentInChildren<Boundary>();
        Debug.Assert(_playerTwoBoundary, "CameraController: Start: Player two's boundary not assigned!");


        StartCoroutine(waitForZoomOut());
    }

    // Update is called once per frame
    void Update()
    {
        // Keep camera in the middle of both players.
        //Vector2 medianPosition = (_playerOne.position + _playerTwo.position) * 0.5f;
        //_targetPos = new Vector3(medianPosition.x, medianPosition.y, _targetPos.z);
        Vector3 medianPosition = (_playerOne.position + _playerTwo.position) * 0.5f;
        Vector3 rayDir = (transform.position - medianPosition).normalized;
        _targetPos = medianPosition + (rayDir * _currZoomLevel);

        Vector2 medianViewPosition = _cam.WorldToViewportPoint(medianPosition);
        Vector2 medianDiff = medianViewPosition - new Vector2(0.5f, 0.5f);
        _targetPos += medianDiff.x * transform.right;
        _targetPos += medianDiff.y * transform.up;

        Debug.DrawRay(medianPosition, rayDir, Color.red);
        Ray midCamRay = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(midCamRay.origin, midCamRay.direction, Color.blue);

        bool arePlayersOverScreenBoundary = _playerOneBoundary.isOverScreenBoundary(new Vector2(1.0f, 1.0f)) ||
                                            _playerTwoBoundary.isOverScreenBoundary(new Vector2(1.0f, 1.0f));

        bool arePlayersInScreenBoundary = _playerOneBoundary.isOverScreenBoundary(new Vector2(-2.0f, -2.0f)) ||
                                    _playerTwoBoundary.isOverScreenBoundary(new Vector2(-2.0f, -2.0f));


        // Zoom out when players are at screen boundary.
        if (arePlayersOverScreenBoundary ||
            //transform.position.z > (_minZoomFactor + 0.05f))
            _currZoomLevel < (_minZoomLevel - 0.05f))
        {
            _currZoomLevel += _zoomSpeed;
            _currZoomLevel = Mathf.Min(_currZoomLevel, _maxZoomLevel);
            _isZoomingOut = true;
            _shouldZoomIn = false;
        }
        // Both players are onscreen, but the camera is still zoomed out.
        else if ((_currZoomLevel > _minZoomLevel) && _shouldZoomIn && !arePlayersInScreenBoundary)
        {
            //// Wait for the camera to finish zooming out bofore zooming in.
                _currZoomLevel = Mathf.Max(_currZoomLevel, _minZoomLevel);
                _currZoomLevel -= _zoomSpeed;
        }
        // Make sure zoom is properly reset.
        //else
        //{
        //    _isZoomingOut = false;
        //    _shouldZoomIn = false;
        //}


        // Smoothly move the camera to it's target position.
        transform.position = Vector3.SmoothDamp(transform.position, _targetPos, ref _targetPosVel, Time.deltaTime * _camSpeed);
    }

    IEnumerator waitForZoomOut()
    {
        while (true)
        {
            while (_isZoomingOut)
            {
                yield return new WaitForSeconds(1.0f);

                if (Vector3.Distance(transform.position, _targetPos) < 0.5f)
                {
                    _isZoomingOut = false;
                    break;
                }
            }

            if (!_shouldZoomIn)
            {
                _shouldZoomIn = true;
            }

            yield return null;
        }
    }
}
