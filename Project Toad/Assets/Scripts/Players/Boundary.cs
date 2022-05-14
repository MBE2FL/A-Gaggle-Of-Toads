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
        Debug.Assert(_collider, "Boundary: Start: Box collider not assigned!");
    }

    // Update is called once per frame
    void Update()
    {
        // Get min and max points of the camera's viewport in world space.
        // Note: Viewport to world space is flipped.
        Vector2 screenWorldMax = _cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, _cam.transform.position.z));
        Vector2 screenWorldMin = _cam.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, _cam.transform.position.z));

        // Clamp this player's position, offsetted by it's colliders size, inside the screen's viewport.
        Vector2 colliderHalfSize = _collider.bounds.extents;
        Vector2 playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, screenWorldMin.x + colliderHalfSize.x, screenWorldMax.x - colliderHalfSize.x);
        playerPos.y = Mathf.Clamp(playerPos.y, screenWorldMin.y + colliderHalfSize.y, screenWorldMax.y - colliderHalfSize.y);
        transform.position = playerPos;
    }

    public bool isAtScreenBoundary(Vector2 offset)
    {
        Vector2 screenWorldMax = _cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, _cam.transform.position.z));
        Vector2 screenWorldMin = _cam.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, _cam.transform.position.z));

        // Clamp this player's position, offsetted by it's colliders size, inside the screen's viewport.
        Vector2 colliderHalfSize = _collider.bounds.extents;
        Vector2 playerPos = transform.position;
        return ((playerPos.x - colliderHalfSize.x - offset.x) < screenWorldMin.x) || ((playerPos.x + colliderHalfSize.x + offset.x) > screenWorldMax.x) ||
                ((playerPos.y - colliderHalfSize.y - offset.y) < screenWorldMin.y) || ((playerPos.y + colliderHalfSize.y + offset.y) > screenWorldMax.y);
    }
}
