﻿namespace Framework.Shared.Options
{
    public class AppSettings
    {
        public string Name { get; set; }

        public bool UseCustomizationData { get; set; }

        public bool ApplyDbMigrations { get; set; }
    }
}