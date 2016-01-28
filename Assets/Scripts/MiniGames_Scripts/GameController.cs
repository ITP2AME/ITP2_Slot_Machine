using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GUIText scoreText;
    private int Score;

    // Use this for initialization
    void Start () {

        Score = 0;
        UpdateScore();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void UpdateScore()
    {

        scoreText.text = "Score: " + Score;
    }

    public void AddScore(int NewScoreValue)
    {

        Score += NewScoreValue;
        UpdateScore();
    }
}
