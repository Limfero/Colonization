using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T[] Prefab;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private int _poolMaxSize = 3;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Spawn(),
            actionOnGet: (obj) => Get(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public T GetObject(Vector3 positon)
    {
        T obj = _pool.Get();
        obj.transform.position = positon;

        return obj;
    }

    public void Relese(T obj) => _pool.Release(obj);

    protected abstract T Spawn();

    private void Get(T obj)
    {
        obj.gameObject.SetActive(true);
    }
}