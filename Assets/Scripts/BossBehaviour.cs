using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    /// <summary>
    /// Variables
    #region Public Fields
    public Animator animator;
    public int bossDamage = 1;
    public float speed;
    public int bossCurrHp = 10;
    public Transform startPos, endPos;
    #endregion Public Fields
    #region Private Fields
    private bool facingRight;
    private bool isHitting;
    private Transform playerPos;
    private BossHealth instance;
    private RaycastHit2D hit;
    private float curTime = 0;
    private float nextDamage = 2;
    private HealthManager inst;
    #endregion Private Fields
    /// </summary>

    void Start()
    {
        bossCurrHp = 10;
        speed = 1f;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        instance = GameObject.FindGameObjectWithTag("BossHealthSystem").GetComponent<BossHealth>();
        inst = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthManager>();
    }

    void LateUpdate()
    {
        bossAI();
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    void bossAI()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > 0.1)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
        if (playerPos.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        if (playerPos.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        if(instance.haloUp)
        {
            this.gameObject.transform.tag = "BossHalo";
            StartCoroutine(haloHandler());          
        }
        else
        {
            this.gameObject.transform.tag = "Boss";
        }
        Debug.DrawLine(startPos.position, endPos.position, Color.red);
        isHitting = Physics2D.Linecast(startPos.position, endPos.position, 1 << LayerMask.NameToLayer("Player"));
        if (isHitting && curTime <= 0)
        {
            curTime = nextDamage;
            Debug.Log("Hitted Player");
            hit = Physics2D.Linecast(startPos.position, endPos.position, 1 << LayerMask.NameToLayer("Player"));
            inst.hurtPlayer(bossDamage);
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    IEnumerator haloHandler()
    {
        animator.SetBool("GhostHalo", true);
        yield return new WaitForSeconds(2f);
        instance.haloUp = false;
        animator.SetBool("GhostHalo", false);
    }
}
