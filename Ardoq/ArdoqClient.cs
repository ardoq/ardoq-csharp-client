using System;
using System.Net.Http;
using System.Text;
using Ardoq.Service;
using Ardoq.Service.Interface;
using Refit;

namespace Ardoq
{
    public class ArdoqClient
    {
        private readonly HttpClient _httpClient;
        private AttachmentService _attachmentService;
        private ComponentService _componentService;
        private FieldService _fieldService;
        private ModelService _modelService;
        private ReferenceService _refrenceService;
        private TagService _tagService;
        private WorkspaceService _workspaceService;

        private ArdoqClient(HttpClient httpClient, string endPoint)
        {
            if (endPoint == null)
            {
                throw new ArgumentNullException("endPoint");
            }
            httpClient.BaseAddress = new Uri(endPoint);
            httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            _httpClient = httpClient;
            EndPoint = endPoint;
        }


        public ArdoqClient(HttpClient httpClient, string endPoint, string token, string org = "personal")
            : this(httpClient, endPoint)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            Token = token;
            Org = org;
            AuthorizationValue = "Token token=" + Token.Trim();
            _httpClient.DefaultRequestHeaders.Add("Authorization", AuthorizationValue);
        }

        public ArdoqClient(HttpClient httpClient, String endPoint, String username, String password,
            string org = "personal") : this(httpClient, endPoint)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            Username = username;
            Password = password;
            Org = org;
            AuthorizationValue = HeaderPassword;
            _httpClient.DefaultRequestHeaders.Add("Authorization", AuthorizationValue);
        }

        public static string ClientVersion
        {
            get
            {
                // TODO USE ASSEMBLY INFO. NOT SUPPORTED BY PORTABLE APIs
                return "1.0.0";
            }
        }

        public static string UserAgent
        {
            get { return "ardoq-dotnet-client-" + ClientVersion; }
        }

        public String EndPoint { get; private set; }
        public string Token { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Org { get; set; }
        private string AuthorizationValue { get; set; }

        private string HeaderPassword
        {
            get
            {
                string combinedPassword = Username + ":" + Password;
                string hashedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(combinedPassword));
                string headerPassword = "Basic " + hashedPassword;
                return headerPassword;
            }
        }

        public ModelService ModelService
        {
            get
            {
                return _modelService ??
                       (_modelService =
                           new ModelService(RestService.For<IDeprecatedModelService>(_httpClient), Org));
            }
        }


        public AttachmentService AttachmentService
        {
            get
            {
                return _attachmentService ??
                       (_attachmentService =
                           new AttachmentService(RestService.For<IAttachmentService>(_httpClient),
                               _httpClient, Org));
            }
        }

        public FieldService FieldService
        {
            get
            {
                return _fieldService ??
                       (_fieldService =
                           new FieldService(RestService.For<IFieldService>(_httpClient), Org));
            }
        }

        public TagService TagService
        {
            get
            {
                return _tagService ??
                       (_tagService = new TagService(RestService.For<ITagService>(_httpClient), Org));
            }
        }

        public ReferenceService ReferenceService
        {
            get
            {
                return _refrenceService ??
                       (_refrenceService =
                           new ReferenceService(RestService.For<IReferenceService>(_httpClient), Org));
            }
        }

        public ComponentService ComponentService
        {
            get
            {
                return _componentService ??
                       (_componentService =
                           new ComponentService(RestService.For<IComponentService>(_httpClient), Org));
            }
        }

        public WorkspaceService WorkspaceService
        {
            get
            {
                return _workspaceService ??
                       (_workspaceService =
                           new WorkspaceService(RestService.For<IWorkspaceService>(_httpClient), Org));
            }
        }
    }
}