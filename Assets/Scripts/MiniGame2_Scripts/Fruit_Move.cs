using UnityEngine;
using System.Collections;

//Deals with interation with the fruits (moving them on click)
public class Fruit_Move : MonoBehaviour {

    //Declaration of vaiables and other useful objects
    public Rigidbody2D rb;
    float thrust = 100f;
    public AudioSource Fruit_pressed;

    // Use this for initialization
    void Start () {

        rb = this.GetComponent<Rigidbody2D>();
        Fruit_pressed = this.GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update () {

        
    }

    //Function that moves the fruits (add forces) when they re clicked
    void OnMouseDown()
    {
    

        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(transform.right * thrust);
            rb.AddForce(transform.up * thrust);

            Fruit_pressed.Play();
        }

    }
}
