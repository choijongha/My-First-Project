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
        // �ƹ� Ŭ���� �ȵǾ�����
        if (!onOneClicked)
        {
            onOneClicked = true;
            timerForDoubleClick = Time.time;            
        }
        // �� �� Ŭ���Ǿ��� ���� Ŭ���� �ȵ� ���¿��� ù��° Ŭ���� �̸��� �ι� ° Ŭ���� �̸��� ���� ��
        else if(!onDoubleClicked && selectedName == hit.collider.name)
        {
            onOneClicked = false;
            onDoubleClicked = true;
            selectedDoubleName = hit.collider.name;
            mainCameraZooming();
        }
        // ���� Ŭ���Ǿ��� ù ��° Ŭ���� �̸��� �� ��° Ŭ���� �̸��� ���� ���� Ŭ���� �̸��� ���� ��
        // ���� Ŭ���� ������Ʈ�� ���� Ŭ��.
        else if(onDoubleClicked && hit.collider.name == selectedDoubleName && selectedName == selectedDoubleName)
        {
            onOneClicked = false;
            onDoubleClicked = false;
            mainCameraZoomOut();
            selectedDoubleName = "";
        }
        // ���� Ŭ���Ǿ��� ù ��° Ŭ���� �̸��� ���� Ŭ���� �̸��� �ٸ� �� 
        // ���� Ŭ���� ������Ʈ �� �ٸ� ������Ʈ ���� Ŭ��.
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

