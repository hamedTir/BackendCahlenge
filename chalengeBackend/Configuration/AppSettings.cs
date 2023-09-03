﻿namespace ChalengeBackend.Configuration
{
    public class AppSettings
    {
        public ConnectionStrings? ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; } = "Data Source=app.db";
    }


}
