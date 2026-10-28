using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    IReadOnlyList<ISpell> AvailableSpells { get; }
    ISpell CurrentSpell { get; }
    void Cast();
    

    void SwitchSpell();
}
