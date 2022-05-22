using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveZoomInState : CameraMoveState
{
    const float _viewportLengthPercent = 100.0f;
    const float _zoomInThreshold = (0.15f * 0.15f) * 100.0f;

    public override CameraMoveState onUpdate()
    {
        // Keep camera in the middle of both players.
        Vector3 medianPosition = (_camController.PlayerOne.position + _camController.PlayerTwo.position) * 0.5f;
        Vector3 rayDir = (_camController.CamTransform.position - medianPosition).normalized;
        _camController.TargetPos = medianPosition + (rayDir * _camController.CurrZoomLevel);

        Vector2 medianViewPosition = _camController.Camera.WorldToViewportPoint(medianPosition);
        Vector2 medianDiff = medianViewPosition - new Vector2(0.5f, 0.5f);
        _camController.TargetPos += medianDiff.x * _camController.CamTransform.right;
        _camController.TargetPos += medianDiff.y * _camController.CamTransform.up;

        Debug.DrawRay(medianPosition, rayDir, Color.red);
        Ray midCamRay = _camController.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(midCamRay.origin, midCamRay.direction, Color.blue);


        // Check if the difference vector between the two players is longer than the viewport's vector.
        Vector2 playerOneMinPos = _camController.PlayerOneBoundary.getMinPosition();
        Vector2 playerTwoMinPos = _camController.PlayerTwoBoundary.getMinPosition();
        Vector2 playerOneMaxPos = _camController.PlayerOneBoundary.getMaxPosition();
        Vector2 playerTwoMaxPos = _camController.PlayerTwoBoundary.getMaxPosition();

        Vector2 playerMinPos;
        playerMinPos.x = playerOneMinPos.x <= playerTwoMinPos.x ? playerOneMinPos.x : playerTwoMinPos.x;
        playerMinPos.y = playerOneMinPos.y <= playerTwoMinPos.y ? playerOneMinPos.y : playerTwoMinPos.y;

        Vector2 playerMaxPos;
        playerMaxPos.x = playerOneMaxPos.x >= playerTwoMaxPos.x ? playerOneMaxPos.x : playerTwoMaxPos.x;
        playerMaxPos.y = playerOneMaxPos.y >= playerTwoMaxPos.y ? playerOneMaxPos.y : playerTwoMaxPos.y;

        Vector2 playerOneViewPos = _camController.Camera.WorldToViewportPoint(playerMinPos);
        Vector2 playerTwoViewPos = _camController.Camera.WorldToViewportPoint(playerMaxPos);
        float playerViewportDiff = _viewportLengthPercent - (playerOneViewPos - playerTwoViewPos).sqrMagnitude * 100.0f;

        // Decrease the zoom level.
        if ((playerViewportDiff > _zoomInThreshold) && (_camController.CurrZoomLevel > _camController.MinZoomLevel))
        {
            _camController.CurrZoomLevel -= _camController.ZoomSpeed;
            _camController.CurrZoomLevel = Mathf.Max(_camController.CurrZoomLevel, _camController.MinZoomLevel);
        }
        // Change to idle state.
        else
        {
            return CameraController.CamMoveIdleState;
        }

        // Remain in this state.
        return null;
    }
}
