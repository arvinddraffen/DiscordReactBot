using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordReactBot.Modules
{
    internal static class AttemptEmojiCodes
    {
        internal const int GREEN_SQUARE = 0x1F7E9;
        internal const int LOUD_SOUND = 0x1F50A;
        internal const int RED_SQUARE = 0x1F7E5;
        internal const int ORANGE_SQUARE = 0x1F7E7;
        internal const int YELLOW_SQUARE = 0x1F7E8;
        internal const int PURPLE_SQUARE = 0x1F7EA;
        internal const int UP_ARROW = 0x2B06;
        internal const int DOWN_ARROW = 0x2B07;
        internal const int WHITE_CHECK_MARK = 0x2705;
    }

    internal static class ReactEmojiCodes
    {
        internal const int LOLLIPOP = 0x1F36D;
        internal const int COOKIE = 0x1F36A;
        internal const int BIRTHDAY_CAKE = 0x1F382;
        internal const int ICE_CREAM = 0x1F366;
        internal const int DOUGHNUT = 0x1F369;
        internal const int MONEY_MOUTH_FACE = 0xD83E;
        internal const int MONEY_BAG = 0xD83D;
        internal const int CHART_WITH_DOWNWARDS_TREND = 0xD83D;
        internal const int CREDIT_CARD = 0xD83D;

        internal readonly static Dictionary<int, int> LUT_ATTEMPT_TO_REACT = new Dictionary<int, int>
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
        internal const string MONEY_MOUTH_FACE = "\uD83E\uDD11";
        internal const string MONEY_BAG = "\uD83D\uDCB0";
        internal const string CHART_WITH_DOWNWARDS_TREND = "\uD83D\uDCC9";
        internal const string CREDIT_CARD = "\uD83D\uDCB3";

        internal readonly static Dictionary<int, string> LUT_EMOJI_HEX_TO_STR = new Dictionary<int, string>
        {
            { ReactEmojiCodes.LOLLIPOP, ReactEmojiCodesAsString.LOLLIPOP },
            { ReactEmojiCodes.DOUGHNUT, ReactEmojiCodesAsString.DOUGHNUT },
            { ReactEmojiCodes.COOKIE, ReactEmojiCodesAsString.COOKIE },
            { ReactEmojiCodes.BIRTHDAY_CAKE, ReactEmojiCodesAsString.BIRTHDAY_CAKE },
            { ReactEmojiCodes.ICE_CREAM, ReactEmojiCodesAsString.ICE_CREAM },
        };
    }
}
