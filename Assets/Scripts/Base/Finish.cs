using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public event Action Finished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            PickingObject pickingObject = unit.GetComponentInChildren<PickingObject>();

            if (pickingObject == null)
                return;

            pickingObject.BreakFree();
            pickingObject.Relese();
            unit.BreakFree();
            unit.Relese();

            Finished?.Invoke();
        }
    }
}
