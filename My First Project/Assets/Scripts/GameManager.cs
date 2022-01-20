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
            selected = true;
            Debug.Log(Input.mousePosition); 
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(worldPoint);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null)
            {
                Vector2 targetPosition = GameObject.Find($"{hit.collider.name}").transform.position;
                Instantiate(seletedRingPrefabs, targetPosition, transform.rotation);

                rectTransform.position = Camera.main.WorldToScreenPoint(targetPosition) - new Vector3(0,30);
                nameBox.SetActive(true);

            }
        }
    }
}
