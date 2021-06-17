using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class LineUpdater : MonoBehaviour
{
    private UILineRenderer connect;

    /* The start and end of the wire (basically what its connecting)
       I left this as gameObjects instead of transforms or vector2's
       so tracing connections is easier later on
       
       i also left them public so its easier to test and debug with them
       in the editor. They can be privatized if this causes issues */
    public GameObject start;
    public GameObject end;

    // Start is called before the first frame update
    void Start()
    {
        /* Reference the linerenderer and assign its required array of points */
        connect = gameObject.GetComponent<UILineRenderer>();
        connect.Points = new Vector2[2];

        gameObject.transform.parent = gameObject.transform.parent.parent;
    }

    // Update is called once per frame
    void Update()
    {
        /* Update all points */
        connect.Points[0] = start.transform.position;
        connect.Points[1] = end.transform.position;
        connect.SetAllDirty();

        /* not sure if this works properly BUT it supposed
           to destroy the line if the connection is broken 
           (Both if statements) */
        if (!start)
            Destroy(gameObject);

        if (end == null)
        {
            Destroy(gameObject);
            Debug.Log("Destroyed connection");
        }
            
    }

    /* Update the line in case it stays rendered and doesnt update after being 
       destroyed by something else */
    void OnDestroy()
    {
        connect.SetAllDirty();
    }
}
