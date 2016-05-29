using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum HeroTypes
{
    NULL,
    PUNCHMAN,
    BLOCK,
    FLASH,
};

public class GameManage : MonoBehaviour
{
    public int gold = 100;
    public AudioSource sound;
    public AudioSource musicPlayer;
    public AudioClip winSound;
    public AudioClip loseSound;
    public Text gotxt;
    private bool _gameover = false;

    public void PlayAudio(AudioClip clip)
    {
        sound.PlayOneShot(clip);
    }

    public bool isGameOver()
    {
        return _gameover;
    }

    public void GameOver()
    {
        _gameover = true;
        gotxt.enabled = true;
        musicPlayer.Stop();
        PlayAudio(loseSound);
    }

    public void Update()
    {
        if (gold >= 50000)
        {
            _gameover = true;
            musicPlayer.Stop();
            PlayAudio(winSound);
            gotxt.text = "EZ GG WP !";
            gotxt.enabled = true;
        }
    }
}
