using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResult : MonoBehaviour
{
    [SerializeField] Text playerName = null;
    [SerializeField] Text playerLastName = null;
    [SerializeField] Text playerPlace = null;
    [SerializeField] Text playerPoints = null;

    public void SetupResult(int place, string name, string lastName, int points)
    {
        playerPlace.text = (place+1).ToString();
        playerName.text = name;
        playerLastName.text = lastName;
        playerPoints.text = points.ToString();
    }
}
