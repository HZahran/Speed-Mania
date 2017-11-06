using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleCollision : MonoBehaviour {

    public AudioClip crash, coin, radar;
    private AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Obstacle"))
        {
            //Game Over
            Debug.Log("Game Over");

            // Play Sound
            audioSrc.clip = crash;
            audioSrc.Play();

            // Set shared preferences score
            PlayerPrefs.SetInt("score", GameManager.score);

            // Game Over Scene
            SceneManager.LoadScene("GameOver");
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Radar"))
        {
            // Lose Score
            GameManager.LoseScore();

            // Play Sound
            audioSrc.clip = radar;
            audioSrc.Play();

        }
        else if (c.gameObject.CompareTag("Coin"))
        {
            // Gain Score
            GameManager.GainScore();

            // Destroy Coin
            Destroy(c.gameObject);

            // Play sound
            audioSrc.clip = coin;
            audioSrc.Play();

        }
        else if (c.gameObject.CompareTag("Floor"))
        {
            GameObject.Find("Floors").GetComponent<FloorGenerator>().CreateFloor();
        }
    }
}
