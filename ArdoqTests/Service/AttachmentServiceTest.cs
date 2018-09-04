using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardoq;
using Ardoq.Models;
using Ardoq.Service.Interface;
using ArdoqTest.Helper;
using NUnit.Framework;
using System.Reflection;

namespace ArdoqTest.Service
{
    [TestFixture]
    public class AttachmentServiceTest
    {
        private string _filename;
        private IArdoqClient _client;
        private IAttachmentService _service;

        [OneTimeSetUp]
        public void Setup()
        {
            _filename = TestUtils.GetTestProperty("filename");
            _client = TestUtils.GetClient();

            _service = _client.AttachmentService;
        }

        private async Task<Workspace> CreateWorkspace()
        {
            return
                await
                    _client.WorkspaceService.CreateWorkspace(new Workspace("my Attachment Test Workspace",
                        TestUtils.GetTestProperty("modelId"), "Hello world!"));
        }

        private async Task DeleteWorkspace(Workspace workspace)
        {
            await _client.WorkspaceService.DeleteWorkspace(workspace.Id);
        }

        private async Task<Attachment> UploadAttachment(Workspace workspace)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                           throw new Exception("Could not get base path");
            var path = Path.Combine(basePath, "TestData", "Media", "ardoq_hero.png");
            var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return await _service.UploadAttachment(workspace.Id, stream, _filename);
        }

        [Test]
        public async Task DeleteGetAttachmentsTest()
        {
            var workspace = await CreateWorkspace();
            var attachments = await _service.GetAttachments(workspace.Id);
            Assert.IsFalse(attachments.Any());
            await UploadAttachment(workspace);
            attachments = await _service.GetAttachments(workspace.Id);
            foreach (var attachment in attachments)
            {
                Assert.NotNull(attachment.Id);
            }

            await _service.DeleteAttachment(workspace.Id, _filename);
            var newList = await _service.GetAttachments(workspace.Id);
            Assert.True(newList.Count == attachments.Count - 1);
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async Task DownloadAttachmentTest()
        {
            var workspace = await CreateWorkspace();
            var attachment = await UploadAttachment(workspace);
            var fileStream = await _service.DownloadAttachment(workspace.Id, _filename);
            Assert.True(fileStream.Length == attachment.Size);
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async Task UploadAttachmentTest()
        {
            var workspace = await CreateWorkspace();
            var attachment = await UploadAttachment(workspace);
            Assert.True(attachment.Filename == _filename);
            await DeleteWorkspace(workspace);
        }
    }
}