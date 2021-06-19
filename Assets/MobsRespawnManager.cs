using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsRespawnManager : MonoBehaviour
{
    public List<Transform> _transformWolfs;
    public GameObject _wolf;
    // Start is called before the first frame update
    void Start()
    {
       // _transformWolfs = new List<Transform>();
        RespawnWolfs();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RespawnWolfs()
    {

        for (int j = 0; j < _transformWolfs.Count; j++)
        {
            var obj = Instantiate(_wolf);
            obj.transform.SetParent(_transformWolfs[j]);
            obj.transform.position = _transformWolfs[j].position;
        }
    }
}

