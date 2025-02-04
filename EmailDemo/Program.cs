

using System;
using System.Net.Mail;
using System.Text;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
namespace EmailDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //Port = 25,

                //sending to a local folder directory
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = @"C:\Temp"
            });

            StringBuilder template = new StringBuilder();
            template.AppendLine("Hello @Model.FirstName");
            template.AppendLine("<p> Thanks for your purchase @Model.ProductName.</p>");
            template.AppendLine("- Team tjtechy");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            // Create a new email to send
            var email = await Email.From("taju@taju.com")
                .To("test@test.com", "Test User")
                .Subject("Hello, Test User")
                .UsingTemplate(template.ToString(), new { FirstName = "Test", ProductName = "Test Product" })

                //.Body("This is a test email from FluentEmail")
                .SendAsync();
            email.Successful.Equals(true);
        }
    }
}

/***
 * The above code sends an email to the specified email address.
 * The template format is Razor template. But not working for now
 */
