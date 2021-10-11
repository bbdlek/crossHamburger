using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 화면에 남은 플레이 시간을 표시
public class TimerManager : MonoBehaviour
{
    public Text timer;
    public float time = 60f;
    private float countDown;

    // Start is called before the first frame update
    void Start()
    {
        countDown = time;
    }

    // 플레이 시간이 끝나면 종료화면으로 전환함
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Floor(countDown) <= 0)
        {
            ChangeScene();
        }
        else
        {
            countDown -= Time.deltaTime;
            timer.text = Mathf.Floor(countDown).ToString();
        }

    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("End");
    }
}
