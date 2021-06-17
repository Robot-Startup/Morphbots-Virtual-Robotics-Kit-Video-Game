using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class TextTypeWriter : MonoBehaviour 
{

	public Text txt;
	string story;
	AudioSource click;
	public AudioSource buttonSound;
	public AudioSource backgroundMusic;
	//public Sprite[] spriteArray;
    //public int currentSprite;
    //public Image morphie;
	public SpriteSwap spriteSwap;
	
	void Awake () 
	{
		//txt = GetComponent<Text> ();
		story = txt.text;
		txt.text = "";
		click = GetComponent<AudioSource>();
		//morphie = GetComponent<Image>();
		backgroundMusic.Play();

		// TODO: add optional delay when to start
		StartCoroutine ("PlayText");
	}

	public void PlayButtonSound()
	{
		buttonSound.Play();
	}

	IEnumerator PlayText()
	{
		//morphie.sprite = spriteArray[currentSprite];
		foreach (char c in story) 
		{
			txt.text += c;
			click.Play();

			//Call function from SpriteSwap.cs to change facial expressions
			//spriteSwap.IncrementChangeSprite();
			spriteSwap.StartCoroutine("SwapExpressions");
			yield return new WaitForSeconds (0.07f);

			//spriteSwap.StartCoroutine("SwapExpressions");
		}
	}

	/*IEnumerator SwapExpressions()
	{
    	morphie.sprite = spriteArray[currentSprite];
		currentSprite++;

        if(currentSprite >= spriteArray.Length)
        {
            currentSprite = 0;
        }
		yield return new WaitForSeconds (0.07f);
    }*/

}
