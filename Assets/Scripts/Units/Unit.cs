using System;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Unit : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent _agent;
    private Vector3 _basePoint;
    private bool _isBusy = false;

    public bool IsBusy => _isBusy;

    private void Awake()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void SetTargetToBase() => _agent.destination = _basePoint;

    public void Init(Vector3 targetPoint, Vector3 basePoint)
    {
        _agent.destination = targetPoint;
        _basePoint = basePoint;
    }

    public void Hold() => _isBusy = true;

    public void BreakFree() => _isBusy = false;

}
