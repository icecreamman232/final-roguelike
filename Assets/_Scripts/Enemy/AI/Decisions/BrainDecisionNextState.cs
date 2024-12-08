namespace SGGames.Scripts.Enemies
{
    /// <summary>
    /// Basic decision which always returns true
    /// </summary>
    public class BrainDecisionNextState : BrainDecision
    {
        public override bool CheckDecision()
        {
            return true;
        }
    }
}

