using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spell;

    public void SpawnSpell()
    {
        Instantiate(_spell, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10), Quaternion.identity);
    }
}
