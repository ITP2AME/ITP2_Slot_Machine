using UnityEngine;
using System.Collections;

public class Wood_spin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //Adds Rotation to the woos panels
        this.transform.Rotate(0, 0, -1.5f);
    }
}
