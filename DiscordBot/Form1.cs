using System;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Discord;

namespace DiscordBot
{
    public partial class Form1 : Form
    {

        private static DiscordClient _bot; // bot

        /**DETAILS FOR LOGIN**/
        private static string admin = "dreameater"; // admin name here
        private static string message; // store received message here
        private static string botChannel = "109469668976140288"; // channel to send most bot messages to

        // Rock/paper/scissors tool
        Random rand = new Random();


        // Reading/Writing tools
        StreamWriter linksWriter;
        StreamWriter scoreWriter;
        StreamReader scoreReader;
        StreamReader linksReader;

        // Scoreboard tools
        Dictionary<string, int> scoreboard = new Dictionary<string, int>();
        ArrayList links = new ArrayList();

        ArrayList duo = new ArrayList();

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

        private void Form1_Load(object sender, EventArgs e)
        {
            _bot = new DiscordClient();
        }

        private void Running()
        {

            enterCred.Invoke((MethodInvoker)delegate
            {
                enterCred.Text = "Connected!";
            });

            message = "";
            
            /*Keep checking for user messages and perform actions*/
            _bot.MessageCreated += bot_MessageCreate;
        }

        private void bot_MessageCreate(object sender, MessageEventArgs e)
        {
            if (!e.Message.IsAuthor)
            {
                if (e.Message.Text.StartsWith("!"))
                {
                    message = e.Message.Text.Remove(0, 1).ToLower();
                    if (listBox1.InvokeRequired)
                    {
                        listBox1.Invoke(new MethodInvoker(delegate 
                        {
                            listBox1.Items.Add(DateTime.Now.ToLongTimeString() + ":" + " command " + "'!" + message + "'" + " from " + e.Message.User.Name);
                        }));
                    }
                    switch (message)
                    {
                        case "help":
                            _bot.SendMessage(e.ChannelId, "Commands include !jackbot !dnd !spam !fkalec !rock/paper/scissors !scores");
                            break;
                        case "spam":
                            _bot.SendMessage(e.ChannelId, "Sorry for someone spamming me :( I promise it's not my fault...");
                            break;
                        case "rock":
                            RockPaperScissors(e, 0);
                            break;
                        case "paper":
                            RockPaperScissors(e, 1);
                            break;
                        case "scissors":
                            RockPaperScissors(e, 2);
                            break;
                        case "fkalec":
                            _bot.SendMessage(e.ChannelId, "fk alec");
                            break;
                        case "jackbot":
                            _bot.SendMessage(e.ChannelId, "Hello my good friend. Hope you have a jooday.");
                            break;
                        case "dnd":
                            _bot.SendMessage(e.ChannelId, "D&D is scheduled for Sunday November 1st, sometime around 1pm.");
                            break;
                        case "quit":
                            Quit(e);
                            break;
                        case "scores":
                            Scores(e);
                            break;
                        case "links":
                            Links(e);
                            break;
                        case "duo":
                            Duo(e);
                            break;
                        case "noduo":
                            if (duo.Contains(e.Message.User.Name))
                            {
                                duo.Remove(e.Message.User.Name);
                                _bot.SendMessage(e.ChannelId, "Removed " + e.Message.User.Name + " from the duo list\n");
                            }
                            break;
                        case "<3":
                            _bot.SendMessage(e.ChannelId, "<3");
                            break;
                    }
                    return;
                }
                else if (e.Message.Text.StartsWith("http"))
                {
                    if (e.Message.User.Name != "Jack Bot")
                    {
                        links.Add(e.Message.Text);
                    }
                    return;
                }
            }
        }
        
        private void RockPaperScissors(MessageEventArgs e, int playerChoice)
        {
            if (!scoreboard.ContainsKey(e.Message.User.Name))
            {
                scoreboard.Add(e.Message.User.Name, 0);
            }

            int num = rand.Next(0, 3); // 0 = rock; 1 = paper; 2 = scissors
            if (num == 0 && playerChoice == 0)
            {
                _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose rock and you chose rock. We tie!");
            }
            else if (num == 0 && playerChoice == 1)
            {
                _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose rock and you chose paper. You win!");
                IncreaseScore(e);
            }
            else if (num == 0 && playerChoice == 2)
            {
                _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose rock and you chose scissors. I win!");
                DeductScore(e);
            }
            else if (num == 1 && playerChoice == 0)
            {
                _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose paper and you chose rock. I win!");
                DeductScore(e);
            }
            else if (num == 1 && playerChoice == 1)
            {
                _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose paper and you chose paper. We tie!");
            }
            else if (num == 1 && playerChoice == 2)
            {
                _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose paper and you chose scissors. You win!");
                IncreaseScore(e);
            }
            else if (num == 2 && playerChoice == 0)
            {
                _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose scissors and you chose rock. You win!");
                IncreaseScore(e);
            }
            else if (num == 2 && playerChoice == 1)
            {
                _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose scissors and you chose paper. I win!");
                DeductScore(e);
            }
            else
            {
               _bot.SendMessage(botChannel, "@" + e.Message.User.Name + ": " + "I chose scissors and you chose scissors. We tie!");
            }
            try
            {
                _bot.DeleteMessage(e.Message);
            }
            catch
            {
                _bot.SendMessage(e.ChannelId, "Your command cannot be deleted in this channel.\n");
            }

        }

        private void DeductScore(MessageEventArgs e)
        {
            scoreboard[e.Message.User.Name] -= 1;
        }

        private void IncreaseScore(MessageEventArgs e)
        {
            scoreboard[e.Message.User.Name] += 1;
        }

        private void Quit(MessageEventArgs e)
        {
            if (e.Message.User.Name == admin)
            {
                _bot.SendMessage(e.ChannelId, "*beep boop* Shutting down...");
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
                _bot.Disconnect();
                System.Environment.Exit(1);
            }
        }

        private void Links(MessageEventArgs e)
        {
            string concat = "";
            foreach (string str in links)
            {
                concat = concat + str + "\n";
            }
            _bot.SendMessage(e.ChannelId, concat);
        }

        private void Scores(MessageEventArgs e)
        {
            string txtScores = "";
            foreach (KeyValuePair<string, int> pair in scoreboard)
            {
                txtScores += (pair.Key + ": " + pair.Value + "\n");
            }
            try
            {
                _bot.DeleteMessage(e.Message);
            }
            catch
            {
                _bot.SendMessage(e.ChannelId, "Your command cannot be deleted in this channel.\n");
            }
            _bot.SendMessage(botChannel, txtScores);
        }

        private void Duo(MessageEventArgs e)
        {
            if (!duo.Contains(e.Message.User.Name))
            {
                duo.Add(e.Message.User.Name);
                _bot.SendMessage(e.ChannelId, "Added " + e.Message.User.Name + " to the duo list!\n");
            }
            string duoList = "Duo list: \n";
            foreach (string d in duo)
            {
                duoList += d + "\n";
            }
            _bot.SendMessage(e.ChannelId, duoList);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(usernameBox.TextLength != 0 && passwordBox.TextLength != 0) // click button to login
            {
                Program.email = usernameBox.Text;
                Program.password = passwordBox.Text;
                try
                {
                    _bot.Connect(Program.email, Program.password).Wait(); // connect to server
                    Thread tr = new Thread(Running);
                    tr.Start();
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
