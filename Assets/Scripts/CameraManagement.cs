using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    /// <summary>
    /// Variables
    #region Public Fields
    public GameObject player;
    #endregion Public Fields
    #region Private Fields
    private Vector3 offset;
    private PlayerManager instance;
    #endregion Private Fields
    /// </summary>

    void Start()
    {
        offset = transform.position - player.transform.position;
        instance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
