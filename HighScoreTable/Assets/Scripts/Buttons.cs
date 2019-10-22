using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    LeagueTable leagueTable;
    Animator animator;

    private void Awake()
    {
        leagueTable = FindObjectOfType<LeagueTable>();
        animator = leagueTable.gameObject.GetComponent<Animator>();
    }

    public void CloseButton()
    {
        animator.SetTrigger("Close");
        leagueTable.SaveData();
    }

    public void PushButton()
    {
        animator.SetTrigger("Push");
        leagueTable.LoadData();
    }
}
