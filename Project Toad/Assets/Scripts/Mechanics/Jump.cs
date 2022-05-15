using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Jump : Mechanic
{
    bool _isJumping;
    bool _onGround;
    [SerializeField]
    float _jumpStrength = 5.0f;
    [SerializeField]
    float _groundCheckDistance = 0.1f;
    Collider _targetCollider;
    [SerializeField]
    int _jumpLayerMask = 1 << 7;
    Rigidbody _targetRigidbody;



    protected override void init()
    {
        base.init();
        InGameActions.Jump.performed += (context) => use();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPickedUp)
        {
            // Check if the character is on the ground or not.
            Vector3 origin = Target.transform.position;
            origin.y -= _targetCollider.bounds.extents.y;
            Ray ray = new Ray(origin, -Target.transform.up);
            Debug.DrawRay(origin, -Target.transform.up * _groundCheckDistance, Color.red);
            if (Physics.Raycast(ray, _groundCheckDistance, _jumpLayerMask))
            {
                _onGround = true;
            }
            else
            {
                _onGround = false;
            }

            // Reset jump flag upon landing back on the ground.
            if (_isJumping && _onGround)
            {
                _isJumping = false;
            }


            if (Keyboard.current.rKey.isPressed)
            {
                OnDropped();
            }
        }
    }

    public override void use()
    {
        if (!_isJumping && _onGround && IsPickedUp)
        {
            _isJumping = true;

            Vector3 jumpForce = _jumpStrength * Target.transform.up;
            _targetRigidbody.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    public override void onPickedUp(GameObject target)
    {
        base.onPickedUp(target);
        _isJumping = false;
        _targetCollider = target.GetComponent<Collider>();
        Debug.Assert(_targetCollider, "Jump: onPickedUp: Collider not assigned!");
        _targetRigidbody = target.GetComponentInParent<Rigidbody>();
        Debug.Assert(_targetRigidbody, "Jump: onPickedUp: Rigidbody not assigned!");
    }

    public override void OnDropped()
    {
        base.OnDropped();
        _isJumping = false;
        _targetCollider = null;
        _targetRigidbody = null;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.tag == "Player")
    //    {
    //        onPickedUp(other.gameObject);
    //    }
    //}
}
