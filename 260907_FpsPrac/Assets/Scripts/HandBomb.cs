using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBomb : MonoBehaviour, IItem
{
    Rigidbody rb;
    PlayerMovement player;

    private void Start()
    {
        Init();
        
    }
    
    public void UseItem(IUseItem owner)
    {
        if (!(owner is PlayerMovement)) return;
        
        player = (PlayerMovement)owner;
        
        rb = player.GetComponent<Rigidbody>();
        rb.AddForce(gameObject.transform.forward, ForceMode.Impulse);
    }

    private void Init()
    {
    }
}
