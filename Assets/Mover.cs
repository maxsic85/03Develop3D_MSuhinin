using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public Transform _lockPositionA;
    public Transform _lockPositionB;
    bool left;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, _lockPositionA.position) < 0.0001f&& left)
        {
            left = false;
            
        }
        else if (Vector3.Distance(transform.position, _lockPositionB.position) < 0.0001f &&!left)
        {
            left = true;
        }

        transform.localPosition =(left)? Vector3.MoveTowards(gameObject.transform.position, _lockPositionA.position, step): Vector3.MoveTowards(gameObject.transform.position, _lockPositionB.position, step);


    }
}
