using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyTime : MonoBehaviour
{
    private static MyTime instance;
    public static MyTime Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MyTime>();
            }
            return instance;
        }
    }

    public float days, hours, minutes, speed;

    private void Start()
    {
        InvokeRepeating("DayTimeGo", 0.1f, 0.01f);
    }
    private void DayTimeGo()
    {
        //������
        minutes += speed / 10;
        if (minutes >= 60)
        {
            //����
            hours += 1;
            if (hours >= 24)
            {
                //���
                days += 1;
                hours = 0;
            }
            minutes = 0;
        }
    }
}
