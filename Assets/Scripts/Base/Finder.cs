using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Finder : MonoBehaviour
{
    [SerializeField] private float _scanningRadius;

    public bool TryFind(out Queue<PickingObject> targets)
    {
        targets = new Queue<PickingObject>();

        List<PickingObject> hits = Physics.OverlapSphere(transform.position, _scanningRadius)
            .Where(hit => hit.TryGetComponent(out PickingObject pickingObject))
            .Select(hit => hit.GetComponent<PickingObject>())
            .Where(hit => hit.IsBusy == false)
            .ToList();

        foreach (var hit in hits)
            targets.Enqueue(hit);

        return targets.Count > 0;
    }
}
