using System;
using UnityEngine;

public class RayCastScript : MonoBehaviour {
    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray ray = new Ray(transform.position, Vector3.right * 100f);
        Gizmos.DrawRay(ray);
    }
}
