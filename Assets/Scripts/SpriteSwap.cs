using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour 
{
    public static SpriteSwap instance;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public int currentSprite;
    public Image morphie;

    void Start()
        {
            //Fetch the Image from the GameObject
            morphie = GetComponent<Image>();
        }

    public void RandomChangeSprite()
    {
        //public Image image = gameObject.GetComponent<Image>().SourceImage;
        //spriteRenderer.sprite = spriteArray[Random.Range(0, spriteArray.Length)];
        morphie.sprite = spriteArray[Random.Range(0, spriteArray.Length)];
    }

    public void SpecificChangeSprite()
    {
        spriteRenderer.sprite = spriteArray[0]; 
    }

    public void IncrementChangeSprite()
    {
        //spriteRenderer.sprite = spriteArray[currentSprite];
        morphie.sprite = spriteArray[currentSprite];
        currentSprite++;

        if(currentSprite >= spriteArray.Length)
        {
            currentSprite = 0;
        }
    }

    public IEnumerator SwapExpressions()
	{
    	morphie.sprite = spriteArray[currentSprite];
		currentSprite++;

        if(currentSprite >= spriteArray.Length)
        {
            currentSprite = 0;
            yield return new WaitForSeconds (2f);
        }
		//yield return new WaitForSeconds (1f);
    }
}
