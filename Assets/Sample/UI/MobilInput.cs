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
        startpos =eventData.position;
       // startpos = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log(">>>OnPointerUp>>>>>>>"+ondragpos.x);
        if (tempdelta >= 1 && ondragpos.x >200)
        {
            home.call(null, true, -1);
        }
        else if (tempdelta < 1 && ondragpos.x <-200)
        {
            home.call(null, true, 1);
        }
        else
        {
            /*滑动不足时，返回滑动值*/
            home.sli(-ondragpos,true);
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        ondragpos = eventData.position - startpos;
       // float t = (ondragpos.x) / home.topitemslength;
        tempdelta = eventData.delta.x;/*拖动的瞬时增量*/
        home.sli(eventData.delta * 5 ,false);    /*滑动过程中，改变位置*/

    }
    void Drag() 
    {
    
    }
}
