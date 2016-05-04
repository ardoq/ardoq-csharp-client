namespace Ardoq.Service
{
    public class ServiceBase
    {
        public ServiceBase()
        {
        }

        protected ServiceBase(string org)
        {
            Org = string.IsNullOrWhiteSpace(org) ? "ardoq" : org;
        }

        protected string Org { get; set; }
    }
}