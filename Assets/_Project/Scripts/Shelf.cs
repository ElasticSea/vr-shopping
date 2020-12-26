using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private BoxCollider space;

    public Bounds Bounds => new Bounds(space.center, space.size);
}