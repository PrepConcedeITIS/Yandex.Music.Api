using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Yandex.Music.Api.Common;
using Yandex.Music.Api.Extensions;
using Yandex.Music.Api.Models.Common;
using Yandex.Music.Api.Models.History;
using Yandex.Music.Api.Requests.Common;

namespace Yandex.Music.Api.Requests.History
{
    [YApiRequest(WebRequestMethods.Http.Post, "play-audio")]
    public class YAddHistoryBuilder : YRequestBuilder<YResponse<string>, YAddHistoryRequestModel>
    {
        public YAddHistoryBuilder(YandexMusicApi yandex, AuthStorage auth) : base(yandex, auth)
        {
        }

        protected override Dictionary<string, string> GetSubstitutions(YAddHistoryRequestModel tuple)
        {
            var (track, clientName, playlistId, fromCache, playId, playTimeStamp, clientDateTime, totalPlayedSeconds,
                endPositionSeconds) = tuple;
            var dateTime = DateTime.Now;
            var trackDurationSeconds = ((int) (track.DurationMs / 1000)).ToString();
            var dictionary = new Dictionary<string, string>
            {
                {"track_id", track.Id},
                {"from-cache", fromCache.ToString()},
                {"from", clientName ?? "android"},
                {"play-id", playId ?? ""},
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
                {"client-now", (clientDateTime ?? dateTime).ToYandexTimeStampFormat()},
            };

            if (track.Albums.FirstOrDefault()?.Id is { } albumId)
            {
                dictionary.Add("album-id", albumId);
            }

            if (playlistId is not null)
            {
                dictionary.Add("playlist-id", playlistId);
            }

            return dictionary;
        }

        protected override HttpContent GetContent(YAddHistoryRequestModel tuple)
        {
            return new FormUrlEncodedContent(GetSubstitutions(tuple));
        }
    }
}