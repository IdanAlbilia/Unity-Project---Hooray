using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    //Score vars
    public int currentScore;
    private int highScore;
    public Rect scoreRect;
    public Rect highScoreRect;

    //Token vars
    private int tokenCount;
    public int myTokens = 0;
    public Rect tokenRect;

    //Level vars
    public int currentLevel;

    //Time vars
    public float startTime;
    private string currentTime;
    public Rect timerRect;
    public Color warningColorTimer;
    public Color defaultColorTimer;

    //GUI vars
    public GUISkin skin;
    public bool winWindow = false;

    //References
    public GameObject tokenParent;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        tokenCount = tokenParent.transform.childCount;
        //if (PlayerPrefs.GetInt("Level Completed") > 0)
       // {
        //    currentLevel = PlayerPrefs.GetInt("Level Completed");
        //}
       // else
       // {
       //     currentLevel = 1;
       // }
        if (PlayerPrefs.GetInt("Level " + currentLevel.ToString() + " score") > 0)
        {
            highScore = PlayerPrefs.GetInt("Level " + currentLevel.ToString() + " score");
        }
    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;
        currentTime = string.Format("{0:0.0}", startTime);
        if(startTime <= 0)
        {
            startTime = 0;
            Destroy(gameObject);
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void CompleteLevel()
    {
        SaveGame();
        winWindow = true;
    }

    void LoadLevel()
    {
        SceneManager.LoadScene("World");
        /*if (currentLevel < 3)
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
        else
        {
            print("You Win!");
            SceneManager.LoadScene("Main Menu");
        }*/
    }
    void SaveGame()
    {
        if (tokenCount == myTokens)
        {
            PlayerPrefs.SetString("Level " + currentLevel.ToString() + " token badge", "Completed");
        }
        if (currentScore > PlayerPrefs.GetInt("Level " + currentLevel.ToString() + " score"))
            PlayerPrefs.SetInt("Level " + currentLevel.ToString() + " score", currentScore);
        currentLevel++;
        PlayerPrefs.SetInt("Level Completed", currentLevel);
    }

    private void OnGUI()
    {
        GUI.skin = skin;
        if (startTime <= 10)
            skin.GetStyle("Timer").normal.textColor = warningColorTimer;
        else
            skin.GetStyle("Timer").normal.textColor = defaultColorTimer;
        GUI.Label(timerRect, "Time left: "+currentTime, skin.GetStyle("Timer"));
        GUI.Label(scoreRect, "Score: " + currentScore, skin.GetStyle("Score"));
        GUI.Label(highScoreRect, "High Score: " + highScore, skin.GetStyle("Score"));
        GUI.Label(tokenRect, "Tokens: " + myTokens + "/" + tokenCount);

        if (winWindow)
        {
            Time.timeScale = 0;
            Rect winScreenRect = new Rect(Screen.width/2 - (150/2), Screen.height/2 - (150/2), 150, 150);
            GUI.Box(winScreenRect, "Finished Level " + (currentLevel-1));
            GUI.Label(new Rect(winScreenRect.x + 36, winScreenRect.y + 25, 150, 150), "Score: " + currentScore);
            GUI.Label(new Rect(winScreenRect.x + 40, winScreenRect.y + 50, 150, 150), "Tokens: " + myTokens);


            if (GUI.Button(new Rect(winScreenRect.x + 36, winScreenRect.y + 74, 80, 22), "Continue"))
            {
                LoadLevel();
            }
            if (GUI.Button(new Rect(winScreenRect.x + 36, winScreenRect.y + 100, 80, 22), "Main Menu"))
                SceneManager.LoadScene("Main Menu");
            if (GUI.Button(new Rect(winScreenRect.x + 36, winScreenRect.y + 126, 80, 22), "Quit"))
                Application.Quit();
        }
    }
}
