using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    LeagueTable leagueTable;

    private void Start()
    {
        leagueTable = animator.gameObject.GetComponent<LeagueTable>();
    }

    public void CloseButton()
    {
        animator.SetBool("isClosed", true);
        animator.SetBool("isPushed", false);
        leagueTable.SaveData();
    }

    public void PushButton()
    {
        animator.SetBool("isClosed", false);
        animator.SetBool("isPushed", true);
        leagueTable.LoadData();
    }
}
