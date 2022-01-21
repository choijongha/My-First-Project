using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StateCheckScript : MonoBehaviour, IPointerDownHandler
{

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    /*public void OnPointerDown(PointerEventData eventData)
    {

        clicked++;
        if (clicked == 1) clicktime = Time.time;
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            Debug.Log("Double CLick: " + this.GetComponent<RectTransform>().name);
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
    }*/
    //Detect current clicks on the GameObject (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        Debug.Log(this.gameObject.name + "Game Object Click in Progress");
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log(name + "No longer being clicked");
    }
}
