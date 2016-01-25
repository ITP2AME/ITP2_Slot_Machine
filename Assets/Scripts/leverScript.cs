using Meshadieme;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class leverScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform trans;

    void Awake()
    {
        trans = GetComponent<Transform>();
    }

    // Use this for initialization
    public void OnBeginDrag(PointerEventData e)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData e)
    {
        Debug.Log("OnEndDrag");
    }
    public void OnDrag(PointerEventData e)
    {
        Debug.Log("OnDrag");
        if (GM.Get().framework.leverMode)
        {
            Debug.Log(e.position.x + " _ " + e.position.y);
            if (e.position.y > 100 && e.position.y < 310) trans.position = new Vector3( trans.position.x, e.position.y, trans.position.z );
        } else
        {
            if (e.position.x > 100 && e.position.x < 310) trans.position = new Vector3( e.position.x, trans.position.y, trans.position.z);
        }
    }
}


