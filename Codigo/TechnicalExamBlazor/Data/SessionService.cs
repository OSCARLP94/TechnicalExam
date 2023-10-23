using TechnicalExamBlazor.Data.Models;

namespace TechnicalExamBlazor.Data
{
    public class SessionService
    {
        private string userMock = "user123";
        private string passMock = "pass123";
        public async Task<string> LoginUser(SessionViewModel logData)
        {
            try
            {
                if (logData.UserName != userMock || logData.Password != passMock)
                    throw new ArgumentException("El usuario y/o clave no existe o coinciden");

                return "token123";
            }catch (Exception ex)
            {
                throw;
            }
        }
    }
}
