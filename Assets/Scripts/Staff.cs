using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    

    public ISpell CurrentSpell => throw new System.NotImplementedException();

    IReadOnlyList<ISpell> IWeapon.AvailableSpells => throw new System.NotImplementedException();

    public void Cast()
    {
        
    }

    

    public void SwitchSpell()
    {
        throw new System.NotImplementedException();
    }
}
