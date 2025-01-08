using SGGames.Scripts.Core;
using SGGames.Scripts.Healths;
using UnityEngine;

public class TestService : MonoBehaviour
{
    [ContextMenu("Test")]
    private void Test()
    {
        var playerHealth = ServiceLocator.GetService<IPlayerHealthService>();
        Debug.Log(((PlayerHealth)playerHealth).CurrentHealth);
    }
}
