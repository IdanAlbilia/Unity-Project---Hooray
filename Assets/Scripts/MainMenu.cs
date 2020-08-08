using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public bool finishedGame = false;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width/2 - 55, 10, 125, 50), "New Game")){
            PlayerPrefs.SetInt("Level Completed", 0);
            //SceneManager.LoadScene("Level 1");
            SceneManager.LoadScene("World");
        }
        if (PlayerPrefs.GetInt("Level Completed") > 0)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 55, 100, 125, 50), "Continue"))
               /* if (PlayerPrefs.GetInt("Level Completed") == 3)
                    finishedGame = true;
                else
                    //SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level Completed"));*/
             SceneManager.LoadScene("World");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 55, 200, 125, 50), "Quit"))
            Application.Quit();

        for (int i = 0; i < 4; i++) {
            if (PlayerPrefs.GetString("Level " + i + " token badge").Equals("Completed"))
                GUI.Label(new Rect(30, (i*20), 200, 200), "Level " + i + " token badge");
        }

        if (GUI.Button(new Rect(Screen.width * 7 / 8, Screen.height - 45, 125, 30), "Delete Progress"))
        {
            finishedGame = false;
            PlayerPrefs.DeleteAll();
        }

        if (finishedGame)
        {
            GUI.Label(new Rect(Screen.width / 3, 150, 350, 200), "You finished the game, press New Game to Start Over.");
        }

        GUI.Label(new Rect(30, Screen.height - 30, 350, 200), "You have " + PlayerPrefs.GetInt("Coins") + " coins.");
    }
}
