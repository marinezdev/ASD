using ASD.Areas.Persona.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Areas.Mailing.Formato
{
    public class EstatusOrdenTrabajo
    {
        public string HtmlEstatusOrdenTrabajo(EmailViewModel email, CTicketViewModel db_ticket, CBitacoraViewModel db_bitaora)
        {
            string redult = $@"
                <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
                <html dir=""ltr"" xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" lang=""es"">
                 <head>
                  <meta content=""width=device-width, initial-scale=1"" name=""viewport"">
                  <meta charset=""UTF-8"">
                  <meta name=""x-apple-disable-message-reformatting"">
                  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                  <meta content=""telephone=no"" name=""format-detection"">
                  <title>Cambio status Orden Trabajo</title><!--[if (mso 16)]>
                    <style type=""text/css"">
                    a {{text-decoration: none;}}
                    </style>
                    <![endif]--><!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]--><!--[if gte mso 9]>
                <xml>
                    <o:OfficeDocumentSettings>
                    <o:AllowPNG></o:AllowPNG>
                    <o:PixelsPerInch>96</o:PixelsPerInch>
                    </o:OfficeDocumentSettings>
                </xml>
                <![endif]-->
                  <style type=""text/css"">
                .rollover span {{
	                font-size:0;
                }}
                #outlook a {{
	                padding:0;
                }}
                a[x-apple-data-detectors] {{
	                color:inherit!important;
	                text-decoration:none!important;
	                font-size:inherit!important;
	                font-family:inherit!important;
	                font-weight:inherit!important;
	                line-height:inherit!important;
                }}
                .es-desk-hidden {{
	                display:none;
	                float:left;
	                overflow:hidden;
	                width:0;
	                max-height:0;
	                line-height:0;
	                mso-hide:all;
                }}
                .es-left {{
	                float:left;
                }}
                .es-right {{
	                float:right;
                }}
                .es-p5 {{
	                padding:5px;
                }}
                .es-p5t {{
	                padding-top:5px;
                }}
                .es-p5b {{
	                padding-bottom:5px;
                }}
                .es-p5l {{
	                padding-left:5px;
                }}
                .es-p5r {{
	                padding-right:5px;
                }}
                .es-p10 {{
	                padding:10px;
                }}
                .es-p10l {{
	                padding-left:10px;
                }}
                .es-p10r {{
	                padding-right:10px;
                }}
                .es-p15 {{
	                padding:15px;
                }}
                .es-p15l {{
	                padding-left:15px;
                }}
                .es-p15r {{
	                padding-right:15px;
                }}
                .es-p25 {{
	                padding:25px;
                }}
                .es-p25t {{
	                padding-top:25px;
                }}
                .es-p25b {{
	                padding-bottom:25px;
                }}
                .es-p25l {{
	                padding-left:25px;
                }}
                .es-p25r {{
	                padding-right:25px;
                }}
                .es-p30 {{
	                padding:30px;
                }}
                .es-p30t {{
	                padding-top:30px;
                }}
                .es-p30b {{
	                padding-bottom:30px;
                }}
                .es-p30l {{
	                padding-left:30px;
                }}
                .es-p30r {{
	                padding-right:30px;
                }}
                .es-p35 {{
	                padding:35px;
                }}
                .es-p35t {{
	                padding-top:35px;
                }}
                .es-p35b {{
	                padding-bottom:35px;
                }}
                .es-p35l {{
	                padding-left:35px;
                }}
                .es-p35r {{
	                padding-right:35px;
                }}
                .es-p40 {{
	                padding:40px;
                }}
                .es-p40t {{
	                padding-top:40px;
                }}
                .es-p40b {{
	                padding-bottom:40px;
                }}
                .es-p40l {{
	                padding-left:40px;
                }}
                .es-menu td {{
	                border:0;
                }}
                .es-menu td a img {{
	                display:inline-block!important;
	                vertical-align:middle;
                }}
                s {{
	                text-decoration:line-through;
                }}
                ul li, ol li {{
	                margin-left:0;
	                margin-bottom:15px;
                }}
                .es-menu td a {{
	                text-decoration:none;
	                display:block;
	                font-family:arial, ""helvetica neue"", helvetica, sans-serif;
                }}
                .es-header-body p, .es-header-body ul li, .es-header-body ol li {{
	                color:rgb(51, 51, 51);
	                font-size:14px;
                }}
                .es-header-body a {{
	                color:rgb(102, 102, 102);
	                font-size:14px;
                }}
                .es-infoblock a {{
	                font-size:12px;
	                color:rgb(204, 204, 204);
                }}
                h1 {{
	                font-size:46px;
	                font-style:normal;
	                font-weight:bold;
	                color:rgb(51, 51, 51);
                }}
                h3 {{
	                font-size:20px;
	                font-style:normal;
	                font-weight:bold;
	                color:rgb(51, 51, 51);
                }}
                .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {{
	                font-size:46px;
                }}
                .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {{
	                font-size:26px;
                }}
                .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {{
	                font-size:20px;
                }}
                @media only screen and (max-width:600px) {{p, ul li, ol li, a {{ line-height:150%!important }} h1, h2, h3, h1 a, h2 a, h3 a {{ line-height:120%!important }} h1 {{ font-size:36px!important; text-align:left }} h2 {{ font-size:26px!important; text-align:left }} h3 {{ font-size:20px!important; text-align:left }} .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {{ font-size:36px!important; text-align:left }} .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {{ font-size:26px!important; text-align:left }} .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {{ font-size:20px!important; text-align:left }} .es-menu td a {{ font-size:12px!important }} .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {{ font-size:14px!important }} .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a {{ font-size:14px!important }} .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {{ font-size:14px!important }} .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {{ font-size:12px!important }} *[class=""gmail-fix""] {{ display:none!important }} .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {{ text-align:center!important }} .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {{ text-align:right!important }} .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {{ text-align:left!important }} .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {{ display:inline!important }} .es-button-border {{ display:inline-block!important }} a.es-button, button.es-button {{ font-size:20px!important; display:inline-block!important }} .es-adaptive table, .es-left, .es-right {{ width:100%!important }} .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {{ width:100%!important; max-width:600px!important }} .es-adapt-td {{ display:block!important; width:100%!important }} .adapt-img {{ width:100%!important; height:auto!important }} .es-m-p0 {{ padding:0!important }} .es-m-p0r {{ padding-right:0!important }} .es-m-p0l {{ padding-left:0!important }} .es-m-p0t {{ padding-top:0!important }} .es-m-p0b {{ padding-bottom:0!important }} .es-m-p20b {{ padding-bottom:20px!important }} .es-mobile-hidden, .es-hidden {{ display:none!important }} tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {{ width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important }} tr.es-desk-hidden {{ display:table-row!important }} table.es-desk-hidden {{ display:table!important }} td.es-desk-menu-hidden {{ display:table-cell!important }} .es-menu td {{ width:1%!important }} table.es-table-not-adapt, .esd-block-html table {{ width:auto!important }} table.es-social {{ display:inline-block!important }} table.es-social td {{ display:inline-block!important }} .es-m-p5 {{ padding:5px!important }} .es-m-p5t {{ padding-top:5px!important }} .es-m-p5b {{ padding-bottom:5px!important }} .es-m-p5r {{ padding-right:5px!important }} .es-m-p5l {{ padding-left:5px!important }} .es-m-p10 {{ padding:10px!important }} .es-m-p10t {{ padding-top:10px!important }} .es-m-p10b {{ padding-bottom:10px!important }} .es-m-p10r {{ padding-right:10px!important }} .es-m-p10l {{ padding-left:10px!important }} .es-m-p15 {{ padding:15px!important }} .es-m-p15t {{ padding-top:15px!important }} .es-m-p15b {{ padding-bottom:15px!important }} .es-m-p15r {{ padding-right:15px!important }} .es-m-p15l {{ padding-left:15px!important }} .es-m-p20 {{ padding:20px!important }} .es-m-p20t {{ padding-top:20px!important }} .es-m-p20r {{ padding-right:20px!important }} .es-m-p20l {{ padding-left:20px!important }} .es-m-p25 {{ padding:25px!important }} .es-m-p25t {{ padding-top:25px!important }} .es-m-p25b {{ padding-bottom:25px!important }} .es-m-p25r {{ padding-right:25px!important }} .es-m-p25l {{ padding-left:25px!important }} .es-m-p30 {{ padding:30px!important }} .es-m-p30t {{ padding-top:30px!important }} .es-m-p30b {{ padding-bottom:30px!important }} .es-m-p30r {{ padding-right:30px!important }} .es-m-p30l {{ padding-left:30px!important }} .es-m-p35 {{ padding:35px!important }} .es-m-p35t {{ padding-top:35px!important }} .es-m-p35b {{ padding-bottom:35px!important }} .es-m-p35r {{ padding-right:35px!important }} .es-m-p35l {{ padding-left:35px!important }} .es-m-p40 {{ padding:40px!important }} .es-m-p40t {{ padding-top:40px!important }} .es-m-p40b {{ padding-bottom:40px!important }} .es-m-p40r {{ padding-right:40px!important }} .es-m-p40l {{ padding-left:40px!important }} .es-desk-hidden {{ display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important }} .h-auto {{ height:auto!important }} }}
                @media screen and (max-width:384px) {{.mail-message-content {{ width:414px!important }} }}
                </style>
                 </head>
                 <body bis_status=""ok"" bis_frame_id=""270"" style=""width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0"">
                  <div dir=""ltr"" class=""es-wrapper-color"" lang=""es"" style=""background-color:#EEEDED""><!--[if gte mso 9]>
			                <v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t"">
				                <v:fill type=""tile"" color=""#eeeded""></v:fill>
			                </v:background>
		                <![endif]-->
                   <table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#EEEDED"" role=""none"">
                     <tr>
                      <td valign=""top"" style=""padding:0;Margin:0"">
                       <table cellpadding=""0"" cellspacing=""0"" class=""es-header"" align=""center"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
                         <tr>
                          <td align=""center"" style=""padding:0;Margin:0"">
                           <table bgcolor=""#ffffff"" class=""es-header-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"">
                             <tr>
                              <td align=""left"" background=""https://www.asae.com.mx/ImgASD/white.jpg"" style=""padding:0;Margin:0;padding-top:15px;padding-left:20px;padding-right:20px;background-image:url(https://www.asae.com.mx/ImgASD/white.jpg);background-repeat:no-repeat;background-position:center top"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                     <tr>
                                      <td align=""center"" class=""es-m-txt-c"" style=""padding:0;Margin:0;padding-bottom:10px;font-size:0px""><img src=""https://www.asae.com.mx/ImgASD/asaelogo.png"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" height=""100"" width=""95""></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table></td>
                         </tr>
                       </table>
                       <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                         <tr>
                          <td align=""center"" style=""padding:0;Margin:0"">
                           <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px"">
                             <tr>
                              <td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                     <tr>
                                      <td align=""center"" style=""padding:0;Margin:0""><h2 style=""Margin:0;line-height:35px;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:29px;font-style:normal;font-weight:bold;color:#333333"">Actualización de Estado de Orden de Trabajo.</h2></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table></td>
                             </tr>
                             <tr>
                              <td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                     <tr>
                                      <td align=""center"" style=""padding:0;Margin:0;font-size:0px""><img src=""https://www.asae.com.mx/ImgASD/update.jpg"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""264"" height=""264""></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table></td>
                             </tr>
                             <tr>
                              <td align=""left"" style=""padding:0;Margin:0;padding-top:15px;padding-left:20px;padding-right:20px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                     <tr>
                                      <td style=""padding:0;Margin:0;padding-top:10px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify"">Estimado/a Supervisor : <strong>{email.Persona.Nombre + " " + email.Persona.ApellidoPaterno + " " + email.Persona.ApellidoMaterno }</strong> </p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify"">Se le informa que se ha realizado un cambio importante en el estado de la orden de trabajo ubicada en el ticket <strong> { db_ticket.Ticket.Titulo } </strong> con Folio <strong> { db_ticket.Folio.Folio } </strong>. <strong>{db_bitaora.Persona.Nombre + " " + db_bitaora.Persona.ApellidoPaterno + " " + db_bitaora.Persona.ApellidoMaterno + " </strong> " + db_bitaora.Bitacora.Nota } y deseamos notificar este cambio, como usuario principal asociado con esta orden.</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify"">Por favor, ten en cuenta que el nuevo estado de la orden de trabajo ha sido actualizado y comunicado a nuestro supervisor interno. Esto es parte de nuestro proceso para mantener una comunicación transparente y eficiente en todas nuestras operaciones.</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify"">Haga clic en el botón a continuación para acceder al sitio y revisar los detalles de la orden actualizada.</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:center"">Gracias por tu atención y colaboración continua.</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:24px;color:#333333;font-size:16px;text-align:center""><br></p></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table></td>
                             </tr>
                             <tr>
                              <td align=""left"" style=""padding:0;Margin:0;padding-bottom:20px;padding-left:20px;padding-right:20px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:separate;border-spacing:0px;border-radius:5px"" role=""presentation"">
                                     <tr>
                                      <td align=""center"" style=""padding:0;Margin:0;padding-top:10px;padding-bottom:10px""><span class=""es-button-border"" style=""border-style:solid;border-color:#2CB543;background:#cc0000;border-width:0px;display:inline-block;border-radius:6px;width:auto""><a href="""" class=""es-button"" target=""_blank"" style=""mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#FFFFFF;font-size:16px;padding:10px 30px 10px 30px;display:inline-block;background:#cc0000;border-radius:6px;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-weight:bold;font-style:normal;line-height:19px;width:auto;text-align:center;mso-padding-alt:0;mso-border-alt:10px solid #cc0000;padding-left:30px;padding-right:30px"">Ir al Sitio</a></span></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table></td>
                         </tr>
                       </table>
                       <table cellpadding=""0"" cellspacing=""0"" class=""es-footer"" align=""center"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
                         <tr>
                          <td align=""center"" style=""padding:0;Margin:0"">
                           <table class=""es-footer-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"" role=""none"">
                             <tr>
                              <td align=""left"" style=""Margin:0;padding-top:20px;padding-bottom:20px;padding-left:20px;padding-right:20px;background-image:url(https://www.asae.com.mx/ImgASD/white.jpg);background-repeat:no-repeat;background-position:center top"" background=""https://www.asae.com.mx/ImgASD/white.jpg"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""left"" style=""padding:0;Margin:0;width:560px"">
                                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                     <tr>
                                      <td align=""center"" style=""padding:0;Margin:0;padding-top:15px;padding-bottom:15px;font-size:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-table-not-adapt es-social"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:40px""><a target=""_blank"" href=""https://www.facebook.com/AsaeConsultoresMX/"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px""><img title=""Facebook"" src=""https://www.asae.com.mx/ImgASD/facebook-logo-colored.png"" alt=""Fb"" width=""32"" height=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:40px""><a target=""_blank"" href=""https://twitter.com/asaeconsultores"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px""><img title=""X"" src=""https://www.asae.com.mx/ImgASD/x-logo-colored.png"" alt=""X"" width=""32"" height=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0""><a target=""_blank"" href=""https://www.linkedin.com/company/asae-consultores/"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px""><img title=""LinkedIn"" src=""https://www.asae.com.mx/ImgASD/linkedin-logo-colored.png"" alt=""In"" width=""32"" height=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                     <tr>
                                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:12px;color:#333333;font-size:8px"">Disclamer Este correo electrónico y/o el material adjunto es para uso exclusivo de la persona o entidad a la que expresamente se le ha enviado, y puede contener información confidencial o material privilegiado. Si usted no es el destinatario legítimo del mismo, por favor repórtelo inmediatamente al remitente del correo y bórrelo. Cualquier revisión, retransmisión, difusión o cualquier otro uso de este correo, por personas o entidades distintas a las del destinatario legítimo, queda expresamente prohibido. Este correo electrónico no pretende ni debe ser considerado como constitutivo de ninguna relación legal, contractual o de otra índole similar Disclamer</p></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table></td>
                         </tr>
                       </table>
                       <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                         <tr>
                          <td class=""es-info-area"" align=""center"" style=""padding:0;Margin:0"">
                           <table class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"" bgcolor=""#FFFFFF"" role=""none"">
                             <tr>
                              <td align=""left"" style=""padding:20px;Margin:0"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                     <tr>
                                      <td align=""left"" class=""es-infoblock"" style=""padding:0;Margin:0;line-height:14px;font-size:12px;color:#CCCCCC""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:14px;color:#CCCCCC;font-size:12px;text-align:center"">© ASAE CONSULTORES&nbsp;</p></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table>
                  </div>
                 </body>
                </html>

             ";

            return redult;
        }
    }
}
