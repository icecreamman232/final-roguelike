using UnityEngine;

namespace SGGames.Scripts.EditorExtensions
{
    public class GroupBooleanAttribute : PropertyAttribute
    {
        public string GroupName { get; private set; }

        public GroupBooleanAttribute(string groupName)
        {
            GroupName = groupName;
        }
    }
}

