using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private bool onSelected;
    private bool onOneClicked;
    private bool onDoubleClicked;
    private float timerForDoubleClick;
    private float delay = 0.5f;
    string selectedName;

    public GameObject seletedRingPrefabs;

    private Camera mainCamera;
    public GameObject nameBox;
    public TextMeshProUGUI nameText;
    private RectTransform rectTransform;

    private Vector2 targetPosition;
    private RaycastHit2D hit;
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rectTransform = nameBox.GetComponent<RectTransform>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectedDown();
        DoubleClickFalse();
         
        if(nameBox.activeSelf) UIName();
    }
    void SelectedDown()
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
        }else if(onDoubleClicked && selectedName == hit.collider.name)
        {
            onOneClicked = false;
            onDoubleClicked = false;
            Debug.Log("Double Click zoom out");
            mainCamera.transform.position = new Vector3(0,0,-10);
            mainCamera.GetComponent<Camera>().orthographicSize = 3f;
        }
    }
    void DoubleClickFalse()
    {
        if (onOneClicked)
        {
            if (Time.time - timerForDoubleClick > delay)
            {
                onOneClicked = false;
                Debug.Log("onOneClicked out");
            }
        }
    }
}

