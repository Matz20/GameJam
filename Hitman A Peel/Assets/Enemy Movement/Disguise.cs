using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disguise : MonoBehaviour
{
    public int disguiseLvl;

    [SerializeField]
    private enum State
    {
        None,
        Janitor,
        Guard
    }
    [SerializeField] private State state;

    private void Awake()
    {
        state = State.None;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.None:
                disguiseLvl = 0;
                break;
            case State.Janitor:
                disguiseLvl = 1;
                break;
            case State.Guard:
                disguiseLvl = 2;
                break;
            default:
                break;
        }


    } 
   

}
