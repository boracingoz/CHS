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
        if (circles != null && circles.Count > 0)
        {
            GameObject topCircle = circles[^1];
            if (topCircle != null && topCircle.scene.isLoaded)
            {
                return topCircle;
            }
            else
            {
                circles.RemoveAt(circles.Count - 1);
                return null;
            }
        }
        return null;
    }


    public GameObject GetAvaibleSocket()
    {
        if (sockets != null && emptySocket >= 0 && emptySocket < sockets.Length)
        {
            return sockets[emptySocket];
        }
        return null;
    }

    public void ChangeSocketTransform(GameObject deleteObj)
    {
        if (deleteObj == null || !deleteObj.scene.isLoaded) return;

        // Circle'ý listeden kaldýr
        circles.Remove(deleteObj);

        if (circles.Count != 0)
        {
            emptySocket--;
            GameObject topCircle = circles[^1];
            if (topCircle != null)
            {
                Circle circleComponent = topCircle.GetComponent<Circle>();
                if (circleComponent != null)
                {
                    circleComponent.canMove = true;
                }
            }
        }
        else
        {
            emptySocket = 0;
        }
    }

    private void UpdateInternalState()
    {
        if (circles.Count != 0)
        {
            emptySocket--;
            GameObject topCircle = circles[^1];
            if (topCircle != null && topCircle.scene.isLoaded)
            {
                Circle circleComponent = topCircle.GetComponent<Circle>();
                if (circleComponent != null)
                {
                    circleComponent.canMove = true;
                }
            }
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
            string firstColor = circles[0].GetComponent<Circle>().color;
            bool allSameColor = true;

            foreach (var circle in circles)
            {
                if (firstColor != circle.GetComponent<Circle>().color)
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
        foreach (var circle in circles.ToArray())
        {
            if (circle != null)
            {
                Circle circleComponent = circle.GetComponent<Circle>();
                if (circleComponent != null && circleComponent.belongToStand == gameObject)
                {
                    circleComponent.canMove = false;
                    Destroy(circle);
                }
            }
        }
        circles.Clear();
    }

    private void OnDestroy()
    {
        if (_gameManager != null)
        {
            if (_gameManager.selectedStand == gameObject)
            {
                _gameManager.selectedStand = null;
            }
        }
    }
}
