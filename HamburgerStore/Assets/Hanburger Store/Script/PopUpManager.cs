using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{

    private void OnEnable()
    {
        gameObject.GetComponent<Text>().DOFade(1.0f, 0f);
        gameObject.GetComponent<Text>().DOFade(0.0f, 2f);
        Invoke(nameof(UI_Disappear), 2f);
    }

    void UI_Disappear()
    {
        gameObject.SetActive(false);
    }
}
