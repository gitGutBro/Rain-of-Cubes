using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour, IInPool
{
    private const float MinBackPoolDelay = 2.5f;
    private const float MaxBackPoolDelay = 5f;

    private Renderer _renderer;
    private bool _isColored;

    public event Action<Cube> ReturnedInPool;
    private WaitForSecondsRealtime BackInPoolDelay => new(UnityEngine.Random.Range(MinBackPoolDelay, MaxBackPoolDelay));

    private void Awake() =>
        Init();

    public bool TryChangeColor()
    {
        if (_isColored)
            return false;

        ChangeColor();
        _isColored = true;

        return true;
    }

    public IEnumerator ReturnInPool()
    {
        yield return BackInPoolDelay;

        _renderer.material.color = Color.white;
        _isColored = false;

        ReturnedInPool?.Invoke(this);
    }

    private void ChangeColor() =>
        _renderer.material.color = UnityEngine.Random.ColorHSV();

    private void Init()
    {
        _renderer = GetComponent<Renderer>();

        _isColored = false;
    }
}