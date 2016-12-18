using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MobilInput : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler {
    public Homepanel home;
    private Vector2 startpos,ondragpos;
    private float tempdelta;

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        startpos = eventData.position;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (tempdelta >= 1)
        {
            home.call(null, true, -1);
        }
        else if (tempdelta < 1)
        {
            home.call(null, true, 1);
        }
//        else {
//
//			Debug.Log("滑动瞬量不足tempdelta===="+tempdelta);
//        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        ondragpos = eventData.position - startpos;
        tempdelta = eventData.delta.x;
        home.sli(ondragpos);
       
    }
    void Drag() 
    {
    
    }
}
