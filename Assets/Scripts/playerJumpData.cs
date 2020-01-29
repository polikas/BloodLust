using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJumpData : MonoBehaviour
{
    public playerData playerData;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
       
    }

    // Update is called once per frame
    public float getJumpData()
    {
        return playerData.jumpData;
    }
    public void setJumpData(float amt)
    {
        playerData.setJumpPower(amt);
    }
}
