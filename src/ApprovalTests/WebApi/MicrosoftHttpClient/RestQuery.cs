﻿using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.WebApi.MicrosoftHttpClient
{
    public abstract class RestQuery<T> : IExecutableQuery, ILoader<T>
    {
        public abstract string GetQuery();

        public abstract string GetBaseAddress();

        public string ExecuteQuery(string query)
        {
            var json = ExecuteAsync(query).Result.Result;
            return json.FormatJson();
        }

        public Task<DownloadStringCompletedEventArgs> ExecuteAsync(string requestUri)
        {
            var uri = new Uri(new Uri(GetBaseAddress()), requestUri);
            try
            {
                using var client = new WebClient
                {
                    Encoding = Encoding.UTF8
                };
                var task = new TaskCompletionSource<DownloadStringCompletedEventArgs>();
                client.DownloadStringCompleted += (sender, args) => { task.SetResult(args); };
                client.DownloadStringAsync(uri);
                return task.Task;
            }
            catch (Exception e)
            {
                throw new Exception($"The following error occured while connecting to:\n{uri}\nError:\n{e.Message}", e);
            }
        }

        public Task<DownloadStringCompletedEventArgs> GetResponse()
        {
            return ExecuteAsync(GetQuery());
        }

        public abstract T Load();
    }
}