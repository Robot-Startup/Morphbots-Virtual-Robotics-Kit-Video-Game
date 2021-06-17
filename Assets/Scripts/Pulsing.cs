using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsing : MonoBehaviour
{
    private bool corountineAllowed;
    AudioSource buttonClick;
    public AudioSource selectedButton;

    // Start is called before the first frame update
    void Start()
    {
        corountineAllowed = true;
        buttonClick = GetComponent<AudioSource>();
        selectedButton = GetComponent<AudioSource>();
    }

    public void OnMouseOver() {
        if (corountineAllowed)
        {
            StartCoroutine("StartPulsing");
        }
    }

    private IEnumerator StartPulsing(){
        corountineAllowed = false;

        buttonClick.Play();
        for (float i = 0; i <= 1; i += 0.1f)
        {
            //buttonClick.Play();
            transform.localScale = new Vector3(
                (Mathf.Lerp(transform.localScale.x, transform.localScale.x + 0.025f, Mathf.SmoothStep(0f,1f,i))),
                (Mathf.Lerp(transform.localScale.y, transform.localScale.y + 0.025f, Mathf.SmoothStep(0f,1f,i))),
                (Mathf.Lerp(transform.localScale.z, transform.localScale.z + 0.025f, Mathf.SmoothStep(0f,1f,i))));
                yield return new WaitForSeconds(0.015f);
        }
        for (float i = 0f; i <= 1; i += 0.1f)
        {
            transform.localScale = new Vector3(
                (Mathf.Lerp(transform.localScale.x, transform.localScale.x - 0.025f, Mathf.SmoothStep(0f,1f,i))),
                (Mathf.Lerp(transform.localScale.y, transform.localScale.y - 0.025f, Mathf.SmoothStep(0f,1f,i))),
                (Mathf.Lerp(transform.localScale.z, transform.localScale.z - 0.025f, Mathf.SmoothStep(0f,1f,i))));
                yield return new WaitForSeconds(0.015f);
        }

        corountineAllowed = true;
    }

    public void SelectedButton()
    {
        selectedButton.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //OnMouseOver();
    }
}
