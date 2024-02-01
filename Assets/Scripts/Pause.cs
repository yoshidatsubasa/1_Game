using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject titleButton;
    [SerializeField] private GameObject haikei;
    [SerializeField] private GameObject menuicon;
    [SerializeField] private GameObject sound;
    [SerializeField] private GameObject soundMute;
    private bool isPaused = false;
    private bool isGameOver = false;

    void Start()
    {
       
        pausePanel.SetActive(false);
        pauseButton.onClick.AddListener(Pouse);
        resumeButton.onClick.AddListener(Resume);
        resetButton.SetActive(false);
        titleButton.SetActive(false);
        haikei.SetActive(false);
        menuicon.SetActive(false);
        sound.SetActive(false);
        soundMute.SetActive(false);
    }

    void Update()
    {
        
        if (!isGameOver && Input.GetButtonDown("Menu")) 
        {
            if (!isPaused)
            {
                Pouse();
            }
            else
            {
                Resume();
            }
        }
    }

    private void Pouse()
    {
        if (!isGameOver)
        {
            Time.timeScale = 0;  // ���Ԓ�~
            isPaused = true;
            pausePanel.SetActive(true);
            resetButton.SetActive(true);
            titleButton.SetActive(true);
            haikei.SetActive(true);
            menuicon.SetActive(true);
            sound.SetActive(true);
            soundMute.SetActive(true);
        }
    }

    private void Resume()
    {
        Time.timeScale = 1;  // �ĊJ
        isPaused = false;
        pausePanel.SetActive(false);
        resetButton.SetActive(false);
        titleButton.SetActive(false);
        haikei.SetActive(false);
        menuicon.SetActive(false);
        sound.SetActive(false);
        soundMute.SetActive(false);
    }

    // �Q�[�����I��������ɌĂ΂��֐��i�S�[�����Ȃǁj
    public void GameOver()
    {
        isGameOver = true;
    }

}