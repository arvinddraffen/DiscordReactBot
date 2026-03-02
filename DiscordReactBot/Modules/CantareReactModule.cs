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

    internal static class AttemptEmojiCodes
    {
        internal const int GREEN_SQUARE = 0x1F7E9;
        internal const int LOUD_SOUND = 0x1F50A;
        internal const int RED_SQUARE = 0x1F7E5;
        internal const int ORANGE_SQUARE = 0x1F7E7;
        internal const int YELLOW_SQUARE = 0x1F7E8;
        internal const int PURPLE_SQUARE = 0x1F7EA;
    }

    internal static class ReactEmojiCodes
    {
        internal const int LOLLIPOP = 0x1F36D;
        internal const int COOKIE = 0x1F36A;
        internal const int BIRTHDAY_CAKE = 0x1F382;
        internal const int ICE_CREAM = 0x1F366;
        internal const int DOUGHNUT = 0x1F369;

        internal readonly static Dictionary<int,int> LUT_ATTEMPT_TO_REACT = new Dictionary<int, int>
        {
            { AttemptEmojiCodes.GREEN_SQUARE, ReactEmojiCodes.DOUGHNUT},
            { AttemptEmojiCodes.RED_SQUARE, ReactEmojiCodes.LOLLIPOP},
            { AttemptEmojiCodes.PURPLE_SQUARE, ReactEmojiCodes.COOKIE},
            { AttemptEmojiCodes.ORANGE_SQUARE, ReactEmojiCodes.BIRTHDAY_CAKE},
            { AttemptEmojiCodes.YELLOW_SQUARE, ReactEmojiCodes.ICE_CREAM},
        };
    }

    internal static class ReactEmojiCodesAsString
    {
        internal const string LOLLIPOP = "\uD83C\uDF6D";
        internal const string COOKIE = "\uD83C\uDF6A";
        internal const string BIRTHDAY_CAKE = "\uD83C\uDF82";
        internal const string ICE_CREAM = "\uD83C\uDF66";
        internal const string DOUGHNUT = "\uD83C\uDF69";
        internal const string LOADING = "<a:loading:1477738710274670734>";
        internal const string REGIONAL_INDICATOR_F = "\uD83C\uDDEB";

        internal readonly static Dictionary<int,string> LUT_EMOJI_HEX_TO_STR = new Dictionary<int, string>
        {
            { ReactEmojiCodes.LOLLIPOP, ReactEmojiCodesAsString.LOLLIPOP },
            { ReactEmojiCodes.DOUGHNUT, ReactEmojiCodesAsString.DOUGHNUT },
            { ReactEmojiCodes.COOKIE, ReactEmojiCodesAsString.COOKIE },
            { ReactEmojiCodes.BIRTHDAY_CAKE, ReactEmojiCodesAsString.BIRTHDAY_CAKE },
            { ReactEmojiCodes.ICE_CREAM, ReactEmojiCodesAsString.ICE_CREAM },
        };
    }
}
