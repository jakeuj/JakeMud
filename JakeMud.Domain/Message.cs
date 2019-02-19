namespace JakeMud.Domain
{
    public enum TargetTypeEnum { None, Caller, All, Others, Client, Group }
    public class Message
    {
        public TargetTypeEnum TargetType { get; }
        public string Context { get; }
        public string TargetID { get; }
        public Message(TargetTypeEnum targetType, string context, string targetID)
        {
            this.TargetType = targetType;
            this.Context = context;
            this.TargetID = targetID;
        }
    }
}
