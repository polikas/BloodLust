using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameControl : MonoBehaviour
{
    /// <summary>
    /// Variables
    #region Private Fields
    private HealthManager instance;
    private PlayerManager inst;
    private ScenesManager scene;
    private SacrificeSpot sacrificeSpot;
    private BossHealth instBoss;
    private UImanager ui;
    private int checker;
    private Scene currentScene;
    private string sceneName;
    private bool jumpPower;
    private bool isPaused;
    #endregion Private Fields
    /// </summary>

    void Start()
    {
        jumpPower = false;
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        checker = 1;
        ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UImanager>();
        instance = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthManager>();
        if(sceneName == "BossScene")
        {
            instBoss = GameObject.FindGameObjectWithTag("BossHealthSystem").GetComponent<BossHealth>();
        }
        inst = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        scene = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ScenesManager>();
        sacrificeSpot = GameObject.FindGameObjectWithTag("sacrificeSpot").GetComponent<SacrificeSpot>();
        Cursor.visible = false;
        resumeGame();
    }

    void Update()
    {
        gameOver();
        powerJump();
        moveToBoss();
        if(sceneName == "BossScene")
        {
            winGame();
        }
      
        setPause();
        if (sceneName == "BossScene" && instance.isDead)
        {
            scene.loadBossScene();
        }
    }

    private void winGame()
    {
        if(instBoss.bossDead)
        {
            scene.loadWinScene();
        }
    }

    private void moveToBoss()
    {
        if (sacrificeSpot.moveToBoss && checker == 1)
        {
            scene.loadBossScene();
            inst.currHealth = 5;
            checker--;
        }
    }

    private void gameOver()
    {
        if(instance.isDead)
        {
            scene.loadGameOverScene();
        }
    }

    public void powerJump()
    {
        if(inst.currentBlood >= 15 && !jumpPower)
        {
            inst.currentBlood -= 15;
            ui.jumpPowerText.gameObject.SetActive(true);
            ui.bloodCounterText.text = inst.currentBlood.ToString();
            inst.jump = 450f;
            inst.playerData.setJumpData(inst.jump);
            jumpPower = true;
            Destroy(ui.jumpPowerText.gameObject, 4f);
        }
    }

    private void pauseGame()
    {
        isPaused = true;
        Cursor.visible = true;
        Time.timeScale = 0;
        ui.setPausePanel(isPaused);
    }
    public void resumeGame()
    {
        isPaused = false;
        Cursor.visible = false;
        Time.timeScale = 1;
        ui.setPausePanel(isPaused);
    }

    private void setPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            resumeGame();
        }
    }
}
