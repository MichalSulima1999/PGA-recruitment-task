using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private Text currentTimeText;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private Text endTimeText;

    public GameObject messagePanel;
    public GameObject messageYes;
    [SerializeField] private GameObject messageNo;
    [SerializeField] private GameObject messageOk;
    [SerializeField] private Text messageTitle;

    private void Start() {
        ResetMessagePanel();
    }

    private void Update() {
        if (!GameManager.gameFinished) {
            TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.currentTime);
            currentTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds); // show currentTime in time format
        }
    }

    public void StartGame() {
        startPanel.SetActive(false);
        GameManager.currentTime = 0f;
        GameManager.gameFinished = false;
    }

    public void TryAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reset scene
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void GameOver() {
        gameOverPanel.SetActive(true);
        TimeSpan timeSpan = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("bestTime"));
        bestTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds); // show best time

        TimeSpan endTimeSpan = TimeSpan.FromSeconds(GameManager.currentTime);
        endTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", endTimeSpan.Minutes, endTimeSpan.Seconds, endTimeSpan.Milliseconds); // show current end time
    }

    // hide panel
    public void ResetMessagePanel() {
        messagePanel.SetActive(false);
        messageYes.SetActive(false);
        messageNo.SetActive(false);
        messageOk.SetActive(false);
        messageYes.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void ShowConfirmPanel(string msgTitle) {
        if (messagePanel.activeSelf)
            return;

        messagePanel.SetActive(true);
        messageYes.SetActive(true);
        messageNo.SetActive(true);
        messageTitle.text = msgTitle;
    }

    public void ShowInfoPanel(string msgTitle) {
        if (messagePanel.activeSelf)
            return;

        messagePanel.SetActive(true);
        messageOk.SetActive(true);
        messageTitle.text = msgTitle;
    }
}
