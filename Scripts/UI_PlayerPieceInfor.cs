using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerPieceInfor : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject Piece2;
    public GameObject Piece3;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Piece2.SetActive(false);
        Piece3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameManager.activeCamp)
        {
            case PlayerContoller.Camp.Group1:
                if (gameManager.Group1Piece.Count == 1)
                {
                    Piece2.SetActive(false);
                    Piece3.SetActive(false);
                }
                else if (gameManager.Group1Piece.Count == 2)
                {
                    Piece2.SetActive(true);
                }
                else if (gameManager.Group1Piece.Count == 3)
                {
                    Piece2.SetActive(true);
                    Piece3.SetActive(true);
                }
                break;

            case PlayerContoller.Camp.Group2:
                if (gameManager.Group2Piece.Count == 1)
                {
                    Piece2.SetActive(false);
                    Piece3.SetActive(false);
                }
                else if (gameManager.Group2Piece.Count == 2)
                {
                    Piece2.SetActive(true);
                }
                else if (gameManager.Group2Piece.Count == 3)
                {
                    Piece2.SetActive(true);
                    Piece3.SetActive(true);
                }
                break;

        }
    }
}
