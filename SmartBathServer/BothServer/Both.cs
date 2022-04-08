using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BothServer
{
    public class Both
    {
        /// <summary>
        /// id ванны
        /// </summary>
        [JsonPropertyName("uId")]
        public long uId { get; set; }

        /// <summary>
        /// температура воды
        /// </summary>
        [JsonPropertyName("temp")]
        public float? temp { get; set; } = null;

        /// <summary>
        /// набирается ли вода
        /// </summary>
        [JsonPropertyName("craneActie")]
        public bool? craneActie { get; set; } = null;

        /// <summary>
        /// открыт ли слив
        /// </summary>
        [JsonPropertyName("drainStatus")]
        public bool? drainStatus { get; set; } = null;

        /// <summary>
        /// цвет воды
        /// </summary>
        [JsonPropertyName("waterColor")]
        public Color? waterColor { get; set; } = null;

        /// <summary>
        /// на сколько процентов ванна заполнена
        /// </summary>
        [JsonPropertyName("fillingProcent")]
        public float? fillingProcent { get; set; } = null;

        public struct Constants
        {
            public static float MinTemp = 5;
            public static float MaxTemp = 65;
            public static float SpeedFillingInSeconds = 0.1f;

            /// <summary>
            /// на сколько заполнилась с предыдущего запроса
            /// </summary>
            /// <param name="strartTime">время предыдущего запроса</param>
            /// <param name="nowTime">текущее время</param>
            /// <param name="drainStatus">открыт ли слив</param>
            /// <param name="craneActie">открыт ли кран</param>
            /// <returns></returns>
            public static float UpdateFillBothInfo(long strartTime, long nowTime, bool drainStatus, bool craneActie)
            {
                float totalSeconds = (float)TimeSpan.FromTicks(nowTime - strartTime).TotalSeconds;
                if (drainStatus)
                {
                    if (craneActie)
                        return 0;
                    else
                        return -(totalSeconds * Both.Constants.SpeedFillingInSeconds);
                }
                else
                {
                    if (craneActie)
                        return (totalSeconds * Both.Constants.SpeedFillingInSeconds);
                    else
                        return 0;
                }
            }
        }

        [JsonPropertyName("timeStamp")]
        public long timeStamp { get; set; }

    }

    /// <summary>
    /// подсветка воды
    /// </summary>
    public enum Color
    {
        Red,
        White, 
        Blue
    }
}
