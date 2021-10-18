using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ingredients : MonoBehaviour
{
    StoreManager _storeManager;

    [SerializeField] List<GameObject> ingredients;
    private void Start()
    {
        if (GameObject.Find("StoreManager"))
            _storeManager = GameObject.Find("StoreManager").GetComponent<StoreManager>();
    }

    void Update()
    {
        ingredients[0].SetActive(_storeManager._breadBottom > 0);
        ingredients[1].SetActive(_storeManager._breadTop > 0);
        ingredients[2].SetActive(_storeManager._lettuce > 0);
        ingredients[3].SetActive(_storeManager._tomato > 0);
        ingredients[4].SetActive(_storeManager._chicken > 0);
        ingredients[5].SetActive(_storeManager._beef > 0);
        ingredients[6].SetActive(_storeManager._bacon > 0);
        ingredients[7].SetActive(_storeManager._egg > 0);
    }
}
