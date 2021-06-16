using UnityEngine;

public class GridMode : MonoBehaviour
{
    [SerializeField] GridCompression gridCompression;

    [SerializeField] GridPlacement gridPlacement;

    [SerializeField] KeyCode switchKey;

    [SerializeField] mode currentMode;

    private enum mode { MOVEMENT, PLACEMENT }

    public void UpdateMode()
    {
        switch (currentMode)
        {
            case mode.MOVEMENT:
                currentMode = mode.PLACEMENT;
                break;

            case mode.PLACEMENT:
                currentMode = mode.MOVEMENT;
                break;
        }

        switch (currentMode)
        {
            case mode.MOVEMENT:
                gridCompression.enabled = true;
                gridPlacement.Disable();

                gridCompression.Compress();
                break;

            case mode.PLACEMENT:
                gridCompression.enabled = false;
                gridPlacement.enabled = true;
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            UpdateMode();
        }
    }
}
