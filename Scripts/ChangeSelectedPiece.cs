using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSelectedPiece : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] pieces;
    public int currentpiece;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            if (i == currentpiece)
            {
                pieces[i].SetActive(true);
            }
            else
            {
                pieces[i].SetActive(false);
            }
        }
    }

    public void ChangeMode(int optionIndex)
    {
        currentpiece = optionIndex;
    }
}
