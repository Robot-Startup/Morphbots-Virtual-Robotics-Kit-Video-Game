using UnityEngine;

public class GridFunctions : MonoBehaviour
{
    [SerializeField] GridMain gridMain;

    const float epsilon = 0.000001f;

    public Vector3Int SnapToGrid(RaycastHit raycastHit)
    {
        Vector3 rawPosition = transform.InverseTransformPoint(raycastHit.point - raycastHit.transform.position);
        Vector3Int newPosition = Vector3Int.RoundToInt(raycastHit.transform.localPosition);

        newPosition.x += (int)(rawPosition.x / gridMain.gridScale + Mathf.Sign(rawPosition.x) * 0.5f + Mathf.Sign(rawPosition.x) * epsilon);
        newPosition.y += (int)(rawPosition.y / gridMain.gridScale + Mathf.Sign(rawPosition.y) * 0.5f + Mathf.Sign(rawPosition.y) * epsilon);
        newPosition.z += (int)(rawPosition.z / gridMain.gridScale + Mathf.Sign(rawPosition.z) * 0.5f + Mathf.Sign(rawPosition.z) * epsilon);

        return newPosition;
    }

    public bool OutOfBounds(Vector3 point)
    {
        if (point.x <= gridMain.gridOrigin.x - gridMain.gridScale / 2 || point.y < gridMain.gridOrigin.y + gridMain.gridScale / 2 || point.z <= gridMain.gridOrigin.z - gridMain.gridScale / 2)
        {
            return true;
        }

        if (point.x >= gridMain.gridOrigin.x + (gridMain.gridSize.x - 0.5f) * gridMain.gridScale || point.y >= gridMain.gridOrigin.y + (gridMain.gridSize.y - 0.5f) * gridMain.gridScale || point.z >= gridMain.gridOrigin.z + (gridMain.gridSize.z - 0.5f) * gridMain.gridScale)
        {
            return true;
        }

        return false;
    }
}
