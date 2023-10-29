﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public List<Cell> pathList = new List<Cell>();
    public List<Cell> towerList = new List<Cell>();
}
