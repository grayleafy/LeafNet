using LeafNet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    ClientEntity clientEntity;
    void Start()
    {
        clientEntity = new ClientEntity();
        clientEntity.BeginConnect(null);
    }

    // Update is called once per frame
    void Update()
    {
        clientEntity.FrameUpdate(10);
    }

    public void ReConnect(Action callback)
    {
        clientEntity.BeginConnect(callback);
    }
}
