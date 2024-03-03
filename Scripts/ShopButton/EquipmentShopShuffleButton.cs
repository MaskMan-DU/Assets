using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentShopShuffleButton : MonoBehaviour
{
    public GameObject[] options;

    public void Shuffle()
    {
        foreach (var i in options)
        {
            i.GetComponent<EquipmentShopButton>().RefreshInformation();
        }
    }
}
