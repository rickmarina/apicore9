namespace com.rorisoft.net
{
    public class CommentModel
    {
        public enum CommentTypeEnum
        {
            WARNING,
            ERROR,
            INFO,
            INFO_MANDATORY
        }

        public CommentTypeEnum CommentType { get; set; }
        public string Comment { get; set; }
    }
}
