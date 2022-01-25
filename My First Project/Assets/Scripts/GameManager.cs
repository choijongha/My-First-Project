using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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
    [SerializeField] TextMeshProUGUI obejectInfoText;

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
        doubleClickdelay = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        MouseClickDown();
        DoubleClickFalse();       
    }
    void MouseClickDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            BoolOnonSelected();
            ObjectInfoText();
        }
    }
    void BoolOnonSelected()
    {
        if (hit.collider != null)
        {
            //Vector2 targetPosition = GameObject.Find($"{hit.collider.name}").transform.position;
            targetPosition = hit.transform.position;
            onSelected = true;
            InstantiateSelectRing();
            DoubleClick();
            UIName();
        }
        else if (EventSystem.current.IsPointerOverGameObject())
        {
            
        }
        else
        {
            onSelected = false;
            onOneClicked = false;
            Destroy(GameObject.FindGameObjectWithTag("SelectedRing"));
            nameBox.SetActive(false);
        }        
    }
    void UIName()
    {
        if (onSelected)
        {
            nameBox.SetActive(true);
            selectedName = hit.collider.name;
            rectTransform.position = Camera.main.WorldToScreenPoint(targetPosition) - new Vector3(0, 60);
        }else if (!onSelected)
        {
            selectedName = "";
        }
        nameText.text = selectedName;
    }
    void InstantiateSelectRing()
    {
        if (onSelected && !GameObject.FindGameObjectWithTag("SelectedRing"))
        {              
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
        }
        else if(!onDoubleClicked && onOneClicked && selectedName == hit.collider.name)
        {
            onOneClicked = false;
            onDoubleClicked = true;
            selectedDoubleName = hit.collider.name;
            mainCameraZooming();
        }
        else if(onDoubleClicked && hit.collider.name == selectedDoubleName && selectedName == selectedDoubleName)
        {
            onOneClicked = false;
            onDoubleClicked = false;
            mainCameraZoomOut();
            selectedDoubleName = "";
        }
        else if(onDoubleClicked && selectedName != selectedDoubleName)
        {
            onOneClicked = false;
            mainCameraZooming();
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
            }
        }
    }
    public void InfoObjects()
    {
        if (!panel.activeSelf) panel.SetActive(true);
        else if (panel.activeSelf) panel.SetActive(false);

    }
    void mainCameraZooming()
    {
        mainCamera.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, -10);
        mainCamera.GetComponent<Camera>().orthographicSize = 1f;
    }
    void mainCameraZoomOut()
    {
        mainCamera.transform.position = new Vector3(0, 0, -10);
        mainCamera.GetComponent<Camera>().orthographicSize = 3f;
    }
    void ObjectInfoText()
    {
        string a = obejectInfoText.text;
        if(selectedName == "Player")
        {
            a = $"name is SinMull, that's mean is fresh gochu. ";
        }
        else if(selectedName == "Lote Book")
        {
            a = "is very expensive! so writing power upping";
        }
        else if(selectedName == "Yoga")
        {
            a = "is good healthy! so dex power upping";
        }

        obejectInfoText.text = $" {selectedName} {a} ";
    }
}

