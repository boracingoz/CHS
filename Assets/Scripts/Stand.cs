using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject movePos;
    public GameObject[] sockets;
    public List<GameObject> circles = new();
    [SerializeField] private GameManager _gameManager;
   

    public GameObject TopMostCircle()
    {
        return circles[^1];
    }
}
