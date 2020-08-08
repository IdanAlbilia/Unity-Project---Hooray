using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int levelToLoad;
    private bool showDetails = false;
    private bool canLoadLevel = false;
    public GameObject padLock;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        int completedLevel = PlayerPrefs.GetInt("Level Completed");
        completedLevel = completedLevel < 1 ? 1 : completedLevel;
        canLoadLevel = levelToLoad <= completedLevel ? true : false;
        if (!canLoadLevel)
        {
            Instantiate(padLock, new Vector3(transform.position.x, transform.position.y , transform.position.z - 1.5f), Quaternion.identity).transform.Rotate(-90f, -90f, 0);
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        showDetails = true;
        if (Input.GetButtonDown("Action") && canLoadLevel)
        {
            SceneManager.LoadScene("Level " + levelToLoad);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        showDetails = false;
    }

    private void OnGUI()
    {
        if(showDetails)
            if(canLoadLevel)
                GUI.Label(new Rect(30, Screen.height*.9f, 200, 40), "Press E to enter level " + levelToLoad);
            else
                GUI.Label(new Rect(30, Screen.height * .9f, 200, 40), "Level " + levelToLoad + " is locked");

    }
}
