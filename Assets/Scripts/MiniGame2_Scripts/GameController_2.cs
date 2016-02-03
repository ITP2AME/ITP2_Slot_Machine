using UnityEngine;
using System.Collections;

public class GameController_2 : MonoBehaviour {

    public GUIText scoreText;
    public GUIText timeText;

    private int Score;
    public float timeRemaining;

    public GameObject CherryPrefab;
    public GameObject LemonPrefab;
    public GameObject OrangePrefab;
    public GameObject BananaPrefab;

    // Use this for initialization
    void Start()
    {

        CherryPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        LemonPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        OrangePrefab.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        BananaPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.1f;

        timeRemaining = 120;
        Score = 0;
        UpdateScore();


    }

    // Update is called once per frame
    void Update()
    {

        timeRemaining -= Time.deltaTime;
        UpdateTime();
      

        if (Score < 0)
        { Score = 0; }
        UpdateScore();

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
        else
        {
            timeText.text = "Time's up! ";
            //Add Exit Game code.
        }
    }

    //void UpdateDifficulty()
    //{
    //    //Increasing Gravity when 30 seconds are left
    //    if (timeRemaining < 30)
    //    {
    //        CherryPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
    //        LemonPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
    //        OrangePrefab.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
    //        BananaPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
    //    }

    //    //Further increasing gravity when 10 seconds are left
    //    if (timeRemaining <= 10)
    //    {
    //        CherryPrefab.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    //        LemonPrefab.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    //        OrangePrefab.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    //        BananaPrefab.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    //    }

    //}
}
