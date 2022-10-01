using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MasterCraft.Client.Components
{
    public partial class MentorProfileInputs : ComponentBase
    {
        [Parameter] public MentorProfileVm MentorProfile { get; set; } = new();

        [Parameter] public UploadFileVm ProfileImage { get; set; } = new();

        private OfferingVm Offering = new();

        private SfUploader UploadControl { get; set; }

        public async Task OnChange(UploadChangeEventArgs args)
        {
            Syncfusion.Blazor.Inputs.UploadFiles newestFile = args.Files[0];

            //-- Get preview for selected file.
            byte[] bytes = newestFile.Stream.ToArray();
            string base64 = Convert.ToBase64String(bytes);
            ProfileImage.Path = @"data:image/" + newestFile.FileInfo.Type + ";base64," + base64;
            ProfileImage.Name = newestFile.FileInfo.Name;
            ProfileImage.Size = newestFile.FileInfo.Size;
            ProfileImage.Stream = newestFile.Stream;

            MentorProfile.ProfileImageUrl = ProfileImage.Path; //-- Till we have blob storage just embed the image data in the url

            //-- Delete any other files that are already in the list.
            foreach (var file in await UploadControl.GetFilesDataAsync())
            {
                if (file.Id != newestFile.FileInfo.Id)
                {
                    await UploadControl.RemoveAsync(new[] { file });
                }
            }
        }

        public void OnRemove(RemovingEventArgs args)
        {
            foreach (var removeFile in args.FilesData)
            {
                if (File.Exists(Path.Combine(@"\Images", removeFile.Name)))
                {
                    File.Delete(Path.Combine(@"\Images\", removeFile.Name));
                }
            }
        }
    }
}
