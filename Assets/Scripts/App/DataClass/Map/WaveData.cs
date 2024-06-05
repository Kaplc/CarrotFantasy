using System;
using System.Collections.Generic;
using App.Static.Enum;

namespace App.DataClass.Map
{
    [Serializable]
    public class WaveData
    {
        public int waveCount;
        public float waveDuration;
        public List<EachWaveData> eachWaveDataList = new List<EachWaveData>();
    }
}