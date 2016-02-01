using UnityEngine;
using System.Collections;

public class Fruit_Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    this.transform.Translate(0.1f, 0.1f, 0);
        //}

    }

    void OnMouseDown()
    {
        // this object was clicked - do something

        this.transform.Translate(1.0f, 1.0f, 0);
        //Destroy(this.gameObject);
    }
}
