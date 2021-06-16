using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMenu : MonoBehaviour
{

    public GameObject PanelMenu;

    public void ShowHideMenu()
    {
        if(PanelMenu != null)
        {
            Animator anim = PanelMenu.GetComponent<Animator>();
            if(anim != null)
            {
                bool isOpen = anim.GetBool("Show");
                anim.SetBool("Show", !isOpen);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
