namespace ASD.Areas.Mailing.Views
{
    public class Mail
    {
        public int id { get; set; }

        public int idStatusSend { get; set; }

        public int aplicacionUserId { get; set; }

        public string aplicacion { get; set; }

        public string origen { get; set; }

        public string destinatario { get; set; }

        public string destinatarioCC { get; set; }

        public string destinatarioCCO { get; set; }

        public string asunto { get; set; }

        public string contenido { get; set; }
        public string token { get; set; }
        public string fechaSolicitud { get; set; }
        public string fechaEnvio { get; set; }
    }
}
