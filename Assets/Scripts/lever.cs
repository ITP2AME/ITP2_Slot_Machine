using UnityEngine;
using System.Collections;
using Meshadieme;

public class lever : MonoBehaviour {

    Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (GM.Get().framework.leverMode == false)
        {
            animator.SetBool("LeverPulled",true);
        }


      
	
	}
}
