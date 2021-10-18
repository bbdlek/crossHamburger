using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public Button startBtn;
    public GameObject _startManager;

    [SerializeField] Text _totalMoney;
    [SerializeField] Text _startBtn;

    // 시작 버튼 클릭 시
    // 효과음이 끝나고 플레이 화면으로 전환
    public void ClickButton()
    {
        AudioSource doorBell = startBtn.GetComponent<AudioSource>();
        doorBell.Play();
        DontDestroyOnLoad(_startManager);
        Invoke("ChangeScene", 2);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Play");
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("TotalMoney"))
        {
            //메인 돈 표시
            _totalMoney.text = PlayerPrefs.GetInt("TotalMoney") + "원";
        }
        else
        {
            PlayerPrefs.SetInt("TotalMoney", 0);
            PlayerPrefs.Save();
            _totalMoney.text = PlayerPrefs.GetInt("TotalMoney") + "원";
        }

        if (PlayerPrefs.HasKey("Day"))
        {
            _startBtn.text = "Day " + PlayerPrefs.GetInt("Day") + " 장사 시작!!!";
        }
        else
        {
            PlayerPrefs.SetInt("Day", 1);
            PlayerPrefs.Save();
            _startBtn.text = "Day " + PlayerPrefs.GetInt("Day") + " 장사 시작!!!";
        }
            

        InitialIngredients();
    }

    public void MoneyUpdate()
    {
        _totalMoney.text = PlayerPrefs.GetInt("TotalMoney") + "원";
    }

    void InitialIngredients()
    {
        if (!PlayerPrefs.HasKey("BreadBottom"))
        {
            PlayerPrefs.SetInt("BreadBottom", 10);
            PlayerPrefs.SetInt("Beef", 10);
            PlayerPrefs.SetInt("Chicken", 10);
            PlayerPrefs.SetInt("Bacon", 10);
            PlayerPrefs.SetInt("Egg", 10);
            PlayerPrefs.SetInt("Tomato", 10);
            PlayerPrefs.SetInt("Lettuce", 10);
            PlayerPrefs.SetInt("BreadTop", 20);
        }
        PlayerPrefs.Save();
    }
}
