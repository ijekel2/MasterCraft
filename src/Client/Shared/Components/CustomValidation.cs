using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Shared.Components
{
    public class CustomValidation : ComponentBase
    {
        private ValidationMessageStore messageStore;

        [CascadingParameter]
        private EditContext CurrentEditContext { get; set; }

        protected override void OnInitialized()
        {
            if (CurrentEditContext is null)
            {
                throw new InvalidOperationException(
                    $"{nameof(CustomValidation)} requires a cascading " +
                    $"parameter of type {nameof(EditContext)}. " +
                    $"For example, you can use {nameof(CustomValidation)} " +
                    $"inside an {nameof(EditForm)}.");
            }

            messageStore = new(CurrentEditContext);

            CurrentEditContext.OnValidationRequested += (s, e) =>
                messageStore?.Clear();
            CurrentEditContext.OnFieldChanged += (s, e) =>
                messageStore?.Clear(e.FieldIdentifier);
        }

        public void DisplayErrors(ProblemDetails errorDetails)
        {
            IDictionary<string, string[]> errors;

            if (errorDetails is ValidationProblemDetails validationErrorDetails)
            {
                errors = validationErrorDetails.Errors;
            }
            else if (errorDetails is not null)
            {
                errors = new Dictionary<string, string[]>()
                {
                    { errorDetails.Title , new string[] { errorDetails.Detail } }
                };
            }
            else
            {
                return;
            }
            
            if (CurrentEditContext is not null)
            {
                foreach (var err in errors)
                {
                    string lKey = string.IsNullOrEmpty(err.Key) ? CurrentEditContext.Model.GetType().Name : err.Key;
                    messageStore?.Add(CurrentEditContext.Field(lKey), err.Value);
                }

                CurrentEditContext.NotifyValidationStateChanged();
            }
        }

        public void ClearErrors()
        {
            messageStore?.Clear();
            CurrentEditContext?.NotifyValidationStateChanged();
        }
    }
}
