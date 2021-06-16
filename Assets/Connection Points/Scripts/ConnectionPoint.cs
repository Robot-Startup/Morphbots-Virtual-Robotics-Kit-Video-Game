using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

public class ConnectionPoint : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public CircuitManager circuitManager;

    // Line renderer to reference as the visual connection
    public UILineRenderer linePrefab;

    // Color the point changes to while being interacted with
    public Color i_color = Color.green;

    // Previous Color (Whatever color the user set it to)
    private Color p_color;

    /* The connection point we're connected to
       Set this public when debugging */
    private GameObject target = null;

    /* This is purely for tracing connections through
       the breadboard, think if it like a linkedlist node
       
       it may be necessary to add a second connection to make
       this into a doubly linked list node. */
    public GameObject circuitConnection;

    private RectTransform icon;
    private Image img;
    private UILineRenderer line = null;
    private LineUpdater l_updater;


    // Start is called before the first frame update
    void Start()
    {
        circuitManager = GameObject.Find("BreadboardHoles").GetComponent<CircuitManager>();
        icon = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        p_color = img.color;
    }

    // When the user begins dragging from the connection point
    public void OnBeginDrag(PointerEventData eventData)
    {
        // remove any connection associated with the drag point from the linked list of components
        // (it will either be a new connection or no connection when the dragging is finished.)
        if (this.l_updater != null) {
            RemoveAssociatedConnection(this.l_updater.start, this.l_updater.end);
        }

        img.color = i_color;

        // Create a line connection if there isnt a line connected
        if (line == null)
        {
            line = Instantiate(linePrefab);
            line.transform.parent = (img.transform.parent);

            line.Points = new Vector2[2];

            line.Points[0] = transform.position;
            line.Points[1] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            /* The LineUpdater will break since were tracking the mouse position
               and not a gameObject position so we disable it and track things
               manually in the onDrag function (For now.) it gets re-enabled
               once the user 'connects' to another connection point */
            l_updater = line.GetComponent<LineUpdater>();
            l_updater.enabled = false;
        }
        //transform.position = Input.mousePosition;
    }


    // While the user is dragging
    public void OnDrag(PointerEventData eventData)
    {
        // Updates the wire to track the mouse while the user holds left click
        line.Points[0] = transform.position;
        line.Points[1] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        line.SetAllDirty();
    }

    // Once the user lets go of the mouse
    public void OnEndDrag(PointerEventData eventData)
    {
        /* Search through all the objects we hovered over 
           and set the connection to the most recent
           connection point */
        for (int i = 0; i < eventData.hovered.Count; i++)
        {
            // Debug.Log(eventData.hovered[i].gameObject.name);

            // Did we find another connection object?
            if (eventData.hovered[i].gameObject.GetComponent("ConnectionPoint") != null)
            {
                // Target the object and set the wires target to the discovered object
                target = eventData.hovered[i].gameObject;

                /* Checks if theres already a wire connection between the two CPs */
                if (target.GetComponent<ConnectionPoint>().target)
                {
                    // edge case to re-add connection to linked list of connections anyway
                    AddAssociatedConnection(this.l_updater.start, this.l_updater.end);

                    Debug.Log("Connection already established, ignoring...");
                    break;
                }

                line.Points[1] = target.transform.position;

                // Set cp color back to its original color
                img.color = p_color;

                /* We need to update the line updater to track both sides of the
                   connection points. This is necessary because without an updater
                   the lines will not move when the connection points move */
                LineUpdater l_updater = line.GetComponent<LineUpdater>();
                l_updater.start = gameObject;
                l_updater.end = target;
                l_updater.enabled = true;
        
                // Add connection to linked list of connections
                AddAssociatedConnection(l_updater.start, l_updater.end);

                return;
            }
        }

        /* No connection was found, Destroy the line and set back to original color */
        Destroy(line.gameObject);
        img.color = p_color;
    }

    // Add connection from the linked list in CircuitManager
    void AddAssociatedConnection(GameObject a, GameObject b) {
        if (b.transform.parent.gameObject.tag == "Battery" || b.transform.parent.gameObject.tag == "LED" || b.transform.parent.gameObject.tag == "Resistor") {
            circuitManager.OnConnect(a, b.transform.parent.gameObject);
        }
        if (a.transform.parent.gameObject.tag == "Battery" || a.transform.parent.gameObject.tag == "LED" || a.transform.parent.gameObject.tag == "Resistor") {
            circuitManager.OnConnect(b, a.transform.parent.gameObject);
        }
    }

    // Remove connection from the linked list in CircuitManager
    void RemoveAssociatedConnection(GameObject a, GameObject b) {
        if (b.transform.parent.gameObject.tag == "Battery" || b.transform.parent.gameObject.tag == "LED" || b.transform.parent.gameObject.tag == "Resistor") {
            circuitManager.OnDisconnect(a, b.transform.parent.gameObject);
        }
        if (a.transform.parent.gameObject.tag == "Battery" || a.transform.parent.gameObject.tag == "LED" || a.transform.parent.gameObject.tag == "Resistor") {
            circuitManager.OnDisconnect(b, a.transform.parent.gameObject);
        }
    }

}
