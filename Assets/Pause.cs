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

    void Start()
    {
       
        pausePanel.SetActive(false);
        pauseButton.onClick.AddListener(Pouse);
        resumeButton.onClick.AddListener(Resume);
        resetButton.SetActive(false);
        titleButton.SetActive(false);
        haikei.SetActive(false);
        menuicon.SetActive(false);
    }

    private void Pouse()
    {
        Time.timeScale = 0;  // éûä‘í‚é~
        pausePanel.SetActive(true);
        resetButton.SetActive(true);
        titleButton.SetActive(true);
        haikei.SetActive(true);
        menuicon.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1;  // çƒäJ
        pausePanel.SetActive(false);
        resetButton.SetActive(false);
        titleButton.SetActive(false);
        haikei.SetActive(false);
        menuicon.SetActive(false);
    }
}