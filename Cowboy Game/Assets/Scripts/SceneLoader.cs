using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("SampleScene");
    }
}

