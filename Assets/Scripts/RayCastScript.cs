using System;
using UnityEngine;

public class RayCastScript : MonoBehaviour
{
    private const float Distance = 11f;
    private readonly Vector3 direction = Vector3.right * Distance;
    private int blockPieceLayerMask;

    private void Awake()
    {
        blockPieceLayerMask = LayerMask.GetMask("BlockPiece");
    }

    private void Update()
    {
        // if (Physics.Raycast(transform.position, direction, out RaycastHit hit, Distance))
        // {
        //     Debug.Log("HIT A BLOCK PIECE!!!");
        // }

        var hits = Physics.RaycastAll(transform.position, direction, Distance, blockPieceLayerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction);
    }
}
