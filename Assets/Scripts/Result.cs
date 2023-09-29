using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public static float score;
    public static int hits;
    public static int miss;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "testScene")
        {
            score = 0f;
            hits = 0;
            miss = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
