using UnityEngine;

public interface ISpell
{
    string Name { get; }
    float ManaCost { get; }
    void Manifest(Transform ManifestPoint);

}
