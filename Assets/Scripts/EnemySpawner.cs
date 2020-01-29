using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Variables
    #region Public Fields
    public GameObject skeletonPrefab;
    #endregion Public Fields
    /// </summary>

    void Start()
    {
        StartCoroutine(spawnHandler());
       
    }

    public IEnumerator spawnHandler()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(skeletonPrefab, transform.position, Quaternion.identity);
        StartCoroutine(spawnHandler());
    } 
}
