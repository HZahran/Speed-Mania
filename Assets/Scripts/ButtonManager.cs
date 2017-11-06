using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Transform mainMenu, optionsMenu;
    public Text info;
    private bool soundOn = true;

    public void Start()
    {
        //soundOn = PlayerPrefs.GetInt("sound") == 1 ? true : false;
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetInt("sound", soundOn ? 1 : 0);
    }

    public void ShowOptionsMenu(bool showOptions)
    {
        mainMenu.gameObject.SetActive(!showOptions);
        optionsMenu.gameObject.SetActive(showOptions);
        info.text = "";
    }

    public void MuteSound()
    {
        soundOn = !soundOn;
        if(!soundOn)
             GameObject.Find("Main Camera").GetComponent<AudioSource>().Pause();
        else
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
    }

    public void HowToPlay()
    {
        info.text = "\tPC\t / \tAndroid\n" +
            "Switch Lanes:\t" + "Left-Right or A-D\t /" + "\tAccelerometer\n" +
            "Jump:\t" + "Space\t /" + "\tJump Btn\n" +
            "Toggle Cameras:\t" + "C\t /" + "\tSwitch Cameras Btn\n" +
            "Pause:\t" + "Escape\t /" + "\tPause Btn";
    }
    public void Credits()
    {
        info.text = "Game Created By H Zahran\n" + "Sounds from www.soundbible.com\n" + "Car model made by Super Icon Ltd";
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
