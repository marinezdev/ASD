using ASD.Areas.Mailing.Views;
using System.Text;
using System.Text.Json;

namespace ASD.Repository.Utilidades
{
    public class Email
    {
        public static async Task<Mail> SendAsync(MailCreate correoNuevo)
        {
            var cliente = new HttpClient();
            string urlApi = "https://mailing.asae.com.mx/api/MailEnvio/";
            Mail newEmail = new Mail();
            cliente.DefaultRequestHeaders.Clear();

            var data = JsonSerializer.Serialize<MailCreate>(correoNuevo);
            HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await cliente.PostAsync(urlApi, content);
            if (httpResponse.IsSuccessStatusCode)
            {
                // Get data about the new Email
                var result = await httpResponse.Content.ReadAsStringAsync();
                newEmail = JsonSerializer.Deserialize<Mail>(result);

                //Console.WriteLine("Correo electrónico creado para: {0} \n\r Token: {1}", newEmail.destinatario, newEmail.token);
            }
            else
            {
               
                //Console.WriteLine(">>> Error. No se pudo comunicar con el servicio web: [" + httpResponse.StatusCode.ToString() + "] " + httpResponse.ReasonPhrase.ToString());
            }

            return newEmail;


        }
    }
}
