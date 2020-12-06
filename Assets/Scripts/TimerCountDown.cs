using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountDown : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 30;
    public bool takingAway = false;

    void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    void Update()
    {
        if(takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(Timetake());
        }
    }

    IEnumerator Timetake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if(secondsLeft <= 0)
        {
            textDisplay.GetComponent<Text>().text = "Time left 00:0" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "Time left 00:" + secondsLeft;
            takingAway = false;
        }
    }
}
