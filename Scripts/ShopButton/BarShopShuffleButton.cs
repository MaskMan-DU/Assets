using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarShopShuffleButton : MonoBehaviour
{
    public GameObject[] options;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shuffle()
    {
        foreach(var i in options)
        {
            i.GetComponent<BarShopButton>().RefreshInformation();
        }
    }
}
