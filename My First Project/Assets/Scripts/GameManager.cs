using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public bool selected;
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
    }
    void Selected()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            BoolOnSelected();
        }
    }
    
    void BoolOnSelected()
    {
        if (hit.collider != null)
        {
            //Vector2 targetPosition = GameObject.Find($"{hit.collider.name}").transform.position;
            targetPosition = hit.transform.position;
            UIName();
            if (!selected && !GameObject.FindGameObjectWithTag("SelectedRing"))
            {
                selected = true;
                nameBox.SetActive(true);
                Instantiate(seletedRingPrefabs, targetPosition, transform.rotation);
            }
            else if (selected && GameObject.FindGameObjectWithTag("SelectedRing"))
            {
                GameObject.FindGameObjectWithTag("SelectedRing").transform.position = targetPosition;
            }
        }
        else
        {
            selected = false;
            Destroy(GameObject.FindGameObjectWithTag("SelectedRing"));
            nameBox.SetActive(false);
        }
    }
    void UIName()
    {
        nameText.text = hit.collider.name;
        rectTransform.position = Camera.main.WorldToScreenPoint(targetPosition) - new Vector3(0, 30);
    }
}
