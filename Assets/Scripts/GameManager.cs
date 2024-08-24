using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject selectedCircle;
    public GameObject selectedGround;
    Circle circle;
    public bool isMove;


    public int targetStandNumber;
    public int currentStandNumber; //tamamlanan stand sayýsý.

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (selectedCircle !=null && selectedCircle != hit.collider.gameObject)
                    {//çemberi gönder. 
                        
                    }
                    else
                    {
                        Stand stand = hit.collider.GetComponent<Stand>();
                        selectedCircle = stand.TopMostCircle();
                        circle = selectedCircle.GetComponent<Circle>();
                        isMove = true;
                    }
                }
            }
        }
    }
}
