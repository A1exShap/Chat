using System.ServiceModel;


namespace Chat
{
    public class ServerUser
    {
        #region Properties

        public int ID { get; set; }

        public string Name { get; set; }

        public OperationContext Context { get; set; }

        #endregion
    }
}
