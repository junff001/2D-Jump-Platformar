using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int stageIndex;
    public GameObject[] stage;

    private void Awake()
    {
        instance = this;
    }

    public void NextStage()
    {
        if(stageIndex < stage.Length - 1) //2
        {
            stage[stageIndex].SetActive(false); // 1. 2. 3
            stageIndex++;
            stage[stageIndex].SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    
}
