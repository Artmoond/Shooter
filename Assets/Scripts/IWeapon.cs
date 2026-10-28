using UnityEngine;

public interface IWeapon
{
    public void Cast()
    {
        CastVisuals(new Vector3());
    }

    public Vector3 CastLogic(Vector3 targetPos);

    public void CastVisuals(Vector3 targetPos)
    {

    }
}
