using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton_Move : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if ( gameManager.activePiece != null )
        {
            var playerContorller = gameManager.activePiece.GetComponent<PlayerContoller>();
            if (playerContorller.steps <= 0)
            {
                this.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        
    }

    public void ActivePieceMovement()
    {
        if (gameManager.activePiece!= null)
        {
            gameManager.ActionCancelButton.SetActive(true);
            gameManager.PieceActionMenu.SetActive(false);

            var playerContorller = gameManager.activePiece.GetComponent<PlayerContoller>();

            if( playerContorller.steps > 0 )
            {
                playerContorller.ResetCellCross(); // ���������ͨ�����趨
                playerContorller.state = PlayerContoller.State.MOVESELECT; // �л�����״̬
                playerContorller.ShowRange(playerContorller.steps); // ��ʾ�ƶ���Χ 
            }
            else
            {
                playerContorller.state = PlayerContoller.State.IDLE;
            }
            
        }
    }
}
