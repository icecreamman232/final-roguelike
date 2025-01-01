using System;
using SGGames.Scripts.Events;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class RoomInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_roomName;
        [SerializeField] private ChangeRoomEvent m_changeRoomEvent;

        private void Awake()
        {
            m_changeRoomEvent.AddListener(OnChangeRoom);
        }

        private void OnDestroy()
        {
            m_changeRoomEvent.RemoveListener(OnChangeRoom);
        }

        private void OnChangeRoom(int areaIndex, int roomIndex)
        {
            //Make room index +1 because room index started from 0. Same for area index
            m_roomName.text = $"Room {areaIndex + 1} - {roomIndex + 1}";
        }
    }
}
