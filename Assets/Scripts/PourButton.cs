using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PourButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler// These are the interfaces the OnPointerUp method requires.
{
    public float pourSpeed = 0.001f;
    public float bubbleSpeed = 0.1f;
    public int bubblePercentage = 50;
    
    private GameObject substance;
    private GameObject bubbleMask;

    private bool isPouring = false;
    private bool donePouring = false;
    private float bubbleHight;

    void Start()
    {
        substance = GameObject.FindWithTag("Substance");
        bubbleMask = GameObject.FindWithTag("BubbleMask");

        StartCoroutine(BubbleCoroutine());
    }

    void Update()
    {
        if (isPouring)
        {
            var scale = substance.transform.localScale;
            scale.y += pourSpeed;
            substance.transform.localScale = scale;
        }
    }

    private IEnumerator BubbleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            var bubbleScale = bubbleMask.transform.localScale;

            if (donePouring && bubbleScale.y< bubbleHight)
            {
                bubbleScale.y += bubbleSpeed;
                bubbleMask.transform.localScale = bubbleScale;
            }
        }
    }

    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        if(GetComponent<Button>().interactable)
        {
            isPouring = true;
        }
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        isPouring =false;
        bubbleHight = substance.transform.localScale.y +
            substance.transform.localScale.y * (bubblePercentage/100f);

        donePouring = true;

        // Disable button:
        GetComponent<Button>().interactable = false;
    }
}
