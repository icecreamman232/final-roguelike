using System;


namespace SGGames.Scripts.Enemies
{
    [Serializable]
    public class BrainTransition
    {
        public BrainDecision BrainDecision;
        public string TrueStateName;
        public string FalseStateName;

        public void Initialize(EnemyBrain brain)
        {
            BrainDecision.Initialize(brain);
        }
        
        public void CheckTransition()
        {
            
        }
    }
}

