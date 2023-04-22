using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Enemy : MonoBehaviour
{
    [SerializeField] private double _health;
    [SerializeField] private double _damage;
    [SerializeField] private double _speed;

    [SerializeField] private Animator animator;

    public double Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public double Damage 
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public double Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public void TakeDamage(double damage) 
    {
        _health -= damage;
    }
    
    /*
    Placeholder function for dealing damage to a GameObject with a player script that contains a TakeDamage function.

    private void DealDamage(Player player)
    {
        player.TakeDamage(_damage);
    }
    */

    public void MoveTo3DPoint(Vector3 positionVector)
    {
        double step = _speed * (double)Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, positionVector, (float)step);
        animator.SetBool("Walk", true);
    }

    /*
    Placeholder function for attacking the player, dealing damage to them and playing an attack animation and sound

    private void Attack(Player player)
    {
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        this.transform.position = player.transform.position + Vector3.back;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        this.transform.LookAt(player.transform);
        MoveTo3DPoint(playerPosition);
    }
}
