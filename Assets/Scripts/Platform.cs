using UnityEngine;

public class Platform : MonoBehaviour 
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent(out Cube cube))
            if (cube.TryChangeColor())
                StartCoroutine(cube.ReturnInPool());
    }
}