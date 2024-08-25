using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject movePos;
    public GameObject[] sockets;
    public int emptySocket;
    public List<GameObject> circles = new();
    [SerializeField] private GameManager _gameManager;

   

    public GameObject TopMostCircle()
    {
        return circles[^1];
    }

    public GameObject GetAvaibleSocket()
    {
        return sockets[emptySocket];
    }

    public void ChangeSocketTransform(GameObject deleteobj)
    {
        circles.Remove(deleteobj);
        if (circles.Count!=0)
        {
            emptySocket--;
            circles[^1].GetComponent<Circle>().canMove = true;
        }
        else
        {
            emptySocket = 0;
        }
    }
}
