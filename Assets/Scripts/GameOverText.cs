using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "Final Score " + PlayerPrefs.GetInt("score");

        // Stop Background Music if muted
        if (PlayerPrefs.GetInt("sound") == 0)
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
