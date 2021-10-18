using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private int _num_Bacon = 0,
        _num_Chicken = 0,
        _num_Lettuce = 0,
        _num_BreadTop = 0,
        _num_Egg = 0,
        _num_Beef = 0,
        _num_Tomato = 0,
        _num_BreadBottom = 0;

    [SerializeField] private List<Text> _costTxt;
    [SerializeField] private List<Text> _numTxt;
    [SerializeField] private Text _totalTxt;
    [SerializeField] private Text _myMoneyTxt;

    [SerializeField] private GameObject _PopUpText;

    public int[] _cost = {200, 200, 100, 100, 100, 100, 300, 100};

    private int _myMoney;
    private int _totalCost = 0;

    private void Start()
    {
        _myMoney = PlayerPrefs.GetInt("TotalMoney");
        _myMoneyTxt.text = _myMoney + "원"; 
        Select_Clear();
        Cost_Update();
    }

    private void Update()
    {
        Cal_Money();
    }

    public void Buy()
    {
        if (_myMoney >= _totalCost)
        {
            _myMoney -= _totalCost;
            _totalCost = 0;
            PlayerPrefs.SetInt("TotalMoney", _myMoney);
            PlayerPrefs.Save();
            _myMoneyTxt.text = _myMoney + "원";
            Item_Update();
        }
        else
        {
            _PopUpText.SetActive(true);
        }
    }
    
    private void Cal_Money()
    {
        _totalCost = _num_Bacon * _cost[0] + _num_Chicken * _cost[1] + _num_Lettuce * _cost[2] + _num_BreadTop * _cost[3] +
                      _num_BreadBottom * _cost[7] + _num_Egg * _cost[4] + _num_Beef * _cost[5] + _num_Tomato * _cost[6];
        _totalTxt.text = _totalCost.ToString();
    }

    private void Item_Update()
    {
        PlayerPrefs.SetInt("Bacon", PlayerPrefs.GetInt("Bacon") + _num_Bacon);
        PlayerPrefs.SetInt("Chicken", PlayerPrefs.GetInt("Chicken") + _num_Chicken);
        PlayerPrefs.SetInt("Lettuce", PlayerPrefs.GetInt("Lettuce") + _num_Lettuce);
        PlayerPrefs.SetInt("BreadTop", PlayerPrefs.GetInt("BreadTop") + _num_BreadTop);
        PlayerPrefs.SetInt("BreadBottom", PlayerPrefs.GetInt("BreadBottom") + _num_BreadBottom);
        PlayerPrefs.SetInt("Egg", PlayerPrefs.GetInt("Egg") + _num_Egg);
        PlayerPrefs.SetInt("Beef", PlayerPrefs.GetInt("Beef") + _num_Beef);
        PlayerPrefs.SetInt("Tomato", PlayerPrefs.GetInt("Tomato") + _num_Tomato);
        PlayerPrefs.Save();
        
        Select_Clear();
    }

    private void Select_Clear()
    {
        _num_Bacon = 0;
        _num_Chicken = 0;
        _num_Lettuce = 0;
        _num_BreadBottom = 0;
        _num_BreadTop = 0;
        _num_Egg = 0;
        _num_Beef = 0;
        _num_Tomato = 0;
        
        _numTxt[0].text = _num_Bacon.ToString();
        _numTxt[1].text = _num_Chicken.ToString();
        _numTxt[2].text = _num_Lettuce.ToString();
        _numTxt[3].text = _num_BreadTop.ToString();
        _numTxt[4].text = _num_Egg.ToString();
        _numTxt[5].text = _num_Beef.ToString();
        _numTxt[6].text = _num_Tomato.ToString();
        _numTxt[7].text = _num_BreadBottom.ToString();
    }
    

    void Cost_Update()
    {
        for (int i = 0; i < 8; i++)
        {
            _costTxt[i].text = _cost[i] + "원";
        } 
    }

    //Bacon
    public void Add_Bacon()
    {
        _num_Bacon++;
        _numTxt[0].text = _num_Bacon.ToString();
    }

    public void Sub_Bacon()
    {
        if(_num_Bacon <= 0)
            return;
        _num_Bacon--;
        _numTxt[0].text = _num_Bacon.ToString();
    }
    
    //Chicken
    public void Add_Chicken()
    {
        _num_Chicken++;
        _numTxt[1].text = _num_Chicken.ToString();
    }

    public void Sub_Chicken()
    {
        if(_num_Chicken <= 0)
            return;
        _num_Chicken--;
        _numTxt[1].text = _num_Chicken.ToString();
    }
    
    //Lettuce
    public void Add_Lettuce()
    {
        _num_Lettuce++;
        _numTxt[2].text = _num_Lettuce.ToString();
    }

    public void Sub_Lettuce()
    {
        if(_num_Lettuce <= 0)
            return;
        _num_Lettuce--;
        _numTxt[2].text = _num_Lettuce.ToString();
    }
    
    //BreadTop
    public void Add_BreadTop()
    {
        _num_BreadTop++;
        _numTxt[3].text = _num_BreadTop.ToString();
    }

    public void Sub_BreadTop()
    {
        if(_num_BreadTop <= 0)
            return;
        _num_BreadTop--;
        _numTxt[3].text = _num_BreadTop.ToString();
    }
    
    //Egg
    public void Add_Egg()
    {
        _num_Egg++;
        _numTxt[4].text = _num_Egg.ToString();
    }

    public void Sub_Egg()
    {
        if(_num_Egg <= 0)
            return;
        _num_Egg--;
        _numTxt[4].text = _num_Egg.ToString();
    }
    
    //Beef
    public void Add_Beef()
    {
        _num_Beef++;
        _numTxt[5].text = _num_Beef.ToString();
    }

    public void Sub_Beef()
    {
        if(_num_Beef <= 0)
            return;
        _num_Beef--;
        _numTxt[5].text = _num_Beef.ToString();
    }
    
    //Tomato
    public void Add_Tomato()
    {
        _num_Tomato++;
        _numTxt[6].text = _num_Tomato.ToString();
    }

    public void Sub_Tomato()
    {
        if(_totalCost <= 0)
            return;
        _num_Tomato--;
        _numTxt[6].text = _num_Tomato.ToString();
    }
    
    //BreadBottom
    public void Add_BreadBottom()
    {
        _num_BreadBottom++;
        _numTxt[7].text = _num_BreadBottom.ToString();
    }

    public void Sub_BreadBottom()
    {
        if(_num_BreadBottom <= 0)
            return;
        _num_BreadBottom--;
        _numTxt[7].text = _num_BreadBottom.ToString();
    }
}
