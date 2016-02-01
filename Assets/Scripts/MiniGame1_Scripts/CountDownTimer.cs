using UnityEngine;
using System.Collections;

public class CountDownTimer : MonoBehaviour {

    float timeRemaining = 60;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        timeRemaining -= Time.deltaTime;

	}

    void OnGui() {

        if (timeRemaining > 0)
        {
            GUI.Label(new Rect(100, 100, 200, 100), "Time Remaining: " + timeRemaining);
        }

        else { GUI.Label(new Rect(100, 100, 100, 100), "Time's up!! "); }
    }
}
