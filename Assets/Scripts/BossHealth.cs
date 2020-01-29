using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    /// <summary>
    /// Variables
    #region Public Fields
    public Slider bossHealthBar;
    public bool haloUp;
    public bool bossDead;
    #endregion Public Fields
    #region Private Fields
    private BossBehaviour inst;
    private Scene currentScene;
    private string sceneName;
    #endregion Private Fields
    /// </summary>

    void Awake()
    {
        bossHealthBar = GetComponent<Slider>();
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "BossScene")
            inst = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBehaviour>();
    }
   
    void Update()
    {
        if (sceneName == "BossScene")
         bossHealth();  
    }

    public void bossHealth()
    {
        bossHealthBar.value = inst.bossCurrHp;
        if (inst.bossCurrHp % 2 == 1)
        {
            haloUp = true;
        }
        else
        {
            haloUp = false;
        }
        if (inst.bossCurrHp <= 0 && !bossDead)
        {
            inst.bossCurrHp = 0;
            bossDead = true;
        }
    }
}
