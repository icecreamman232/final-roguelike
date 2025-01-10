using SGGames.Scripts.Core;
using SGGames.Scripts.Modifiers;
using UnityEngine;

public class TestModifier : MonoBehaviour
{
    public Modifier Modifier;

    [ContextMenu("TEST")]
    private void Test()
    {
        ServiceLocator.GetService<ModifierHandler>().RegisterModifier(Modifier);
    }
}
