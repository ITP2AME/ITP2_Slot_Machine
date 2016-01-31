using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GUIText scoreText;
    public GUIText timeText;

    private int Score;
    float timeRemaining = 60;

    // Use this for initialization
    void Start () {

        Score = 0;
        UpdateScore();
       

    }
	
	// Update is called once per frame
	void Update () {

        timeRemaining -= Time.deltaTime;
        UpdateTime();
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

    public void SubtractScore(int NewScoreValue)
    {
        if (Score > 0)
        {
            Score -= NewScoreValue;
            UpdateScore();
        }
    }


    void UpdateTime()
    {
        if (timeRemaining > 0) { timeText.text = "Time: " + (int)timeRemaining; }
        else { timeText.text = "Time's up! ";
            //Add Exit Game code.
        }
    }
}
