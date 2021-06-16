using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems; // This is so that you can extend the pointer handlers
 
public class ColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler { // Extends the pointer handlers
 
    //public Text label;
    Toggle m_Toggle;
    public Text m_Text;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

        //Initialise the Text to say the first state of the Toggle
        //m_Text.text = "First Value : " + m_Toggle.isOn;
    }

    //Output the new state of the Toggle into Text
    public void ToggleValueChanged(Toggle change)
    {
        m_Text.color =  Color.green;
    }

    // Test for enter and exit:
    public void OnPointerEnter(PointerEventData eventData) {
          //GetComponent<Text>().color = Color.green; // Changes the color of the text
          m_Text.color =  Color.blue;
    }
 
    public void OnPointerExit(PointerEventData eventData) {
          //GetComponent<Text>().color = Color.white; // Changes the color of the text
          m_Text.color =  Color.white;
    }

    /*public void TurnGreen(){
        label.color = Color.green;
        //GetComponent<Text>().color = Color.green;
    }*/
}
