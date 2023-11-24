using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Stats : MonoBehaviour
{
    public Image hpBar;
    public Image mpBar;

    public void UpdateHPBar(float value)
    {
        hpBar.fillAmount = value;
    }

    public void UpdateMPBar(float value)
    {
        mpBar.fillAmount = value;
    }
}
