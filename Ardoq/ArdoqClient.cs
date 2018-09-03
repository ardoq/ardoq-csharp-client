using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service;
using Ardoq.Service.Interface;
using Refit;

namespace Ardoq
{
    public class ArdoqClient : IArdoqClient
    {
        private readonly HttpClient _httpClient;
        private AttachmentService _attachmentService;
        private ComponentService _componentService;
        private FieldService _fieldService;
        private ModelService _modelService;
        private ReferenceService _referenceService;
        private TagService _tagService;
        private FolderService _folderService;
        private WorkspaceService _workspaceService;
        private NotifyService _notifyService;

        private ArdoqClient(HttpClient httpClient, string endPoint)
        {
            if (endPoint == null)
            {
                throw new ArgumentNullException(nameof(endPoint));
            }

            httpClient.BaseAddress = new Uri(endPoint);
            httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            _httpClient = httpClient;
            EndPoint = endPoint;
        }


        public ArdoqClient(HttpClient httpClient, string endPoint, string token, string orgLabel) : this(httpClient,
            endPoint)
        {
            Token = token ?? throw new ArgumentNullException("Missing token");
            Org = orgLabel ?? throw new ArgumentNullException("Missing organization Label");
            AuthorizationValue = "Token token=" + Token.Trim();
            _httpClient.DefaultRequestHeaders.Add("Authorization", AuthorizationValue);
        }

        public ArdoqClient(HttpClient httpClient, string endPoint, string username, string password, string orgLabel) :
            this(httpClient, endPoint)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Org = orgLabel;
            AuthorizationValue = HeaderPassword;
            _httpClient.DefaultRequestHeaders.Add("Authorization", AuthorizationValue);
        }

        public static string ClientVersion
        {
            get
            {
                // TODO USE ASSEMBLY INFO. NOT SUPPORTED BY PORTABLE APIs
                return "2.0.4";
            }
        }

        public static string UserAgent
        {
            get { return "ardoq-dotnet-client-" + ClientVersion; }
        }

        public string EndPoint { get; private set; }

        public string Token { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string Org { get; set; }

        private string AuthorizationValue { get; set; }

        private string HeaderPassword
        {
            get
            {
                var combinedPassword = Username + ":" + Password;
                var hashedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(combinedPassword));
                var headerPassword = "Basic " + hashedPassword;
                return headerPassword;
            }
        }


        public INotifyService NotifyService
        {
            get
            {
                return _notifyService ??
                       (_notifyService =
                           new NotifyService(RestService.For<INotifyService>(_httpClient), _httpClient));
            }
        }

        public IDeprecatedModelService ModelService
        {
            get
            {
                return _modelService ??
                       (_modelService =
                           new ModelService(RestService.For<IDeprecatedModelService>(_httpClient), _httpClient, Org));
            }
        }


        public IAttachmentService AttachmentService
        {
            get
            {
                return _attachmentService ??
                       (_attachmentService =
                           new AttachmentService(RestService.For<IAttachmentService>(_httpClient),
                               _httpClient, Org));
            }
        }

        public IFieldService FieldService
        {
            get
            {
                return _fieldService ??
                       (_fieldService =
                           new FieldService(RestService.For<IFieldService>(_httpClient), Org));
            }
        }

        public ITagService TagService
        {
            get
            {
                return _tagService ??
                       (_tagService = new TagService(RestService.For<ITagService>(_httpClient), Org));
            }
        }

        public IFolderService FolderService
        {
            get
            {
                return _folderService ??
                       (_folderService = new FolderService(RestService.For<IFolderService>(_httpClient), Org));
            }
        }

        public IReferenceService ReferenceService
        {
            get
            {
                return _referenceService ??
                       (_referenceService =
                           new ReferenceService(RestService.For<IReferenceService>(_httpClient), Org));
            }
        }

        public IComponentService ComponentService
        {
            get
            {
                return _componentService ??
                       (_componentService =
                           new ComponentService(RestService.For<IComponentService>(_httpClient), Org));
            }
        }

        public IWorkspaceService WorkspaceService
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