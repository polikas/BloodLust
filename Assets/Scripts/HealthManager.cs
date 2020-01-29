using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    /// <summary>
    /// Variables
    #region Public Fields
    public Slider healthBar;
    public bool isDead;
    public bool bossDead;
    public bool haloUp;
    #endregion Public Fields
    #region Private Fields
    private PlayerManager instance;
    private BossBehaviour inst;
    private Scene currentScene;
    private string sceneName;
    private AudioSource playerDamage;
    #endregion Private Fields
    /// </summary>

    void Start()
    {
        playerDamage = GetComponent<AudioSource>();
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        isDead = false;
        bossDead = false;
        instance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        if(sceneName == "BossScene")
        inst = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBehaviour>();
    }

    void Awake()
    {
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = instance.currHealth;
        if(instance.currHealth <= 0 && !isDead)
        {
            instance.currHealth = 0;
            isDead = true;
        }
    }

    public void hurtPlayer(int dmg)
    {
        instance.currHealth -= dmg;
        playerDamage.Play();
        StartCoroutine(playerDamageFeedback());
    }

    public void hurtBoss(int damage)
    {
        inst.bossCurrHp -= damage;
        StartCoroutine(damageBossFeedback());
    }

    IEnumerator playerDamageFeedback()
    {
        instance.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        yield return new WaitForSeconds(2f);
        instance.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }

    IEnumerator damageBossFeedback()
    {
        inst.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
        yield return new WaitForSeconds(0.2f);
        inst.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }
}
