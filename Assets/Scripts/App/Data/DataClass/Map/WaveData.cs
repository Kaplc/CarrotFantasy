using System;
using System.Collections.Generic;

namespace App.Data.DataClass.Map
{
    [Serializable]
    public class WaveData
    {
        public int waveCount;
        public float waveDuration;
        public List<EachWaveData> eachWaveDataList = new List<EachWaveData>();
    }
}