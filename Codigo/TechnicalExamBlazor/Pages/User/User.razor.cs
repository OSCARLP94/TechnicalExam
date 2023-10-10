using Microsoft.AspNetCore.Components;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TechnicalExamBlazor.Data;
using TechnicalExamBlazor.Data.Models;

namespace TechnicalExamBlazor.Pages.User
{
    public class UserBase : ComponentBase, IDisposable
    {
        [Inject]
        UserService userService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected IEnumerable<UserViewModel> users;
        protected bool showAlert;
        protected string alertMessage;

        protected void CloseAlert()
        {
            showAlert = false;
        }

        protected override async Task OnInitializedAsync()
        {
            userService.OnUserUpdated += OnUserPageUpdated;
            users = await userService.GetListUsers();
        }

        /// <summary>
        /// Pasos a seguir cuando se detecte un usuario actualizado
        /// </summary>
        private async Task OnUserPageUpdated()
        {
            alertMessage = "Un usuario ha sido actualizado o registrado";
            showAlert = true;
            users = await userService.GetListUsers();
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Descarga excel de usuarios
        /// </summary>
        /// <returns></returns>
        protected async Task DownloadMyExcel()
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Usuarios");

            // Crear la fila de encabezados
            IRow headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("Nombre");
            headerRow.CreateCell(1).SetCellValue("Fecha nacimiento");
            headerRow.CreateCell(2).SetCellValue("Sexo");

            // Llenar el contenido del archivo Excel
            int rowIndex = 1;
            foreach (var user in users)
            {
                IRow dataRow = sheet.CreateRow(rowIndex++);
                dataRow.CreateCell(0).SetCellValue(user.Name);
                dataRow.CreateCell(1).SetCellValue(user.BirthDate.ToString("dd/MM/yyyy"));
                dataRow.CreateCell(2).SetCellValue(user.Sex);
            }

            // Guardar el libro de trabajo en un MemoryStream
            using (var memoryStream = new System.IO.MemoryStream())
            {
                workbook.Write(memoryStream);
                var content = memoryStream.ToArray();

                // Descargar el archivo Excel en el navegador
                var fileName = $"usuarios{DateTime.Now.ToShortDateString()}.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                NavigationManager.NavigateTo($"data:{contentType};base64,{Convert.ToBase64String(content)}", true);
            }
        }

        /// <summary>
        /// Navega hacia pagina transversal dependiendo la accion
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="userId"></param>
        protected void GoToTransversal(string actionName, string? userId)
        {
            // Codificar los valores de los parámetros en la URL
            var encodedActionName = Uri.EscapeDataString(actionName);
            var encodedUserId = Uri.EscapeDataString(userId??"");

            // Construir la URL con los parámetros
            var url = $"/transversal/{encodedActionName}/{encodedUserId}";

            // Navegar a la URL con los parámetros
            NavigationManager.NavigateTo(url);
        }

        public void Dispose()
        {
            userService.OnUserUpdated -= OnUserPageUpdated;
        }
    }
}
