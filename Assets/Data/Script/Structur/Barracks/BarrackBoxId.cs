using UnityEngine;

public class BarrackBoxId : MonoBehaviour
{
    [SerializeField] private Barrack _barrack;

    public Barrack Barrack => _barrack;
}
