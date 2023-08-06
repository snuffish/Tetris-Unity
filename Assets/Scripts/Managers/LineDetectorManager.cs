using System;
using UnityEngine;

public class LineDetectorManager : MonoBehaviour
{
    private readonly float rayDistance = 11f;
    // private GridMatrixCell[,] gridMatrix;

    private void Update()
    {
        for (int y = 0; y <= 19; y++)
        {
            CastRowRay(y);
        }
    }

    private void CastRowRay(int y)
    {
        var hits = Physics.RaycastAll(GetRowVector(y), Vector3.right, rayDistance);
        foreach (var hit in hits) {
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int y = 0; y <= 19; y++)
        {
            var rayDirection = Vector3.right * rayDistance;
            Gizmos.DrawRay(GetRowVector(y), rayDirection);
        }
    }

    private Vector3 GetRowVector(int y) => new(-1, y, 0);
}

// class GridMatrixCell
// {
//     public int X { get; }
//     public int Y { get; }
//
//     public GridMatrixCell(int x, int y)
//     {
//         X = x;
//         Y = y;
//     }
// }
