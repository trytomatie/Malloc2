using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackHandler : MonoBehaviour
{
    private Rigidbody2D _myRig;
    private float _durration = 0;
    private Vector2 _direction = new Vector2(0,0);
    private float _strenght = 0;
    private bool _organic = false;
    private bool _organicKnockbackApplied = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _myRig= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_organicKnockbackApplied)
        {
            return;
        }
        if(_durration > 0)
        {
            _durration -= Time.deltaTime;
            if(_organic)
            {
                _myRig.AddForce(_direction);
                _organicKnockbackApplied = true;
                _durration = 0;
                return;
            }
            else
            {
                _myRig.velocity = _direction * _strenght;
                if(GetComponent<AIPath>() != null)
                { 
                    GetComponent<AIPath>().canMove = false;
                }
            }
        }
        if(_durration < 0)
        {
            if (GetComponent<AIPath>() != null)
            {
                GetComponent<AIPath>().canMove = true;
            }
            _durration = 0;
        }

    }

    public void ApplyKnockback(Vector2 direction, float strenght, float durration,bool organic)
    {
        _direction = direction;
        _strenght = strenght;
        _durration = durration;
        _organic = organic;
        _organicKnockbackApplied = false;
    }
}
