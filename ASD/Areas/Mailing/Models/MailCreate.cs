namespace ASD.Areas.Mailing.Views
{
    public class MailCreate
    {
        public required string Token { get; set; }

        public required int AplicacionUserId { get; set; }

        public required string Aplicacion { get; set; }

        public required string Origen { get; set; }

        public required string Destinatario { get; set; }

        public required string DestinatarioCC { get; set; }

        public required string DestinatarioCCO { get; set; }

        public required string Asunto { get; set; }

        public required string Contenido { get; set; }
        public required string fechaEnvio { get; set; }
    }
}
