using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    public Vector3 targetPosition;
    public bool selected;
    public GameObject seletedRingPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
            Instantiate(seletedRingPrefabs, targetPosition, transform.rotation);
        }
    }
}
