using System;
using Yandex.Music.Api.Models.Track;

namespace Yandex.Music.Api.Models.History
{
    public record YAddHistoryRequestModel(YTrack Track, string ClientName = null, string PlaylistId = null,
        bool FromCache = false,
        string PlayId = null, DateTime? PlayTimeStamp = null, DateTime? ClientDateTime = null,
        int? TotalPlayedSeconds = null,
        int? EndPositionSeconds = null);
}