// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MailChimpTest.cs" company="Brandon Seydel">
//   N/A
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MailChimp.Net.Core;
using MailChimp.Net.Interfaces;
using MailChimp.Net.Models;
using Microsoft.Extensions.Configuration;

namespace MailChimp.Net.Tests
{
    /// <summary>
    /// The mail chimp test.
    /// </summary>
    public abstract class MailChimpTest
    {
        /// <summary>
        /// The _mail chimp manager.
        /// </summary>
        protected IMailChimpManager MailChimpManager;

        internal List GetMailChimpList(string listName = "TestList") => new List
        {
            Name = listName,
            PermissionReminder = "none",
            Contact = new Contact
            {
                Address1 = "TEST",
                City = "Bettendorf",
                Country = "USA",
                State = "IA",
                Zip = "61250",
                Company = "TEST"
            },
            CampaignDefaults = new CampaignDefaults
            {
                FromEmail = "test@test.com",
                FromName = "test",
                Language = "EN",
                Subject = "Yo"
            }
        };

        internal List GetGdprMailChimpList()
        {
            var list = GetMailChimpList("GDPRTestList");
            list.MarketingPermissions = true;
            return list;
        }

        internal async Task ClearLists(params string[] listToDeleteNames)
        {
            var lists = await MailChimpManager.Lists.GetAllAsync().ConfigureAwait(false);
            var listsToDelete = listToDeleteNames.Any()
                ? lists.Where(i => listToDeleteNames.Contains(i.Name, StringComparer.OrdinalIgnoreCase))
                : lists;

            await Task.WhenAll(listsToDelete.Select(x => MailChimpManager.Lists.DeleteAsync(x.Id))).ConfigureAwait(false);
        }

        private async Task ClearCampaigns()
        {
            var campaigns = await this.MailChimpManager.Campaigns.GetAllAsync().ConfigureAwait(false);
            await Task.WhenAll(campaigns.Select(x => MailChimpManager.Campaigns.DeleteAsync(x.Id))).ConfigureAwait(false);
        }

        internal async Task ClearMailChimpAsync()
        {
            await ClearLists().ConfigureAwait(false);
            await ClearCampaigns().ConfigureAwait(false);
        }

        public IConfiguration Configuration { get; }


        /// <summary>
        /// The initialize.
        /// </summary>
        public MailChimpTest()
        {
            var EV = "MAILCHIMP_API_KEY_TEST";

            var ApiKey = System.Environment.GetEnvironmentVariable(EV);

            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                var Message = ""
                    + $"WARNING: THERE ARE TESTS THAT DELETE ALL DATA.  ONLY USE TEST ACCOUNTS."
                    + $"Please set the {EV} environmental variable to run tests.\n"
                    + $"This can be done by running the following PowerShell command and restarting Visual Studio:\n\n"
                    + $"[System.Environment]::SetEnvironmentVariable('{EV}','XXXXXXXXXX', [System.EnvironmentVariableTarget]::User)"
                    ;

                throw new Exception(Message);
            }

            this.MailChimpManager = new MailChimpManager(ApiKey);
            RunBeforeTestFixture().Wait();
        }

        internal virtual Task RunBeforeTestFixture()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// The hash.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal string Hash(string emailAddress)
        {
            using (var md5 = MD5.Create()) return md5.GetHash(emailAddress);
        }
    }
}