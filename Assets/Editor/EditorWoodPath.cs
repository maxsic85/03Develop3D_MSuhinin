using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EditorWoodPath : EditorWindow
{
    public Color _color;
    public MeshRenderer _mr;
    public Material _mat;
    public MeshCollider _col;
    GameObject main;


    [MenuItem("Editor/ Create / Others")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EditorWoodPath), false, "Editor");
    }

    private void OnGUI()
    {
        _mr = EditorGUILayout.ObjectField("MeshRend", _mr, typeof(MeshRenderer), true) as MeshRenderer;
        _mat = EditorGUILayout.ObjectField("Mat", _mat, typeof(Material), true) as Material;
        _col = EditorGUILayout.ObjectField("MeshCol", _col, typeof(MeshCollider), true) as MeshCollider;



        if (GUILayout.Button("Create"))
        {
            main = Instantiate(_mr.gameObject, Camera.main.transform.position + Vector3.forward*4, Quaternion.identity);
            main.AddComponent<MeshCollider>();
            var mcal = main.GetComponent<MeshCollider>();
            mcal.convex = true;
            mcal = _col;
            main.AddComponent<Rigidbody>();
        }

        else if (main)
        {
            //  _color = RGBSlider(new Rect(10, 30, 200, 20), _color);
            // _mr.sharedMaterial.color = _color;

          
        }

    }
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMaxValue, float sliderMinValue, string labelText) // �� �������� MinValue
    {
        // ������ ������������� � ������������ � ������������ � ������� ������� � ������� 
        Rect labelRect = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(labelRect, labelText);   // ������ Label �� ������

        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height); // ����� ������� ��������
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, 0.0f, sliderMaxValue); // ������������ ������� � ��������� ��� ��������
        return sliderValue; // ���������� �������� ��������
    }
    Color RGBSlider(Rect screenRect, Color rgb)
    {
        // ��������� ���������������� �������, ������ ���
        rgb.r = LabelSlider(screenRect, rgb.r, 1.0f, 0, "Red");

        // ������ ����������
        screenRect.y += 20;
        rgb.g = LabelSlider(screenRect, rgb.g, 1.0f, 0, "Green");

        screenRect.y += 20;
        rgb.b = LabelSlider(screenRect, rgb.b, 1.0f, 0, "Blue");

        screenRect.y += 20;
        rgb.a = LabelSlider(screenRect, rgb.a, 1.0f, 0, "alpha");

        return rgb; // ���������� ����
    }
}
