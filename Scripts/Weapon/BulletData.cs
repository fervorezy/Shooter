using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Objects/Ammo", order = 51)]
public class BulletData : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _flightRange;

    public int Damage => _damage;
    public float Speed => _speed;
    public float FlightRange => _flightRange;
}