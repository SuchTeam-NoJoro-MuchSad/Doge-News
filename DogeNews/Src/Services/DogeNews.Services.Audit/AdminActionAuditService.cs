﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Services.Audit.Contracts;
using DogeNews.Services.Http.Contracts;
using Newtonsoft.Json;

using Ninject.Extensions.Interception;

namespace DogeNews.Services.Audit
{
    public class AdminActionAuditService : IAdminActionAuditService
    {
        private readonly IProjectableRepository<User> userRepository;
        private readonly IProjectableRepository<AdminActionLog> adminActionLogRepository;
        private readonly IHttpContextService httpContextService;
        private readonly INewsData newsData;

        public AdminActionAuditService(IProjectableRepository<User> userRepository,
            IProjectableRepository<AdminActionLog> adminActionLogRepository,
            IHttpContextService httpContextService,
            INewsData newsData)
        {
            this.userRepository = userRepository;
            this.adminActionLogRepository = adminActionLogRepository;
            this.httpContextService = httpContextService;
            this.newsData = newsData;
        }

        public void LogAdminActionToDatabase(IInvocation invocation)
        {
            string username = this.httpContextService.GetLoggedInUserUsername();

            User foundUser = this.userRepository.GetFirst(x => x.UserName == username);

            var bulder = new StringBuilder();
            var mappedParameters = MapParameters(invocation.Request.Arguments, invocation.Request.Method.GetParameters())
            .ToDictionary(x => x.Key, x => x.Value);

            foreach (var argument in mappedParameters)
            {
                bulder.AppendLine($"{argument.Key} : {JsonConvert.SerializeObject(argument.Value)}");
            }

            AdminActionLog log = new AdminActionLog
            {
                User = foundUser,
                InvokedMethodName = invocation.Request.Method.DeclaringType?.FullName + "." + invocation.Request.Method.Name,
                InvokedMethodArguments = bulder.ToString()
            };

            this.adminActionLogRepository.Add(log);
            this.newsData.Commit();
        }

        public IEnumerable<AdminActionLog> GetAllActionLogs()
        {
            return this.adminActionLogRepository.GetAll();
        }

        private IEnumerable<KeyValuePair<string, object>> MapParameters(object[] arguments, ParameterInfo[] getParameters)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                yield return new KeyValuePair<string, object>(getParameters[i].Name, arguments[i]);
            }
        }
    }
}