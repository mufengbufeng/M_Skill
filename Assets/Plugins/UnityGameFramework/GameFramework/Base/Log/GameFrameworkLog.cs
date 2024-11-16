//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// 游戏框架日志类。
    /// </summary>
    public static partial class GameFrameworkLog
    {
        private static ILogHelper s_LogHelper = null;

        /// <summary>
        /// 设置游戏框架日志辅助器。
        /// </summary>
        /// <param name="logHelper">要设置的游戏框架日志辅助器。</param>
        public static void SetLogHelper(ILogHelper logHelper)
        {
            s_LogHelper = logHelper;
        }

        public static void Log(GameFrameworkLogLevel level, object message)
        {
            if (s_LogHelper == null)
            {
                throw new GameFrameworkException("You must set log helper first.");
            }

            s_LogHelper.Log(level, message);
        }

        public static void Info(object message)
        {
            Log(GameFrameworkLogLevel.Info, message);
        }

        public static void Warning(object message)
        {
            Log(GameFrameworkLogLevel.Warning, message);
        }

        public static void Error(object message)
        {
            Log(GameFrameworkLogLevel.Error, message);
        }

        public static void Fatal(object message)
        {
            Log(GameFrameworkLogLevel.Fatal, message);
        }
    }
}