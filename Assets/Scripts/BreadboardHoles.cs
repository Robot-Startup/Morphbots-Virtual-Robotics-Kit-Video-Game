using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadboardHoles : MonoBehaviour
{
    void Start()
    {
        
    }
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 255f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }
}
