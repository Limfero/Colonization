using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceFinder : MonoBehaviour
{
    [SerializeField] private float _scanningRadius;
    [SerializeField] private Finish _finish;

    private List<Resource> _busyResources = new();

    private void OnEnable()
    {
        _finish.ResourceCollected += RemoveResource;
    }

    private void OnDisable()
    {
        _finish.ResourceCollected -= RemoveResource;
    }

    public bool TryFind(out Resource target)
    {
        target = null;

        IEnumerable<Resource> hits = Physics.OverlapSphere(transform.position, _scanningRadius, LayerMask.GetMask(nameof(Resource)))
            .Select(hit => hit.GetComponent<Resource>())
            .Where(resource => _busyResources.Contains(resource) == false);

        target = hits.FirstOrDefault();
        _busyResources.Add(target);

        return target != null;
    }

    private void RemoveResource(Resource resource) => _busyResources.Remove(resource);
}
