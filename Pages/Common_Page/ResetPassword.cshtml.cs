using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_02.Models;
using System.Net;
using System.Net.Mail;

namespace PRN211_Project.Pages.Common_Page
{
    public class ResetPasswordModel : PageModel
    {
        public User user { get; set; }
        public IActionResult OnPost(IFormCollection form)
        {
            string emailTo = form["email"];
            user = PRN221_Project_02Context.instance.Users.SingleOrDefault(x => x.Email.Equals(emailTo));
            if (user != null)
            {
                string code = GenerateCode();
                user.CodePwd = code;
                PRN221_Project_02Context.instance.Users.Update(user);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            var verifyLink = "https://localhost:7132/Common_Page/ChangeNewPassword?email=" + user.Email;
            sendEmail("kieuphuonganh1112@gmail.com", verifyLink);
            return RedirectToPage("/Common_Page/ChangeNewPassword");
        }
        public void sendEmail(string emailTo, string verifyLink)
        {
            try
            {
                String content = "<!DOCTYPE html>"
                    + "<html xmlns=\"http://www.w3.org/1999/xhtml\">"
                    + "<head>"
                    + "  <title></title>"
                    + "  <!--[if !mso]><!-- -->"
                    + "  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">"
                    + "  <!--<![endif]-->"
                    + "  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">"
                    + "  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
                    + "  <style type=\"text/css\">"
                    + "    #outlook a {"
                    + "      padding: 0;"
                    + "    }"
                    + "    .ReadMsgBody {"
                    + "      width: 100%;"
                    + "    }"
                    + "    .ExternalClass {"
                    + "      width: 100%;"
                    + "    }"
                    + "    .ExternalClass * {"
                    + "      line-height: 100%;"
                    + "    }"
                    + "    body {"
                    + "      margin: 0;"
                    + "      padding: 0;"
                    + "      -webkit-text-size-adjust: 100%;"
                    + "      -ms-text-size-adjust: 100%;"
                    + "    }"
                    + "    table,"
                    + "    td {"
                    + "      border-collapse: collapse;"
                    + "      mso-table-lspace: 0pt;"
                    + "      mso-table-rspace: 0pt;"
                    + "    }"
                    + "  </style>"
                    + "  <!--[if !mso]><!-->"
                    + "  <style type=\"text/css\">"
                    + "    @media only screen and (max-width:480px) {"
                    + "      @-ms-viewport {"
                    + "        width: 320px;"
                    + "      }"
                    + "      @viewport {"
                    + "        width: 320px;"
                    + "      }"
                    + "    }"
                    + "  </style>"
                    + "  <!--<![endif]-->"
                    + "  <!--[if mso]><xml>  <o:OfficeDocumentSettings>    <o:AllowPNG/>    <o:PixelsPerInch>96</o:PixelsPerInch>  </o:OfficeDocumentSettings></xml><![endif]-->"
                    + "  <!--[if lte mso 11]><style type=\"text/css\">  .outlook-group-fix {    width:100% !important;  }</style><![endif]-->"
                    + "  <!--[if !mso]><!-->"
                    + "  <link href=\"https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;600&display=swap\" rel=\"stylesheet\" type=\"text/css\">"
                    + "  <style type=\"text/css\">"
                    + "    @import url('https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;600&display=swap');"
                    + "  </style>"
                    + "  <!--<![endif]-->"
                    + "  <style type=\"text/css\">"
                    + "    @media only screen and (max-width:595px) {"
                    + "      .container {"
                    + "        width: 100% !important;"
                    + "      }"
                    + "      .button {"
                    + "        display: block !important;"
                    + "        width: auto !important;"
                    + "      }"
                    + "    }"
                    + "  </style>"
                    + "</head>"
                    + "<body style=\"font-family: 'Inter', sans-serif; background: #E5E5E5;\">"
                    + "  <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"center\" bgcolor=\"#F6FAFB\">"
                    + "    <tbody>"
                    + "      <tr>"
                    + "        <td valign=\"top\" align=\"center\">"
                    + "          <table class=\"container\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">"
                    + "            <tbody>"
                    + "              <tr>"
                    + "                <td style=\"padding:48px 0 30px 0; text-align: center; font-size: 24px; color: #4C83EE;font-weight:bold \">"
                    + "                  .DOT Moblie"
                    + "                </td>"
                    + "              </tr>"
                    + "              <tr>"
                    + "                <td class=\"main-content\" style=\"padding: 48px 30px 40px; color: #000000;\" bgcolor=\"#ffffff\">"
                    + "                  <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">"
                    + "                    <tbody style=\"padding: 40px 0\">"
                    + "                      <tr>"
                    + "                        <td style=\"padding: 0 0 24px 0; font-size: 20px; line-height: 150%; font-weight: bold; color: #000000; letter-spacing: 0.01em;text-align:center\">"
                    + "                          Thank you for siging in                        </td>"
                    + "                      </tr>"
                    + "                      <tr>"
                    + "                        <td style=\"padding: 0 0 10px 0; font-size: 24px; line-height: 150%; font-weight: 400; color: #000000; letter-spacing: 0.01em; text-align:center\">"
                    + "                          Verify your email address"
                    + "                        </td>"
                    + "                      </tr>"
                    + "                      <tr>"
                    + "                        <td style=\"padding: 0 0 16px 0; font-size: 14px; line-height: 150%; font-weight: 400; color: #000000; letter-spacing: 0.01em;\">"
                    + "                          "
                    + "                        </td>"
                    + "                      </tr>"
                    + "                      <tr>"
                    + "                        <td style=\"padding: 0 0 24px 0;text-align:center;\">"
                    + "                          <a class=\"button\" href=\"" + verifyLink + "\" title=\"Reset Password\" style=\"width: 50%; background: #4C83EE; text-decoration: none; display: inline-block; padding: 10px 0; color: #fff; font-size: 14px; line-height: 21px; text-align: center; font-weight: bold; border-radius: 7px;\">Verify Your Account</a>"
                    + "                        </td>"
                    + "                      </tr>"
                    + "                      <tr>"
                    + "                        <td style=\"padding: 0 0 16px 0; font-size: 14px; line-height: 150%; font-weight: 400;letter-spacing: 0.01em;\">"
                    + "                         "
                    + "                        </td>"
                    + "                      </tr>"
                    + "                      <tr>"
                    + "                        <td style=\"padding: 0 0 8px;\">"
                    + "                          <a style=\"display: flex; justify-content: space-between; align-items: center; padding: 28px 24px; border-radius: 4px; text-decoration: none;\" href=\"{{get_started_link}}\">"
                    + "<span style=\"width: 90%; font-size: 14px; line-height: 150%; font-weight: bold; letter-spacing: 0.01em;\"></span>"
                    + "                            <span style=\"width: 10%; float: right;\">"
                    + "                            </span>"
                    + "                          </a>"
                    + "                        </td>"
                    + "                      </tr>"
                    + "                      <tr>"
                    + "                        <td style=\"padding: 0 0 60px;\">"
                    + "                          <a style=\"display: flex; padding: 28px 24px; border-radius: 4px;  text-decoration: none;\" href=\"{{onboarding_video_link}}\">"
                    + "                            <span style=\"width: 90%; font-size: 14px; line-height: 150%; font-weight: bold; letter-spacing: 0.01em;\"></span>"
                    + "                            <span style=\"width: 10%; float: right;\">"
                    + "                            </span>"
                    + "                          </a>"
                    + "                        </td>"
                    + "                      </tr>"
                    + "                      <tr>"
                    + "                      <tr>"
                    + "                        <td style=\"padding: 0 0 16px;\">"
                    + "                        </td>"
                    + "                      </tr>"
                    + "                      <tr>"
                    + "                        <td style=\"font-size: 14px; line-height: 170%; font-weight: 400; letter-spacing: 0.01em;\">"
                    + "                         "
                    + "                        </td>"
                    + "                      </tr>"
                    + "                    </tbody>"
                    + "                  </table>"
                    + "                </td>"
                    + "              </tr>"
                    + "              <tr>"
                    + "                <td style=\"padding: 24px 0 48px; font-size: 0px;\">"
                    + "                  <!--[if mso | IE]>      <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">        <tr>          <td style=\"vertical-align:top;width:300px;\">      <![endif]-->"
                    + "                  <div class=\"outlook-group-fix\" style=\"padding: 0 0 20px 0; vertical-align: top; display: inline-block; text-align: center; width:100%;\">"
                    + "                    <span style=\"padding: 0; font-size: 11px; line-height: 15px; font-weight: normal;\">"
                    + "                    </div>"
                    + "                  </div>"
                    + "                  <!--[if mso | IE]>      </td></tr></table>      <![endif]-->"
                    + "                </td>"
                    + "              </tr>"
                    + "            </tbody>"
                    + "          </table>"
                    + "        </td>"
                    + "      </tr>"
                    + "    </tbody>"
                    + "  </table>"
                    + "</body>"
                    + "</html>";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("mobiledot1002@gmail.com");
                mailMessage.To.Add(emailTo);
                mailMessage.Subject = "Reset Password";
                mailMessage.Body = content;
                mailMessage.IsBodyHtml = true; // Enable HTML rendering

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("mobiledot1002@gmail.com", "edgt qkpj pvjk iibi"),
                    EnableSsl = true
                };

                client.Send(mailMessage);
                Console.WriteLine("Email Sent Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public string GenerateCode()
        {
            Random random = new Random();
            return random.Next(100000, 1000000).ToString();
        }
    }
}
