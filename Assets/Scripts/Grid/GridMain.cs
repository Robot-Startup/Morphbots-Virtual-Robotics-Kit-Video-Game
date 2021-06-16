using UnityEngine;

public class GridMain : MonoBehaviour
{
    public float gridScale;

    public GridNode[,,] grid;

    public Vector3 gridOrigin;

    public Vector3Int gridSize;

    [SerializeField] GameObject platformRef;

    [SerializeField] GridMode gridMode;

    [SerializeField] Transform gridParent, platformParent;

    private void Awake()
    {
        gridSize.y += 1;
        grid = new GridNode[gridSize.x, gridSize.y, gridSize.z];
    }

    private void Start()
    {
        gridParent.localScale = gridScale * Vector3.one;
        gridParent.position = gridOrigin;

        CreatePlatform();
        CreateGrid();

        gridMode.UpdateMode();
    }

    private void CreatePlatform()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.z; z++)
            {
                GameObject platform = Instantiate(platformRef, platformParent);

                platform.transform.localPosition = new Vector3Int(x, 0, z);
                platform.name = "Platform(" + x + ", 0, " + z + ")";

                grid[x, 0, z] = new GridNode(true, new Vector3Int(x, 0, z));
            }
        }
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 1; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    grid[x, y, z] = new GridNode(false, new Vector3Int(x, y, z));
                }
            }
        }
    }
}
