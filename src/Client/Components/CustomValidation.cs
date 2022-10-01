using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MasterCraft.Client.Components
{
    public class CustomValidation : ComponentBase
    {
        private ValidationMessageStore _messageStore;
        [CascadingParameter] private EditContext CurrentEditContext { get; set; }

        public bool HasGeneralErrors { get; private set; }

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


            _messageStore = new(CurrentEditContext);

            CurrentEditContext.OnValidationRequested += (s, e) =>
            {
                _messageStore?.Clear();
                HasGeneralErrors = false;
            };

            CurrentEditContext.OnFieldChanged += (s, e) =>
            {
                _messageStore?.Clear(e.FieldIdentifier);
                if (string.IsNullOrEmpty(e.FieldIdentifier.FieldName))
                {
                    HasGeneralErrors = false;
                }
            };
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
                    _messageStore?.Clear(CurrentEditContext.Field(lKey));
                    _messageStore?.Add(CurrentEditContext.Field(lKey), err.Value);

                    if (string.IsNullOrEmpty(err.Key))
                    {
                        HasGeneralErrors = true; 
                    }
                }

                CurrentEditContext.NotifyValidationStateChanged();
            }
        }

        public void ClearErrors()
        {
            StateHasChanged();
        }
    }
}
