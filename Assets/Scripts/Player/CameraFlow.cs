using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Vector3 ofside;
    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        ofside.x = player.forward.x*2.5f;
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(player.position.x +ofside.x,
            player.position.y+ofside.y,
        player.position.z + ofside.z),50*Time.deltaTime);
        // Debug.Log(player.forward.x);
    }
}
