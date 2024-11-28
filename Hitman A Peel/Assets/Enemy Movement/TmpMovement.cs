using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey.MonoBehaviours;

public class TmpMovement : MonoBehaviour
{
    public static TmpMovement Instance { get; private set; }

    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    //[SerializeField] private FieldOfView fieldOfView;

    private void Awake()
    {
        
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
       // fieldOfView.SetOrigin(transform.position);
    } 
    public Vector3 getPosistion()
    {
        Vector3 position = transform.position;
        return position;
    }
    public Transform GetTransform()
    {
        Transform trans = transform;
        return trans;
    }
}
