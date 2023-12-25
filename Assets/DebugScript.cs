using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{

    public void ChangeScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
        Time.timeScale = 1;
        Coincounter.coinCount = 0;
       
    }

    public void ChangeScene2(string nextScene)
    {
        SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1;
        Coincounter.coinCount = 0;
    }
    public void ChangeScene3(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
        Time.timeScale = 1;
        Coincounter2.coinCount = 0;
    }
    public void ChangeScene4(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
        Time.timeScale = 1;
        Coincounter.coinCount = 0;

    }
    public void SceneReset()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
        Time.timeScale = 1;
        Coincounter.coinCount = 0;
        Coincounter2.coinCount = 0;
    }

    public void ChangeResult(string nextScene)
    {

        SceneManager.LoadScene("Result");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
          {
            SceneManager.LoadScene("Level2");
            Time.timeScale = 1;
            Coincounter.coinCount = 0;
          }
      

    }
    

}
