
using SGGames.Scripts.Common;
using SGGames.Scripts.Data;
using SGGames.Scripts.EditorExtensions;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class BrainActionSwitchToRandomState : BrainAction
    {
        [SerializeField] private WeighRandomnessData m_randomnessData;
        [SerializeField] private string[] m_stateNames;
        #if UNITY_EDITOR
        [SerializeField][ReadOnly] private string m_lastChosenState;
        #endif
        public override void DoAction()
        {
            var chance = RandomController.GetRandomIntInRange(0, 100);
            var actionIndex = m_randomnessData.GetTriggerEventIndex(chance);
            #if UNITY_EDITOR
            m_lastChosenState = m_stateNames[actionIndex];
            #endif
            m_brain.TransitionToState(m_stateNames[actionIndex]);
        }
    }
}

