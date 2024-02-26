using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Button startButton;

    public bool group1Confirm = false;
    public bool group2Confirm = false;

    public PieceProperties.Profession group1Profession = PieceProperties.Profession.Cowboy;
    public PieceProperties.Profession group2Profession = PieceProperties.Profession.Cowboy;

    public PieceProperties.Ability group1Ability = PieceProperties.Ability.Spiritual_Leader;
    public PieceProperties.Ability group2Ability = PieceProperties.Ability.Spiritual_Leader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (group1Confirm && group2Confirm)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    public void Group1ConfirmChange()
    {
        if (group1Confirm)
        {
            group1Confirm = false;
        }
        else
        {
            group1Confirm= true;
        }
    }

    public void Group2ConfirmChange()
    {
        if (group2Confirm)
        {
            group2Confirm = false;
        }
        else
        {
            group2Confirm= true;
        }
    }

    public void Group1PieceProfessionSelect(int optionIndex)
    {
        group1Profession = (PieceProperties.Profession)optionIndex;
    }

    public void Group2PieceProfessionSelect(int optionIndex)
    {
        group2Profession = (PieceProperties.Profession)optionIndex;
    }

    public void Group1PieceSkillSelect(int optionIndex)
    {
        group1Ability= (PieceProperties.Ability)optionIndex;
    }

    public void Group2PieceSkillSelect(int optionIndex)
    {
        group2Ability = (PieceProperties.Ability)optionIndex;
    }


    public void TransferDataAndStartGame()
    {




        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
