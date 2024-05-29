using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {
    private Sprite[] diceSides;

    private SpriteRenderer rend;

    public int result = 0;

    public bool isRolling;

	private void Start () {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
	}

    public IEnumerator Roll()
    {
        isRolling = true;
        int randomDiceSide = 0;

        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        result = randomDiceSide + 1;
        yield return Reroll(result);

        isRolling = false;
    }

    public IEnumerator Reroll(int result)
    {
        if (result == 6)
        {
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(Roll());
        }
    }
}
