using System.Collections;
using System.Linq;
using UnityEngine;

public class ResourceSpawner : Spawner<PickingObject>
{
    [SerializeField] private Transform[] _spawnPosints;
    [SerializeField] private float _repeatRate = 3f;

    private float _checkRadius = 1f;

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    protected override PickingObject Spawn()
    {
        PickingObject @object = Instantiate(Prefab[Random.Range(0, Prefab.Length)], transform.position, Quaternion.identity);
        @object.Init(this);

        return @object;
    }

    private IEnumerator Countdown()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            Vector3 spawnPoint = _spawnPosints[Random.Range(0, _spawnPosints.Length)].position;

            if(Physics.OverlapSphere(spawnPoint, _checkRadius).Where(hit => hit.TryGetComponent(out PickingObject @object)).ToList().Count == 0)
                GetObject(spawnPoint);

            yield return wait;
        }
    }
}
