using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private int score = 0;
    public Text scoreText;

    public AudioSource bgm;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetScore(int line)
    {
        int currentAddScore = (int)Mathf.Pow(line, 2);

        score += currentAddScore;

        if (currentAddScore > 0)
        {
            GetComponents<AudioSource>()[0].Play();
        }

        scoreText.text = "Score:" + '\n' + score.ToString();
    }

    public void SetGameOver()
    {
        scoreText.text = "GAME OVER";
        GetComponents<AudioSource>()[1].Play();
        Time.timeScale = 0;
        bgm.Stop();
    }
}
