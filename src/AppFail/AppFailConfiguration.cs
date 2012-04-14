﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AppFail.Filtering;

namespace AppFail
{
    public class AppFailConfiguration : ConfigurationSection
    {
        public static AppFailConfiguration Current
        {
            get
            {
                return (AppFailConfiguration)ConfigurationManager.GetSection("appFail");
            }
        }

        [ConfigurationProperty("apiToken", IsRequired = true)]
        public string ApiToken
        {
            get { return (string)this["apiToken"]; }
        }

        [ConfigurationProperty("reportingMinimumBatchSize", DefaultValue = 10)]
        public int ReportingMinimumBatchSize
        {
            get { return (int)this["reportingMinimumBatchSize"]; }
        }

        [ConfigurationProperty("reportingOccurrenceMaxSizeBytes", DefaultValue = (long)102400)] // 100KB per occurrence
        public long ReportingOccurrenceMaxSizeBytes
        {
            get { return (long)this["reportingOccurrenceMaxSizeBytes"]; }
        }

        [ConfigurationProperty("reportingMaximumIntervalMinutes", DefaultValue = 1)]
        public int ReportingMaximumIntervalMinutes
        {
            get { return (int)this["reportingMaximumIntervalMinutes"]; }
        }

        [ConfigurationProperty("baseApiUrl", DefaultValue = "http://api.appfail.net/")]
        public string BaseApiUrl
        {
            get { return (string)this["baseApiUrl"]; }
        }

        [ConfigurationProperty("reportCurrentUsername", DefaultValue = true)]
        public bool ReportCurrentUsername
        {
            get { return (bool)this["reportCurrentUsername"]; }
        }

        /// <summary>
        /// An example of what could go into web.config
        ///  <appFail apiToken="0ece6d98-7769-xxx" baseApiUrl="http://mysite.com/" >
        ///   <ignore>
        ///         <add type="HttpExceptionType" value="NotImplemented" />
        ///         <add type="HttpStatusCode" value="404" />
        ///         <add type="ExceptionMessage" value="^favicon" />
        ///   </ignore>
        /// </appFail>
        /// </summary>
        [ConfigurationProperty("ignore", IsRequired = false, IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConfigurationElement))]
        public ReferencedConfigurationElementCollection<IgnoreElement> Ignore
        {
            get { return (ReferencedConfigurationElementCollection<IgnoreElement>)this["ignore"]; }
        }
    }
}