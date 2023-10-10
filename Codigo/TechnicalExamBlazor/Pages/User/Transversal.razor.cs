using Microsoft.AspNetCore.Components;
using TechnicalExamBlazor.Data;
using TechnicalExamBlazor.Data.Models;
using TechnicalExamBlazor.ProjectResources;

namespace TechnicalExamBlazor.Pages.User
{
    public class TransversalBase : ComponentBase
    {

        [Inject]
        UserService userService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string ActionName {  get; set; }

        [Parameter]
        public string? UserId { get; set; }

        protected UserViewModel? CurrentUser { get; set; }

        protected int MyRandomId = new Random().Next(0, 1000);

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            try
            {
                CurrentUser = new UserViewModel();

                if ((ActionName == Resource.EditAction || ActionName == Resource.DeleteAction)
               && !string.IsNullOrEmpty(UserId))
                    CurrentUser = await userService.GetUser(UserId);
            }
            catch (Exception ex)
            {
                throw;
            }     
        }

        protected async Task HandleAddSubmit()
        {
            try
            {
                if (await userService.RegisterUser(CurrentUser))
                    NavigationManager.NavigateTo("/user");

            }catch (Exception ex)
            {

            }
        }

        protected async Task HandleEditSubmit()
        {
            try
            {
                if (await userService.UpdateUser(CurrentUser))
                    NavigationManager.NavigateTo("/user");
            }
            catch (Exception ex)
            {

            }
        }

        protected async Task HandleDeleteSubmit()
        {
            try
            {
                if (await userService.DeleteUser(CurrentUser.Id))
                    NavigationManager.NavigateTo("/user");
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
