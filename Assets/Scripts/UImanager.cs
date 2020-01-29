using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    /// <summary>
    /// Variables
    #region Public Fields
    public Text bloodCounterText;
    public Text jumpPowerText;
    public GameObject sacrificePanel, pausePanel;
    #endregion Public Fields
    /// </summary>

    void Awake()
    {
        jumpPowerText.gameObject.SetActive(false);
        bloodCounterText.text = "0";
        sacrificePanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
    }

    public void getBloodText(int tempBlood)
    {
        bloodCounterText.text = tempBlood.ToString();
    }

    public void setPausePanel(bool k)
    {
        if (k)
            pausePanel.SetActive(k);
        else
            pausePanel.SetActive(k);
    }
}
