using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCancellButton : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void CancelAciton()
    {
        if (gameManager.activePiece != null)
        {
            var playerContorller = gameManager.activePiece.GetComponent<PlayerContoller>();

            // 按下该按钮后，棋子会进入IDLE状态，无论现在处于何种状态，清除移动范围和攻击范围的网格标识
            playerContorller.state = PlayerContoller.State.IDLE;
            playerContorller.CleanRange(playerContorller.moveRange);
            playerContorller.CleanRange(playerContorller.attackRangeCellList);
            // 还需要清理道具范围

            gameManager.PieceActionMenu.SetActive(true);
            gameManager.ActionCancelButton.SetActive(false);
        }
    }
}
