using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour, IInPool
{
    private T _template;

    private Queue<T> _pool;

    public Pool(T obj, int startCapacity) =>
        Init(obj, startCapacity);
    
    public T GetObject(Vector3 position) 
    {
        if (_pool.Count == 0)
            PutObject();

        T obj = _pool.Dequeue();

        obj.transform.position = position;
        obj.gameObject.SetActive(true);

        return obj;
    }

    public void ReturnObject(T obj) 
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    private void PutObject() 
    {
        T obj = Object.Instantiate(_template);
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    private void Init(T template, int startCapacity)
    {
        _template = template;

        _pool = new Queue<T>();

        for (int i = 0; i < startCapacity; i++)
            PutObject();
    }
}