using UnityEngine;
using System.Collections;

public class Fruit_Move : MonoBehaviour {

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

        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    this.transform.Translate(0.1f, 0.1f, 0);
        //}

    }

    void OnMouseDown()
    {
        // this object was clicked - do something

        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(transform.right * thrust);
            rb.AddForce(transform.up * thrust);

            Fruit_pressed.Play();
        }

       

        //Destroy(this.gameObject);
    }
}
