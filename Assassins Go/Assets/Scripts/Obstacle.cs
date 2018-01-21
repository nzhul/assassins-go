using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Obstacle : MonoBehaviour
{

    //BoxCollider boxCollider;

    //private void Awake()
    //{
    //    boxCollider = GetComponent<BoxCollider>();
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, .5f);
        Gizmos.DrawCube(transform.position, new Vector3(1f, 1f, 1f));
    }

}
