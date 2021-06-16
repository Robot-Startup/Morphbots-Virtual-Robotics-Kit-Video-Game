using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    [SerializeField] float maxRotationX, minRotationX, movementSpeed, rotationSpeed, startingRotationX;

    [Range(0f, 360f)]
    [SerializeField] float startingRotationY;

    [SerializeField] KeyCode translateKey;

    [SerializeField] GridMain gridMain;

    private Vector3 rotation;

    private void Start()
    {
        rotation = this.transform.eulerAngles;

        // Sets the object's x and y rotations to startingRotationX (clamped) and startingRotationY (0 to 360 degrees) respectively
        rotation.x = Mathf.Clamp(startingRotationX, minRotationX, maxRotationX);
        rotation.y = startingRotationY;

        this.transform.eulerAngles = rotation;

        // Moves the object to the center of the instantiated platform
        this.transform.position = new Vector3((gridMain.gridSize.x - 1) * gridMain.gridScale / 2 + gridMain.gridOrigin.x, gridMain.gridScale / 2 + gridMain.gridOrigin.y, (gridMain.gridSize.z - 1) * gridMain.gridScale / 2 + gridMain.gridOrigin.z);
    }

    private void Update()
    {
        if (!Input.GetKey(translateKey))
        {
            float x = Input.GetAxisRaw("Vertical") * rotationSpeed * Time.deltaTime;
            float y = Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;

            rotation.x = Mathf.Clamp(rotation.x + x, minRotationX, maxRotationX);
            rotation.y -= y;

            this.transform.eulerAngles = rotation;
        }

        else
        {
            /*
            float x = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
            float z = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

            this.transform.Translate(x * transform.right + z * transform.forward);
            */
        }
    }
}