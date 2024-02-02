using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    [Range(0.1f, 100f)]
    public float SpawnTime;

    [Range(0.1f, 100f)]
    public float WaveTimeSpawn;

    [Header("Waves")]
    public List<Wave> Waves;
}

[System.Serializable]
public class Wave
{
    public List<SubWave> SubWaves;
}

[System.Serializable]
public class SubWave
{
    public List<Part> Parts;

    [Range(0f, 100f)]
    public float TimeNextSubWave;
}

[System.Serializable]
public class Part
{
    public TypeUnit TypeUnit;
    public VariertyUnit UnitVar;

    [Range(0, 100)]
    public int Number;
}
