using System;
using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField][Range(0.2f, 10f)] private float _delayInSeconds;
    [Space]
    [SerializeField] private CubePoolController _cubePoolController;

    private CubeSpawner[] _spawners;

    private int RandomSpawnerIndex => UnityEngine.Random.Range(0, _spawners.Length - 1);

    private void Awake() =>
        Init();

    private IEnumerator StartSpawn()
    {
        WaitForSecondsRealtime delay = new(_delayInSeconds);

        while(enabled)
        {
            CubeSpawner spawner = _spawners[RandomSpawnerIndex];
            Cube cube = _cubePoolController.GetCube(spawner.transform.position);
            cube.ReturnedInPool += ReturnCube;

            yield return delay;
        }
    }

    private void ReturnCube(Cube cube)
    {
        cube.ReturnedInPool -= ReturnCube;
        _cubePoolController.ReturnCube(cube);
    }

    private void Init()
    {
        _cubePoolController.Init();
        _spawners = GetComponentsInChildren<CubeSpawner>();

        StartCoroutine(StartSpawn());
    }
}