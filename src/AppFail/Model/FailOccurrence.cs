﻿using System;

namespace AppFail.Model
{
    internal class FailOccurrence
    {
        internal FailOccurrence(string exceptionType, string stackTrace, string relativeUrl, string verb, string relativeReferrerUrl, string exceptionMessage, DateTime occurrenceTimeUtc, string user, string[][] postValuePairs, string [][] queryValuePairs)
        {
            ExceptionType = exceptionType;
            StackTrace = stackTrace;
            HttpVerb = verb;
            RelativeReferrerUrl = relativeReferrerUrl;
            ExceptionMessage = exceptionMessage;
            RelativeUrl = relativeUrl;
            ApplicationType = "ASP.NET";
            OccurrenceTimeUtc = occurrenceTimeUtc.ToString("MM/dd/yyyy HH:mm:ss.FFFF");
            User = user;
            PostValuePairs = postValuePairs;
            QueryValuePairs = queryValuePairs;
        }

        public string RelativeUrl { get; private set; }
        public string ExceptionType { get; private set; }
        public string StackTrace { get; private set; }
        public string HttpVerb { get; private set; }
        public string RelativeReferrerUrl { get; private set; }
        public string ExceptionMessage { get; private set; }
        public string ApplicationType { get; private set; }
        public string OccurrenceTimeUtc { get; private set; }
        public string User { get; private set; }
        public string[][] PostValuePairs { get; private set; }
        public string[][] QueryValuePairs { get; private set; }
    }
}
