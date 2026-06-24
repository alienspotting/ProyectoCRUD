namespace ProyectoPrueba2
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread] 
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmPantallaBienvenida()); /* configura el punto de entrada de la aplicación. crea la pantalla de bienvenida y
                                                           * la muestra como ventana principal.
                                                           * Cuando esa pantalla se cierra, la aplicación termina*/
        }
    }
}