﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 默认游戏框架日志辅助器。
    /// </summary>
    public class DefaultLogHelper : GameFrameworkLog.ILogHelper
    {
        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        public void Log(GameFrameworkLogLevel level, object message)
        {
            switch (level)
            {
                case GameFrameworkLogLevel.Debug:
                    Debug.Log($"<color=#2BD988>{message}</color>");
                    break;

                case GameFrameworkLogLevel.Info:
                    Debug.Log($"<color=#2BD988>{message}</color>");
                    break;

                case GameFrameworkLogLevel.Warning:
                    Debug.LogWarning($"<color=#F2A20C>{message}</color>");
                    break;

                case GameFrameworkLogLevel.Error:
                    Debug.LogError($"<color=#F22E2E>{message}</color>");
                    break;
                case GameFrameworkLogLevel.Fatal:
                    Debug.LogError($"<color=#F22E2E>{message}</color>");
                    break;

                default:
                    throw new GameFrameworkException(message.ToString());
            }
        }
    }
}