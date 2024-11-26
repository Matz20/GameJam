using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpots;
    private Vector2 direction;
    private void Start()
    {
        waitTime = startWaitTime;
        randomSpots = Random.Range(0, moveSpots.Length); 
        
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpots].position,speed * Time.deltaTime);

        if (Vector2.Distance(transform.position,moveSpots[randomSpots].position)< 0.2f)
        {
            if (waitTime <= 0)
            {
                
                lookAt();
                randomSpots = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }
        
    }
    private void lookAt()
    {
        direction = moveSpots[randomSpots].position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

}
