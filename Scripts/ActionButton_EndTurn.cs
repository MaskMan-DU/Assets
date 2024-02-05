using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton_EndTurn : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void EndTurn()
    {
        if (gameManager.activePiece != null)
        {
            var playerContorller = gameManager.activePiece.GetComponent<PlayerContoller>();

            // 按下该按钮后，棋子会进入ENDTURN状态
            playerContorller.state = PlayerContoller.State.ENDTURN;

            gameManager.PieceActionMenu.SetActive(false);
            gameManager.activePiece = null;
            gameManager.state = GameManager.State.ChangeToOtherSide;
        }
    }
}
