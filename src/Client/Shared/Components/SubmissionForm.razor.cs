using MasterCraft.Client.Common.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MasterCraft.Client.Shared.Components
{
    public partial class SubmissionForm<TRequest, TResponse> : ComponentBase
    {
        private bool showSpinner = false;
        private EditForm editForm;
        private CustomValidation customValidation = new();
        private Dictionary<string, object> SubmitAttribute = new Dictionary<string, object>()
        {
            {"type","submit" }
        };

        [Parameter]
        public TRequest Request { get; set; }

        [Parameter]
        public Func<Task<ApiResponse<TResponse>>> OnValidSubmit { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string FormTitle { get; set; }

        [Parameter]
        public string ButtonText { get; set; }

        private async Task OnSubmitClick()
        {
            showSpinner = true;

            try
            {
                editForm?.ClearValidationMessages();

                ApiResponse<TResponse> apiResponse = await OnValidSubmit.Invoke();

                if (!apiResponse.Success)
                {
                    customValidation?.DisplayErrors(apiResponse.ErrorDetails);
                }
            }
            finally
            {
                showSpinner = false;
                StateHasChanged();
            }
        }
    }

    /// <summary>
    /// Contains extension methods for working with the <see cref="EditForm"/> class.
    /// </summary>
    public static class EditFormExtensions
    {
        /// <summary>
        /// Clears all validation messages from the <see cref="EditContext"/> of the given <see cref="EditForm"/>.
        /// </summary>
        /// <param name="editForm">The <see cref="EditForm"/> to use.</param>
        /// <param name="revalidate">
        /// Specifies whether the <see cref="EditContext"/> of the given <see cref="EditForm"/> should revalidate after all validation messages have been cleared.
        /// </param>
        /// <param name="markAsUnmodified">
        /// Specifies whether the <see cref="EditContext"/> of the given <see cref="EditForm"/> should be marked as unmodified.
        /// This will affect the assignment of css classes to a form's input controls in Blazor.
        /// </param>
        /// <remarks>
        /// This extension method should be on EditContext, but EditForm is being used until the fix for issue
        /// <see href="https://github.com/dotnet/aspnetcore/issues/12238"/> is officially released.
        /// </remarks>
        public static void ClearValidationMessages(this EditForm editForm, bool revalidate = false, bool markAsUnmodified = false)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            object GetInstanceField(Type type, object instance, string fieldName)
            {
                var fieldInfo = type.GetField(fieldName, bindingFlags);
                return fieldInfo.GetValue(instance);
            }

            var editContext = editForm.EditContext == null
                ? GetInstanceField(typeof(EditForm), editForm, "_fixedEditContext") as EditContext
                : editForm.EditContext;

            var fieldStates = GetInstanceField(typeof(EditContext), editContext, "_fieldStates");
            var clearMethodInfo = typeof(HashSet<ValidationMessageStore>).GetMethod("Clear", bindingFlags);

            foreach (DictionaryEntry kv in (IDictionary)fieldStates)
            {
                var messageStores = GetInstanceField(kv.Value.GetType(), kv.Value, "_validationMessageStores");

                if (messageStores != null)
                {
                    clearMethodInfo.Invoke(messageStores, null);
                }
            }

            if (markAsUnmodified)
                editContext.MarkAsUnmodified();

            if (revalidate)
                editContext.Validate();
        }
    }
}
