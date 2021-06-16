using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] float defaultZoom, maxZoom, minZoom, zoomSpeed;

    [SerializeField] GridMain gridMain;

    private Vector3 localPos;

    private void Awake()
    {
        float gScale = gridMain.gridScale;

        defaultZoom *= gScale;
        maxZoom *= gScale;
        minZoom *= gScale;
        zoomSpeed *= gScale;

        localPos = this.transform.localPosition;
        localPos.z = Mathf.Clamp(defaultZoom, minZoom, maxZoom);
        this.transform.localPosition = localPos;
    }

    private void Update()
    {
        float zoom = Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;

        localPos.z = Mathf.Clamp(localPos.z + zoom, minZoom, maxZoom);
        this.transform.localPosition = localPos;
    }
}
