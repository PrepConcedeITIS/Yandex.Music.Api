using System.Threading.Tasks;
using Yandex.Music.Api.Common;
using Yandex.Music.Api.Models.Track;
using Yandex.Music.Api.Requests.History;

namespace Yandex.Music.Api.API
{
    /// <summary>
    /// API для взаимодействия с треками
    /// </summary>
    public class YHistoryAPI : YCommonAPI
    {
        public YHistoryAPI(YandexMusicApi yandex) : base(yandex)
        {
        }

        /// <summary>
        /// Добавить альбом в список лайкнутых
        /// </summary>
        /// <param name="storage">Хранилище</param>
        /// <param name="track">Трек</param>
        /// <param name="playlistId"></param>
        /// <param name="totalPlayedSeconds"></param>
        /// <returns></returns>
        public async Task<bool> AddTrackToHistoryAsync(AuthStorage storage, AddHistoryRequestModel model)
        {
            var result = await new YAddHistoryBuilder(api, storage)
                .Build(model)
                .GetResponseAsync();
            return result?.Result == "ok";
        }
    }
}