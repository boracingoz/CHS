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
                isSelected = true;
                break;
            case "changePos":
                _getAssignedStand = stand;
                belongToCircleSockets = socket;
                _movePos = goToObj;
                changePos = true;
                break;
            case "backToSocket":
                backToSocket = true;
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
                if (belongToStand != null)
                {
                    Stand stand = belongToStand.GetComponent<Stand>();
                    if (stand != null && stand.circles.Count > 1)
                    {
                        GameObject previousCircle = stand.circles[^2];
                        if (previousCircle != null)
                        {
                            previousCircle.GetComponent<Circle>().canMove = false;
                        }
                    }
                }
                gameManager.isMove = false;
            }
        }
        if (backToSocket)
        {
            transform.position = Vector3.Lerp(transform.position, belongToCircleSockets.transform.position, .2f);
            if (Vector3.Distance(transform.position, belongToCircleSockets.transform.position) < .10f)
            {
                transform.position = belongToCircleSockets.transform.position;
                backToSocket = false;
                gameManager.isMove = false;
            }
        }
    }

    private void OnDestroy()
    {
        if (gameManager != null && gameManager.selectedCircle == gameObject)
        {
            gameManager.selectedCircle = null;
        }
    }
}
