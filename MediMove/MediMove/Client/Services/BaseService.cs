using MediMove.Client.temp;
using Newtonsoft.Json;

namespace MediMove.Client.Services
{
    public class BaseService
    {
        protected async Task<string> DeserializeError(HttpResponseMessage? response)
        {
            
            if (!response.IsSuccessStatusCode)
            {

                var responseContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                var errorMessage = "";
                foreach (var field in errorResponse.Errors)
                {
                    errorMessage += field.Key + ": " + string.Join("; ", field.Value) + "\n";
                }

                return errorMessage;

            }
            return "Success";
            
        }
    }
}
