using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSettings : MonoBehaviour
{
    [SerializeField]
    Color Sky, Equator, Ground, SunColor; // Цвета солнца, и Ambient Skybox
    [SerializeField]
    float RotateSpeed; // Скорость вращения солнца
    private const float minutesForAround = 360f / 1440f;
    Light Sun; // Ссылка на источник освещения

    void Start()
    {
        Sun = GetComponent<Light>();
    }
    void Update()
    {
        SetRenderer();
        if (transform.name == "Moon")
        {
            transform.localRotation = Quaternion.Euler(((MyTime.Instance.minutes + (MyTime.Instance.hours * 60)) * minutesForAround), 0f, 0f);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(((MyTime.Instance.minutes + (MyTime.Instance.hours * 60)) * minutesForAround) - 180.0f, 0f, 0f);
        }
    }
    private void SetRenderer()
    {
        RenderSettings.ambientSkyColor = Sky;
        RenderSettings.ambientGroundColor = Ground;
        RenderSettings.ambientEquatorColor = Equator;
        // Настраиваем цвет солнца
        Sun.color = SunColor;
    }
}
