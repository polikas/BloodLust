using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeSpot : MonoBehaviour
{
    /// <summary>
    /// Variables
    #region Public Fields
    public bool moveToBoss;
    #endregion Public Fields
    #region Private Fields
    private UImanager instance;
    private PlayerManager inst;
    #endregion Private Fields
    /// </summary>


    void Start()
    {
        instance = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UImanager>();
        inst = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        moveToBoss = false;
    }

    void Update()
    {
        if (inst.currentBlood >= 30 && Input.GetKeyDown(KeyCode.F))
        {
            moveToBoss = true;
            Debug.Log(moveToBoss);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
     {
        if(collision.transform.tag == "Player")
        {
            instance.sacrificePanel.gameObject.SetActive(true);
           
        }
        else
        {
            instance.sacrificePanel.gameObject.SetActive(false);
        }
     }
}
