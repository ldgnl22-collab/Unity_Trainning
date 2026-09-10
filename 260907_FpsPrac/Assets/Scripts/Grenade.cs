using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Rigidbody rb;
    PlayerMovement player;
    
    GameObject _grenadeInstance;

    private Vector3 _throwPos;

    private void Start()
    {
        Init();
    }
    
    public void UseItem(IUseItem owner)
    {
        if (!(owner is PlayerMovement)) return;
        
        player = (PlayerMovement)owner;
        
        _grenadeInstance = Instantiate(gameObject, player._grenadePos.position, Quaternion.identity);
        _grenadeInstance.GetComponent<Rigidbody>().AddForce(player.transform.forward, ForceMode.Impulse);
    }

    private void Init()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
}
