using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PourButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler// These are the interfaces the OnPointerUp method requires.
{
    public float pourSpeed = 0.001f;
    public float bubbleSpeed = 0.1f;
    public bool donePouring = false;

    public AudioSource pouringSFX;
    public GameManager gameManager;

    private GameObject substance;
    private GameObject bubbleMask;
    private GameObject flame;
    private GameObject pourColor;

    private bool isPouring = false;
    private float bubbleHight;

    void Start()
    {
        substance = GameObject.FindWithTag("Substance");
        bubbleMask = GameObject.FindWithTag("BubbleMask");
        flame = GameObject.FindWithTag("Flame");
        pourColor = GameObject.FindWithTag("PourColor");

        StartCoroutine(BubbleCoroutine());
    }

    void Update()
    {
        if (isPouring)
        {
            // Check if the substance's y scale is less than 0.19
            if (substance.transform.localScale.y < 0.15f)
            {
                var scale = substance.transform.localScale;
                scale.y += pourSpeed; // Increase the y scale
                substance.transform.localScale = scale;
            }
            else
            {
                StopPouring(); // Stop pouring when the y scale reaches 0.19
            }

            // Debugging to verify the y scale
            Debug.Log($"Substance Y Scale: {substance.transform.localScale.y}");
        }
    }

    private void StopPouring()
    {
        pourColor.GetComponent<SpriteRenderer>().enabled = false;
        isPouring = false;
        pouringSFX.Stop();
        Debug.Log("Pouring stopped: Substance y scale reached 0.19.");
    }

    private IEnumerator BubbleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            var bubbleScale = bubbleMask.transform.localScale;

            if (donePouring && bubbleScale.y< bubbleHight)
            {
                flame.GetComponent<SpriteRenderer>().enabled = true;
                bubbleScale.y += bubbleSpeed;
                bubbleMask.transform.localScale = bubbleScale;
            }
            else if(donePouring && bubbleMask.TryGetComponent(out BoxCollider2D collider2D)==false)
            {
                bubbleMask.AddComponent<BoxCollider2D>();
                bubbleMask.GetComponent<BoxCollider2D>().isTrigger = true;
                bubbleMask.GetComponent<BoxCollider2D>().offset= new Vector2(0,1f);
                bubbleMask.GetComponent<BoxCollider2D>().size= new Vector2(1f,0.01f);
            }
        }
    }

    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        if(GetComponent<Button>().interactable)
        {
            pourColor.GetComponent<SpriteRenderer>().enabled = true;
            isPouring = true;
            pouringSFX.Play();
        }
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        pourColor.GetComponent<SpriteRenderer>().enabled = false;
        isPouring =false;
        pouringSFX.Stop();
        bubbleHight = substance.transform.localScale.y +
            substance.transform.localScale.y * (gameManager.GetComponent<GameManager>().percentage/100f);

        // Disable button:
        GetComponent<Button>().interactable = false;

        StartCoroutine(DonePouringDelay());
    }

    IEnumerator DonePouringDelay()
    {
        yield return new WaitForSeconds(0.7f);
        donePouring = true;

    }
}
