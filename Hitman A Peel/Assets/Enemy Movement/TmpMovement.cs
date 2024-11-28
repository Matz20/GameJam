using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey.MonoBehaviours;

public class TmpMovement : MonoBehaviour
{
    public static TmpMovement Instance { get; private set; }
    private PlayerInputHandler inputHandler;

    Rigidbody2D body;
    [SerializeField] private Weapon weaponEquipped;
    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    //[SerializeField] private FieldOfView fieldOfView;

    private void Awake()
    {
        inputHandler = PlayerInputHandler.Instance;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (inputHandler.AttackTriggered && weaponEquipped != null)
        {
            Debug.Log($"Attacking with weapon {weaponEquipped.name}");
           // weaponEquipped.AttackWithEquipedWeapon(transform.position);
        }

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
