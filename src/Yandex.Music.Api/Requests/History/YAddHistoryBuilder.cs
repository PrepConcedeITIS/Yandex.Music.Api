using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Yandex.Music.Api.Common;
using Yandex.Music.Api.Models.Common;
using Yandex.Music.Api.Models.Library;
using Yandex.Music.Api.Models.Track;
using Yandex.Music.Api.Requests.Common;

namespace Yandex.Music.Api.Requests.History
{
    [YApiRequest(WebRequestMethods.Http.Post, "play-audio")]
    public class YAddHistoryBuilder : YRequestBuilder<YResponse<string>, (YTrack Track, string ClientName, string PlaylistId, int? TotalPlayedSeconds, int? EndPositionSeconds)>
    {
        public YAddHistoryBuilder(YandexMusicApi yandex, AuthStorage auth) : base(yandex, auth)
        {
        }
        
        protected override Dictionary<string, string> GetSubstitutions((YTrack Track, string ClientName, string PlaylistId, int? TotalPlayedSeconds, int? EndPositionSeconds) tuple)
        {
            var (track, clientName, playlistId, totalPlayedSeconds, endPositionSeconds) = tuple;
            var isoTimeStamp = DateTime.UtcNow.ToString("O");
            var timeStamp = isoTimeStamp.Remove(isoTimeStamp.Length - 1 - 4, 4);
            var trackDurationSeconds = ((int)(track.DurationMs / 1000)).ToString();
            return new Dictionary<string, string> {
                { "track_id", track.Id },
                { "from-cache", false.ToString() },
                { "from", clientName ?? "android" },
                { "play-id", string.Empty },
                { "timestamp", timeStamp },
                { "uid", storage.User.Uid },
                { "track-length-seconds", trackDurationSeconds },
                { "total-played-seconds", totalPlayedSeconds.HasValue ? totalPlayedSeconds.Value.ToString() : trackDurationSeconds },
                { "end-position-seconds", endPositionSeconds.HasValue ? endPositionSeconds.Value.ToString() : trackDurationSeconds },
                { "album-id", track.Albums.FirstOrDefault()?.Title ?? string.Empty },
                { "playlist-id", playlistId },
                { "client-now", timeStamp },
            };
        }

        protected override HttpContent GetContent((YTrack Track, string ClientName, string PlaylistId, int? TotalPlayedSeconds, int? EndPositionSeconds) tuple)
        {
            return new FormUrlEncodedContent(GetSubstitutions(tuple));
        }
    }
}