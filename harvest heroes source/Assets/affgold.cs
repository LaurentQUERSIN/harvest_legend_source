using UnityEngine;
using UnityEngine.UI;

public class affgold : MonoBehaviour
{
    public GameManage gm;
    public Text txt;

    public void Update()
    {
        txt.text = gm.gold.ToString();
    }
}
