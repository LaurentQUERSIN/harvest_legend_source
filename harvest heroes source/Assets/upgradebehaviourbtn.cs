using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class upgradebehaviourbtn : MonoBehaviour
{
    public HeroTypes type;
    public FarmBehaviour farm;
    public Button btn;

    public void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            farm.Upgrade(type);
        });
    }
}
