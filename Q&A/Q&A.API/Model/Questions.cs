using System.Runtime.Serialization;

namespace Q_A.API.Model
{
    public class Questions
    {
        [DataMember(Order =1)]
        public string Category { get; set; }
        [DataMember(Order = 2)]
        public string Title { get; set; }
        [DataMember(Order = 3)]
        public string Description { get; set; }
    }
}
