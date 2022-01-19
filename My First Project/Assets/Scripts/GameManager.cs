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
    void Start()
    {
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
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                Vector2 targetPosition = GameObject.Find($"{hit.collider.name}").transform.position;
                Instantiate(seletedRingPrefabs, targetPosition, transform.rotation);
                nameText.text = hit.collider.name;
                nameBox.transform.position = targetPosition - new Vector2(0, -20);
                nameBox.SetActive(true);

            }
        }
    }
}
