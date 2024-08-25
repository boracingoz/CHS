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
    private GameObject _getAssignedStand;

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
                _getAssignedStand = stand;
                belongToCircleSockets =  socket;
                _movePos = goToObj;
                changePos = true;
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
        if (changePos)
        {
            transform.position = Vector3.Lerp(transform.position, _movePos.transform.position, .2f);
            if (Vector3.Distance(transform.position, _movePos.transform.position) < .10f)
            {
                changePos = false;
                socketSeated = true;

            }
        }
        if (socketSeated)
        {
            transform.position = Vector3.Lerp(transform.position, belongToCircleSockets.transform.position, .2f);
            if (Vector3.Distance(transform.position, belongToCircleSockets.transform.position) < .10f)
            {
                transform.position = belongToCircleSockets.transform.position;
                socketSeated = false;

                belongToStand = _getAssignedStand;

                if (belongToStand.GetComponent<Stand>().circles.Count>1)
                {
                    belongToStand.GetComponent<Stand>().circles[^2].GetComponent<Circle>().canMove = false;
                }
                gameManager.isMove = false;
            }
        }
    }
}
