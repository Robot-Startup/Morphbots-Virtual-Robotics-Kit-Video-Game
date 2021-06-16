using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

///<summary> Handles UI toggle buttons </summary>
public class IconToggle : MonoBehaviour
{
    public Sprite m_iconTrue;
    public Sprite m_iconFalse;
    public bool m_defaulIconState = true;
    Image m_image;

    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponent<Image>();
        m_image.sprite = (m_defaulIconState) ? m_iconTrue:m_iconFalse;
    }

    // Update is called once per frame
    public void ToggleIcon(bool state)
    {
        if (!m_image || !m_iconTrue || !m_iconFalse)
        {
            Debug.LogWarning("WARNING! IconToggle MISSING iconTrue of iconFalse!");
            return;
        }
        m_image.sprite = (state) ? m_iconTrue : m_iconFalse;
    }
}
