using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class IngredientManager : MonoBehaviour
{
    [SerializeField] private StoreManager _storeManager;
    [SerializeField] GameObject _panel;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _toggleImage;
    [SerializeField] private Text[] _ing_text;

    [SerializeField] private Sprite[] _t_sprites;

    private void Update()
    {
        if(_toggle.isOn)
            UI_Appear();
        else UI_Disappear();
        
        CheckStock();
    }

    void UI_Disappear()
    {
        _toggleImage.sprite = _t_sprites[0];
        RectTransform rt = _panel.GetComponent<RectTransform>();

        if (rt.position.x > -800.0f)
        {
            _panel.GetComponent<Image>().DOFade(0.0f, 0.2f);
            rt.DOAnchorPosX(-800.0f, 0.2f);
        }
    }

    void UI_Appear()
    {
        _toggleImage.sprite = _t_sprites[1];
        RectTransform rt = _panel.GetComponent<RectTransform>();
        
        if (rt.position.x < 0f)
        {
            _panel.GetComponent<Image>().DOFade(0.4f, 0.2f);
            rt.DOAnchorPosX(0.0f, 0.2f);
        }
    }

    void CheckStock()
    {
        _ing_text[0].text = "X " + _storeManager._breadTop;
        _ing_text[1].text = "X " + _storeManager._lettuce;
        _ing_text[2].text = "X " + _storeManager._chicken;
        _ing_text[3].text = "X " + _storeManager._bacon;
        _ing_text[4].text = "X " + _storeManager._breadBottom;
        _ing_text[5].text = "X " + _storeManager._tomato;
        _ing_text[6].text = "X " + _storeManager._beef;
        _ing_text[7].text = "X " + _storeManager._egg;
    }
}
