namespace Ardoq.Service
{
    public class ServiceBase
    {
        public ServiceBase()
        {
        }

        protected ServiceBase(string org)
        {
            Org = string.IsNullOrWhiteSpace(org) ? "personal" : org;
        }

        protected string Org { get; set; }
    }
}