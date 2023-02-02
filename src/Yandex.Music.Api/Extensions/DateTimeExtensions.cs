using System;
using System.Text;

namespace Yandex.Music.Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToYandexTimeStampFormat(this DateTime dateTime)
        {
            var sb = new StringBuilder();
            sb.Append(dateTime.ToString("yyyy-MM-dd"));
            sb.Append('T');
            sb.Append(dateTime.ToString("T"));
            sb.Append('.');
            sb.Append(dateTime.Millisecond.ToString("D3"));
            sb.Append('Z');
            return sb.ToString();
        }
    }
}