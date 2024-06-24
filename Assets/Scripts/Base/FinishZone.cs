using System;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField] private Spawner<Resource> _resourceSpawner;
    [SerializeField] private ResourceBase _resourceBase;

    public event Action<Unit> UnitBack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit) && unit.IsBusy == true)
        {
            unit.BreakFree(_resourceSpawner, _resourceBase);

            UnitBack?.Invoke(unit);
        }
    }
}
