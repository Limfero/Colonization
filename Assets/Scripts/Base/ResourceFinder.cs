using System.Linq;
using UnityEngine;

public class ResourceFinder : MonoBehaviour
{
    [SerializeField] private float _scanningRadius;
    [SerializeField] private ResourceBase _resourceBase;

    public bool TryFind(out Resource target)
    {
        target = Physics.OverlapSphere(transform.position, _scanningRadius, LayerMask.GetMask(nameof(Resource)))
            .Select(hit => hit.GetComponent<Resource>())
            .Where(resource => _resourceBase.CheckResourceOnBusy(resource) == false)
            .FirstOrDefault();

        if(target != null)
            _resourceBase.AddBusyResource(target);

        return target != null;
    }
}
