using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

   

   

    // Use this for initialization
    void Start()
    {

      


    }

    // Update is called once per frame
    void Update()
    {

    }

    //Detects collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        //In minigame 1 simply destroy the fruits
        if (col.gameObject.tag == "Cherry" ||
           col.gameObject.tag == "Lemon" ||
           col.gameObject.tag == "Orange" ||
           col.gameObject.tag == "Banana")
        {

            Destroy(col.gameObject);

        }

        //In minigame 2 also subtracts points
        if (col.gameObject.tag == "Cherry_2" ||
            col.gameObject.tag == "Lemon_2" ||
            col.gameObject.tag == "Orange_2" ||
            col.gameObject.tag == "Banana_2")
        {

            Destroy(col.gameObject);
            

        }


    }
}
