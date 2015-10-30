using System;
using System.Windows.Forms;
using Discord;

namespace DiscordBot
{
    public partial class Form1 : Form
    {

        private static DiscordClient _bot; // bot

        /**DETAILS FOR LOGIN**/
        private static string email;
        private static string password;

        private static string admin = "dreameater"; // admin name here
        private static string message; // store received message here

        public Form1()
        {
            InitializeComponent();
        }

        private void Running()
        {
            _bot.Connect(email, password).Wait(); // connect to server
            enterCred.Text = "Connected!";
            message = "";

            /*Keep checking for user messages and perform actions*/
            _bot.MessageCreated += async (s, e) =>
            {
                if (!e.Message.IsAuthor)
                {
                    if (e.Message.Text.StartsWith("!"))
                    {
                        message = e.Message.Text.Remove(0, 1).ToLower();
                        if (listBox1.InvokeRequired)
                        {
                            listBox1.Invoke(new MethodInvoker(delegate {
                                listBox1.Items.Add(DateTime.Now.ToLongTimeString() + ":" + " command " + "'!" + message + "'" + " from " + e.Message.User.Name);
                            }));
                        }

                        if (message == "help")
                        {
                            await _bot.SendMessage(e.ChannelId, "Commands include !jackbot !dnd !spam !fkalec");
                        } else if (message == "spam")
                        {
                            await _bot.SendMessage(e.ChannelId, "Sorry for someone spamming me :( I promise it's not my fault...");
                        } else if (message == "fkalec")
                        {
                            await _bot.SendMessage(e.ChannelId, "fk alec");
                        } else if (message == "jackbot")
                        {
                            await _bot.SendMessage(e.ChannelId, "Hello my good friend. Hope you have a jooday.");
                        } else if (message == "dnd")
                        {
                            await _bot.SendMessage(e.ChannelId, "D&D is scheduled for Sunday November 1st, sometime around 1pm.");
                        } else if (message == "quit")
                        {
                            if (e.Message.User.Name == admin)
                            {
                                await _bot.SendMessage(e.ChannelId, "*beep boop* Shutting down...");
                                System.Environment.Exit(1);
                            }
                        }
                    }
                }
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _bot = new DiscordClient();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(usernameBox.TextLength != 0 && passwordBox.TextLength != 0) // click button to login
            {
                email = usernameBox.Text;
                password = passwordBox.Text;
                Running();
            }
        }
    }
}
