using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TGS;

public class UI_PieceButton : MonoBehaviour
{
    private GameManager gameManager;
    private CameraController Camera;
    private TerrainGridSystem tgs;

    public Image ProfessionImage;
    public Image WeaponImage;
    public Image EquipmentImage;
    public TMP_Text ProfessionLevel;
    public TMP_Text WeaponLevel;
    public TMP_Text EquipmentLevel;
    public TMP_Text Ability;

    public int PieceIndex = 1;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Camera = GameObject.Find("Camera Rig").GetComponent<CameraController>();    
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePieceInfor();
        tgs = TerrainGridSystem.instance;
    }

    public void UpdatePieceInfor()
    {
        if (gameManager.Group1Piece[PieceIndex - 1].GetComponent<PlayerContoller>().state == PlayerContoller.State.WAITFORNEXTTURN && gameManager.activeCamp == PlayerContoller.Camp.Group1)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else if (gameManager.Group2Piece[PieceIndex - 1].GetComponent<PlayerContoller>().state == PlayerContoller.State.WAITFORNEXTTURN && gameManager.activeCamp == PlayerContoller.Camp.Group2)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }


    }

    public void SelectPiece()
    {
        switch (gameManager.activeCamp)
        {
            case PlayerContoller.Camp.Group1:
                var targetPieceController = gameManager.Group1Piece[PieceIndex-1].GetComponent<PlayerContoller>();
                if (targetPieceController.state != PlayerContoller.State.WAITFORNEXTTURN && gameManager.state == GameManager.State.SelectPiece)
                {
                    gameManager.state = GameManager.State.PieceAct;
                    gameManager.activePiece = gameManager.Group1Piece[PieceIndex - 1];
                    gameManager.PieceActionMenu.SetActive(true);                
                }
                break;

            case PlayerContoller.Camp.Group2:
                targetPieceController = gameManager.Group2Piece[PieceIndex - 1].GetComponent<PlayerContoller>();
                if (targetPieceController.state != PlayerContoller.State.WAITFORNEXTTURN && gameManager.state == GameManager.State.SelectPiece)
                {
                    gameManager.state = GameManager.State.PieceAct;
                    gameManager.activePiece = gameManager.Group2Piece[PieceIndex - 1];
                    gameManager.PieceActionMenu.SetActive(true);
                }
                break;

        }

        Camera.newPosition.x = tgs.CellGetPosition(gameManager.activePiece.GetComponent<PlayerContoller>().currentCellIndex, true).x;
        Camera.newPosition.z = tgs.CellGetPosition(gameManager.activePiece.GetComponent<PlayerContoller>().currentCellIndex, true).z;
    }
}
