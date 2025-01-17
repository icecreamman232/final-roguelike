
using System.Numerics;

namespace SGGames.Scripts.Core
{
    public interface IMoveable
    {
        Vector2 Direction{get;set;}
        float CurrentSpeed { get; set; }
        bool CanMove { get; set; }
        void ToggleMovement(bool canMove);
        bool CheckObstacle();
        void UpdateMovement();
        void ModifySpeed(float addedSpeed);
        void ResetSpeed();
    }
}

