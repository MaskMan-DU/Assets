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

    public GameObject actionCheck;

    public Image ProfessionImage;
    public Image WeaponImage;
    public Image EquipmentImage;
    public TMP_Text ProfessionLevel;
    public TMP_Text WeaponLevel;
    public TMP_Text EquipmentLevel;
    public TMP_Text Ability;
    public Slider LifeValue;

    public PlayerContoller.Camp camp = PlayerContoller.Camp.Group1;

    public int PieceIndex;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Camera = GameObject.Find("Camera Rig").GetComponent<CameraController>();
        tgs = TerrainGridSystem.instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePieceInfor();
        UpdateLifeValue();
    }

    public void UpdateLifeValue()
    {
        switch (camp)
        {
            case PlayerContoller.Camp.Group1:

                if (gameManager.Group1Piece.Count >= PieceIndex)
                {
                    var piece = gameManager.Group1Piece[PieceIndex - 1].GetComponent<PieceProperties>();
                    LifeValue.value = piece.currentLifeValue / piece.finalLifeValue;
                }
                

                break;
            case PlayerContoller.Camp.Group2:
                if (gameManager.Group2Piece.Count >= PieceIndex)
                {
                    var piece = gameManager.Group2Piece[PieceIndex - 1].GetComponent<PieceProperties>();
                    LifeValue.value = piece.currentLifeValue / piece.finalLifeValue;
                }
                break;
        }
    }

    public void UpdatePieceInfor()
    {
        switch (camp)
        {
            case PlayerContoller.Camp.Group1:

                if (gameManager.Group1Piece.Count >= PieceIndex)
                {
                    var piece = gameManager.Group1Piece[PieceIndex - 1].GetComponent<PieceProperties>();
                    ProfessionLevel.text = "Level: " + piece.PieceLevel.ToString();
                    WeaponLevel.text = "Level: " + piece.WeaponLevel.ToString();

                    ProfessionImage.sprite = Resources.Load<Sprite>("Images/UI/" + piece.pieceProfession.ToString() + "" + piece.PieceLevel);
                    WeaponImage.sprite = Resources.Load<Sprite>("Images/UI/" + piece.pieceWeapon.ToString() + "" + piece.WeaponLevel);
                    EquipmentImage.sprite = Resources.Load<Sprite>("Images/UI/" + piece.equipment.ToString() + "" + piece.equipmentLevel);

                    if (piece.equipment != PieceProperties.Equipment.None)
                    {
                        EquipmentImage.color = Color.white;
                        EquipmentLevel.text = "Level: " + piece.equipmentLevel.ToString();
                    }
                    else
                    {
                        EquipmentImage.color = new Color(0f, 0f, 0f, 0.3f);
                        EquipmentLevel.text = "No Equipment";
                    }
                    
                    Ability.text = piece.ability.ToString();

                    var pieceController = gameManager.Group1Piece[PieceIndex - 1].GetComponent<PlayerContoller>();
                    if (pieceController.state != PlayerContoller.State.WAITFORNEXTTURN && gameManager.activeCamp == camp)
                    {
                        actionCheck.SetActive(true);
                    }
                    else
                    {
                        actionCheck.SetActive(false);
                    }

                }


                break;
            case PlayerContoller.Camp.Group2:
                if (gameManager.Group2Piece.Count >= PieceIndex)
                {
                    var piece = gameManager.Group2Piece[PieceIndex - 1].GetComponent<PieceProperties>();
                    ProfessionLevel.text = "Level: " + piece.PieceLevel.ToString();
                    WeaponLevel.text = "Level: " + piece.WeaponLevel.ToString();

                    ProfessionImage.sprite = Resources.Load<Sprite>("Images/UI/" + piece.pieceProfession.ToString() + "" + piece.PieceLevel);
                    WeaponImage.sprite = Resources.Load<Sprite>("Images/UI/" + piece.pieceWeapon.ToString() + "" + piece.WeaponLevel);
                    EquipmentImage.sprite = Resources.Load<Sprite>("Images/UI/" + piece.equipment.ToString() + "" + piece.equipmentLevel);

                    if (piece.equipment != PieceProperties.Equipment.None)
                    {
                        EquipmentImage.color = Color.white;
                        EquipmentLevel.text = "Level: " + piece.equipmentLevel.ToString();
                    }
                    else
                    {
                        EquipmentImage.color = new Color(0f, 0f, 0f, 0.3f);
                        EquipmentLevel.text = "No Equipment";
                    }
                    Ability.text = piece.ability.ToString();

                    var pieceController = gameManager.Group2Piece[PieceIndex - 1].GetComponent<PlayerContoller>();
                    if (pieceController.state != PlayerContoller.State.WAITFORNEXTTURN && gameManager.activeCamp == camp)
                    {
                        actionCheck.SetActive(true);
                    }
                    else
                    {
                        actionCheck.SetActive(false);
                    }
                }
                break;
        }
    }

    public void SelectPiece()
    {
        switch (camp)
        {
            case PlayerContoller.Camp.Group1:
                if (gameManager.activeCamp == PlayerContoller.Camp.Group1)
                {
                    var targetPieceController = gameManager.Group1Piece[PieceIndex - 1].GetComponent<PlayerContoller>();
                    if (targetPieceController.state != PlayerContoller.State.WAITFORNEXTTURN && gameManager.state == GameManager.State.SelectPiece)
                    {
                        gameManager.state = GameManager.State.PieceAct;
                        gameManager.activePiece = gameManager.Group1Piece[PieceIndex - 1];
                        gameManager.PieceActionMenu.SetActive(true);
                    }
                }
                break;

            case PlayerContoller.Camp.Group2:
                if (gameManager.activeCamp == PlayerContoller.Camp.Group2)
                {
                    var targetPieceController = gameManager.Group2Piece[PieceIndex - 1].GetComponent<PlayerContoller>();
                    if (targetPieceController.state != PlayerContoller.State.WAITFORNEXTTURN && gameManager.state == GameManager.State.SelectPiece)
                    {
                        gameManager.state = GameManager.State.PieceAct;
                        gameManager.activePiece = gameManager.Group2Piece[PieceIndex - 1];
                        gameManager.PieceActionMenu.SetActive(true);
                    }
                }
                
                break;

        }

        Camera.newPosition.x = tgs.CellGetPosition(gameManager.activePiece.GetComponent<PlayerContoller>().currentCellIndex, true).x;
        Camera.newPosition.z = tgs.CellGetPosition(gameManager.activePiece.GetComponent<PlayerContoller>().currentCellIndex, true).z;
    }
}
