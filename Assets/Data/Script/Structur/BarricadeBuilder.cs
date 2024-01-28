using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarricadeBuilder : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointPosition;
    [SerializeField] private TMP_Text _textBarricadesCount;

    private List<Barricade> _barricades = new List<Barricade>();
    private int _barricadesCount = 0;

    public void InitBuilder(Barricade baricadePrefab, int numberBarricade)
    {
        Spawn(baricadePrefab, numberBarricade);
    }

    public void GetBarricade()
    {
        Barricade barricade = TryGetBarricade();

        if(barricade != null)
        {
            barricade.ChangePosition(_spawnPointPosition.position);
            barricade.MakeInaccessible();
            _barricadesCount--;
        }

        _textBarricadesCount.text = _barricadesCount.ToString();
    }

    public Barricade TryGetBarricade()
    {
        foreach(Barricade barricade in _barricades)
        {
            if(barricade.IsAccessible)
                return barricade;
        }

        return null;
    }

    private void Spawn(Barricade prefab, int number)
    {
        for(int i = 0; i < number; i++)
        {
            var build = Instantiate(prefab, transform);
            build.MakeAvailable();
            _barricades.Add(build);
            _barricadesCount++;
        }

        _textBarricadesCount.text = _barricadesCount.ToString();
    }
}
