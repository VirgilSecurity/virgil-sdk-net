using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgil.SDK.Keys.Tests
{
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;

    public class Messages
    {
        public List<Message> messages { get; set; }
    }

    public class Message
    {
        public int seconds_ago { get; set; }
        public string id { get; set; }
        public string to { get; set; }
        public long time { get; set; }
        public string subject { get; set; }
        public string fromfull { get; set; }
        public bool been_read { get; set; }
        public string from { get; set; }
        public string ip { get; set; }
    }

    public class Part
    {
        public Dictionary<string,string> headers { get; set; }
        public string body { get; set; }
    }

    public class Email
    {
        public Dictionary<string, string> headers { get; set; }
        public int seconds_ago { get; set; }
        public string id { get; set; }
        public string to { get; set; }
        public long time { get; set; }
        public string subject { get; set; }
        public string fromfull { get; set; }
        public List<Part> parts { get; set; }
        public bool been_read { get; set; }
        public string from { get; set; }
        public string ip { get; set; }
    }

    public static class Extensions
    {
        private static readonly Regex reg = new Regex("Your confirmation code is.+([A-Z0-9]{6})", RegexOptions.Compiled);

        public static string FindCode(this Email email)
        {
            var part = email.parts.First(it => it.body.Contains("Your confirmation code is"));
            var match = reg.Match(part.body);
            return match.Groups[1].Value;
        }
    }

    public class EmailResponse
    {
        public int apiInboxFetchesLeft { get; set; }
        public int apiEmailFetchesLeft { get; set; }
        public Email data { get; set; }
        public int forwardsLeft { get; set; }
    }

    public class Mailinator
    {
        private const string Api = "https://api.mailinator.com/api/";
        private const string Token = "3b0f46370d9f44cb9b5ac0e80dda97d7";

        public static async Task<IEnumerable<Message>> FetchInbox(string inbox)
        {
            var url = $"{Api}inbox?to={inbox}&token={Token}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);
            var messages = JsonConvert.DeserializeObject<Messages>(response).messages;
            return messages;
        }

        public static async Task<Email> FetchEmail(string messageId)
        {
            var url = $"{Api}email?msgid={messageId}&token={Token}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);
            var emR = JsonConvert.DeserializeObject<EmailResponse>(response);
            return emR.data;
        }
        
        public static async Task<string> GetConfirmationCodeFromLatestEmail(string inbox, bool isWait = false)
        {
            while (true)
            {
                await Task.Delay(2000);

                var mails = await FetchInbox(inbox);
                var messageId = mails.OrderByDescending(it => it.time).FirstOrDefault()?.id;

                if (messageId == null && isWait)
                {
                    continue;
                }

                var extractedCode = (await FetchEmail(messageId))?.FindCode();
                return extractedCode;
            }
        }

        public static async Task<Email> GetLatestEmail(string inbox)
        {
            var mails = await FetchInbox(inbox);
            var messageId = mails.OrderByDescending(it => it.time).FirstOrDefault()?.id;

            return await FetchEmail(messageId);
        }

        public static string GetRandomEmailName()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16).ToLowerInvariant() + "@mailinator.com";
        }
    }
}
