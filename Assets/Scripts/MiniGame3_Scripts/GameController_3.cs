﻿using UnityEngine;
using System.Collections;

public class GameController_3 : MonoBehaviour {

    public GUIText scoreText;
    public GUIText timeText;

    private int Score;
    public float timeRemaining;

    public AudioSource Good_Fruit;
    public AudioSource Bad_Fruit;

    public GameObject CherryPrefab;
    public GameObject LemonPrefab;
    public GameObject OrangePrefab;
    public GameObject BananaPrefab;

    // Use this for initialization
    void Start()
    {

        //probably not needed here
        CherryPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        LemonPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        OrangePrefab.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        BananaPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.1f;

        timeRemaining = 60;
        Score = 0;
        UpdateScore();


    }

    // Update is called once per frame
    void Update()
    {

        timeRemaining -= Time.deltaTime;
        UpdateTime();
        UpdateDifficulty();

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
        Good_Fruit.Play();
        if (timeRemaining > 30) { Score += NewScoreValue; }
        if (timeRemaining <= 30 && timeRemaining > 10) { Score += NewScoreValue * 2; }
        if (timeRemaining <= 10) { Score += NewScoreValue * 3; }

        UpdateScore();
    }

    public void SubtractScore(int NewScoreValue)
    {
        Bad_Fruit.Play();
        if (Score > 0)
        {
            if (timeRemaining > 30) { Score -= NewScoreValue; }
            if (timeRemaining <= 30 && timeRemaining > 10) { Score -= NewScoreValue * 2; }
            if (timeRemaining <= 10) { Score -= NewScoreValue * 3; }

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

    void UpdateDifficulty()
    {
        //Increasing Gravity when 30 seconds are left
        if (timeRemaining < 30)
        {
            CherryPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            LemonPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            OrangePrefab.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            BananaPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        }

        //Further increasing gravity when 10 seconds are left
        if (timeRemaining <= 10)
        {
            CherryPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            LemonPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            OrangePrefab.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            BananaPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
        }

    }
}
