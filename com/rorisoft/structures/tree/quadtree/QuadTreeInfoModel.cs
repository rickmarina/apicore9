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
        public long queries { get; set; }
        public STATUS status { get; set; }
        public long totalPois { get; set; }


    }
}
