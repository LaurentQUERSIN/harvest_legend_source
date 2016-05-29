using UnityEngine;
using System.Collections.Generic;

public class Heroes : MonoBehaviour
{
    public HeroTypes type;
    public int life = 0;
    public int damage = 0;
    public float frequence = 0;
    public float speed = 0;

    public  Vector3 basePos;
    public Vector3 enemyPos;
    public FarmBehaviour _farm;
    public GameManage gm;
    public VilainManager vm;

    private Rigidbody rb;
    public int offset = 0;

    public float _lastAttack;
    public Vilain enemy = null;

    public void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public void Update()
    {
        _lastAttack += Time.deltaTime;
        if (gm.isGameOver() == true)
            Destroy(this.gameObject);
        else
            move();
    }

    public void move()
    {   

         GoToVilain();
         if (_lastAttack >= frequence && enemy != null)
        {
            _lastAttack = 0;
            if (enemy.GetDamage(damage) == true)
                enemy = null;
        }

    }

    public void GoToBase()
    {
        if (this.transform.position.x > basePos.x)
            rb.velocity = new Vector3(-speed, 0, 0);
        else
        {
            rb.velocity = Vector3.zero;
            this.transform.position = basePos;
        }
    }

    public void GoToVilain()
    {
        if (this.transform.position.x < enemyPos.x)
            rb.velocity = new Vector3(speed, 0, 0);
        else
        {
            rb.velocity = Vector3.zero;
            gm.gold += 100;
            _farm.RemoveHero(type, true);
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.GetComponent<Vilain>();
        }
    }

    public bool GetDamage(int dgt)
    {
        life -= dgt;
        if (life <= 0)
        {
            _farm.RemoveHero(type, false);
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }
}   

