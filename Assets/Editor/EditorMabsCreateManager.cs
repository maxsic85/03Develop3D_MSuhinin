using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(MobsRespawnManager))]
public class EditorMabsCreateManager : Editor
{
    public MobsRespawnManager tm;
    Transform _newTr;
    public void OnEnable()
    {
        tm = (MobsRespawnManager)target;


    }

    public override void OnInspectorGUI()
    {
      
        EditorGUILayout.BeginVertical("BOX");
        EditorGUILayout.HelpBox("Волки", MessageType.None);
        tm._wolf = ((GameObject)EditorGUILayout.ObjectField("Волк", tm._wolf, typeof(GameObject), true));

        for (int i = 0; i < tm._transformWolfs.Count; i++)
        {
            tm._transformWolfs[i] = ((Transform)EditorGUILayout.ObjectField($"Точка волк {i}", tm._transformWolfs[i], typeof(Transform), true));

        }
        if (GUILayout.Button("Добавить точку", GUILayout.Height(20)))
        {
            tm._transformWolfs.Add( _newTr);
        }
        if (GUILayout.Button("Удалить точку", GUILayout.Height(20)))
        {
            if(tm._transformWolfs.Count>0)
            tm._transformWolfs.RemoveAt(0);
        }
        //tm.cntWolfOnSceeene = EditorGUILayout.IntField("Кол-во на сцене", tm.cntWolfOnSceeene);
        //tm.Wolfs.respawnTime = EditorGUILayout.FloatField("Время респауна", tm.Wolfs.respawnTime);
        //tm.Wolfs.dropItem = ((GameObject)EditorGUILayout.ObjectField("Дроп", tm.Wolfs.dropItem, typeof(GameObject), true));
        EditorGUILayout.EndVertical();



    }



    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}
