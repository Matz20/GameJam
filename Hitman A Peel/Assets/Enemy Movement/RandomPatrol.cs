using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPatrol : MonoBehaviour
{
    [SerializeField] private enum State
    {
        Roaming, 
        Chasing,
        Attacking
    }
    [SerializeField] private State state;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    [SerializeField] private float startWaitTime;
    [SerializeField] private Weapon weaponEquipped;
    public GameObject hero; 
    public Disguise dis;
    [SerializeField] private int minDisLvl;
    [SerializeField] private float targetRange;
    [SerializeField] private float attackRange;


    [SerializeField] private Transform moveSpot;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private int randomSpots;
    [SerializeField] private Vector3 direction;


    //[SerializeField] private FieldOfView fieldOfView;

    private void Awake()
    {
        state = State.Roaming;
    }

    private void Start()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
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
                
                    lookAt(hero.transform);
                    weaponEquipped.AttackWithEquipedWeapon(direction);
                    Debug.Log("Is attacking");
                    if (Vector3.Distance(transform.position, hero.transform.position) > attackRange)
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
        lookAt(moveSpot);
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
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
        
        if (Vector3.Distance(transform.position, hero.transform.position) < targetRange && dis.disguiseLvl < minDisLvl)
        {
            state = State.Chasing;
            
        }
    } 
    private void Chasing()
    {
        lookAt(hero.transform);
        transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, speed * Time.deltaTime);
        //fieldOfView.SetOrigin(transform.position);
        Attack();
        if (Vector3.Distance(transform.position, hero.transform.position) > targetRange)
        {
            state = State.Roaming;

        }
    }
    private void Attack()
    {
        if (Vector3.Distance(transform.position, hero.transform.position) < attackRange)
        {
            state = State.Attacking;

        }
        

    }

}
