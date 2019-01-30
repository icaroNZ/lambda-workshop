using System;
using System.Collections.Generic;

namespace HelloLambda
{
    public static class MonitoringClient
    {
        private const string Prefix = "HelloLambda";

        public static void Increment(string metricName, string tags = "")
        {
            var epoch = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000;

            Console.WriteLine($"MONITORING|{epoch}|1|count|{Prefix}{metricName}|#{tags}");
        }
    }
}
