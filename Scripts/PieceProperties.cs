using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceProperties : MonoBehaviour
{
    public enum Profession
    {

    }

    public enum Weapon
    {

    }


    [Header("��������")]
    public int lifeValue;

    public int plusMovement;
    public int attackRange;

    public Profession pieceProfession;
    public int PieceLevel;

    public Weapon pieceWeapon;
    public int WeaponLevel;


    [Header("ʵʱ����")]
    public int currentLifeValue;

    // Start is called before the first frame update
    void Start()
    {
        currentLifeValue = lifeValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
