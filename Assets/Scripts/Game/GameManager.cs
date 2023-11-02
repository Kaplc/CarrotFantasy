using System;
using System.Collections;
using System.Collections.Generic;
using Script.FrameWork.MusicManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PoolManager poolManager;
    public BinaryManager binaryManager;
    public MusicManger musicManger;

    private void Awake()
    {
        poolManager = PoolManager.Instance;
        binaryManager = BinaryManager.Instance;
        musicManger = MusicManger.Instance;
    }
}
