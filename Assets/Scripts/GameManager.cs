using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject selectedCircle;
    public GameObject selectedStand;
    Circle circle;
    public bool isMove;
    public int targetStandColor;

    private int _completedOfColors;

    void Update()
    {
        if (selectedCircle != null && !selectedCircle.scene.isLoaded)
        {
            selectedCircle = null;
            circle = null;
        }

        if (selectedStand != null && !selectedStand.scene.isLoaded)
        {
            selectedStand = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (selectedCircle != null && selectedStand != null && selectedStand != hit.collider.gameObject)
                    {
                        Stand stand = hit.collider.GetComponent<Stand>();
                        if (stand != null && stand.circles != null && stand.circles.Count != 4)
                        {
                            if (stand.circles.Count == 0)
                            {
                                MoveCircleToEmptyStand(stand, hit.collider.gameObject);
                            }
                            else if (circle != null && stand.circles[^1] != null)
                            {
                                Circle topCircle = stand.circles[^1].GetComponent<Circle>();
                                if (topCircle != null && circle.color == topCircle.color)
                                {
                                    MoveCircleToStand(stand, hit.collider.gameObject);
                                }
                                else
                                {
                                    ReturnCircleToOriginalPosition();
                                }
                            }
                        }
                        else
                        {
                            ReturnCircleToOriginalPosition();
                        }
                    }
                    else if (selectedStand == hit.collider.gameObject)
                    {
                        ReturnCircleToOriginalPosition();
                    }
                    else
                    {
                        Stand stand = hit.collider.GetComponent<Stand>();
                        if (stand != null)
                        {
                            GameObject topCircle = stand.TopMostCircle();
                            if (topCircle != null)
                            {
                                selectedCircle = topCircle;
                                circle = selectedCircle.GetComponent<Circle>();
                                if (circle != null && circle.canMove)
                                {
                                    isMove = true;
                                    circle.Move("IsSelected", null, null, stand.movePos);
                                    selectedStand = circle.belongToStand;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void MoveCircleToStand(Stand stand, GameObject hitObject)
    {
        if (selectedStand != null)
        {
            Stand currentStand = selectedStand.GetComponent<Stand>();
            if (currentStand != null)
            {
                currentStand.ChangeSocketTransform(selectedCircle);
                circle.Move("changePos", hitObject, stand.GetAvaibleSocket(), stand.movePos);
                stand.emptySocket++;
                stand.circles.Add(selectedCircle);
                stand.CircleController();
                ResetSelection();
            }
        }
    }

    private void MoveCircleToEmptyStand(Stand stand, GameObject hitObject)
    {
        if (selectedStand != null)
        {
            Stand currentStand = selectedStand.GetComponent<Stand>();
            if (currentStand != null)
            {
                currentStand.ChangeSocketTransform(selectedCircle);
                circle.Move("changePos", hitObject, stand.GetAvaibleSocket(), stand.movePos);
                stand.emptySocket++;
                stand.circles.Add(selectedCircle);
                stand.CircleController();
                ResetSelection();
            }
        }
    }

    private void ReturnCircleToOriginalPosition()
    {
        if (circle != null)
        {
            circle.Move("backToSocket");
            ResetSelection();
        }
    }

    private void ResetSelection()
    {
        selectedCircle = null;
        selectedStand = null;
    }

    public void CompletedOfColors()
    {
        _completedOfColors++;
        if (_completedOfColors == targetStandColor)
        {
            Debug.Log("Kazandýn");
        }
    }
}
