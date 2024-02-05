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

            // ���¸ð�ť�����ӻ����ENDTURN״̬
            playerContorller.state = PlayerContoller.State.ENDTURN;

            gameManager.PieceActionMenu.SetActive(false);
            gameManager.activePiece = null;
            gameManager.state = GameManager.State.ChangeToOtherSide;
        }
    }
}
