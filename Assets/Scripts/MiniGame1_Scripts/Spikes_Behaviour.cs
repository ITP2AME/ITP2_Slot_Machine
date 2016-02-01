using UnityEngine;
using System.Collections;

public class Spikes_Behaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Cherry" ||
           col.gameObject.tag == "Lemon"   ||
           col.gameObject.tag == "Orange"  ||
           col.gameObject.tag == "Banana")
        {

            Destroy(col.gameObject);

        }
    }
}
