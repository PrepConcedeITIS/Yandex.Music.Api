using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Yandex.Music.Api.Common;
using Yandex.Music.Api.Extensions;
using Yandex.Music.Api.Models.Common;
using Yandex.Music.Api.Models.Track;
using Yandex.Music.Api.Requests.Common;

namespace Yandex.Music.Api.Requests.History
{
    public record AddHistoryRequestModel(YTrack Track, string ClientName = null, string PlaylistId = null,
        bool FromCache = false,
        string PlayId = null, DateTime? PlayTimeStamp = null, DateTime? ClientDateTime = null,
        int? TotalPlayedSeconds = null,
        int? EndPositionSeconds = null);

    [YApiRequest(WebRequestMethods.Http.Post, "play-audio")]
    public class YAddHistoryBuilder : YRequestBuilder<YResponse<string>, AddHistoryRequestModel>
    {
        public YAddHistoryBuilder(YandexMusicApi yandex, AuthStorage auth) : base(yandex, auth)
        {
        }

        protected override Dictionary<string, string> GetSubstitutions(AddHistoryRequestModel tuple)
        {
            var (track, clientName, playlistId, fromCache, playId, playTimeStamp, clientDateTime, totalPlayedSeconds,
                endPositionSeconds) = tuple;
            var dateTime = DateTime.Now;
            var trackDurationSeconds = ((int) (track.DurationMs / 1000)).ToString();
            return new Dictionary<string, string>
            {
                {"track_id", track.Id},
                {"from-cache", fromCache.ToString()},
                {"from", clientName ?? "android"},
                {"play-id", playId ?? "None"},
                {"timestamp", (playTimeStamp ?? dateTime).ToYandexTimeStampFormat()},
                {"uid", storage.User.Uid},
                {"track-length-seconds", trackDurationSeconds},
                {
                    "total-played-seconds",
                    totalPlayedSeconds.HasValue ? totalPlayedSeconds.Value.ToString() : trackDurationSeconds
                },
                {
                    "end-position-seconds",
                    endPositionSeconds.HasValue ? endPositionSeconds.Value.ToString() : trackDurationSeconds
                },
                {"album-id", track.Albums.FirstOrDefault()?.Title ?? "None"},
                {"playlist-id", playlistId ?? "None"},
                {"client-now", (clientDateTime ?? dateTime).ToYandexTimeStampFormat()},
            };
        }

        protected override HttpContent GetContent(AddHistoryRequestModel tuple)
        {
            return new FormUrlEncodedContent(GetSubstitutions(tuple));
        }
    }
}