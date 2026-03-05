using Discord;
using System.Text;

namespace DiscordReactBot.Modules
{
    internal class CantareReactModule
    {
        [Discord.Interactions.RequireBotPermission(ChannelPermission.AddReactions)]
        internal async Task ReactToMessage(IMessage message)
        {
            if (message.Content.Contains("Cantare", StringComparison.InvariantCulture))
            {
                string emojis = message.Content.Split('\n')[1];
                int offset = 0;
                int greenSquareIndex = -1;
                foreach (var rune in emojis.EnumerateRunes())
                {
                    if (IsEmoji(rune))
                    {
                        if (IsAllowedEmoji(rune))
                        {
                            if (offset == 0)
                            {
                                if (!(rune.Value == AttemptEmojiCodes.LOUD_SOUND))
                                {
                                    break;
                                }
                            }
                            else if (rune.Value == AttemptEmojiCodes.GREEN_SQUARE)
                            {
                                greenSquareIndex = offset;
                                break;
                            }
                            offset++;
                        }
                    }
                }

                if (offset > 2)
                {
                    var loadingEmote = Emote.Parse(ReactEmojiCodesAsString.LOADING);
                    var fEmoji = new Emoji(ReactEmojiCodesAsString.REGIONAL_INDICATOR_F);
                    await message.AddReactionAsync(loadingEmote);
                    await message.AddReactionAsync(fEmoji);
                }
                else if (offset == 1)
                {
                    var doughnutEmoji = new Emoji(ReactEmojiCodesAsString.DOUGHNUT);
                    await message.AddReactionAsync(doughnutEmoji);
                }
                else if (offset == 2)
                {
                    if (Rune.TryGetRuneAt(emojis, 2, out Rune firstSquare))
                    {
                        int emojiCode = ReactEmojiCodes.LUT_ATTEMPT_TO_REACT[firstSquare.Value];
                        var reactEmoji = new Emoji(ReactEmojiCodesAsString.LUT_EMOJI_HEX_TO_STR[emojiCode]);
                        await message.AddReactionAsync(reactEmoji);
                    }
                }
            }
        }

        private bool IsEmoji(Rune rune)
        {
            return rune.Value >= 0x1F300 && rune.Value <= 0x1F9FF || rune.Value >= 0x2600 && rune.Value <= 0x26FF;
        }

        private bool IsAllowedEmoji(Rune rune)
        {
            return rune.Value switch
            {
                AttemptEmojiCodes.LOUD_SOUND => true,
                AttemptEmojiCodes.ORANGE_SQUARE => true,
                AttemptEmojiCodes.YELLOW_SQUARE => true,
                AttemptEmojiCodes.PURPLE_SQUARE => true,
                AttemptEmojiCodes.RED_SQUARE => true,
                AttemptEmojiCodes.GREEN_SQUARE => true,
                _ => false
            };
        }
    }
}
