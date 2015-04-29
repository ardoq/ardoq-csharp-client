using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardoq;
using Ardoq.Models;
using Ardoq.Service;
using Ardoq.Service.Interface;
using ArdoqTest.Helper;
using NUnit.Framework;

namespace ArdoqTest.Service
{
    [TestFixture]
    public class AttachmentServiceTest
    {
        private String filename;
        private IArdoqClient client;
        private IAttachmentService service;

        [TestFixtureSetUp]
        public void Setup()
        {
            filename = TestUtils.GetTestPropery("filename");
            client = TestUtils.GetClient();

            service = client.AttachmentService;
        }

        private async Task<Workspace> CreateWorkspace()
        {
            return
                await
                    client.WorkspaceService.CreateWorkspace(new Workspace("my Attachment Test Workspace",
                        TestUtils.GetTestPropery("modelId"), "Hello world!"));
        }

        private async Task DeleteWorkspace(Workspace workspace)
        {
            await client.WorkspaceService.DeleteWorkspace(workspace.Id);
        }

        private async Task<Attachment> UploadAttachment(Workspace workspace)
        {
            string path = Directory.GetCurrentDirectory() + @"\TestData\Media\ardoq_hero.png";
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return await service.UploadAttachment(workspace.Id, stream, filename);
        }

        [Test]
        public async Task DeleteGetAttachmentsTest()
        {
            Workspace workspace = await CreateWorkspace();
            List<Attachment> attachments = await service.GetAttachments(workspace.Id);
            Assert.IsFalse(attachments.Any());
            await UploadAttachment(workspace);
            attachments = await service.GetAttachments(workspace.Id);
            foreach (Attachment attachment in attachments)
            {
                Assert.NotNull(attachment.Id);
            }
            await service.DeleteAttachment(workspace.Id, filename);
            List<Attachment> newList = await service.GetAttachments(workspace.Id);
            Assert.True(newList.Count == attachments.Count - 1);
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async void DownloadAttachmentTest()
        {
            Workspace workspace = await CreateWorkspace();
            Attachment attachment = await UploadAttachment(workspace);
            Stream fileStream = await service.DownloadAttachment(workspace.Id, filename);
            Assert.True(fileStream.Length == attachment.Size);
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async void UploadAttachmentTest()
        {
            Workspace workspace = await CreateWorkspace();
            Attachment attachment = await UploadAttachment(workspace);
            Assert.True(attachment.Filename == filename);
            await DeleteWorkspace(workspace);
        }
    }
}