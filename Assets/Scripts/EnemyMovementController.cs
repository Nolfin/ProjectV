using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class EnemyMovementController : MonoBehaviour
{
    public int velocity;
    public GameObject target;
    public GameObject[] players;
    private PhotonView photonView;
    public float distanceX;
    public float distanceY;
    

    void Update()
    {
        distanceX = -transform.position.x + target.transform.position.x;
        distanceY = -transform.position.y + target.transform.position.y;
        photonView = this.GetComponent<PhotonView>();
        players = GameObject.FindGameObjectsWithTag("Player");
        photonView.RPC(nameof(ChangeTarget), RpcTarget.All);
        photonView.RPC(nameof(FollowPlayer), RpcTarget.All);
        
    }

    [PunRPC]
    void FollowPlayer()
    {
        if(transform.position != target.transform.position) transform.position = transform.position + new Vector3(velocity * Time.deltaTime * distanceX / (Math.Abs(distanceX) + Math.Abs(distanceY)), velocity * Time.deltaTime * distanceY / (Math.Abs(distanceX) + Math.Abs(distanceY)), 0);
    }

    [PunRPC]
    void ChangeTarget()
    {
        double distance = 1000;
        foreach (GameObject player in players)
        {
            double distanceToPlayer = Math.Pow(Math.Pow(transform.position.x - player.transform.position.x, 2) + Math.Pow(transform.position.y - player.transform.position.y, 2), 1 / 2);
            if (distanceToPlayer < distance)
            {
                distance = distanceToPlayer;
                target = player;
            }
        }
    }
}
