using System.Collections;
using System.Collections.Generic;
using TGS;
using UnityEngine;

public class GroupProperties : MonoBehaviour
{
    TerrainGridSystem tgs;
    private GameManager gameManager;
    private TGSSetting tgsSetting;

    public enum Camp
    {
        Group1,
        Group2,
    }

    public Camp camp;
    public List<GameObject> pieceGroup = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        tgs = TerrainGridSystem.instance;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tgsSetting = GameObject.Find("GameManager").GetComponent<TGSSetting>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (camp)
        {
            case Camp.Group1:
                pieceGroup = gameManager.Group1Piece;

                break;
            case Camp.Group2:
                pieceGroup = gameManager.Group2Piece;

                break;
        }
    }

    /// <summary>
    /// All team members' weapon damage +1 (+2, +4)
    /// </summary>
    public void Spiritual_Leader()
    {

    }

    public void Nutritionist()
    {

    }
}
