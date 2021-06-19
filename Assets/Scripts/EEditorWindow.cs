using UnityEngine;

public class EEditorWindow : MonoBehaviour
{
    private float _myHP;

#if UNITY_EDITOR
    void OnGUI()
    {
        _myHP = GetComponent<HP>().Health;
        _myHP = labelSlider(new Rect(10, 10, 200, 20), _myHP, 100.0f,0, "HP"); // Отрисовка HP
        
    }

    float labelSlider(Rect screenRect, float sliderValue, float sliderMaxValue,float sliderMinValue, string labelText)
    {
        Rect Label = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(Label, labelText);
        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height); // Задаём размеры слайдера
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, 0.0f, sliderMaxValue); // Вырисовываем слайдер и считываем его параметр
        return sliderValue; // Возвращаем значение слайдера
    }
#endif
}
