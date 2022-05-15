using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class PointToPointMovement : MonoBehaviour
{
    NavMeshAgent _agent;
    Vector3 _targetPos;
    [SerializeField]
    Vector3 _offset;
    [SerializeField]
    SplineContainer _movementPath;
    Vector3 _startingPos;
    [SerializeField]
    float _splineTotalTime;
    float _splineCurrTime;
    bool _reverseSpline;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        Debug.Assert(_agent, "PointToPointMovement: Start: NavMeshAgent not assigned!");


        if (!_movementPath)
        {
            _startingPos = transform.position;
            _targetPos = _startingPos + _offset;
            _agent.SetDestination(_targetPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(_agent.remainingDistance, 0.0f) && !_movementPath)
        {
            _offset *= -1.0f;
            _targetPos = _startingPos + _offset;
            _agent.SetDestination(_targetPos);
        }
        else
        {
            float splinePos = _splineCurrTime / _splineTotalTime;

            if (splinePos >= 0.0f && splinePos <= 1.0f)
            {
                _splineCurrTime += !_reverseSpline ? Time.deltaTime : -Time.deltaTime;
            }
            else
            {
                _reverseSpline = !_reverseSpline;
                _splineCurrTime += !_reverseSpline ? Time.deltaTime : -Time.deltaTime;
            }

            //_agent.SetDestination(_movementPath.EvaluatePosition(splinePos));
            _agent.nextPosition = _movementPath.EvaluatePosition(splinePos);
        }
    }
}
