using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CashManager : MonoBehaviour
{
    public Text cash;
    StoreManager storeManager;

    // Start is called before the first frame update
    void Start()
    {
        storeManager = GameObject.Find("StoreManager").GetComponent<StoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // 화면의 텍스트에 수입을 표시
        cash.text = storeManager.wage.ToString();
    }
}
