using UnityEngine;

public class GridNode
{
    public bool isTaken;

    public GameObject currentMorphBot = null;

    public Vector3Int localPos;

    public GridNode(bool _isTaken, Vector3Int _localPos)
    {
        isTaken = _isTaken;
        localPos = _localPos;
    }
}
