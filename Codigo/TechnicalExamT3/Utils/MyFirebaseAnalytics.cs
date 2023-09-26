
using Newtonsoft.Json;

namespace TechnicalExamT3.Utils
{
    public class MyFirebaseAnalytics : IMyFirebaseAnalytics
    {
        private readonly HttpClient _httpClient;

        public MyFirebaseAnalytics(string pathJsonOAuth) {
            // Inicializar Firebase con las credenciales
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://firebase.googleapis.com/v1beta/projects/your-project-id/events/");

        }
        public async Task<dynamic> SendEventAsync(string eventName, object eventSend)
        {
            try
            {
                var eventData = new
                {
                    name = eventName,
                    eventParams = eventSend
                };

                var json = JsonConvert.SerializeObject(eventData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    // El evento se envió correctamente.
                }
                else
                {
                    // Manejar el error si la solicitud no se completó con éxito.
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
