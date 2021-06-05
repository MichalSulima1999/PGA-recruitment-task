using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float currentTime;
    public static bool gameFinished = true;

    private UISystem uiSystem;

    // Start is called before the first frame update
    void Start()
    {
        uiSystem = GameObject.FindGameObjectWithTag("UI").GetComponent<UISystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameFinished) {
            currentTime += Time.deltaTime; // count time
        }
    }

    // handle game over
    public void GameOver() {
        gameFinished = true;
        if(PlayerPrefs.GetFloat("bestTime") > currentTime || PlayerPrefs.GetFloat("bestTime") == 0)
            PlayerPrefs.SetFloat("bestTime", currentTime); //save best time

        uiSystem.GameOver();
    }
}
