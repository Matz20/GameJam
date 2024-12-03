using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollideEnter2D(Collision2D collision)
    {
      if(collision.gameObject.TryGetComponent<Fjende>(out Fjende fjendeComponent))
        {
            fjendeComponent.TakeDamage(10);
        }  
    
        Destroy(gameObject);
    }

}
