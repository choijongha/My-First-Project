using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    bool onSelected;
    bool onOneClicked;
    bool onDoubleClicked;
    float timerForDoubleClick;
    float doubleClickdelay;
    string selectedName;
    string selectedDoubleName;

    [SerializeField] GameObject seletedRingPrefabs;
    [SerializeField] GameObject nameBox;
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI nameText;

    Camera mainCamera;
    RectTransform rectTransform;

    Vector2 targetPosition;
    RaycastHit2D hit;
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rectTransform = nameBox.GetComponent<RectTransform>();

    }
    void Start()
    {
        doubleClickdelay = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        MouseClickDown();
        DoubleClickFalse();
         
        if(nameBox.activeSelf) UIName();
    }
    void MouseClickDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            BoolOnonSelected();   
        }
    }

    void BoolOnonSelected()
    {
        if (hit.collider != null)
        {
            //Vector2 targetPosition = GameObject.Find($"{hit.collider.name}").transform.position;
            targetPosition = hit.transform.position;
            
            InstantiateSelectRing();
            DoubleClick();
        }
        
        else
        {
            onSelected = false;
            Destroy(GameObject.FindGameObjectWithTag("SelectedRing"));
            nameBox.SetActive(false);
        }
        
    }
    void UIName()
    {
        nameText.text = hit.collider.name;
        rectTransform.position = Camera.main.WorldToScreenPoint(targetPosition) - new Vector3(0, 60);
    }
    void InstantiateSelectRing()
    {
        if (!onSelected && !GameObject.FindGameObjectWithTag("SelectedRing"))
        {
            onSelected = true;
            nameBox.SetActive(true);
            Instantiate(seletedRingPrefabs, targetPosition, transform.rotation);
        }
        else if (onSelected && GameObject.FindGameObjectWithTag("SelectedRing"))
        {
            GameObject.FindGameObjectWithTag("SelectedRing").transform.position = targetPosition;
        }
    }
    void DoubleClick()
    {
        if (!onOneClicked)
        {
            onOneClicked = true;
            timerForDoubleClick = Time.time;
            selectedName = hit.collider.name;
        }
        else if(!onDoubleClicked && onOneClicked && selectedName == hit.collider.name)
        {
            onOneClicked = false;
            onDoubleClicked = true;
            Debug.Log("Double Click zoom");
            mainCamera.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y,-10);
            mainCamera.GetComponent<Camera>().orthographicSize = 1f;
            selectedDoubleName = hit.collider.name;
        }
        else if(onDoubleClicked && selectedName == selectedDoubleName)
        {
            onOneClicked = false;
            onDoubleClicked = false;
            Debug.Log("Double Click zoom out");
            mainCamera.transform.position = new Vector3(0,0,-10);
            mainCamera.GetComponent<Camera>().orthographicSize = 3f;
        }else if(onDoubleClicked && selectedName != selectedDoubleName)
        {
            onOneClicked = false;
            Debug.Log("Double Click zoom");
            mainCamera.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, -10);
            selectedDoubleName = hit.collider.name;
        }
    }
    void DoubleClickFalse()
    {
        if (onOneClicked)
        {
            if (Time.time - timerForDoubleClick > doubleClickdelay)
            {
                onOneClicked = false;
                Debug.Log("onOneClicked out");
            }
        }
    }
    public void InfoObjects()
    {
        panel.SetActive(true);
    }
}

