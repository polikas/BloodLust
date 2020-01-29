using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Customers/Settings")]
public class playerData : ScriptableObject
{
    public float jumpData;
    public void setJumpPower(float jump)
    {
        jumpData = jump;
    }
}
