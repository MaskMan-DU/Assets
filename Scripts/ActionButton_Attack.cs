using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton_Attack : MonoBehaviour
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
            if (playerContorller.hasAttack)
            {
                this.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = true;
            }
        }

    }

    public void Attack()
    {
        if (gameManager.activePiece != null)
        {
            gameManager.ActionCancelButton.SetActive(true);
            gameManager.PieceActionMenu.SetActive(false);

            var playerContorller = gameManager.activePiece.GetComponent<PlayerContoller>();

            if (!playerContorller.hasAttack)
            {
                playerContorller.ResetCellCross(); // ���������ͨ�����趨
                playerContorller.state = PlayerContoller.State.ATTACKSELECT; // �л�����״̬
                playerContorller.ShowRange(playerContorller.attackRange); // ��ʾ������Χ 
            }
            else
            {
                playerContorller.state = PlayerContoller.State.IDLE;
            }

        }
    }
}
