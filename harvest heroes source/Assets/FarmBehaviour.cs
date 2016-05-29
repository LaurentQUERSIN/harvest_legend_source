using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class FarmBehaviour : MonoBehaviour
{
    public GameObject PunchMan;
    public GameObject Block;
    public GameObject Flash;
    public AudioClip deadClip;
    public AudioClip bonusSound;
    public Image fullImg;

    public Sprite pms;
    public Sprite bs;
    public Sprite fs;
    public Sprite nulls;

    public Dictionary<HeroTypes, int> _heroes = new Dictionary<HeroTypes, int>();
    public Dictionary<HeroTypes, int> _maxHeroes = new Dictionary<HeroTypes, int>();
    public HeroTypes[] _sproutsTypes = new HeroTypes[16];
    public float[] _sproutsTimers = new float[16];
    public Image[] _images = new Image[16];
    public Dictionary<HeroTypes, Sprite> _colors = new Dictionary<HeroTypes, Sprite>();

    public float timer = 5;

    public GameManage gm;
    public VilainManager vm;

    public Dictionary<HeroTypes, int> _upgrades = new Dictionary<HeroTypes, int>();

    public void Start()
    {
        _heroes.Add(HeroTypes.PUNCHMAN, 0);
        _heroes.Add(HeroTypes.BLOCK, 0);
        _heroes.Add(HeroTypes.FLASH, 0);
        _maxHeroes.Add(HeroTypes.PUNCHMAN, 1);
        _maxHeroes.Add(HeroTypes.BLOCK, 1);
        _maxHeroes.Add(HeroTypes.FLASH, 2);

        _colors.Add(HeroTypes.NULL, nulls);
        _colors.Add(HeroTypes.PUNCHMAN, pms);
        _colors.Add(HeroTypes.BLOCK, bs);
        _colors.Add(HeroTypes.FLASH, fs);

        _upgrades.Add(HeroTypes.PUNCHMAN, 100);
        _upgrades.Add(HeroTypes.BLOCK, 100);
        _upgrades.Add(HeroTypes.FLASH, 100);

        for (uint i = 0; i < 16; ++i)
        {
            _sproutsTypes[i] = HeroTypes.NULL;
            _sproutsTimers[i] = 0;
        }
    }

    public void Update()
    {
        bool full = false;
        if (gm.isGameOver())
            return;
        for(uint i = 0; i < 16; ++i)
        {
            if (_sproutsTypes[i] != HeroTypes.NULL)
            {
                _sproutsTimers[i] += Time.deltaTime;
                GameObject go = null;
                _images[i].color = new Color(1, 1, 1, 0.2f + _sproutsTimers[i] / timer);
                if (_sproutsTimers[i] >= timer)
                {
                    if (_sproutsTypes[i] == HeroTypes.PUNCHMAN)
                    {
                        go = Instantiate(PunchMan);
                    }
                    if (_sproutsTypes[i] == HeroTypes.BLOCK)
                    {
                        go = Instantiate(Block);
                    }
                    if (_sproutsTypes[i] == HeroTypes.FLASH)
                    {
                        go = Instantiate(Flash);
                    }
                    if (go != null)
                    {
                        Debug.Log("hero created");
                        Heroes h = go.GetComponent<Heroes>();
                        h.gm = gm;
                        h._farm = this;
                        h.vm = vm;
                    }

                    _images[i].sprite = _colors[HeroTypes.NULL];
                    _images[i].color = Color.clear;
                    _sproutsTypes[i] = HeroTypes.NULL;
                    _sproutsTimers[i] = 0;
                }
            }
            else
                full = true;
            if (!full)
                fullImg.gameObject.active = true;
            else
                fullImg.gameObject.active = false;
        }
    }

    public bool AddSprout(HeroTypes tp)
    {
        Debug.Log("hero creation request" + tp);
        if (gm.isGameOver())
            return false;
        if (_heroes[tp] < _maxHeroes[tp])
        {
            for (uint i = 0; i < 16; ++i)
            {
                if (_sproutsTypes[i] == HeroTypes.NULL)
                {
                    _heroes[tp] += 1;
                    _images[i].sprite = _colors[tp];
                    _images[i].color = new Color(1, 1, 1, 0.1f);
                    _sproutsTypes[i] = tp;
                    return (true);
                }
            }
        }
        return (false);
    }

    public void RemoveHero(HeroTypes tp, bool bonus)
    {
        Debug.Log("hero dead");
        _heroes[tp] -= 1;
        if (_heroes[tp] < 0)
            _heroes[tp] = 0;
        if (bonus)
            gm.PlayAudio(bonusSound);
        else
            gm.PlayAudio(deadClip);
    }

    public void Upgrade(HeroTypes tp)
    {
        if (gm.gold >= _upgrades[tp] && _maxHeroes[tp] < 16)
        {
            gm.gold -= _upgrades[tp];
            _maxHeroes[tp] += 1;
            _upgrades[tp] = System.Convert.ToInt32( 100 * (System.Math.Pow(1.15f, _maxHeroes[tp])));
        }
    }
};
