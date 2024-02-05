using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton_Finish : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void FinishPieceAction()
    {
        if (gameManager.activePiece != null)
        {
            var playerContorller = gameManager.activePiece.GetComponent<PlayerContoller>();

            playerContorller.state = PlayerContoller.State.IDLE;
            gameManager.PieceActionMenu.SetActive(false);
            gameManager.activePiece = null;
            gameManager.state = GameManager.State.SelectPiece;
        }
    }
}
