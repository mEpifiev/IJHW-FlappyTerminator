using System.Collections.Generic;
using UnityEngine;

public class GenericPool<T> : MonoBehaviour where T : MonoBehaviour
{
    private Queue<T> _pool = new();
    private List<T> _allObjects = new();

    public IEnumerable<T> AllObjects => _allObjects;

    public T GetObject(T @object)
    {
        if (_pool.Count == 0)
            Create(@object);

        return _pool.Dequeue(); 
    }

    public void ReleaseObject(T @object)
    {
        @object.gameObject.SetActive(false);
        _pool.Enqueue(@object);
    }

    public void ReleaseAllObjects()
    {
        foreach (T @object in _allObjects)
            if(@object.gameObject.activeSelf)
                ReleaseObject(@object);
    }

    private T Create(T prefab)
    {
        T @object = Instantiate(prefab);

        @object.gameObject.SetActive(false);
        _allObjects.Add(@object);
        _pool.Enqueue(@object);

        return @object;
    }
}
