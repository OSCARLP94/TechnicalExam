using Microsoft.AspNetCore.Components;
using TechnicalExamBlazor.Data;
using TechnicalExamBlazor.Data.Models;

namespace TechnicalExamBlazor.Pages.Session
{
    public class SessionLoginBase : LayoutComponentBase
    {
        protected SessionViewModel sessionModel { get; set; }

        [Inject]
        private SessionService sessionService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        private SessionProvider? SessionProvider { get; set; }

        protected bool showAlert;
        protected string alertMessage;
        protected override async Task OnInitializedAsync()
        {
            sessionModel = new();

        }
        protected async Task LoginSubmit()
        {
            try
            {
                var token = await sessionService.LoginUser(sessionModel);
                if (token != null)
                {
                    if (SessionProvider is not null)
                        await SessionProvider.SaveLocalTokenAsync(token);

                    NavigationManager.NavigateTo("/Home");
                }

            }catch (Exception ex)
            {
                showAlert = true; alertMessage=ex.Message;
            }
        }
        protected void CloseAlert()
        {
            showAlert = false;
        }
    }
}
