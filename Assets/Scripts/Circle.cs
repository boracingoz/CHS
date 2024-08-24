using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public GameObject belongToStand;
    public GameObject belongToCircleSockets;
    public bool canMove;
    public string color;
    public GameManager gameManager;


    private GameObject _movePos;
    private GameObject _belongToStand;

    bool isSelected, changePos, socketSeated, backToSocket;

    public void Move(string transaction, GameObject stand = null, GameObject socket = null, GameObject goToObj = null)
    {
        switch (transaction)
        {
            case "IsSelected":
                _movePos = goToObj;
                isSelected = true; // Bu satýrý ekleyin
                break;

            case "changePos":
                break;

            case "socketSeated":
                break;

            case "backToSocket":
                break;
        }  
    }

    private void Update()
    {
        if (isSelected) 
        {
            transform.position = Vector3.Lerp(transform.position, _movePos.transform.position, .2f);
            if (Vector3.Distance(transform.position, _movePos.transform.position) < .10f)
            {
                isSelected = false;
             
            }
        }
    }
}
