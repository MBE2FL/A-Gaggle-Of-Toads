using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    Camera _cam;
    Collider _collider;


    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        Debug.Assert(_cam, "Boundary: Start: Camera not assigned!");
        _collider = GetComponent<Collider>();
        Debug.Assert(_collider, "Boundary: Start: Collider not assigned!");
    }

    // Update is called once per frame
    void Update()
    {
        //// Get min and max points of the camera's viewport in world space.
        //// Note: Viewport to world space is flipped.
        //Vector2 screenWorldMax = _cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, _cam.transform.position.z));
        //Vector2 screenWorldMin = _cam.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, _cam.transform.position.z));

        //// Clamp this player's position, offsetted by it's colliders size, inside the screen's viewport.
        //Vector2 colliderHalfSize = _collider.bounds.extents;
        //Vector2 playerPos = transform.position;
        //playerPos.x = Mathf.Clamp(playerPos.x, screenWorldMin.x + colliderHalfSize.x, screenWorldMax.x - colliderHalfSize.x);
        //playerPos.y = Mathf.Clamp(playerPos.y, screenWorldMin.y + colliderHalfSize.y, screenWorldMax.y - colliderHalfSize.y);
        //transform.position = playerPos;


        // Clamp this player's position, offsetted by it's colliders size, inside the screen's viewport.
        Vector2 viewDiff = Vector2.zero;

        Vector2 colliderHalfSize = _collider.bounds.extents;

        Vector2 playerMinViewPos = transform.position;
        playerMinViewPos -= colliderHalfSize;
        playerMinViewPos = _cam.WorldToViewportPoint(playerMinViewPos);
        
        if (playerMinViewPos.x < 0.0f)
        {
            viewDiff.x -= playerMinViewPos.x;
        }
        if (playerMinViewPos.y < 0.0f)
        {
            viewDiff.y -= playerMinViewPos.y;
        }

        Vector2 playerMaxViewPos = transform.position;
        playerMaxViewPos += colliderHalfSize;
        playerMaxViewPos = _cam.WorldToViewportPoint(playerMaxViewPos);

        if (playerMaxViewPos.x > 1.0f)
        {
            viewDiff.x -= playerMaxViewPos.x;
        }
        if (playerMaxViewPos.y > 1.0f)
        {
            viewDiff.y -= playerMaxViewPos.y;
        }


        Vector3 currPos = transform.position;
        currPos += viewDiff.x * transform.right;
        currPos += viewDiff.y * transform.up;
        //transform.position = currPos;
    }

    public bool isAtScreenBoundary(Vector2 offset)
    {
        //Vector2 screenWorldMax = _cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, _cam.transform.position.z));
        //Vector2 screenWorldMin = _cam.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, _cam.transform.position.z));

        //// Clamp this player's position, offsetted by it's colliders size, inside the screen's viewport.
        //Vector2 colliderHalfSize = _collider.bounds.extents;
        //Vector2 playerPos = transform.position;
        //return ((playerPos.x - colliderHalfSize.x - offset.x) < screenWorldMin.x) || ((playerPos.x + colliderHalfSize.x + offset.x) > screenWorldMax.x) ||
        //        ((playerPos.y - colliderHalfSize.y - offset.y) < screenWorldMin.y) || ((playerPos.y + colliderHalfSize.y + offset.y) > screenWorldMax.y);


        // Clamp this player's position, offsetted by it's colliders size, inside the screen's viewport.
        Vector2 colliderHalfSize = _collider.bounds.extents;
        Vector2 playerMinViewPos = transform.position;
        playerMinViewPos.x -= colliderHalfSize.x + offset.x;
        playerMinViewPos.y -= colliderHalfSize.y + offset.y;
        playerMinViewPos = _cam.WorldToViewportPoint(playerMinViewPos);

        Vector2 playerMaxViewPos = transform.position;
        playerMaxViewPos.x += colliderHalfSize.x + offset.x;
        playerMaxViewPos.y += colliderHalfSize.y + offset.y;
        playerMaxViewPos = _cam.WorldToViewportPoint(playerMaxViewPos);

        return (playerMinViewPos.x < 0.0f || playerMaxViewPos.x > 1.0f ||
                playerMinViewPos.y < 0.0f || playerMaxViewPos.y > 1.0f);
    }

    //void OnDrawGizmosSelected()
    //{
    //    // Draw a semitransparent blue cube at the transforms position
    //    Gizmos.color = new Color(1, 0, 0, 1.0f);
    //    Gizmos.DrawCube(_cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, _cam.transform.position.z)), new Vector3(1, 1, 1));
    //    Gizmos.DrawCube(_cam.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, _cam.transform.position.z)), new Vector3(1, 1, 1));
    //}
}
