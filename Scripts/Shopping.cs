using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopping : MonoBehaviour
{
    public GameObject[] shopItems;
    public int[] rate = new int[] { 19, 19, 19, 19, 5, 5, 5, 5, 1, 1, 1, 1 };
    int totalRate = 100;

    // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Rand(rate, totalRate);
        Debug.Log(randomIndex);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public int Rand(int[] rate, int total)
    {
        int r = Random.Range(1, total + 1);
        int t = 0;
        for (int i = 0; i < rate.Length; i++)
        {
            t += rate[i];
            if (r < t)
            {
                rate[i] -= 1;
                totalRate -= 1;
                return i;
            }
        }
        return 0;
    }
}
