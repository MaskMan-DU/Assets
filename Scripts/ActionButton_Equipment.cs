using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton_Equipment : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.activePiece != null)
        {
            var playerContorller = gameManager.activePiece.GetComponent<PlayerContoller>();
            var pieceProperties = gameManager.activePiece.GetComponent<PieceProperties>();

            if (pieceProperties.equipment == PieceProperties.Equipment.Bulletproof_Vest)
            {
                this.gameObject.GetComponent<Button>().interactable = false;
            }else if (playerContorller.usedEquipment)
            {
                this.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = true;
            }
        }

    }

    public void UseEquipment()
    {
        if (gameManager.activePiece != null)
        {
            var playerContorller = gameManager.activePiece.GetComponent<PlayerContoller>();
            var pieceProperties = gameManager.activePiece.GetComponent<PieceProperties>();
            playerContorller.state = PlayerContoller.State.EQUIPMENTPOSITONSELECT; // 切换棋子状态

            if (!playerContorller.usedEquipment)
            {
                playerContorller.ResetCellCross(); // 先清除不能通过的设定
                
                if (pieceProperties.equipment == PieceProperties.Equipment.Wire)
                {
                    
                    gameManager.ActionCancelButton.SetActive(true);
                    gameManager.PieceActionMenu.SetActive(false);
                    playerContorller.ShowRange(playerContorller.equipmentRange); // 显示攻击范围 
                }
            }
            else
            {
                playerContorller.state = PlayerContoller.State.IDLE;
            }

        }
    }
}
