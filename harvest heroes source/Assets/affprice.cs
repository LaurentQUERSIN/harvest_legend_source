using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class affprice : MonoBehaviour
{
    public HeroTypes type;
    public FarmBehaviour farm;
    public Text txt;

    public void Update()
    {
        txt.text = farm._upgrades[type].ToString();
    }
}
