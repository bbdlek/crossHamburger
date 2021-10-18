using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndManager : MonoBehaviour
{
    public Button endButton;
    public Text cash;
    public AudioClip audioClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // 가게 마감을 알리는 종소리 효과음을 재생
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();

        // 수입을 화면에 표시함
        cash.text = StoreManager.income.ToString() + " 원";

        //수입을 전체 돈에 저장
        PlayerPrefs.SetInt("TotalMoney", (int)StoreManager.income);
        PlayerPrefs.Save();
    }

    // 마감 버튼 클릭 시 시작화면으로 전환
    // Update is called once per frame
    public void ClickButton()
    {
        DayUpdate();
        Destroy(GameObject.Find("StartManager"));
        ChangeScene();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Start");
    }

    void DayUpdate()
    {
        PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
        PlayerPrefs.Save();
    }
}
