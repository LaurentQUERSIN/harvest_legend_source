using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class heroesNbrAff : MonoBehaviour
{
    public FarmBehaviour farm;

    public Text pmt;
    public Text bt;
    public Text ft;

    public void Update()
    {
        pmt.text = farm._heroes[HeroTypes.PUNCHMAN].ToString() + "/" + farm._maxHeroes[HeroTypes.PUNCHMAN].ToString();
        bt.text = farm._heroes[HeroTypes.BLOCK].ToString() + "/" + farm._maxHeroes[HeroTypes.BLOCK].ToString();
        ft.text = farm._heroes[HeroTypes.FLASH].ToString() + "/" + farm._maxHeroes[HeroTypes.FLASH].ToString();
    }
}
