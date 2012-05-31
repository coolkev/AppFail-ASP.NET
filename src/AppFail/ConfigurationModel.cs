﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Appfail.Reporting.Filtering;

namespace Appfail.Reporting
{
    internal class ConfigurationModel
    {
        private ConfigurationModel()
        {
            FilteredExceptionsByType = new List<Type>();
            FilteredExceptionsByRegex = new List<Regex>();
            FilteredExceptionsByLambda = new List<Func<Exception, bool>>();
            FilteredExceptionsByHttpStatusCode = new List<HttpStatusCode>();
            FilteredExceptionByRelativeUrlsContaining = new List<string>();
            FilteredExceptionByRelativeUrlsStartingWith = new List<string>();
            FilteredPostNamesContaining = new List<string>();
        }

        static ConfigurationModel()
        {
            Instance = new ConfigurationModel();
        }

        public static ConfigurationModel Instance { get; internal set; }

        public Func<string> UserPopulationCallback { get; set; }

        private int? _reportingMinimumBatchSize;
        public int ReportingMinimumBatchSize
        {
            get { return _reportingMinimumBatchSize ?? AppfailConfiguration.Current.ReportingMinimumBatchSize; }
            set { _reportingMinimumBatchSize = value; }
        }

        private TimeSpan? _reportingMaximumInterval;
        public TimeSpan ReportingMaximumInterval
        {
            get { return _reportingMaximumInterval ?? TimeSpan.FromMinutes(AppfailConfiguration.Current.ReportingMaximumIntervalMinutes); }
            set { _reportingMaximumInterval = value; }
        }

        public Func<string> PopulateUsernameFrom { get; set; }

        private string _apiToken;
        public string ApiToken
        {
            get { return _apiToken ?? AppfailConfiguration.Current.ApiToken; }
            set { _apiToken = value; }
        }

        private string _baseApiUrl;
        public string BaseApiUrl
        {
            get { return EnforceTrailingSlash(_baseApiUrl ?? AppfailConfiguration.Current.BaseApiUrl); }
            set { _baseApiUrl = value; }
        }

        private bool? _reportCurrentUsername;
        public bool ReportCurrentUsername
        {
            get { return _reportCurrentUsername ?? AppfailConfiguration.Current.ReportCurrentUsername; }
            set { _reportCurrentUsername = value; }
        }

        private long? _reportingOccurrenceMaxSizeBytes;
        public long ReportingOccurrenceMaxSizeBytes
        {
            get { return _reportingOccurrenceMaxSizeBytes ?? AppfailConfiguration.Current.ReportingOccurrenceMaxSizeBytes; }
            set { _reportingOccurrenceMaxSizeBytes = value; }
        }

        public int ReportingSubmissionAttempts
        {
            get { return 3; }
        }

        private string EnforceTrailingSlash(string url)
        {
            return url.Trim().EndsWith("/") ? url : url.Trim() + "/";
        }

        private List<Type> _filteredExceptionsByType;
        public List<Type> FilteredExceptionsByType
        {
            get { return _filteredExceptionsByType; }
            set { _filteredExceptionsByType = value; }
        }

        private List<Regex> _filteredExceptionsByRegex;
        public List<Regex> FilteredExceptionsByRegex
        {
            get { return _filteredExceptionsByRegex; }
            set { _filteredExceptionsByRegex = value; }
        }

        private List<Func<Exception, bool>> _filteredExceptionsByLambda;
        public List<Func<Exception, bool>> FilteredExceptionsByLambda
        {
            get { return _filteredExceptionsByLambda; }
            set { _filteredExceptionsByLambda = value; }
        }

        private List<HttpStatusCode> _filteredExceptionsByHttpStatusCode;
        public List<HttpStatusCode> FilteredExceptionsByHttpStatusCode
        {
            get { return _filteredExceptionsByHttpStatusCode; }
            set { _filteredExceptionsByHttpStatusCode = value; }
        }

        private List<string> _filteredExceptionByRelativeUrlsStartingWith;
        public List<string> FilteredExceptionByRelativeUrlsStartingWith
        {
            get { return _filteredExceptionByRelativeUrlsStartingWith; }
            set { _filteredExceptionByRelativeUrlsStartingWith = value; }
        }

        private List<string> _filteredExceptionByUrlsContaining;
        public List<string> FilteredExceptionByRelativeUrlsContaining
        {
            get { return _filteredExceptionByUrlsContaining; }
            set { _filteredExceptionByUrlsContaining = value; }
        }

        private List<string> _filteredPostNamesContaining;
        public List<string> FilteredPostNamesContaining
        {
            get { return _filteredPostNamesContaining; }
            set { _filteredPostNamesContaining = value; }
        }

        public ReferencedConfigurationElementCollection<IgnoreExceptionElement> IgnoreExceptionSettingsFromWebConfig
        {
            get { return AppfailConfiguration.Current.IgnoreExceptions; }
        }

        public ReferencedConfigurationElementCollection<IgnorePostValueElement> IgnorePostValuesSettingsFromWebConfig
        {
            get { return AppfailConfiguration.Current.IgnorePostValues; }
        }
    }
}
