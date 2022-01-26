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
    public bool onObjectUsed;
    float timerForDoubleClick;
    float doubleClickdelay;
    public string selectedName { get; private set; }
    string selectedDoubleName;
    string objectInfo;


    [SerializeField] GameObject seletedRingPrefabs;
    [SerializeField] GameObject nameBox;
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI nameText;
    public TextMeshProUGUI objectInfoText;

    Camera mainCamera;
    RectTransform rectTransform;
    
    Rigidbody2D player;

    Vector2 targetPosition;
    RaycastHit2D hit;
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rectTransform = nameBox.GetComponent<RectTransform>();
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();

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
    public void FixedUpdate()
    {
        if (onObjectUsed)
        {
            Vector3 targetPos = GameObject.Find($"{selectedName}").transform.position;
            if (player.GetComponent<Collider2D>().OverlapPoint(targetPos))
            {
                onObjectUsed = false;
            }
            player.MovePosition(player.transform.position + targetPos* Time.fixedDeltaTime);
        }
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
            onSelected = true;
            InstantiateSelectRing();
            DoubleClick();
            UIName();
            ObjectInfoText();
        }
        else if (EventSystem.current.IsPointerOverGameObject())
        {}
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
        // 아무 클릭도 안되어있음
        if (!onOneClicked)
        {
            onOneClicked = true;
            timerForDoubleClick = Time.time;            
        }
        // 한 번 클릭되었고 더블 클릭이 안된 상태에서 첫번째 클릭된 이름과 두번 째 클릭한 이름이 같을 때
        else if(!onDoubleClicked && selectedName == hit.collider.name)
        {
            onOneClicked = false;
            onDoubleClicked = true;
            selectedDoubleName = hit.collider.name;
            mainCameraZooming();
        }
        // 더블 클릭되었고 첫 번째 클릭된 이름과 두 번째 클릭한 이름이 같고 더블 클릭된 이름도 같을 때
        // 더블 클릭된 오브젝트를 더블 클릭.
        else if(onDoubleClicked && hit.collider.name == selectedDoubleName && selectedName == selectedDoubleName)
        {
            onOneClicked = false;
            onDoubleClicked = false;
            mainCameraZoomOut();
            selectedDoubleName = "";
        }
        // 더블 클릭되었고 첫 번째 클릭된 이름과 더블 클릭된 이름이 다를 때 
        // 더블 클릭된 오브젝트 외 다른 오브젝트 더블 클릭.
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
    public void ObjectUse()
    {
        onObjectUsed = true;
    }
    void ObjectInfoText()
    {
        objectInfo = GameObject.Find($"{selectedName}").GetComponent<ObjectInfoScript>().objectInfoTextString;
        objectInfoText.text = $" {selectedName} {objectInfo} ";
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
    
}

