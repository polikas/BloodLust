using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    /// <summary>
    /// Variables
    #region Public Fields
    public Animator animator;
    public int skeletonDamage = 1;
    public float speed;
    public Transform startPos, endPos;
    #endregion Public Fields
    #region Private Fields
    private bool facingRight;
    private Transform playerPos;
    private HealthManager instance;
    private RaycastHit2D hit;
    private float curTime = 0;
    private float nextDamage = 2;
    private bool isHitting;
    private PlayerManager player;
    #endregion Private Fields
    /// </summary>

    void Start()
    {
        speed = 1f;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        instance = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        isHitting = false;
    }

    void LateUpdate()
    {
        skeletonAI();
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    void skeletonAI()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > 0.3)
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
        Debug.DrawLine(startPos.position, endPos.position, Color.red);
        isHitting = Physics2D.Linecast(startPos.position, endPos.position, 1 << LayerMask.NameToLayer("Player"));
        if (isHitting && curTime <= 0)
        {
            curTime = nextDamage;
            Debug.Log("Hitted Player");
            hit = Physics2D.Linecast(startPos.position, endPos.position, 1 << LayerMask.NameToLayer("Player"));
            instance.hurtPlayer(skeletonDamage);
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }
}
