using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PourButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler// These are the interfaces the OnPointerUp method requires.
{
    public float pourSpeed = 0.001f;
    private GameObject substance;
    private bool isPouring = false;

    void Start()
    {
        substance = GameObject.FindWithTag("Substance");
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

    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        isPouring = true;
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        isPouring =false;
    }
}
