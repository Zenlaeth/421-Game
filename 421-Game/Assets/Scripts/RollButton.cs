using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class RollButton : MonoBehaviour
{
    private int currentPlayerPoints = 0;

    private TextMeshProUGUI playerPointsTMP;

    public TextMeshProUGUI[] playersPointsTMP;

    public GameObject dices;

    private bool dicesRolling = false;

    public void Pressed()
    {
        HandlePlayer();
        
        if (dicesRolling == false)
        {
            RollDices();
            StartCoroutine(HandlePoints());
        }
    }

    public void HandlePlayer()
    {
        currentPlayerPoints += 1;

        if (currentPlayerPoints > playersPointsTMP.Length - 1)
        {
            currentPlayerPoints = 0;
        }

        playerPointsTMP = playersPointsTMP[currentPlayerPoints];
    }

    public void RollDices()
    {
        dicesRolling = true;
        foreach (Transform dice in dices.transform)
        {
            StartCoroutine(dice.GetComponent<Dice>().Roll());
        }
    }

    public IEnumerator HandlePoints()
    {
        // await all dices rolled
        List<Dice> dicesList = dices.GetComponentsInChildren<Dice>().ToList();
        yield return new WaitUntil(() => !dicesList.Any(d => d.isRolling == true));

        // recover results & detect victory
        List<int> results = new List<int>();
        foreach (Dice dice in dicesList)
        {
            results.Add(dice.result);
        }

        if (results.Contains(4) && results.Contains(2) && results.Contains(1))
        {
            int addedPoints = int.Parse(playerPointsTMP.text) + 1;
            playerPointsTMP.text = addedPoints.ToString();
        }

        dicesRolling = false;
    }
}
