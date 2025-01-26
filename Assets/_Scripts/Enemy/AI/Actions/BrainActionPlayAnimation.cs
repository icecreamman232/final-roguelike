using SGGames.Scripts.Common;
using SGGames.Scripts.EditorExtensions;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    /// <summary>
    /// Play an animation using AnimationParameter component.
    /// </summary>
    public class BrainActionPlayAnimation : BrainAction
    {
        [SerializeField] private AnimationParameter m_animationParameter;
        [GroupBoolean("Animation")]
        public bool SetTrigger;
        [GroupBoolean("Animation")]
        public bool SetBool;
        [ShowIf("SetBool",true)]
        public bool BooleanValue;
        [GroupBoolean("Animation")]
        public bool SetFloat;
        [ShowIf("SetFloat",true)]
        public float FloatValue;
        [GroupBoolean("Animation")]
        public bool SetInt;
        [ShowIf("SetInt",true)]
        public int IntValue;
        
        public override void DoAction()
        {
            if (SetTrigger)
            {
                m_animationParameter.SetTrigger();
            }
            else if (SetBool)
            {
                m_animationParameter.SetBool(BooleanValue);
            }
            else if (SetFloat)
            {
                m_animationParameter.SetFloat(FloatValue);
            }
            else if (SetInt)
            {
                m_animationParameter.SetInt(IntValue);
            }
        }
    }
}

