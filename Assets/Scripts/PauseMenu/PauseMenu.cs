using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public bool opened = false;
    public CanvasGroup cv;
    public PlayerController pl;

    private void Start()
    {
        cv = GetComponent<CanvasGroup>();
        pl = GameManager.instance.player;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            opened = !opened;

            if (opened)
            {
                pl.canBeControlled = false;
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cv.alpha = 1f;
                cv.interactable = true;
                cv.blocksRaycasts = true;
            }
            else
            {
                CloseMenu();
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CloseMenu()
    {
        pl.canBeControlled = true;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cv.alpha = 0f;
        cv.interactable = false;
        cv.blocksRaycasts = false;
        opened = false;
    }

}
