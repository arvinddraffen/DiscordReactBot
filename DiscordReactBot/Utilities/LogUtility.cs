using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace DiscordReactBot.Utilities
{
    /// <summary>
    /// Handles logging for Discord.Net's log events.
    /// </summary>
    internal class LogUtility
    {
        public static Task Log(ILogger logger, LogMessage msg, DiscordSocketClient discordConnection, IEnumerable<ulong> adminUserIDs)
        {
            var messageData = msg.ToString();
            switch (msg.Severity)
            {
                case LogSeverity.Verbose:
                    logger.LogInformation(messageData);
                    break;

                case LogSeverity.Info:
                    logger.LogInformation(messageData);
                    break;

                case LogSeverity.Warning:
                    logger.LogWarning(messageData);
                    break;

                case LogSeverity.Error:
                    logger.LogError(messageData);
                    SendDirectMessage(discordConnection, adminUserIDs, messageData);
                    break;

                case LogSeverity.Critical:
                    logger.LogCritical(messageData);
                    SendDirectMessage(discordConnection, adminUserIDs, messageData);
                    break;
            }
            return Task.CompletedTask;
        }

        private static void SendDirectMessage(DiscordSocketClient discordConnection, IEnumerable<ulong> adminUserIDs, string logData)
        {
            foreach (var userID in adminUserIDs)
            {
                var user = discordConnection.GetUserAsync(userID).Result;
                try
                {
                    user.SendMessageAsync($"An Error has occured!\n{logData}").Wait();
                }
                catch (Exception) { }
            }
        }
    }
}