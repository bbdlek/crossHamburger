using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public Button startBtn;
    public GameObject _startManager;

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
}
