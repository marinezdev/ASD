using System;
using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Mailing.Formato
{
    public class PasswordRequestResset
    {
		public string HTMLPasswordRequestResset(PasswordResetRequestsViewModel R)
		{
			string Result = $@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
            <html dir=""ltr"" xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" lang=""und"">
             <head>
              <meta charset=""UTF-8"">
              <meta content=""width=device-width, initial-scale=1"" name=""viewport"">
              <meta name=""x-apple-disable-message-reformatting"">
              <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
              <meta content=""telephone=no"" name=""format-detection"">
              <title>Password</title><!--[if (mso 16)]>
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
            #outlook a {{
	            padding:0;
            }}
            .es-button {{
	            mso-style-priority:100!important;
	            text-decoration:none!important;
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
            [data-ogsb] .es-button.es-button-1712274372025 {{
	            padding:10px!important;
            }}
            @media only screen and (max-width:600px) {{p, ul li, ol li, a {{ line-height:150%!important }} h1, h2, h3, h1 a, h2 a, h3 a {{ line-height:120%!important }} h1 {{ font-size:36px!important; text-align:left }} h2 {{ font-size:26px!important; text-align:left }} h3 {{ font-size:20px!important; text-align:left }} .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {{ font-size:36px!important; text-align:left }} .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {{ font-size:26px!important; text-align:left }} .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {{ font-size:20px!important; text-align:left }} .es-menu td a {{ font-size:12px!important }} .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {{ font-size:14px!important }} .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a {{ font-size:14px!important }} .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {{ font-size:14px!important }} .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {{ font-size:12px!important }} *[class=""gmail-fix""] {{ display:none!important }} .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {{ text-align:center!important }} .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {{ text-align:right!important }} .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {{ text-align:left!important }} .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {{ display:inline!important }} .es-button-border {{ display:inline-block!important }} a.es-button, button.es-button {{ font-size:20px!important; display:inline-block!important }} .es-adaptive table, .es-left, .es-right {{ width:100%!important }} .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {{ width:100%!important; max-width:600px!important }} .es-adapt-td {{ display:block!important; width:100%!important }} .adapt-img {{ width:100%!important; height:auto!important }} .es-m-p0 {{ padding:0!important }} .es-m-p0r {{ padding-right:0!important }} .es-m-p0l {{ padding-left:0!important }} .es-m-p0t {{ padding-top:0!important }} .es-m-p0b {{ padding-bottom:0!important }} .es-m-p20b {{ padding-bottom:20px!important }} .es-mobile-hidden, .es-hidden {{ display:none!important }} tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {{ width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important }} tr.es-desk-hidden {{ display:table-row!important }} table.es-desk-hidden {{ display:table!important }} td.es-desk-menu-hidden {{ display:table-cell!important }} .es-menu td {{ width:1%!important }} table.es-table-not-adapt, .esd-block-html table {{ width:auto!important }} table.es-social {{ display:inline-block!important }} table.es-social td {{ display:inline-block!important }} .es-m-p5 {{ padding:5px!important }} .es-m-p5t {{ padding-top:5px!important }} .es-m-p5b {{ padding-bottom:5px!important }} .es-m-p5r {{ padding-right:5px!important }} .es-m-p5l {{ padding-left:5px!important }} .es-m-p10 {{ padding:10px!important }} .es-m-p10t {{ padding-top:10px!important }} .es-m-p10b {{ padding-bottom:10px!important }} .es-m-p10r {{ padding-right:10px!important }} .es-m-p10l {{ padding-left:10px!important }} .es-m-p15 {{ padding:15px!important }} .es-m-p15t {{ padding-top:15px!important }} .es-m-p15b {{ padding-bottom:15px!important }} .es-m-p15r {{ padding-right:15px!important }} .es-m-p15l {{ padding-left:15px!important }} .es-m-p20 {{ padding:20px!important }} .es-m-p20t {{ padding-top:20px!important }} .es-m-p20r {{ padding-right:20px!important }} .es-m-p20l {{ padding-left:20px!important }} .es-m-p25 {{ padding:25px!important }} .es-m-p25t {{ padding-top:25px!important }} .es-m-p25b {{ padding-bottom:25px!important }} .es-m-p25r {{ padding-right:25px!important }} .es-m-p25l {{ padding-left:25px!important }} .es-m-p30 {{ padding:30px!important }} .es-m-p30t {{ padding-top:30px!important }} .es-m-p30b {{ padding-bottom:30px!important }} .es-m-p30r {{ padding-right:30px!important }} .es-m-p30l {{ padding-left:30px!important }} .es-m-p35 {{ padding:35px!important }} .es-m-p35t {{ padding-top:35px!important }} .es-m-p35b {{ padding-bottom:35px!important }} .es-m-p35r {{ padding-right:35px!important }} .es-m-p35l {{ padding-left:35px!important }} .es-m-p40 {{ padding:40px!important }} .es-m-p40t {{ padding-top:40px!important }} .es-m-p40b {{ padding-bottom:40px!important }} .es-m-p40r {{ padding-right:40px!important }} .es-m-p40l {{ padding-left:40px!important }} .es-desk-hidden {{ display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important }} .h-auto {{ height:auto!important }} }}
            @media screen and (max-width:384px) {{.mail-message-content {{ width:414px!important }} }}
            </style>
             </head>
             <body style=""width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0"">
              <div dir=""ltr"" class=""es-wrapper-color"" lang=""und"" style=""background-color:#FAFAFA""><!--[if gte mso 9]>
			            <v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t"">
				            <v:fill type=""tile"" color=""#fafafa""></v:fill>
			            </v:background>
		            <![endif]-->
               <table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#FAFAFA"">
                 <tr>
                  <td valign=""top"" style=""padding:0;Margin:0"">
                   <table cellpadding=""0"" cellspacing=""0"" class=""es-header"" align=""center"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
                     <tr>
                      <td align=""center"" bgcolor=""#efefef"" style=""padding:0;Margin:0;background-color:#efefef"">
                       <table class=""es-header-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"" role=""none"">
                         <tr>
                          <td class=""es-m-p15"" align=""left"" bgcolor=""#ffffff"" style=""padding:20px;Margin:0;background-color:#ffffff;background-image:url(https://eeqxsjo.stripocdn.email/content/guids/CABINET_842ce9e62b282f8d21335414a3229424c7c776ec080d0c53971ef3a97f157d33/images/oooo.jpg);background-repeat:repeat;background-position:center top"" background=""https://eeqxsjo.stripocdn.email/content/guids/CABINET_842ce9e62b282f8d21335414a3229424c7c776ec080d0c53971ef3a97f157d33/images/oooo.jpg"">
                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                             <tr>
                              <td class=""es-m-p0r"" valign=""top"" align=""center"" style=""padding:0;Margin:0;width:560px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" class=""es-m-p0"" style=""padding:0;Margin:0;padding-bottom:10px;font-size:0px""><img src=""https://eeqxsjo.stripocdn.email/content/guids/CABINET_842ce9e62b282f8d21335414a3229424c7c776ec080d0c53971ef3a97f157d33/images/logoasaetransformed.png"" alt=""Logo"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic;font-size:12px"" width=""120"" title=""Logo""></td>
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
                      <td align=""center"" bgcolor=""#efefef"" style=""padding:0;Margin:0;background-color:#efefef"">
                       <table class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"" role=""none"">
                         <tr>
                          <td class=""es-m-p20t es-m-p20b"" align=""left"" bgcolor=""#ffffff"" style=""padding:0;Margin:0;padding-top:15px;padding-left:20px;padding-right:20px;background-color:#ffffff"">
                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                             <tr>
                              <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" bgcolor=""#ffffff"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#ffffff"" role=""presentation"">
                                <!--[if !mso]><!-- -->
                                 <tr class=""es-desk-hidden"" style=""display:none;float:left;overflow:hidden;width:0;max-height:0;line-height:0;mso-hide:all"">
                                  <td align=""center"" class=""h-auto es-m-p5"" valign=""middle"" height=""38"" style=""padding:0;Margin:0""><h5 style=""Margin:0;line-height:22px;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:18px""><strong>Recuperación de contraseña</strong></h5></td>
                                 </tr><!--<![endif]-->
                                 <tr class=""es-mobile-hidden"">
                                  <td align=""center"" class=""es-m-p20t es-m-p20b es-m-p15r es-m-p15l es-m-txt-c"" style=""padding:0;Margin:0;padding-top:10px;padding-bottom:10px;font-size:0px""><img src=""https://eeqxsjo.stripocdn.email/content/guids/CABINET_842ce9e62b282f8d21335414a3229424c7c776ec080d0c53971ef3a97f157d33/images/pass.png"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""195""></td>
                                 </tr><!--[if !mso]><!-- -->
                                 <tr class=""es-desk-hidden"" style=""display:none;float:left;overflow:hidden;width:0;max-height:0;line-height:0;mso-hide:all"">
                                  <td align=""center"" class=""es-m-txt-c es-m-p20t es-m-p20b es-m-p15r es-m-p15l"" style=""padding:10px;Margin:0;font-size:0px""><img src=""https://eeqxsjo.stripocdn.email/content/guids/CABINET_842ce9e62b282f8d21335414a3229424c7c776ec080d0c53971ef3a97f157d33/images/pass.png"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""140""></td>
                                 </tr><!--<![endif]-->
                                 <tr class=""es-mobile-hidden"">
                                  <td align=""center"" class=""es-m-txt-c es-m-p5"" style=""Margin:0;padding-top:15px;padding-bottom:15px;padding-left:40px;padding-right:40px""><h1 style=""Margin:0;line-height:36px;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:30px;font-style:normal;font-weight:bold;color:#333333"">Recuperación de contraseña</h1></td>
                                 </tr>
                                 <tr>
                                  <td style=""padding:10px;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify"">Hola, Estimado <strong>{R.Usuario.Usuario}</strong>,</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify"">Se solicitó un restablecimiento de contraseña para tu cuenta <u><font color=""#0000ff""><strong>{R.Email.Email}</strong></font></u>, haz clic en el botón que aparece a continuación para cambiar tu contraseña.</p></td>
                                 </tr>
                                 <tr class=""es-mobile-hidden"">
                                  <td align=""center"" class=""es-m-p25"" style=""padding:0;Margin:0;padding-top:10px;padding-bottom:10px""><span class=""es-button-border"" style=""border-style:solid;border-color:#2cb543;background:#e69138;border-width:0px;display:inline-block;border-radius:6px;width:auto""><a href=""{R.Direccion}/Administracion/PasswordResetRequests/ActualizarPass?cont={R.Token}"" class=""es-button"" target=""_blank"" style=""mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#FFFFFF;font-size:13px;padding:10px 30px 10px 30px;display:inline-block;background:#e69138;border-radius:6px;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-weight:normal;font-style:normal;line-height:16px;width:auto;text-align:center;mso-padding-alt:0;mso-border-alt:10px solid #e69138;padding-left:30px;padding-right:30px"">RESTAURAR CONTRASEÑA</a></span></td>
                                 </tr><!--[if !mso]><!-- -->
                                 <tr class=""es-desk-hidden"" style=""display:none;float:left;overflow:hidden;width:0;max-height:0;line-height:0;mso-hide:all"">
                                  <td align=""center"" class=""es-m-txt-c h-auto es-m-p10"" valign=""middle"" height=""50"" style=""padding:0;Margin:0""><span class=""es-button-border"" style=""border-style:solid;border-color:#2CB543;background:#e69138;border-width:0px;display:inline-block;border-radius:6px;width:auto""><a href=""www.google.com"" class=""es-button es-button-1712274372025"" target=""_blank"" style=""mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#FFFFFF;font-size:8px;padding:10px;display:inline-block;background:#e69138;border-radius:6px;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-weight:normal;font-style:normal;line-height:10px;width:auto;text-align:center;mso-padding-alt:0;mso-border-alt:10px solid #e69138"">Restaurar contraseña</a></span></td>
                                 </tr><!--<![endif]-->
                                 <tr>
                                  <td style=""padding:10px;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify"">Si tu no realizaste la solicitud de cambio de contraseña, solo ignora este mensaje.&nbsp;</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify"">Este enlace solo es válido dentro de los proximos 60 minutos.</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:center"">Atentamente,&nbsp;</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:center"">ASAE CONSULTORES</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px;text-align:justify""></p></td>
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
                      <td align=""center"" bgcolor=""#efefef"" style=""padding:0;Margin:0;background-color:#efefef"">
                       <table class=""es-footer-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"" role=""none"">
                         <tr>
                          <td align=""left"" bgcolor=""#ffffff"" style=""padding:0;Margin:0;background-color:#ffffff;background-image:url(https://eeqxsjo.stripocdn.email/content/guids/CABINET_842ce9e62b282f8d21335414a3229424c7c776ec080d0c53971ef3a97f157d33/images/mm.jpg);background-repeat:repeat;background-position:left top"" background=""https://eeqxsjo.stripocdn.email/content/guids/CABINET_842ce9e62b282f8d21335414a3229424c7c776ec080d0c53971ef3a97f157d33/images/mm.jpg"">
                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                             <tr>
                              <td align=""left"" style=""padding:0;Margin:0;width:600px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0;padding-top:15px;padding-bottom:15px;font-size:0"">
                                   <table cellpadding=""0"" cellspacing=""0"" class=""es-table-not-adapt es-social"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                     <tr>
                                      <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:40px""><a target=""_blank"" href=""https://www.facebook.com/AsaeConsultoresMX?mibextid=ZbWKwL"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px""><img title=""Facebook"" src=""https://eeqxsjo.stripocdn.email/content/assets/img/social-icons/circle-colored/facebook-circle-colored.png"" alt=""Fb"" width=""24"" height=""24"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                                      <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:40px""><a target=""_blank"" href=""https://twitter.com/asaeconsultores"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px""><img title=""X"" src=""https://eeqxsjo.stripocdn.email/content/assets/img/social-icons/circle-colored/x-circle-colored.png"" alt=""X"" width=""24"" height=""24"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                                      <td align=""center"" valign=""top"" style=""padding:0;Margin:0""><a target=""_blank"" href=""https://www.linkedin.com/company/asae-consultores/"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px""><img title=""LinkedIn"" src=""https://eeqxsjo.stripocdn.email/content/assets/img/social-icons/circle-colored/linkedin-circle-colored.png"" alt=""In"" width=""24"" height=""24"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                                 <tr class=""es-mobile-hidden"">
                                  <td align=""center"" class=""es-m-p5t es-m-p10b es-m-txt-c"" style=""padding:0;Margin:0;padding-left:10px;padding-right:10px;padding-bottom:15px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:10px;color:#ffffff;font-size:8px"">Disclamer Este correo electrónico y/o el material adjunto es para uso exclusivo de la persona o entidad a la que expresamente se le ha enviado, y puede contener información confidencial o material privilegiado. Si usted no es el destinatario legítimo del mismo, por favor repórtelo inmediatamente al remitente del correo y bórrelo. Cualquier revisión, retransmisión, difusión o cualquier otro uso de este correo, por personas o entidades distintas a las del destinatario legítimo, queda expresamente prohibido. Este correo electrónico no pretende ni debe ser considerado como constitutivo de ninguna relación legal, contractual o de otra índole similar&nbsp;Disclamer</p></td>
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
                      <td class=""es-info-area"" align=""center"" bgcolor=""#efefef"" style=""padding:0;Margin:0;background-color:#efefef"">
                       <table class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#ffffff;width:600px"" bgcolor=""#ffffff"" role=""none"">
                         <tr>
                          <td align=""left"" style=""padding:20px;Margin:0"">
                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                             <tr>
                              <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" class=""es-infoblock"" style=""padding:0;Margin:0;line-height:14px;font-size:12px;color:#CCCCCC""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:14px;color:#CCCCCC;font-size:12px""><a target=""_blank"" href="""" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px""></a>© ASAE CONSULTORES<a target=""_blank"" href="""" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px""></a></p></td>
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
            </html>";
            return Result;
        }

    }
}

