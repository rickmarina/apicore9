using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace com.rorisoft.structures.tree
{
    [Serializable]
    public class QuadTreeInfoModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum STATUS
        {
            EMPTY,
            READY,
            LOADING_DATA,
            ERROR
        }
        public long queries { get; set; } = 0;
        public STATUS status { get; set; } = STATUS.EMPTY;
        public long totalPois { get; set; } = 0;
        public DateTime lastUpdated {get;set;} = DateTime.UtcNow;


    }
}
