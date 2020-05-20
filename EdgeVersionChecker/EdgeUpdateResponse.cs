using System.Runtime.Serialization;

namespace EdgeVersionChecker
{
    [DataContract]
    public class EdgeUpdateResponse
    {
        [DataMember(Name = "ContentId")]
        public ContentId ContentId;
    }

    [DataContract]
    public class ContentId
    {
        [DataMember(Name = "Namespace")]
        public string Namespace;

        [DataMember(Name = "Name")]
        public string Name;

        [DataMember(Name = "Version")]
        public string Version;
    }
}
