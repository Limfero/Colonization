using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class UnitBuilder : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void Init(Vector3 target)
    {
        _agent.destination = target;
    }
}
