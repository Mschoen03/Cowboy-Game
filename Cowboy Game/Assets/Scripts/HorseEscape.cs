using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseEscape : MonoBehaviour, IInteractable
{
    public GameObject winText;

    private bool hasWon = false;

    void Start()
    {
        winText.SetActive(false);
    }

    public void Interact()
    {
        // Prevent multiple activations
        if (hasWon)
            return;

        // Check score requirement
        if (ScoreManager.instance.score >= 3000)
        {
            WinGame();
        }
        else
        {
            Debug.Log("Need 3000 score to escape!");
        }
    }

    void WinGame()
    {
        hasWon = true;

        Debug.Log("YOU WIN");

        winText.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
