using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerPieceInfor : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject Group1Piece2;
    public GameObject Group1Piece3;

    public GameObject Group2Piece2;
    public GameObject Group2Piece3;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Group1Piece2.SetActive(false);
        Group1Piece3.SetActive(false);

        Group2Piece2.SetActive(false);
        Group2Piece3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.Group1Piece.Count == 1)
        {
            Group1Piece2.SetActive(false);
            Group1Piece3.SetActive(false);
        }
        else if (gameManager.Group1Piece.Count == 2)
        {
            Group1Piece2.SetActive(true);
        }
        else if (gameManager.Group1Piece.Count == 3)
        {
            Group1Piece2.SetActive(true);
            Group1Piece3.SetActive(true);
        }

        if (gameManager.Group2Piece.Count == 1)
        {
            Group2Piece2.SetActive(false);
            Group2Piece3.SetActive(false);
        }
        else if (gameManager.Group2Piece.Count == 2)
        {
            Group2Piece2.SetActive(true);
        }
        else if (gameManager.Group2Piece.Count == 3)
        {
            Group2Piece2.SetActive(true);
            Group2Piece3.SetActive(true);
        }


        /*switch (gameManager.activeCamp)
        {
            case PlayerContoller.Camp.Group1:
                
                break;

            case PlayerContoller.Camp.Group2:
                
                break;

        }*/
    }
}
