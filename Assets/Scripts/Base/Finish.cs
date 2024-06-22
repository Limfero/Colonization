using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private Spawner<Unit> _unitSpawner;
    [SerializeField] private Spawner<Resource> _resourceSpawner;

    public event Action UnitBack;
    public event Action<Resource> ResourceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit) && unit.IsBusy == true)
        {
            unit.BreakFree();
            _unitSpawner.Relese(unit);

            UnitBack?.Invoke();
        }

        if (other.TryGetComponent(out Resource resource))
        {
            resource.BreakFree();
            _resourceSpawner.Relese(resource);

            ResourceCollected?.Invoke(resource);
        }
    }
}
