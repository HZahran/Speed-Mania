using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gm; // Applying Singleton Pattern

    // Score
    public static int score = 0;
    private const int scoreChange = 10;

    // Floor dimensions
    public static float width = 6f;
    public static float length = 150f;

    // Lanes middle positions on x
    public static Dictionary<Lane, float> lanePositions;

    //void Awake()
    //{
    //    score = 0;
    //    DontDestroyOnLoad(gameObject);
    //    if (gm == null)
    //        gm = this;
    //    else if (gm != this)
    //        Destroy(gameObject);
    //}

    // Use this for initialization
    void Start () {

        gm = this;

        // Reset Score
        score = 0;

        // Assign Lanes
        lanePositions = new Dictionary<Lane, float>();
        float laneMidSize = width / 4; //half lane size

        lanePositions.Add(Lane.Left, - laneMidSize);
        lanePositions.Add(Lane.Middle, 0);
        lanePositions.Add(Lane.Right, laneMidSize);

        // Stop Background Music if muted
        if (PlayerPrefs.GetInt("sound") == 0)
            GetComponent<AudioSource>().enabled = false;
	}

    public static void GainScore()
    {
        score += scoreChange;
        GameObject.Find("Score").GetComponent<Text>().text = "Score : " + score;
    }

    public static void LoseScore()
    {
        score -= scoreChange * 5;
        score = Math.Max(score, 0);
        GameObject.Find("Score").GetComponent<Text>().text = "Score : " + score;
    }
}
