using System.Runtime.Serialization;

namespace EdgeVersionChecker
{
    [DataContract]
    public class EdgeUpdateRequest
    {
        [DataMember(Name = "targetingAttributes")]
        public TargetingAttributes targetingAttributes;
    }

    [DataContract]
    public class TargetingAttributes
    {
        [DataMember(Name = "AppAp")]
        public string AppAp;

        public string AppCohort;
        public string AppCohortHint;
        public string AppCohortName;
        public string AppLang;
        public string AppRollout;
        public string AppTargetVersionPrefix;
        public string AppVersion;
        public bool IsInternalUser;
        public bool IsMachine;
        public string OsArch;
        public string OsPlatform;
        public string OsVersion;
        public string Updater;
        public string UpdaterVersion;
    }
}
