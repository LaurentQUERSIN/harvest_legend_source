using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class btnBehaviour : MonoBehaviour
{
    public Button btn;
    public HeroTypes tp;
    public FarmBehaviour farm;

    public void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            farm.AddSprout(tp);
        });
    }
}
