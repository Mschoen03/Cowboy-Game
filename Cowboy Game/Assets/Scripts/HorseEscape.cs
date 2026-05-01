using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HorseEscape : MonoBehaviour
{
    public GameObject winText;

    private bool playerInRange = false;

    void Start()
    {
        winText.SetActive(false);
    }

    void Update()
    {
        // TEST SCORE = 100
        if (playerInRange && ScoreManager.instance.score >= 100)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                WinGame();
            }
        }
    }

    void WinGame()
    {
        Debug.Log("YOU WIN");

        winText.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered horse area");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left horse area");
            playerInRange = false;
        }
    }
}
