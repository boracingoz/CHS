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
    private int _circileCount;
   

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

    public void CircleController()
    {
        if (circles.Count == 4)
        {
            string color = circles[0].GetComponent<Circle>().color;
            bool allSameColor = true;

            foreach (var circle in circles)
            {
                if (color != circle.GetComponent<Circle>().color)
                {
                    allSameColor = false;
                    break;
                }
            }

            if (allSameColor)
            {
                Debug.Log("Kazandýnýz!");
                StartCoroutine(DestroyStandAfterDelay(1f));
                _gameManager.CompletedOfColors();
            }
        }
    }

    private IEnumerator DestroyStandAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CompletedOfStand();
        Destroy(gameObject);
    }

    void CompletedOfStand()
    {
        foreach (var circle in circles)
        {
            circle.GetComponent<Circle>().canMove = false;
            Destroy(circle);
        }
    }
}
