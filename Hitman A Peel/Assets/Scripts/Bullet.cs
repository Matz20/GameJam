using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OncollideEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }






}
