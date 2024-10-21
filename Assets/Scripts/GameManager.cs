using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject selectedCircle;
    public GameObject selectedStand;
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
                    if (selectedCircle != null && selectedStand != hit.collider.gameObject)
                    {
                        Stand stand = hit.collider.GetComponent<Stand>();

                        if (stand.circles.Count != 4 & stand.circles.Count !=0)
                        {
                            if (circle.color == stand.circles[^1].GetComponent<Circle>().color)
                            {
                                selectedStand.GetComponent<Stand>().ChangeSocketTransform(selectedCircle);

                                circle.Move("changePos", hit.collider.gameObject, stand.GetAvaibleSocket(), stand.movePos);

                                stand.emptySocket++;
                                stand.circles.Add(selectedCircle);

                                selectedCircle = null;
                                selectedStand = null;
                            }
                            else
                            {
                                circle.Move("backToSocket");
                                selectedCircle = null;
                                selectedStand = null;
                            }
                        }
                        else if (stand.circles.Count == 0)
                        {
                            selectedStand.GetComponent<Stand>().ChangeSocketTransform(selectedCircle);

                            circle.Move("changePos", hit.collider.gameObject, stand.GetAvaibleSocket(), stand.movePos);

                            stand.emptySocket++;
                            stand.circles.Add(selectedCircle);

                            selectedCircle = null;
                            selectedStand = null;
                        }
                        else
                        {
                            circle.Move("backToSocket");
                            selectedCircle = null;
                            selectedStand = null;
                        }
                    }
                    else if (selectedStand == hit.collider.gameObject)
                    {
                        circle.Move("backToSocket");
                        selectedCircle = null;
                        selectedStand = null;
                    }
                    else
                    {
                        Stand stand = hit.collider.GetComponent<Stand>();
                        selectedCircle = stand.TopMostCircle();
                        circle = selectedCircle.GetComponent<Circle>();
                        isMove = true;
                        if (circle.canMove)
                        {
                            circle.Move("IsSelected", null, null, circle.belongToStand.GetComponent<Stand>().movePos);
                            selectedStand = circle.belongToStand;
                        }
                    }
                }
            }
        }
    }
}
