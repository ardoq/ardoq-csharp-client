using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service;
using Ardoq.Service.Interface;

namespace Ardoq
{
    public interface IArdoqClient
    {
        IAttachmentService AttachmentService { get; }
        IComponentService ComponentService { get; }
        IFieldService FieldService { get; }
        IModelService ModelService { get; }
        IReferenceService ReferenceService { get; }
        ITagService TagService { get; }
        IFolderService FolderService { get; }
        IWorkspaceService WorkspaceService { get; }
        INotifyService NotifyService { get; }

        string Org { get; set; }
    }
}
