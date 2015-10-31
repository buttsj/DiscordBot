using System;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.Generic;
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
        private static string botChannel = "109469668976140288"; // channel to send most bot messages to

        Random rand = new Random();
        Boolean RPS = false;
        Boolean win = false;
        Boolean loss = false;

        StreamWriter linksWriter;
        StreamWriter scoreWriter;
        StreamReader scoreReader;
        StreamReader linksReader;

        Dictionary<string, int> scoreboard = new Dictionary<string, int>();
        ArrayList links = new ArrayList();

        public Form1()
        {
            if (File.Exists("C:\\Scoreboard.txt"))
            {
                using (scoreReader = new StreamReader("C:\\Scoreboard.txt"))
                {
                    try
                    {
                        do
                        {
                            string tmp = scoreReader.ReadLine();
                            string[] words = tmp.Split(' ');
                            string username = "";
                            int score = 0;
                            int count = 0;
                            while (count != words.Length - 1)
                            {
                                username += words[count];
                                if (count != words.Length - 2)
                                {
                                    username += " ";
                                }
                                count++;
                            }
                            score = Int32.Parse(words[count]);
                            scoreboard.Add(username, score);
                        }
                        while (scoreReader.Peek() != -1);
                    }
                    catch
                    {
                        // empty
                    }
                    finally
                    {
                        scoreReader.Close();
                    }
                }
            }
            if (File.Exists("C:\\Links.txt"))
            {
                using (linksReader = new StreamReader("C:\\Links.txt"))
                {
                    try
                    {
                        do
                        {
                            string tmp = linksReader.ReadLine();
                            links.Add(tmp);
                        }
                        while (linksReader.Peek() != -1);
                    }
                    catch
                    {
                        // empty
                    }
                    finally
                    {
                        linksReader.Close();
                    }
                }
            }
            InitializeComponent();
        }

        private void Running()
        {
            
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
                            await _bot.SendMessage(e.ChannelId, "Commands include !jackbot !dnd !spam !fkalec !rock/paper/scissors !scores");
                        } else if (message == "spam")
                        {
                            await _bot.SendMessage(e.ChannelId, "Sorry for someone spamming me :( I promise it's not my fault...");
                        } else if (message == "rock") {
                            RPS = true;
                            int num = rand.Next(0, 3); // 0 = rock; 1 = paper; 2 = scissors
                            if (num == 0)
                            {
                                await _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose rock! We tie!");
                            }
                            else if (num == 1)
                            {
                                await _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose paper! I win! (-1 score)");
                                loss = true;
                            }
                            else
                            {
                                await _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose scissors! You win! (+1 score)");
                                win = true;
                            }
                            try
                            {
                                await _bot.DeleteMessage(e.Message);
                            }
                            catch
                            {
                                await _bot.SendMessage(e.ChannelId, "Your command cannot be deleted in this channel.\n");
                            }
                        } else if (message == "paper")
                        {
                            RPS = true;
                            int num = rand.Next(0, 3); // 0 = rock; 1 = paper; 2 = scissors
                            if (num == 0)
                            {
                                await _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose rock! You win! (+1 score)");
                                win = true;
                            }
                            else if (num == 1)
                            {
                                await _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose paper! We tie!");
                            }
                            else
                            {
                                await _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose scissors! I win! (-1 score)");
                                loss = true;
                            }
                            try
                            {
                                await _bot.DeleteMessage(e.Message);
                            }
                            catch
                            {
                                await _bot.SendMessage(e.ChannelId, "Your command cannot be deleted in this channel.\n");
                            }
                        } else if (message == "scissors")
                        {
                            RPS = true;
                            int num = rand.Next(0, 3); // 0 = rock; 1 = paper; 2 = scissors
                            if (num == 0)
                            {
                                await _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose rock! I win! (-1 score)");
                                loss = true;
                            }
                            else if (num == 1)
                            {
                                await _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose paper! You win! (+1 score)");
                                win = true;
                            }
                            else
                            {
                                await _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose scissors! We tie!");
                            }
                            try
                            {
                                await _bot.DeleteMessage(e.Message);
                            }
                            catch
                            {
                                await _bot.SendMessage(e.ChannelId, "Your command cannot be deleted in this channel.\n");
                            }
                        } else if (message == "fkalec")
                        {
                            await _bot.SendMessage(e.ChannelId, "fk alec");
                        } else if (message == "jackbot")
                        {
                            await _bot.SendMessage(e.ChannelId, "Hello my good friend. Hope you have a jooday.");
                        } else if (message == "dnd")
                        {
                            await _bot.SendMessage(e.ChannelId, "D&D is scheduled for Sunday November 1st, sometime around 1pm.");
                        } else if (message == "quit" && e.Message.User.Name == admin)
                        {
                            await _bot.SendMessage(e.ChannelId, "*beep boop* Shutting down...");
                            scoreWriter = new StreamWriter("C:\\Scoreboard.txt");
                            foreach (KeyValuePair<string, int> pair in scoreboard)
                            {
                                scoreWriter.WriteLine(pair.Key + " " + pair.Value);
                            }
                            scoreWriter.Close();

                            linksWriter = new StreamWriter("C:\\Links.txt");
                            foreach (string str in links)
                            {
                                linksWriter.WriteLine(str);
                            }
                            linksWriter.Close();
                            System.Environment.Exit(1);
                        } else if (message == "scores")
                        {
                            string txtScores = "";
                            foreach (KeyValuePair<string, int> pair in scoreboard)
                            {
                                txtScores += (pair.Key + ": " + pair.Value + "\n");
                            }
                            try
                            {
                                await _bot.DeleteMessage(e.Message);
                            }
                            catch
                            {
                                await _bot.SendMessage(e.ChannelId, "Your command cannot be deleted in this channel.\n");
                            }
                            await _bot.SendMessage(botChannel, txtScores);
                        } else if (message == "links")
                        {
                            string concat = "";
                            foreach (string str in links)
                            {
                                concat = concat + str + "\n";
                            }
                            await _bot.SendMessage(e.ChannelId, concat);
                        }
                        if (RPS == true)
                        {
                            if (!scoreboard.ContainsKey(e.Message.User.Name))
                            {
                                scoreboard.Add(e.Message.User.Name, 0);
                            }
                            if (win == true)
                            {
                                scoreboard[e.Message.User.Name] += 1;
                            }
                            else if (loss == true)
                            {
                                scoreboard[e.Message.User.Name] -= 1;
                                if (scoreboard[e.Message.User.Name] == -1)
                                {
                                    scoreboard[e.Message.User.Name] = 0;
                                }
                            }
                            RPS = false;
                            win = false;
                            loss = false;
                        }
                    } else if (e.Message.Text.StartsWith("http"))
                    {
                        if (e.Message.User.Name != "Jack Bot")
                        {
                            links.Add(e.Message.Text);
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
                try
                {
                    _bot.Connect(email, password).Wait(); // connect to server
                    Running();
                }
                catch
                {
                    listBox1.Items.Add("Invalid Username/Password");
                }
            }
        }

        private void github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch
            {
                // link didn't work
            }
        }

        private void VisitLink()
        {
            github.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/buttsj");
        }
    }
}
