using UnityEngine;

public class SpellFireball : MonoBehaviour, ISpell
{
    public string Name { get; } = "Fireball";

    public float ManaCost { get; } = 10f;

    public void Manifest(Transform ManifestPoint)
    {
        
    }
}
