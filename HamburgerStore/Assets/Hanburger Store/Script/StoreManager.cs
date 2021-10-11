using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

// 기본적으로 햄버거 가게와 관련된 클래스
// 손님, 주문, 햄버거 만들기에 대한 모든 것들
public class StoreManager : MonoBehaviour
{
    public Camera _arCamera;
    public GameObject _thePerson, _theBoard, _theStore;
    public GameObject _theBurger, _theCustomed;
    public Transform _storePoint;
    public static uint income;
    public uint wage = 0;
    public AudioClip[] _sound = new AudioClip[3];
    private AudioSource audioSource;
    
    private GameObject tPerson;
    private GameObject tBurger, tCustomed;
    private GameObject tBoard, tStore;
    private Vector3 onBoard = new Vector3(0, 0.02f, 0);

    private float randomX, y = -0.95f, randomZ;
    private bool isPerson = false, isBoard = false;
    private bool isBurger = false, isPerfect = false;
    private enum Sound { Customer = 0, Wrong, Cash };

    private enum Ingredients { BreadBottom = 0, Beef, Chicken, Bacon, Egg, Tomato, Lettuce, BreadTop, Count };
    public GameObject[] _ingredients = new GameObject[(int)Ingredients.Count];
    private Queue<Ingredients> hamburger = new Queue<Ingredients>();


    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        tStore = Instantiate(_theStore);
        Invoke("FirstPerson", 3);
    }

    // Update is called once per frame
    void Update()
    {
        SetObjectCenter();
        if (isPerson)
            TouchObject();
    }

    private void FirstPerson()
    {
        Debug.Log("Person created");
        tPerson = Instantiate(_thePerson);
        isPerson = true;
        tBoard = Instantiate(_theBoard);
        tBoard.SetActive(false);
        NewPerson();
    }

    // 새 손님이 발생 (위치변경)
    private void NewPerson()
    {
        setAudio(Sound.Customer);
        tPerson.SetActive(true);
        Debug.Log("Person transformed");
        randomX = Random.Range(-0.65f, 0.7f);
        randomZ = Random.Range(1.3f, 1.7f);
        tPerson.transform.position = _storePoint.transform.position + new Vector3(randomX, y, randomZ);

        WhichHamburger();
    }

    // 플레이어의 앞뒤 움직임과 상관없이
    // AR 카메라의 상대적인 좌표에 오브젝트를 고정함
    private void SetObjectCenter()
    {
        tStore.transform.position = _storePoint.transform.position;
        if (isPerson)
        {
            tPerson.transform.position = _storePoint.transform.position + new Vector3(randomX, y, randomZ);
            tBurger.transform.position = randomX >= 0 ?
                                        tPerson.transform.position + new Vector3(-0.45f, 1.0f, -0.2f) :
                                        tPerson.transform.position + new Vector3(0.45f, 1.0f, -0.2f);
            tBoard.transform.position = _storePoint.transform.position +new Vector3(0, 0.007f, 0.12f);
            tCustomed.transform.position = tBoard.transform.position + new Vector3(0, 0, 0);
        }
    }

    // isBoard : 도마가 있는가?
    // 도마가 있어야 햄버거를 만들 수 있음
    private void ChangeMode()
    {
        if (isBoard)
        {
            tBoard.SetActive(false);
            isBoard = false;
            Debug.Log("Hide the board.");
        }
        else
        {
            tBoard.SetActive(true);
            isBoard = true;
            Debug.Log("Reveal the Board.");
        }
    }

    // 손님과 햄버거 재료들을 터치했을 때 동작함
    private void TouchObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPosition = Input.mousePosition;

            RaycastHit hitObj;
            Ray ray = _arCamera.ScreenPointToRay(touchPosition);
            Physics.Raycast(ray, out hitObj, Mathf.Infinity);

            string name = hitObj.collider.gameObject.name;

            // 도마가 있고 터치한 오브젝트가 햄버거 재료이라면
            // 햄버거를 만들 수 있음
            if (isBoard)
            {
                Ingredients igdt = MakeHamburger(name);
                Ingredients igdtQ = hamburger.Dequeue();

                hamburger.Enqueue(igdtQ);
                if (igdt != igdtQ)
                {
                    setAudio(Sound.Wrong);
                    InitialCustomed();
                    ResetHamburger();
                }
                if (igdtQ == Ingredients.BreadTop){

                    Debug.Log("Perfect Burger!");
                    isPerfect = true;
                    tBurger.SetActive(false);
                    ChangeMode();
                }
            }

            // 도마가 없고 터치한 오브젝트가 사람이라면
            // 햄버거가 완성되었을 때 돈을 받고 새손님이 발생함
            // 그게 아니면 도마를 생성함
            else if (name.Contains("Male"))
            {
                if (isPerfect)
                {
                    wage += CountMoney();
                    income = wage;
                    setAudio(Sound.Cash);
                    isPerfect = false;
                    InitialCustomed();
                    tPerson.SetActive(false);
                    Invoke("NewPerson", 4);
                }
                else ChangeMode();
            }
        }
    }

    // 손님이 주문하고자하는 햄버거를 손님 옆에 띄움
    private void WhichHamburger()
    {
        hamburger.Clear();
        hamburger.Enqueue(Ingredients.BreadBottom);
        RandomIngredients();
        hamburger.Enqueue(Ingredients.BreadTop);

        if (isBurger) { Destroy(tBurger); Destroy(tCustomed); }
        tBurger = Instantiate(_theBurger);
        tCustomed = Instantiate(_theCustomed);
        isBurger = true;

        Vector3 space = new Vector3(0f, 0.03f, 0.01f);
        Vector3 current = new Vector3(0, 0, 0);

        Debug.Log("===== hamburger =====");
        foreach (Ingredients igdt in hamburger)
        {
            switch (igdt)
            {
                case Ingredients.BreadBottom:
                    Instantiate(_ingredients[0], current, Quaternion.identity).transform.SetParent(tBurger.transform, false);
                    break;
                case Ingredients.Beef:
                    Instantiate(_ingredients[1], current, Quaternion.identity).transform.SetParent(tBurger.transform, false);
                    break;
                case Ingredients.Chicken:
                    Instantiate(_ingredients[2], current, Quaternion.identity).transform.SetParent(tBurger.transform, false);
                    break;
                case Ingredients.Bacon:
                    Instantiate(_ingredients[3], current, Quaternion.identity).transform.SetParent(tBurger.transform, false);
                    break;
                case Ingredients.Egg:
                    Instantiate(_ingredients[4], current, Quaternion.identity).transform.SetParent(tBurger.transform, false);
                    break;
                case Ingredients.Tomato:
                    Instantiate(_ingredients[5], current, Quaternion.identity).transform.SetParent(tBurger.transform, false);
                    break;
                case Ingredients.Lettuce:
                    Instantiate(_ingredients[6], current, Quaternion.identity).transform.SetParent(tBurger.transform, false);
                    break;
                case Ingredients.BreadTop:
                    Instantiate(_ingredients[7], current, Quaternion.identity).transform.SetParent(tBurger.transform, false);
                    break;
                default:
                    break;
            }
            current += space;
        }

        tBurger.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    // 손님이 주문하고자하는 햄버거를 무작위로 생성함
    private void RandomIngredients()
    {
        int cnt = Random.Range(2, 6);
        for (int i = 0; i < cnt; i++)
        {
            Ingredients igdt = (Ingredients)Random.Range((int)Ingredients.Beef, (int)Ingredients.BreadTop);
            hamburger.Enqueue(igdt);
        }
    }

    // 플레이어가 재료를 선택해서 햄버거를 쌓을 수 있음
    private Ingredients MakeHamburger(string str)
    {
        Ingredients igdt = Ingredients.BreadBottom;
        switch (str)
        {
            case "BreadBottom":
                Instantiate(_ingredients[0], onBoard, Quaternion.identity).transform.SetParent(tCustomed.transform, false);
                onBoard += new Vector3(0, +0.003f, 0);
                igdt = Ingredients.BreadBottom;
                break;
            case "Beef":
                Instantiate(_ingredients[1], onBoard, Quaternion.identity).transform.SetParent(tCustomed.transform, false);
                onBoard += new Vector3(0, +0.01f, 0);
                igdt = Ingredients.Beef;
                break;
            case "Chicken":
                Instantiate(_ingredients[2], onBoard, Quaternion.identity).transform.SetParent(tCustomed.transform, false);
                onBoard += new Vector3(0, +0.01f, 0);
                igdt = Ingredients.Chicken;
                break;
            case "Bacon":
                Instantiate(_ingredients[3], onBoard, Quaternion.identity).transform.SetParent(tCustomed.transform, false);
                onBoard += new Vector3(0, -0.005f, 0);
                igdt = Ingredients.Bacon;
                break;
            case "Egg":
                Instantiate(_ingredients[4], onBoard, Quaternion.identity).transform.SetParent(tCustomed.transform, false);
                onBoard += new Vector3(0, -0.01f, 0);
                igdt = Ingredients.Egg;
                break;
            case "Tomato":
                Instantiate(_ingredients[5], onBoard, Quaternion.identity).transform.SetParent(tCustomed.transform, false);
                onBoard += new Vector3(0, -0.005f, 0);
                igdt = Ingredients.Tomato;
                break;
            case "Lettuce":
                Instantiate(_ingredients[6], onBoard, Quaternion.identity).transform.SetParent(tCustomed.transform, false);
                onBoard += new Vector3(0, -0.015f, 0);
                igdt = Ingredients.Lettuce;
                break;
            case "BreadTop":
                Instantiate(_ingredients[7], onBoard, Quaternion.identity).transform.SetParent(tCustomed.transform, false);
                igdt = Ingredients.BreadTop;
                break;
        }
        onBoard += new Vector3(0, 0.02f, 0);
        Debug.Log("ingredient: "+igdt);
        return igdt;
    }

    // 햄버거를 새로 만들기 위해 오브젝트를 파괴하고 생성함
    private void InitialCustomed()
    {
        Destroy(tCustomed);
        onBoard = new Vector3(0, 0.02f, 0);
        tCustomed = Instantiate(_theCustomed);   
    }

    // 새 손님이 왔을 때 무작위 햄버거를 만들 수 있도록 queue를 초기화
    private void ResetHamburger(){
        while (true)
        {
            Ingredients igdtQ = hamburger.Dequeue();
            hamburger.Enqueue(igdtQ);

            if (igdtQ == Ingredients.BreadTop)
                break;
        }
    }
    
    // 손님이 온 경우, 틀린 재료를 선택한 경우,
    // 손님에게 햄버거를 전달한 경우의 효과음을 관리
    private void setAudio(Sound snd){
        audioSource.clip = _sound[(int)snd];
        audioSource.Play();
    }

    // 손님이 주문한 햄버거가 완성되어 전달할 때
    // 햄버거 금액을 계산
    private uint CountMoney(){
        uint money = 0;
        while (true)
        {
            Ingredients igdtQ = hamburger.Dequeue();
            hamburger.Enqueue(igdtQ);

            switch (igdtQ)
            {
                case Ingredients.BreadBottom:
                    money += 500;
                    break;
                case Ingredients.Beef:
                    money += 500;
                    break;
                case Ingredients.Chicken:
                    money += 400;
                    break;
                case Ingredients.Bacon:
                    money += 300;
                    break;
                case Ingredients.Egg:
                    money += 200;
                    break;
                case Ingredients.Tomato:
                    money += 200;
                    break;
                case Ingredients.Lettuce:
                    money += 200;
                    break;
            }

            if (igdtQ == Ingredients.BreadTop)
                break;
        }

        return money;
    }
}