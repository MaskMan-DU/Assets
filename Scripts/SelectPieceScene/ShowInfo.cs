using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    public GameObject[] uniqueAbilities;
    public int currentAbility;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < uniqueAbilities.Length; i++)
        {
            if (i == currentAbility)
            {
                uniqueAbilities[i].SetActive(true);
            }
            else
            {
                uniqueAbilities[i].SetActive(false);
            }
        }
    }

    public void ChangeAbility(int optionIndex)
    {
        currentAbility = optionIndex;
    }
}
