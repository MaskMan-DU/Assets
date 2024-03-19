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

    public static PieceProperties.Profession group1Profession;
    public static PieceProperties.Profession group2Profession;

    public static PieceProperties.Ability group1Ability;
    public static PieceProperties.Ability group2Ability;

    // Start is called before the first frame update
    void Start()
    {
        group1Profession = PieceProperties.Profession.Cowboy;
        group2Profession = PieceProperties.Profession.Cowboy;

        group1Ability = PieceProperties.Ability.Spiritual_Leader;
        group2Ability = PieceProperties.Ability.Spiritual_Leader;
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
        /*PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("group1Profession", (int)group1Profession);
        PlayerPrefs.SetInt("group1Ability", (int)group1Ability);

        PlayerPrefs.SetInt("group2Profession", (int)group2Profession);
        PlayerPrefs.SetInt("group2Ability", (int)group2Ability);

        PlayerPrefs.Save();*/

        /*DataSave.group1Profession = group1Profession;
        DataSave.group1Ability = group1Ability;

        DataSave.group2Ability = group2Ability;
        DataSave.group2Profession = group2Profession;*/

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
