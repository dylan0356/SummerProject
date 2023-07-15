using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreScript : MonoBehaviour
{
    public static int highScore = 0;
    public GameObject highScoreUI;

    TextMeshProUGUI highScoreText;

    void Start()
    {
        highScoreText = highScoreUI.GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        highScoreText.text = "High Score: " + highScore;
        if (ScoreScript.scoreVal > highScore) {
            highScore = ScoreScript.scoreVal;
            highScoreText.text = "High Score: " + highScore;
        }
    }
}
