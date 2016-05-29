using UnityEngine;
using System.Collections;

public class Vilain : MonoBehaviour
{
    public VilainTypes type;
    public int life = 0;
    public int damage = 0;
    public float frequence = 0;
    public float speed = 0;

    public Vector3 basePos;
    public Vector3 enemyPos;
    public GameManage gm;
    public VilainManager vm;


    private Rigidbody rb;
    public int offset = 0;

    public float _lastAttack;
    public Heroes enemy = null;

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
        if (_lastAttack >= frequence && enemy != null)
        {
            _lastAttack = 0;
            if (enemy.GetDamage(damage) == true)
                enemy = null;
        }
    }

    public void move()
    {
        if (this.transform.position.x > basePos.x)
            rb.velocity = new Vector3(-speed, 0, 0);
        else
        {
            vm.Win();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ally")
        {
            enemy = other.GetComponent<Heroes>();
        }
    }

    public bool GetDamage(int dgt)
    {
        life -= dgt;
        if (life <= 0)
        {
            vm.RemoveVilain(type);
            Destroy(this.gameObject);
            return (true);
        }
        return (false);
    }
}
