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

            // ���¸ð�ť�����ӻ����IDLE״̬���������ڴ��ں���״̬������ƶ���Χ�͹�����Χ�������ʶ
            playerContorller.state = PlayerContoller.State.IDLE;
            playerContorller.CleanRange(playerContorller.moveRange);
            playerContorller.CleanRange(playerContorller.attackRangeCellList);
            // ����Ҫ������߷�Χ

            gameManager.PieceActionMenu.SetActive(true);
            gameManager.ActionCancelButton.SetActive(false);
        }
    }
}
