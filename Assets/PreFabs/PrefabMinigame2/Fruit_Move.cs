using UnityEngine;
using System.Collections;

public class Fruit_Move : MonoBehaviour {

    public Rigidbody2D rb;
    float thrust = 50f;

    // Use this for initialization
    void Start () {

        rb = this.GetComponent<Rigidbody2D>();

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

        //rb.transform.Translate(1.0f, 1.0f, 0);

        rb.AddForce(transform.right * thrust);
        rb.AddForce(transform.up * thrust);

        //Destroy(this.gameObject);
    }
}
