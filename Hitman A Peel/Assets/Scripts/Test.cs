    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // List of strings
    private List<string> words;

    // Start is called before the first frame update
    void Start()
    {
        words = new List<string>();
        words.Add("This");
        words.Add("Is a");
        words.Add("List");
    }
    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}