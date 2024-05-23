using System;
using UnityEngine;

[Serializable]
public struct CubePoolController
{
    [SerializeField] private Cube _template;
    [Space]
    [SerializeField][Range(10, 30)] private int _startCapacity;

    private Pool<Cube> _pool;

    public readonly Cube GetCube(Vector3 position) =>
        _pool.GetObject(position);

    public readonly void ReturnCube(Cube cube) =>
        _pool.ReturnObject(cube);

    public void Init() =>
        _pool = new(_template, _startCapacity);
}