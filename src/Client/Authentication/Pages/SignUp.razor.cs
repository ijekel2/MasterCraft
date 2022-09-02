using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Client.Shared.Components;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication.Pages
{
    public partial class SignUp : ComponentBase
    {
        private RegisterUserVm request = new();

        [Inject] ApiClient ApiClient { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] AuthenticationService AuthService { get; set; }
        [CascadingParameter] ErrorHandler ErrorHandler {get; set; }
        public UploadFileVm ProfileImage { get; set; } = new();
        private SfUploader UploadControl { get; set; }

        private async Task<ApiResponse<EmptyVm>> OnRegisterClick()
        {
            ApiResponse<EmptyVm> apiResponse = new();
            try
            {
                apiResponse =
                    await ApiClient.PostAsync<RegisterUserVm, EmptyVm>("register", request);

                if (apiResponse.Success)
                {
                    var loginResponse = await AuthService.Login(new GenerateTokenVm() { Username = request.Email, Password = request.Password });

                    if (loginResponse.Success)
                    {
                        Navigation.NavigateTo("/setup/profile");

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler?.ProcessError(ex);
            }

            return apiResponse;
        }

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

            request.ProfileImageUrl = ProfileImage.Path; //-- Till we have blob storage just embed the image data in the url

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
