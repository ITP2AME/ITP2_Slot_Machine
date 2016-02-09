using Meshadieme;
using UnityEngine;
using System.Collections;

//Game controller for Mini game 2
public class GameController_2 : MonoBehaviour {

    //Declaration of variables and other useful objects
    public GUIText scoreText;
    public GUIText timeText;

    public int Score;
    public float timeRemaining;

    public GameObject CherryPrefab;
    public GameObject LemonPrefab;
    public GameObject OrangePrefab;
    public GameObject BananaPrefab;

    Fruit_Generator newGen;

    // Use this for initialization
    public void Init()
    {
        newGen = GameObject.FindWithTag("Gen2").GetComponent<Fruit_Generator>();
        newGen.Init();

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

    //Updates the score displayed
    void UpdateScore()
    {
        scoreText.text = "Score: " + Score;
    }

    //Adds points to the score
    public void AddScore(int NewScoreValue)
    {

        Score += NewScoreValue;
        UpdateScore();
    }

    //Subtracts points to the score
    public void SubtractScore(int NewScoreValue)
    {
        if (Score > 0)
        {
            Score -= NewScoreValue;
            UpdateScore();
        }
    }

    //Updates the remaining time for the minigame
    void UpdateTime()
    {
        if (timeRemaining > 0) { timeText.text = "Time: " + (int)timeRemaining; }
        else                   {timeText.text = "Time's up! "; GM.Get().framework.endMiniGame();}
    }

}
