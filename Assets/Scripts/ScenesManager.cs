using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ScenesManager : MonoBehaviour
{
    public void loadMainGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
    }

    public void loadWinScene()
    {
        SceneManager.LoadScene("WinScene");
        Cursor.visible = true;
    }

    public void loadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
        Cursor.visible = true;
    }

    public void loadBossScene()
    {
        SceneManager.LoadScene("BossScene");
    }

    public void closeApp()
    {
        Application.Quit();
    }
}
