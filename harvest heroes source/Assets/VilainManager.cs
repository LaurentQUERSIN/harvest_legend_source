using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum VilainTypes
{
    NULL,
    COMMON,
    BOSS
}

public class VilainManager : MonoBehaviour
{
    public GameObject Common;
    public GameObject Boss;
    public AudioClip deadClip;
    public float defeated = 0;
    public int vilains = 0;
    public GameManage gm;

    public float _spawnFrequency = 10;
    public float _lastSpawn = 0;

    public bool isVilain()
    {
        if (vilains == 0)
            return false;
        return (true);
    }

    public void RemoveVilain(VilainTypes tp)
    {
        vilains = vilains - 1;
        if (vilains < 0)
            vilains = 0;
        gm.PlayAudio(deadClip);
        if (gm.isGameOver() == false)
        {
            defeated += 1;
            if (tp == VilainTypes.COMMON)
            {
                double rand = Random.Range(1, 3);
                gm.gold += System.Convert.ToInt32(rand * 1.05 * defeated);
            }
            if (tp == VilainTypes.BOSS)
            {
                gm.gold += System.Convert.ToInt32( 10 * defeated);
            }
        }
    }

    public void Win()
    {
        gm.GameOver();
    }

    public void Update()
    {
        if (gm.isGameOver())
            return;
        float freq = defeated / 20f;
        if (freq > _spawnFrequency - 0.2f)
            freq = _spawnFrequency - 0.2f;
        _lastSpawn += Time.deltaTime;
        if (_lastSpawn > _spawnFrequency - freq && vilains < 25)
        {
            _lastSpawn = 0;
            vilains += 1;
            int r = Random.Range(0, 100);
            if (r > 105 - (defeated / 2) && defeated > 15)
            {
                GameObject go = Instantiate(Boss);
                Vilain v = go.GetComponent<Vilain>();
                v.gm = gm;
                v.vm = this;
            }
            else
            {
                GameObject go = Instantiate(Common);
                Vilain v = go.GetComponent<Vilain>();
                v.gm = gm;
                v.vm = this;
            }
        }
    }
}
