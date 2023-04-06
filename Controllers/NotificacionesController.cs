using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using ms_notificaciones.Models;
namespace ms_notificaciones.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificacionesController : ControllerBase
{
    [Route("correo")]
    [HttpPost]
    public async Task<ActionResult> EnviarCorreo(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("valentina.acosta30784@ucaldas.edu.co", "Valentina Acosta Lopez");
        var subject = datos.asuntoCorreo;
        var to = new EmailAddress(datos.correoDestino, datos.nombreDestino);
        var plainTextContent = "plain text content";
        var htmlContent = datos.contenidoCorreo;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted){
            return Ok("Correo enviado a la direccion " + datos.correoDestino);
        }else{
            return BadRequest("Error enciando mensaje a la direccion "+ datos.correoDestino); 
        }

    }
   
}
