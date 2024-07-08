using System;
using Controllers;
using Models;
using Stream.Controllers;
using UnityEngine;

namespace Stream
{
    public class DataStream : MonoBehaviour
    {
        private ServerChatConfigController _serverChatConfigController;
        private ServerUserConfigController _serverUserConfigController;

        private void Awake()
        {
            _serverChatConfigController = new ServerChatConfigController();
            _serverUserConfigController = new ServerUserConfigController();

            _serverUserConfigController.LoadFromJson();
            _serverChatConfigController.LoadFromJson();
        }

        public bool CompareUserConfig(string jsonClientUserConfig, out string jsonChangedUserConfig)
        {
            var clientUserConfig = JsonUtility.FromJson<UserConfigController>(jsonClientUserConfig);

            if (_serverUserConfigController.UserStorage.Equals(clientUserConfig.UserStorage))
            {
                jsonChangedUserConfig = null;
                return true;
            }

            jsonChangedUserConfig = JsonUtility.ToJson(_serverUserConfigController.UserStorage);
            return false;
        }

        public bool CompareChatConfig(string jsonClientChatConfig, out string jsonChangedChatConfig)
        {
            var clientChatConfig = JsonUtility.FromJson<ChatConfigController>(jsonClientChatConfig);

            if (_serverChatConfigController.ChatHistory.Equals(clientChatConfig.ChatHistory))
            {
                jsonChangedChatConfig = null;
                return true;
            }

            jsonChangedChatConfig = JsonUtility.ToJson(_serverChatConfigController.ChatHistory);
            return false;
        }

    }
}