using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStoreShuffleButton : MonoBehaviour
{
    public GameObject[] options;

    public void Shuffle()
    {
        foreach (var i in options)
        {
            i.GetComponent<WeaponStoreButton>().RefreshInformation();
        }
    }
}
