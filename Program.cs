using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeleBot
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage

        static ITelegramBotClient botClient;

        static List<string> fud = new List<string>();
        static List<string> things = new List<string>();
        static string fudlistname = "Fud";
        static string thingslistname = "Things";
        static void Main()
        {
            //var config = new JobHostConfiguration();

            //if (config.IsDevelopment)
            //{
            //    config.UseDevelopmentSettings();
            //}

            //var host = new JobHost(config);
            //// The following code ensures that the WebJob will be running continuously
            //host.RunAndBlock();
            botClient = new TelegramBotClient("1690828545:AAHBMEX78Q6ELA3RyNCZqeo5o7mIihwJH7M");
            

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

                botClient.OnMessage += Bot_OnMessage;

                while (true)
                { }
              
                botClient.StartReceiving();

                //botClient.StopReceiving();

        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text.StartsWith("/commands") == true)
            {
                botClient.DeleteMessageAsync(
                    chatId: e.Message.Chat,
                    messageId: e.Message.MessageId
                    );
                string output = "";

                output += "Commands: " + "\n";
                output += "/foodlistname, /thingslistname <name>    renames list to <name>\n";
                output += "/addfud /addthings <item>   add <item> to list\n";
                output += "/showfud /showthings     shows the list\n";
                output += "/removefud /removethings <item>  remove <item> from list\n";

                Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat, // or a chat id: 123456789
                    text: output
                );

            }

            if (e.Message.Text.StartsWith("/foodlistname") == true)
            {
                botClient.DeleteMessageAsync(
                    chatId: e.Message.Chat,
                    messageId: e.Message.MessageId
                    );

                e.Message.Text = e.Message.Text.Remove(0, 14);

                fudlistname = e.Message.Text;

                Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat, // or a chat id: 123456789
                    text: "Renamed fud: " + e.Message.Text
                );
            }

            if (e.Message.Text.StartsWith("/thingslistname") == true)
            {
                botClient.DeleteMessageAsync(
                    chatId: e.Message.Chat,
                    messageId: e.Message.MessageId
                    );

                e.Message.Text = e.Message.Text.Remove(0, 16);

                thingslistname = e.Message.Text;

                Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat, // or a chat id: 123456789
                    text: "Rename things: " + e.Message.Text
                );
            }

            if (e.Message.Text.StartsWith("/addfud") == true)
            {
               botClient.DeleteMessageAsync(
                    chatId: e.Message.Chat,
                    messageId: e.Message.MessageId
                    );
                e.Message.Text = e.Message.Text.Remove(0, 7);

                fud.Add(e.Message.Text);

                Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat, // or a chat id: 123456789
                    text: "Added: " + e.Message.Text
                );
            }

            if (e.Message.Text.StartsWith("/showfud") == true)
            {
                botClient.DeleteMessageAsync(
                    chatId: e.Message.Chat,
                    messageId: e.Message.MessageId
                    );
                e.Message.Text = e.Message.Text.Remove(0, 8);
                if (fud.Count > 0)
                {
                    int counter = 1;
                    string output = "";
                    output += fudlistname + "\n";
                    foreach (string food in fud)
                    {
                        output += counter + ") " + food + "\n";
                        counter++;
                    }
                    Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat, // or a chat id: 123456789
                    text: output
                    );
                }
            }
            if (e.Message.Text.StartsWith("/removefud") == true)
            {
                botClient.DeleteMessageAsync(
                    chatId: e.Message.Chat,
                    messageId: e.Message.MessageId
                    );
                e.Message.Text = e.Message.Text.Remove(0, 10);
                if (fud.Contains(e.Message.Text)== true)
                {
                    fud.Remove(e.Message.Text);
                    Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat, // or a chat id: 123456789
                    text: "Removed: " + e.Message.Text
                );
                }
            }


            if (e.Message.Text.StartsWith("/addthings") == true)
            {
                botClient.DeleteMessageAsync(
                     chatId: e.Message.Chat,
                     messageId: e.Message.MessageId
                     );
                e.Message.Text = e.Message.Text.Remove(0, 10);

                things.Add(e.Message.Text);

                Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat, // or a chat id: 123456789
                    text: "Added: " + e.Message.Text
                );
            }

            if (e.Message.Text.StartsWith("/showthings") == true)
            {
                botClient.DeleteMessageAsync(
                    chatId: e.Message.Chat,
                    messageId: e.Message.MessageId
                    );
                e.Message.Text = e.Message.Text.Remove(0, 11);
                if (things.Count > 0)
                {
                    int counter = 1;
                    string output = "";
                    output += thingslistname + "\n";
                    foreach (string food in things)
                    {
                        output += counter + ") " + food + "\n";
                        counter++;
                    }
                    Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat, // or a chat id: 123456789
                    text: output
                    );
                }
            }
            if (e.Message.Text.StartsWith("/removethings") == true)
            {
                botClient.DeleteMessageAsync(
                    chatId: e.Message.Chat,
                    messageId: e.Message.MessageId
                    );
                e.Message.Text = e.Message.Text.Remove(0, 13);
                if (things.Contains(e.Message.Text) == true)
                {
                    things.Remove(e.Message.Text);
                    Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat, // or a chat id: 123456789
                    text: "Removed: " + e.Message.Text
                );
                }
            }
        }
    }
}
