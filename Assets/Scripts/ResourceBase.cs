using System.Collections.Generic;
using UnityEngine;

public class ResourceBase : MonoBehaviour
{
    private List<Resource> _busyResources = new();

    public bool CheckResourceOnBusy(Resource resource) => _busyResources.Contains(resource);

    public void AddBusyResource(Resource resource) => _busyResources.Add(resource);

    public void RemoveResource(Resource resource) => _busyResources.Remove(resource);
}
