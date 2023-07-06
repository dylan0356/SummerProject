using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public static int scoreVal = 0;
    public GameObject scoreUI;

    TextMeshProUGUI scoreTextUI;

    
    void Start()
    {
        scoreTextUI = scoreUI.GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        scoreTextUI.text = "Score: " + scoreVal;
    }

    public static void AddScore(int score)
    {
        scoreVal += score;
    }

    public static void ResetScore() {
        scoreVal = 0;
    }
}
