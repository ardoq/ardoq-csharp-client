using Ardoq.Service.Interface;

namespace Ardoq
{
    public interface IArdoqClient
    {
        IAttachmentService AttachmentService { get; }
        IComponentService ComponentService { get; }
        IFieldService FieldService { get; }
        IDeprecatedModelService ModelService { get; }
        IReferenceService ReferenceService { get; }
        ITagService TagService { get; }
        IFolderService FolderService { get; }
        IWorkspaceService WorkspaceService { get; }
        INotifyService NotifyService { get; }

        string Org { get; set; }
    }
}
