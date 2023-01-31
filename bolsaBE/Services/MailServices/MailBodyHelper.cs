namespace bolsaBE.Services.MailServices
{
    public static class MailBodyHelper
    {
        public static string MailGenericWelcomeBody(string userName)
        {
            return @"
               <html>
                    <body>
                        <p>
                            Hola #user#,
                            <br />
                            Bienvenido a la bolsa de trabajo de la UTN-FRRO!
                            <br />
                            Ya puede acceder a la plataforma utilizando las credenciales ingresadas en el registro.
                            <br />
                            Deberá completar todos los datos obligatorios de su perfil para que luego un administrador valide su cuenta.
                            <br />
                            Atentamente,
                            <br />
                            El equipo de sistemas.
                        </p>
                    </body>
                </html>
                ".Replace("#user#", userName);
        }
        public static string MailGenericValidateBody(string userName)
        {
            return @"
               <html>
                    <body>
                        <p>
                            Hola #user#,
                            <br />
                            Buenas noticias!
                            <br />
                            Su usuario ya fue validado por un administrador.
                            <br />
                            Ahora puede acceder a todas las funcionalidades de la Bolsa de Trabajo de la UTN.
                            <br />
                            Atentamente,
                            <br />
                            El equipo de sistemas.
                        </p>
                    </body>
                </html>
                ".Replace("#user#", userName);
        }
    }
}
