using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    
    [SerializeField] int score = 0;
    [SerializeField] Text scoreText;
    float timer;
    int cachscore;
   

    // Use this for initialization
    void Start()
    {
       // int score = 0;
        scoreText.text = score.ToString();
    }

    void Update()
    {
        if (FindObjectOfType<Player>().isAlive)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                AddToScore(10);
                timer = 0;
                cachscore = score;
            }
        }
        else
        {
            score = cachscore/2 ;
           // scoreText.text = score.ToString();
        }

    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

}