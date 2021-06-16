using UnityEngine;

public class GridPlacement : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    [SerializeField] float raycastDistance;

    [SerializeField] GridFunctions gridFunctions;

    [SerializeField] GridMain gridMain;

    [SerializeField] GameObject placementBlockRef, morphBotsRef;

    [SerializeField] LayerMask gameLayers;

    [SerializeField] Transform gridParent, morphBotsParent;

    private GameObject hoveredMorphBot, placementBlock;

    private void Awake()
    {
        placementBlock = Instantiate(placementBlockRef, gridParent);
        placementBlock.name = "Placement Block";

        raycastDistance *= gridMain.gridScale;
    }

    private void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, raycastDistance, gameLayers))
        {
            Vector3Int newPos = gridFunctions.SnapToGrid(raycastHit);

            if (!gridFunctions.OutOfBounds(raycastHit.point) && !gridMain.grid[newPos.x, newPos.y, newPos.z].isTaken)
            {
                if (!placementBlock.activeSelf)
                {
                    placementBlock.SetActive(true);
                }

                placementBlock.transform.localPosition = newPos;

                if (Input.GetMouseButtonDown(0))
                {
                    GameObject morphBot = Instantiate(morphBotsRef, placementBlock.transform.position, Quaternion.identity, morphBotsParent);
                    Vector3Int pos = Vector3Int.RoundToInt(morphBot.transform.localPosition);

                    morphBot.name = "MorphBot(" + pos.x + ", " + pos.y + ", " + pos.z + ")";

                    gridMain.grid[pos.x, pos.y, pos.z].isTaken = true;
                    gridMain.grid[pos.x, pos.y, pos.z].currentMorphBot = morphBot;
                    gridMain.grid[pos.x, pos.y, pos.z].localPos = pos;
                }
            }

            else if (placementBlock.activeSelf)
            {
                placementBlock.SetActive(false);
            }

            // If staring at a MorphBot, select it
            if (raycastHit.transform.gameObject.layer == 8)
            {
                hoveredMorphBot = raycastHit.transform.gameObject;
            }

            // If staring at anything else, clear the current MorphBot
            else if (hoveredMorphBot != null)
            {
                hoveredMorphBot = null;
            }

            // Destroy MorphBot if one has been selected
            if (Input.GetMouseButtonDown(1) && hoveredMorphBot != null)
            {
                Vector3Int hoveredPos = Vector3Int.RoundToInt(hoveredMorphBot.transform.localPosition);
                gridMain.grid[hoveredPos.x, hoveredPos.y, hoveredPos.z].isTaken = false;
                gridMain.grid[hoveredPos.x, hoveredPos.y, hoveredPos.z].currentMorphBot = null;

                Destroy(hoveredMorphBot);
            }
        }

        else if (placementBlock.activeSelf)
        {
            placementBlock.SetActive(false);
        }
    }

    public void Disable()
    {
        placementBlock.SetActive(false);
        this.enabled = false;
    }
}
