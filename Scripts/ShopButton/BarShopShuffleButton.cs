using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarShopShuffleButton : MonoBehaviour
{
    public GameObject[] options;

    public void Shuffle()
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManager.activePiece != null)
        {
            switch (gameManager.activeCamp)
            {
                case PlayerContoller.Camp.Group1:
                    if (gameManager.group1Coin >= 5)
                    {
                        gameManager.group1Coin -= 5;
                        foreach (var i in options)
                        {
                            i.GetComponent<WeaponStoreButton>().RefreshInformation();
                        }
                    }
                    break;
                case PlayerContoller.Camp.Group2:
                    if (gameManager.group2Coin >= 5)
                    {
                        gameManager.group2Coin -= 5;
                        foreach (var i in options)
                        {
                            i.GetComponent<WeaponStoreButton>().RefreshInformation();
                        }
                    }
                    break;
            }
        }
    }
}
