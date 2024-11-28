using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private enum State
    {
        Roaming, 
        Chasing,
        Attacking
    }
    private State state;
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public Weapon weaponEquipped;
    public GameObject player;
    public float targetRange; 
    public float attackRange;
   

    public Transform[] moveSpots;
    private int randomSpots;
    private Vector3 direction;

    private bool isPatroling;

    //[SerializeField] private FieldOfView fieldOfView;

    private void Awake()
    {
        state = State.Roaming;
    }

    private void Start()
    {
        waitTime = startWaitTime;
        randomSpots = Random.Range(0, moveSpots.Length);
        isPatroling = true;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Roaming:
                patroling();
                break;
            case State.Chasing:
                Chasing();
                break;
            case State.Attacking:
                
                    lookAt(player.transform);
                    weaponEquipped.AttackWithEquipedWeapon(direction);
                    Debug.Log("Is attacking");
                    if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
                    {
                        state = State.Chasing;
                    }
                
                break;
            default:
                break;
        }

       
        
    }
    private void lookAt(Transform pos)
    {
        direction = pos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
       // fieldOfView.SetAimDirection(direction);
    }
    private void patroling()
    {
        FindTarget();
        lookAt(moveSpots[randomSpots]);
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpots].position, speed * Time.deltaTime);
        //fieldOfView.SetOrigin(transform.position);

        if (Vector2.Distance(transform.position, moveSpots[randomSpots].position) < 0.2f)
        {
            if (waitTime <= 0)
            {

                randomSpots = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }

    }
    private void FindTarget()
    {
        
        if (Vector3.Distance(transform.position, player.transform.position) < targetRange)
        {
            state = State.Chasing;
            
        }
    } 
    private void Chasing()
    {
        lookAt(player.transform);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //fieldOfView.SetOrigin(transform.position);
        Attack();
        if (Vector3.Distance(transform.position, player.transform.position) > targetRange)
        {
            state = State.Roaming;

        }
    }
    private void Attack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            state = State.Attacking;

        }
        

    }

}
