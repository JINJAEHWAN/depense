using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하기 위한 참조

public class Stage : MonoBehaviour
{
    public void LevelEasyScene()
    {
        SceneManager.LoadScene("easy");
    }

    public void LevelNormalScene()
    {
        SceneManager.LoadScene("normal");
    }
    public void LevelHardScene()
    {
        SceneManager.LoadScene("hard");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
