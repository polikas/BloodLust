using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// Variables   
    #region Public Fields
    public int currHealth = 5;
    public int currentBlood;
    public int playerDamage = 1;
    public float speed = 5.0f;
    public float jump;
    public float horizontalMove = 0f;
    public bool isGrounded;
    public playerJumpData playerData;
    public Transform nomrlaAtkStart, normalAtkEnd, specialCastStart, specialCastEnd;
    public Animator animator;
    public GameObject particle;
    #endregion Public Fields
    #region Private Fields
    private int maxAttacks;
    private bool hitted;
    private Rigidbody2D player;
    private Vector2 movement;
    private RaycastHit2D hitEnemy;
    private UImanager instance;
    private HealthManager inst;
    private BossBehaviour boss;
    private AudioSource swordSound;
    #endregion Private Fields
    /// </summary>

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        swordSound = GetComponent<AudioSource>();
        bool hitted = false;
        instance = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UImanager>();
        inst = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthManager>();
        currentBlood = 0;
        if (sceneName == "GamePlay")
            playerData.setJumpData(300f);
        if (sceneName == "BossScene")
        boss = GameObject.Find("Boss").GetComponent<BossBehaviour>();
        jump = playerData.getJumpData();
    }

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        isGrounded = true;
        maxAttacks = 1;
        playerData = GameObject.Find("playerData").GetComponent<playerJumpData>();
    }

    void FixedUpdate()
    {
        playerMove();
        playerAttack();
        specialAttack();
    }

    private void Update()
    {
       if(currentBlood < 0)
       {
            currentBlood = 0;
       }
    }
    public void playerMove()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontalMove, 0f);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));  

        if (Input.GetKey(KeyCode.A))
        {
            Vector2 movement = new Vector2(horizontalMove, 0.0f);
            transform.eulerAngles = new Vector2(0, 180);
            if (isGrounded)
            {
                player.velocity = movement * speed;
            }
        }
        if (Input.GetKeyUp(KeyCode.A) && isGrounded)
        {
            player.velocity = Vector2.zero;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector2 movement = new Vector2(horizontalMove, 0.0f);
            transform.eulerAngles = new Vector2(0, 0);
            if (isGrounded)
            {
                player.velocity = movement * speed;
            }
        }
        if(Input.GetKeyUp(KeyCode.D) && isGrounded)
        {
            player.velocity = Vector2.zero;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Is jumping");
            player.AddForce(new Vector2(0.0f, jump));
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    public void playerAttack()
    {
        if (Input.GetMouseButton(0) && maxAttacks == 1 && isGrounded)
        {
            swordSound.Play();
            maxAttacks--;
            animator.SetBool("isAttacking", true);
            StartCoroutine(nextAttack());
            Debug.DrawLine(nomrlaAtkStart.position, normalAtkEnd.position, Color.red);
            hitted = Physics2D.Linecast(nomrlaAtkStart.position, normalAtkEnd.position, 1 << LayerMask.NameToLayer("Enemy"));
            if(hitted)
            {
                Debug.Log("Hitted Enemy");
                hitEnemy = Physics2D.Linecast(nomrlaAtkStart.position, normalAtkEnd.position, 1 << LayerMask.NameToLayer("Enemy"));  
                Destroy(hitEnemy.collider.gameObject);
                currentBlood++;
                instance.getBloodText(currentBlood);
            }
            if(hitted = Physics2D.Linecast(nomrlaAtkStart.position, normalAtkEnd.position, 1 << LayerMask.NameToLayer("Boss")))
            {
                hitEnemy = Physics2D.Linecast(nomrlaAtkStart.position, normalAtkEnd.position, 1 << LayerMask.NameToLayer("Boss"));
                if(hitEnemy.transform.tag == "Boss")
                {
                    Debug.Log("Hitted Boss");
                    inst.hurtBoss(playerDamage);
                }
                if(hitEnemy.transform.tag == "BossHalo")
                {
                    hitted = false;
                }
            }
            if (!hitted)
            {
                Debug.Log("No enemys");
            }
        }
    }

    public IEnumerator nextAttack()
    {
        yield return new WaitForSeconds(0.5f);
        maxAttacks++;
        animator.SetBool("isAttacking", false);
    }

    public IEnumerator SpecialAtk()
    {
        particle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        maxAttacks++;
        animator.SetBool("SpecialMove", false);
        particle.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" || collision.collider.tag == "Enemy")
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            Debug.Log("Is grounded");
        }
    }

    public void specialAttack()
    {
        if (Input.GetMouseButton(1) && isGrounded && currentBlood >= 5 && maxAttacks == 1)
        {
            maxAttacks--;
            animator.SetBool("SpecialMove", true);
            StartCoroutine(SpecialAtk());
            RaycastHit2D[] hits;
            Vector2 fwd = transform.TransformDirection(Vector2.right) * 10;
            hits = Physics2D.RaycastAll(specialCastStart.transform.position, fwd, 20f, 1 << LayerMask.NameToLayer("Enemy"));
            Debug.DrawLine(specialCastStart.transform.position, fwd, Color.red);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                Debug.Log("Special hitted Enemy");
                Destroy(hit.collider.gameObject);
                currentBlood++;
            }          
            currentBlood -= 5;
            instance.getBloodText(currentBlood);
        }
    }
}
