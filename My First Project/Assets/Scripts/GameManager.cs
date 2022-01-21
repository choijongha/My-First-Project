using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public bool onSelected;
    public bool onOneClicked;
    public bool onDoubleClicked;
    public float timerForDoubleClick;
    public float delay = 0.5f;

    public GameObject seletedRingPrefabs;
    public GameObject mainCamera;
    public GameObject nameBox;
    public TextMeshProUGUI nameText;
    public RectTransform rectTransform;

    private Vector2 targetPosition;
    private RaycastHit2D hit;

    void Start()
    {
        rectTransform = nameBox.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Selected();
        DoubleClickFalse();
    }
    void Selected()
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
            UIName();
            DoubleClick();
            if (!onSelected && !GameObject.FindGameObjectWithTag("SelectedRing"))
            {
                onSelected = true;
                nameBox.SetActive(true);
                Instantiate(seletedRingPrefabs, targetPosition, transform.rotation);
            }
            else if (onSelected && GameObject.FindGameObjectWithTag("SelectedRing"))
            {
                GameObject.FindGameObjectWithTag("SelectedRing").transform.position = targetPosition;
                if (Time.time - timerForDoubleClick > delay) ;
            }
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
        rectTransform.position = Camera.main.WorldToScreenPoint(targetPosition) - new Vector3(0, 30);
    }
    void DoubleClick()
    {
        if (!onOneClicked)
        {
            onOneClicked = true;
            timerForDoubleClick = Time.time;
        }
        else
        {
            onOneClicked = false;
            onDoubleClicked = true;
            Debug.Log("Double Click");
            mainCamera.GetComponent<Camera>().orthographicSize = 1f;
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

