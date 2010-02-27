﻿/******************************************************************************\
 * IceChat 2009 Internet Relay Chat Client
 *
 * Copyright (C) 2010 Paul Vanderzee <snerf@icechat.net>
 *                                    <www.icechat.net> 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2, or (at your option)
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
 *
 * Please consult the LICENSE.txt file included with this project for
 * more details
 *
\******************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

using IceChat.Properties;
using IceChatPlugin;

namespace IceChat
{
    public partial class FormMain
    {

        /// <summary>
        /// Received a CTCP Message
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="nick">The Nick who sent the CTCP Message</param>
        /// <param name="ctcp">The CTCP Message</param>
        private void OnCtcpMessage(IRCConnection connection, string nick, string ctcp)
        {
            //check if CTCP's are enabled
            if (connection.ServerSetting.DisableCTCP)
                return;

            string msg = GetMessageFormat("Ctcp Request");
            msg = msg.Replace("$nick", nick);
            msg = msg.Replace("$ctcp", ctcp);
            CurrentWindowMessage(connection, msg, 7, false);

            msg = GetMessageFormat("Ctcp Reply");
            msg = msg.Replace("$nick", nick);
            msg = msg.Replace("$ctcp", ctcp);

            switch (ctcp)
            {
                case "VERSION":
                    SendData(connection, "NOTICE " + nick + " :" + ((char)1).ToString() + "VERSION " + Settings.Default.ProgramID + " " + Settings.Default.Version + ((char)1).ToString());
                    msg = msg.Replace("$reply", Settings.Default.ProgramID + " " + Settings.Default.Version);
                    CurrentWindowMessage(connection, msg, 7, false);
                    break;
                case "PING":
                    SendData(connection, "NOTICE " + nick + " :" + ((char)1).ToString() + "PING " + System.Environment.TickCount.ToString() + ((char)1).ToString());
                    msg = msg.Replace("$reply", System.Environment.TickCount.ToString());
                    CurrentWindowMessage(connection, msg, 7, false);
                    break;
                case "TIME":
                    SendData(connection, "NOTICE " + nick + " :" + ((char)1).ToString() + "TIME " + System.DateTime.Now.ToString() + ((char)1).ToString());
                    msg = msg.Replace("$reply", System.DateTime.Now.ToString());
                    CurrentWindowMessage(connection, msg, 7, false);
                    break;
                case "USERINFO":
                    SendData(connection, "NOTICE " + nick + " :" + ((char)1).ToString() + "USERINFO IceChat IRC Client : Download at http://www.icechat.net" + ((char)1).ToString());
                    msg = msg.Replace("$reply", "IceChat IRC Client : Download at http://www.icechat.net");
                    CurrentWindowMessage(connection, msg, 7, false);
                    break;
                case "CLIENTINFO":
                    SendData(connection, "NOTICE " + nick + " :" + ((char)1).ToString() + "CLIENTINFO This client supports: UserInfo, Finger, Version, Source, Ping, Time and ClientInfo" + ((char)1).ToString());
                    msg = msg.Replace("$reply", "This client supports: UserInfo, Finger, Version, Source, Ping, Time and ClientInfo");
                    CurrentWindowMessage(connection, msg, 7, false);
                    break;
                case "SOURCE":
                    SendData(connection, "NOTICE " + nick + " :" + ((char)1).ToString() + "SOURCE " + Settings.Default.ProgramID + " " + Settings.Default.Version + " http://www.icechat.net" + ((char)1).ToString());
                    msg = msg.Replace("$reply", Settings.Default.ProgramID + " " + Settings.Default.Version + " http://www.icechat.net");
                    CurrentWindowMessage(connection, msg, 7, false);
                    break;
                case "FINGER":
                    SendData(connection, "NOTICE " + nick + " :" + ((char)1).ToString() + "FINGER Stop fingering me" + ((char)1).ToString());
                    msg = msg.Replace("$reply", "Stop fingering me");
                    CurrentWindowMessage(connection, msg, 7, false);
                    break;

            }
        }

        /// <summary>
        /// Received a User Notice
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="nick">The Nick who sent the Notice</param>
        /// <param name="message">The Notice message</param>
        private void OnUserNotice(IRCConnection connection, string nick, string message)
        {
            string msg = GetMessageFormat("User Notice");
            msg = msg.Replace("$server", connection.ServerSetting.ServerName);
            msg = msg.Replace("$nick", nick);
            msg = msg.Replace("$message", message);
            CurrentWindowMessage(connection, msg, 1, false);
        }
        /// <summary>
        /// Received the full host for a userreply
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="fullhost">The full user host Nick+=Ident@Host</param>
        private void OnUserHostReply(IRCConnection connection, string fullhost)
        {
            string host = fullhost.Substring(fullhost.IndexOf('@') + 1);
            string nick = "";
            if (fullhost.IndexOf('*') > -1)
                nick = fullhost.Substring(0, fullhost.IndexOf('*'));
            else
                nick = fullhost.Substring(0, fullhost.IndexOf('='));

            //update the internal addresslist and check for user in all channels
            InternalAddressList ial = new InternalAddressList(nick, host, "");

            if (!connection.ServerSetting.IAL.ContainsKey(nick))
                connection.ServerSetting.IAL.Add(nick, ial);
            else
                ((InternalAddressList)connection.ServerSetting.IAL[nick]).Host = host;

        }

        /// <summary>
        /// Received a Server Notice 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="message"></param>
        private void OnServerNotice(IRCConnection connection, string message)
        {
            string msg = GetMessageFormat("Server Notice");
            msg = msg.Replace("$server", connection.ServerSetting.ServerName);
            msg = msg.Replace("$message", message);
            //((ConsoleTabWindow)tabMain.TabPages[0]).AddText(connection, msg, 1, false);

            mainTabControl.GetTabPage("Console").AddText(connection, msg, 1, false);

        }

        /// <summary>
        /// Send out a message to be parsed
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="data">The message to be parsed</param>
        private void OutGoingCommand(IRCConnection connection, string data)
        {
            ParseOutGoingCommand(connection, data);
        }

        /// <summary>
        /// Recieved a Standard Server Message
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="message">The Server message</param>
        private void OnServerMessage(IRCConnection connection, string message)
        {
            //goes to the console
            string msg = GetMessageFormat("Server Message");
            msg = msg.Replace("$server", connection.ServerSetting.ServerName);
            msg = msg.Replace("$message", message);
            //((ConsoleTabWindow)tabMain.TabPages[0]).AddText(connection,msg, 1, false);            
            //((ConsoleTabWindow)tabMain.TabPages[0]).LastMessageType = ServerMessageType.ServerMessage;

            mainTabControl.GetTabPage("Console").AddText(connection, msg, 1, false);
            mainTabControl.GetTabPage("Console").LastMessageType = ServerMessageType.ServerMessage;

        }

        /// <summary>
        /// Received Server Message of the Day
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="message">Message</param>
        private void OnServerMOTD(IRCConnection connection, string message)
        {
            string msg = GetMessageFormat("Server MOTD");
            msg = msg.Replace("$server", connection.ServerSetting.ServerName);
            msg = msg.Replace("$message", message);
            //((ConsoleTabWindow)tabMain.TabPages[0]).AddText(connection,msg, 1, false);
            //((ConsoleTabWindow)tabMain.TabPages[0]).LastMessageType = ServerMessageType.ServerMessage;

            mainTabControl.GetTabPage("Console").AddText(connection, msg, 1, false);
            mainTabControl.GetTabPage("Console").LastMessageType = ServerMessageType.ServerMessage;

        }

        /// <summary>
        /// Received a Channel for the Server Channel List
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="channel">Channel Name</param>
        /// <param name="users">Total Users in Channel</param>
        /// <param name="topic">Channel Topic</param>
        private void OnChannelList(IRCConnection connection, string channel, string users, string topic)
        {
            //will make a seperate window for this eventually
            //((ConsoleTabWindow)tabMain.TabPages[0]).AddText(connection,channel + " : " + users + " : " + topic, 7, false);

            mainTabControl.GetTabPage("Console").AddText(connection, channel + " : " + users + " : " + topic, 7, false);
        }


        /// <summary>
        /// Received a Server/Connection Error
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="message">Error Message</param>
        private void OnServerError(IRCConnection connection, string message)
        {
            string[] msgs = message.Split('\n');
            foreach (string msg in msgs)
            {
                if (msg.Length > 0)
                {
                    //goes to the console                        
                    string error = GetMessageFormat("Server Error");
                    error = error.Replace("$server", connection.ServerSetting.ServerName);
                    error = error.Replace("$message", msg);

                    // ((ConsoleTabWindow)tabMain.TabPages[0]).AddText(connection, error, 4, false);
                    mainTabControl.GetTabPage("Console").AddText(connection, error, 4, false);

                    //send it to the current window as well
                    if (CurrentWindowType != IceTabPage.WindowType.Console)
                        CurrentWindowMessage(connection, error, 4, false);

                    //((ConsoleTabWindow)tabMain.TabPages[0]).LastMessageType = ServerMessageType.ServerMessage;
                    mainTabControl.GetTabPage("Console").LastMessageType = ServerMessageType.ServerMessage;
                }
            }

        }

        /// <summary>
        /// Received Whois Data on a Nick
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="nick">The nick whois data is from</param>
        /// <param name="data">The Whois data</param>
        private void OnWhoisData(IRCConnection connection, string nick, string data)
        {
            string msg = GetMessageFormat("User Whois");
            msg = msg.Replace("$nick", nick);
            msg = msg.Replace("$data", data);

            //check if there is a query window open
            IceTabPage t = GetWindow(connection, nick, IceTabPage.WindowType.Query);
            if (t != null)
            {
                t.TextWindow.AppendText(msg, 1);
                t.LastMessageType = ServerMessageType.Message;
            }
            else
            {
                //send whois data to the current window instead
                CurrentWindowMessage(connection, msg, 1, false);
            }
        }

        /// <summary>
        /// Received a Query/Private Message action
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="nick">Nick who sent the action</param>
        /// <param name="message">Query Action Message</param>
        private void OnQueryAction(IRCConnection connection, string nick, string host, string message)
        {
            if (!mainTabControl.WindowExists(connection, nick, IceTabPage.WindowType.Query) && iceChatOptions.DisableQueries)
                return;

            if (!mainTabControl.WindowExists(connection, nick, IceTabPage.WindowType.Query))
                AddWindow(connection, nick, IceTabPage.WindowType.Channel);

            IceTabPage t = GetWindow(connection, nick, IceTabPage.WindowType.Query);
            if (t != null)
            {
                string msg = GetMessageFormat("Private Action");
                msg = msg.Replace("$nick", nick).Replace("$host", host);
                msg = msg.Replace("$message", message);

                bool ishandled = false;
                PluginArgs args = new PluginArgs(t.TextWindow, "", nick, host, msg);
                args.Connection = connection;

                foreach (IPluginIceChat ipc in loadedPlugins)
                {
                    if (ipc.QueryAction(args) == true)
                        ishandled = true;
                }

                if (!ishandled)
                    t.TextWindow.AppendText(msg, 1);

                t.LastMessageType = ServerMessageType.Action;
            }
        }
        /// <summary>
        /// Received a Query/Private Message 
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="nick">Nick who sent the message</param>
        /// <param name="message">Query Message</param>
        private void OnQueryMessage(IRCConnection connection, string nick, string host, string message)
        {
            if (!mainTabControl.WindowExists(connection, nick, IceTabPage.WindowType.Query) && iceChatOptions.DisableQueries)
                return;

            if (!mainTabControl.WindowExists(connection, nick, IceTabPage.WindowType.Query))
                AddWindow(connection, nick, IceTabPage.WindowType.Query);

            IceTabPage t = GetWindow(connection, nick, IceTabPage.WindowType.Query);
            if (t != null)
            {
                string msg = GetMessageFormat("Private Message");
                msg = msg.Replace("$nick", nick).Replace("$host", host);
                msg = msg.Replace("$message", message);

                bool ishandled = false;
                PluginArgs args = new PluginArgs(t.TextWindow, "", nick, host, msg);
                args.Connection = connection;

                foreach (IPluginIceChat ipc in loadedPlugins)
                {
                    if (ipc.QueryMessage(args) == true)
                        ishandled = true;
                }

                if (!ishandled)
                    t.TextWindow.AppendText(msg, 1);

                t.LastMessageType = ServerMessageType.Message;
            }
        }

        /// <summary>
        /// Received a Channel Action
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="channel">Which Channel it is from</param>
        /// <param name="nick">Nick who sent the action</param>
        /// <param name="message">Channel action</param>
        private void OnChannelAction(IRCConnection connection, string channel, string nick, string host, string message)
        {
            IceTabPage t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            if (t != null)
            {
                string msg = GetMessageFormat("Channel Action");
                msg = msg.Replace("$nick", nick).Replace("$channel", channel);
                msg = msg.Replace("$color", "");
                msg = msg.Replace("$message", message);

                bool ishandled = false;
                PluginArgs args = new PluginArgs(t.TextWindow, channel, nick, host, msg);
                args.Connection = connection;

                foreach (IPluginIceChat ipc in loadedPlugins)
                {
                    if (ipc.ChannelMessage(args) == true)
                        ishandled = true;
                }

                if (!ishandled)
                    t.TextWindow.AppendText(msg, 1);

                t.LastMessageType = ServerMessageType.Action;
            }
        }

        /// <summary>
        /// Received a Channel Message
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="channel">Which Channel it is from</param>
        /// <param name="nick">Nick who sent the message</param>
        /// <param name="message">Channel Message</param>
        private void OnChannelMessage(IRCConnection connection, string channel, string nick, string host, string message)
        {
            IceTabPage t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            if (t != null)
            {
                string msg = GetMessageFormat("Channel Message");
                msg = msg.Replace("$nick", nick).Replace("$channel", channel).Replace("$host", host);

                //assign $color to the nickname color
                //get the user mode for the nickname                
                if (msg.Contains("$color") && t.NickExists(nick))
                {
                    User u = t.GetNick(nick);

                    for (int i = 0; i < u.Level.Length; i++)
                    {
                        if (u.Level[i])
                        {
                            if (connection.ServerSetting.StatusModes[0][i] == 'q')
                                msg = msg.Replace("$color", ((char)3).ToString() + iceChatColors.ChannelOwnerColor);
                            else if (connection.ServerSetting.StatusModes[0][i] == 'a')
                                msg = msg.Replace("$color", ((char)3).ToString() + iceChatColors.ChannelAdminColor);
                            else if (connection.ServerSetting.StatusModes[0][i] == 'o')
                                msg = msg.Replace("$color", ((char)3).ToString() + iceChatColors.ChannelOpColor);
                            else if (connection.ServerSetting.StatusModes[0][i] == 'h')
                                msg = msg.Replace("$color", ((char)3).ToString() + iceChatColors.ChannelHalfOpColor);
                            else if (connection.ServerSetting.StatusModes[0][i] == 'v')
                                msg = msg.Replace("$color", ((char)3).ToString() + iceChatColors.ChannelVoiceColor);
                            else
                                msg = msg.Replace("$color", ((char)3).ToString() + iceChatColors.ChannelOwnerColor);

                            break;
                        }
                    }

                    if (msg.Contains("$color"))
                        msg = msg.Replace("$color", ((char)3).ToString() + iceChatColors.ChannelRegularColor);

                }

                //check if the nickname exists
                if (t.NickExists(nick))
                    msg = msg.Replace("$status", t.GetNick(nick).ToString().Replace(nick, ""));
                else
                    msg = msg.Replace("$status", "");

                msg = msg.Replace("$message", message);

                bool ishandled = false;
                PluginArgs args = new PluginArgs(t.TextWindow, channel, nick, host, msg);
                args.Connection = connection;

                foreach (IPluginIceChat ipc in loadedPlugins)
                {
                    
                    if (ipc.ChannelMessage(args) == true)
                        ishandled = true;
                }

                if (!ishandled)
                    t.TextWindow.AppendText(msg, 1);

                t.LastMessageType = ServerMessageType.Message;
            }
        }

        /// <summary>
        /// Received a Standard/generic Channel Message
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="channel">Which Channel it is from</param>
        /// <param name="message">Channel Message</param>
        private void OnGenericChannelMessage(IRCConnection connection, string channel, string message)
        {
            IceTabPage t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            if (t != null)
            {
                string msg = GetMessageFormat("Channel Other");
                msg = msg.Replace("$message", message);

                t.TextWindow.AppendText(msg, 1);
                t.LastMessageType = ServerMessageType.Other;
            }
        }

        /// <summary>
        /// A User Quit the Server
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="user">Which Nick quit the Server</param>
        /// <param name="reason">Quit Reason</param>
        private void OnServerQuit(IRCConnection connection, string nick, string host, string reason)
        {
            foreach (IceTabPage t in mainTabControl.TabPages)
            {
                if (t.Connection == connection)
                {
                    string msg = GetMessageFormat("Server Quit");
                    msg = msg.Replace("$nick", nick);
                    msg = msg.Replace("$host", host);
                    msg = msg.Replace("$reason", reason);

                    bool ishandled = false;
                    PluginArgs args = new PluginArgs(t.TextWindow, "", nick, host, msg);
                    args.Extra = reason;

                    args.Connection = connection;

                    if (t.WindowStyle == IceTabPage.WindowType.Channel)
                    {
                        if (t.NickExists(nick) == true)
                        {
                            foreach (IPluginIceChat ipc in loadedPlugins)
                            {
                                if (ipc.ServerQuit(args) == true)
                                    ishandled = true;
                            }

                            if (!ishandled)
                                t.TextWindow.AppendText(msg, 1);

                            t.LastMessageType = ServerMessageType.QuitServer;
                            t.RemoveNick(nick);
                        }
                    }
                    if (t.WindowStyle == IceTabPage.WindowType.Query)
                    {
                        if (t.TabCaption == nick)
                        {
                            foreach (IPluginIceChat ipc in loadedPlugins)
                            {
                                if (ipc.ServerQuit(args) == true)
                                    ishandled = true;
                            }

                            if (!ishandled)
                                t.TextWindow.AppendText(msg, 1);

                            t.LastMessageType = ServerMessageType.QuitServer;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A User Joined a Channel
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="channel">Which Channel was Joined</param>
        /// <param name="user">Full User Host of who Joined</param>
        /// <param name="refresh">Whether to Refresh the Nick List</param>
        private void OnChannelJoin(IRCConnection connection, string channel, string nick, string host, bool refresh)
        {
            IceTabPage t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            if (t != null)
            {
                if (refresh)
                {
                    string msg = GetMessageFormat("Channel Join");
                    msg = msg.Replace("$nick", nick).Replace("$channel", channel).Replace("$host", host);

                    bool ishandled = false;
                    PluginArgs args = new PluginArgs(t.TextWindow, channel, nick, host, msg);
                    args.Connection = connection;

                    foreach (IPluginIceChat ipc in loadedPlugins)
                    {
                        if (ipc.ChannelJoin(args) == true)
                            ishandled = true;
                    }

                    if (!ishandled)
                        t.TextWindow.AppendText(msg, 1);
                }

                t.AddNick(nick, refresh);
                t.LastMessageType = ServerMessageType.JoinChannel;
            }
        }

        /// <summary>
        /// A User Parted/Left a Channel
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="channel">Which Channel was Parted</param>
        /// <param name="user">Full User Host of who Parted</param>
        /// <param name="reason">Part Reason (if any)</param>
        private void OnChannelPart(IRCConnection connection, string channel, string nick, string host, string reason)
        {
            IceTabPage t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            if (t != null)
            {
                string msg = GetMessageFormat("Channel Part");
                msg = msg.Replace("$channel", channel).Replace("$nick", nick).Replace("$host", host).Replace("$reason", reason);

                bool ishandled = false;
                PluginArgs args = new PluginArgs(t.TextWindow, channel, nick, host, msg);
                args.Connection = connection;

                foreach (IPluginIceChat ipc in loadedPlugins)
                {
                    if (ipc.ChannelPart(args) == true)
                        ishandled = true;
                }

                if (!ishandled)
                    t.TextWindow.AppendText(msg, 1);

                t.RemoveNick(nick);
                t.LastMessageType = ServerMessageType.PartChannel;
            }
        }

        /// <summary>
        /// A User was kicked from a Channel
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="channel">Which Channel the User was Kicked from</param>
        /// <param name="nick">Nickname of who was Kicked</param>
        /// <param name="reason">Kick Reason</param>
        /// <param name="kickUser">Full User Host of Who kicked the User</param>
        private void OnKickNick(IRCConnection connection, string channel, string nick, string reason, string kickUser)
        {
            IceTabPage t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            if (t != null)
            {
                string kickNick = NickFromFullHost(kickUser);
                string kickHost = HostFromFullHost(kickUser);

                string msg = GetMessageFormat("Channel Kick");
                msg = msg.Replace("$nick", kickNick);
                msg = msg.Replace("$host", kickHost);
                msg = msg.Replace("$kickee", nick);
                msg = msg.Replace("$channel", channel);
                msg = msg.Replace("$reason", reason);

                t.TextWindow.AppendText(msg, 1);
                t.RemoveNick(nick);
                t.LastMessageType = ServerMessageType.Other;
            }
        }

        /// <summary>
        /// You have Joined a Channel
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="channel">Which Channel you joined</param>
        private void OnChannelJoinSelf(IRCConnection connection, string channel)
        {
            //check if channel window already exists
            IceTabPage t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            if (t == null)
                AddWindow(connection, channel, IceTabPage.WindowType.Channel);
        }

        /// <summary>
        /// You have Parted/Left a Channel
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="channel">Which Channel you parted</param>
        private void OnChannelPartSelf(IRCConnection connection, string channel)
        {
            string reason = "";
            string msg = GetMessageFormat("Self Channel Part");
            msg = msg.Replace("$nick", connection.ServerSetting.NickName).Replace("$channel", channel);
            msg = msg.Replace("$reason", reason);

            IceTabPage t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            if (t != null)
            {
                t.IsFullyJoined = false;
                t.ClearNicks();
            }

            mainTabControl.GetTabPage("Console").AddText(connection, msg, 1, false);

        }

        /// <summary>
        /// You where Kicked from a Channel
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="channel">Which Channel you were kicked from</param>
        /// <param name="reason">Kick Reason</param>
        /// <param name="kickUser">Full User Host of who kicked you</param>
        private void OnKickSelf(IRCConnection connection, string channel, string reason, string kickUser)
        {
            try
            {
                RemoveWindow(connection, channel);

                string nick = NickFromFullHost(kickUser);
                string host = HostFromFullHost(kickUser);

                string msg = GetMessageFormat("Self Channel Kick");
                msg = msg.Replace("$nick", connection.ServerSetting.NickName);
                msg = msg.Replace("$kicker", nick);
                msg = msg.Replace("$host", host);
                msg = msg.Replace("$channel", channel);
                msg = msg.Replace("$reason", reason);

                mainTabControl.GetTabPage("Console").AddText(connection, msg, 1, false);

            }
            catch (Exception e)
            {
                WriteErrorFile("OnKickSelf Error:" + e.Message, e.StackTrace);
            }
        }

        /// <summary>
        /// A User changed their Nick Name
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="oldnick">Original Nick Name</param>
        /// <param name="newnick">New Nick Name</param>
        private void OnChangeNick(IRCConnection connection, string oldnick, string newnick, string host)
        {
            try
            {
                if (CurrentWindowType == IceTabPage.WindowType.Console)
                {
                    if (inputPanel.CurrentConnection == connection)
                    {
                        StatusText(inputPanel.CurrentConnection.ServerSetting.NickName + " connected to " + inputPanel.CurrentConnection.ServerSetting.RealServerName);

                        if (connection.ServerSetting.NickName == newnick)
                        {
                            string msg = GetMessageFormat("Self Nick Change");
                            msg = msg.Replace("$nick", oldnick).Replace("$newnick", newnick).Replace("$host", host); ;

                            mainTabControl.GetTabPage("Console").AddText(connection, msg, 1, false);

                        }
                    }
                }

                foreach (IceTabPage t in mainTabControl.TabPages)
                {
                    if (t.Connection == connection)
                    {
                        if (t.WindowStyle == IceTabPage.WindowType.Channel)
                        {
                            if (t.NickExists(oldnick))
                            {
                                if (connection.ServerSetting.NickName == newnick)
                                {
                                    string msg = GetMessageFormat("Self Nick Change");
                                    msg = msg.Replace("$nick", oldnick).Replace("$newnick", newnick).Replace("$host", host); ;

                                    t.TextWindow.AppendText(msg, 1);
                                    //update status bar as well if current channel
                                    if ((inputPanel.CurrentConnection == connection) && (CurrentWindowType == IceTabPage.WindowType.Channel))
                                    {
                                        if (CurrentWindow == t)
                                            StatusText(t.Connection.ServerSetting.NickName + " in " + t.TabCaption + " [" + t.ChannelModes + "] {" + t.Connection.ServerSetting.RealServerName + "}");
                                    }
                                }
                                else
                                {
                                    string msg = GetMessageFormat("Channel Nick Change");
                                    msg = msg.Replace("$nick", oldnick).Replace("$newnick", newnick).Replace("$host", host);

                                    t.TextWindow.AppendText(msg, 1);
                                }

                                User u = t.GetNick(oldnick);
                                string nick = newnick;
                                if (u != null)
                                {
                                    for (int i = 0; i < u.Level.Length; i++)
                                    {
                                        if (u.Level[i])
                                        {
                                            if (!nick.StartsWith(connection.ServerSetting.StatusModes[1][i].ToString()))
                                                nick = connection.ServerSetting.StatusModes[1][i] + nick;
                                            break;
                                        }
                                    }

                                    t.RemoveNick(oldnick);
                                    t.AddNick(nick, true);
                                    t.LastMessageType = ServerMessageType.Other;
                                }

                                if ((nickList.CurrentWindow.TabCaption == t.TabCaption) && (nickList.CurrentWindow.Connection == t.Connection))
                                    nickList.RefreshList(t);

                            }
                        }
                        else if (t.WindowStyle == IceTabPage.WindowType.Query)
                        {

                            if (t.TabCaption == newnick)
                            {
                                string msg = GetMessageFormat("Nick Change");
                                msg = msg.Replace("$nick", oldnick);
                                msg = msg.Replace("$newnick", newnick);
                                msg = msg.Replace("$host", host);

                                if (connection.ServerSetting.NickName == newnick)
                                {
                                    t.TextWindow.AppendText(msg, 1);
                                    if ((inputPanel.CurrentConnection == connection) && (CurrentWindowType == IceTabPage.WindowType.Query))
                                    {
                                        if (CurrentWindow == t)
                                            StatusText(t.Connection.ServerSetting.NickName + " in " + t.TabCaption + " [" + t.ChannelModes + "] {" + t.Connection.ServerSetting.RealServerName + "}");
                                    }
                                }
                                else
                                    t.TextWindow.AppendText(msg, 1);

                                if ((nickList.CurrentWindow.TabCaption == newnick) && (nickList.CurrentWindow.Connection == t.Connection))
                                    nickList.RefreshList(t);

                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorFile("OnChangeNick Error:" + e.Message, e.StackTrace);
            }
        }

        /// <summary>
        /// Channel Topic Changed
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="channel">Which Channel the Topic changed for</param>
        /// <param name="nick">Nick who changed the Topic</param>
        /// <param name="topic">New Channel Topic</param>
        private void OnChannelTopic(IRCConnection connection, string channel, string nick, string host, string topic)
        {
            IceTabPage t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            if (t != null)
            {

                //t.ChannelTopic = topic;
                string msgt = GetMessageFormat("Channel Topic Text");
                msgt = msgt.Replace("$channel", channel);
                msgt = msgt.Replace("$topic", topic);
                t.ChannelTopic = msgt;

                if (nick.Length > 0)
                {
                    string msg = GetMessageFormat("Channel Topic Change");
                    msg = msg.Replace("$nick", nick);
                    msg = msg.Replace("$host", host);
                    msg = msg.Replace("$channel", channel);
                    msg = msg.Replace("$topic", topic);
                    t.TextWindow.AppendText(msg, 1);
                }
                else
                {
                    t.TextWindow.AppendText(msgt, 1);
                }

                t.LastMessageType = ServerMessageType.Other;
            }
        }

        /// <summary>
        /// Your User Mode for the Server has Changed
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="nick">Your Nick Name</param>
        /// <param name="mode">New User Mode(s)</param>
        private void OnUserMode(IRCConnection connection, string nick, string mode)
        {
            string msg = GetMessageFormat("Server Mode");
            msg = msg.Replace("$server", connection.ServerSetting.ServerName);
            msg = msg.Replace("$mode", mode);
            msg = msg.Replace("$nick", nick);

            mainTabControl.GetTabPage("Console").AddText(connection, msg, 1, false);

            //parse out the user modes
            //set the mode in Server Setting

        }

        /// <summary>
        /// Channel Mode Changed
        /// </summary>
        /// <param name="connection">Which Connection it came from</param>
        /// <param name="modeSetter">Who set the mode(s)</param>
        /// <param name="channel">Channel which mode change is for</param>
        /// <param name="fullmode">All the modes and parameters</param>
        private void OnChannelMode(IRCConnection connection, string modeSetter, string modeSetterHost, string channel, string fullmode)
        {
            try
            {
                string mode = "";
                string parameter = "";

                if (fullmode.IndexOf(' ') == -1)
                {
                    mode = fullmode;
                }
                else
                {
                    mode = fullmode.Substring(0, fullmode.IndexOf(' '));
                    parameter = fullmode.Substring(fullmode.IndexOf(' ') + 1);
                }

                string msg = GetMessageFormat("Channel Mode");
                msg = msg.Replace("$modeparam", parameter);
                msg = msg.Replace("$mode", mode);
                msg = msg.Replace("$nick", modeSetter);
                msg = msg.Replace("$host", modeSetterHost);
                msg = msg.Replace("$channel", channel);

                //TabWindow chan = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
                IceTabPage chan = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
                if (chan != null)
                {
                    //chan.UpdateNick(modeSetter, modeSetterHost);

                    if (modeSetter != channel)
                    {
                        //System.Diagnostics.Debug.WriteLine(msg);
                        WindowMessage(connection, channel, msg, 1, false);
                        //now update the modes accordingly

                    }
                    else
                    {
                        chan.ChannelModes = fullmode.Trim();

                    }


                    string[] parameters = parameter.Split(new char[] { ' ' });

                    bool addMode = false;
                    int modelength = mode.Length;
                    string temp;

                    IEnumerator parametersEnumerator = parameters.GetEnumerator();
                    parametersEnumerator.MoveNext();
                    for (int i = 0; i < modelength; i++)
                    {
                        switch (mode[i])
                        {
                            case '-':
                                addMode = false;
                                break;
                            case '+':
                                addMode = true;
                                break;
                            case 'b':
                                //handle bans seperately
                                temp = (string)parametersEnumerator.Current;
                                parametersEnumerator.MoveNext();
                                break;
                            default:
                                //check if it's a status mode which can vary by server
                                temp = (string)parametersEnumerator.Current;
                                for (int j = 0; j < connection.ServerSetting.StatusModes[0].Length; j++)
                                {
                                    if (mode[i] == connection.ServerSetting.StatusModes[0][j])
                                    {
                                        chan.UpdateNick(temp, connection.ServerSetting.StatusModes[1][j].ToString(), addMode);
                                        break;
                                    }
                                }

                                //check if the mode has a parameter (CHANMODES= from 005)
                                //System.Diagnostics.Debug.WriteLine(connection.ServerSetting.ChannelModeParams.ToString() + ":" + mode[i]);
                                if (connection.ServerSetting.ChannelModeParams.Contains(mode[i].ToString()))
                                {
                                    //mode has parameter
                                    temp = (string)parametersEnumerator.Current;
                                    parametersEnumerator.MoveNext();
                                    chan.UpdateChannelMode(mode[i], temp, addMode);
                                }
                                else
                                    //check if it is an actual channel mode, and not a user mode
                                    if (connection.ServerSetting.ChannelModeNoParams.Contains(mode[i].ToString()))
                                        chan.UpdateChannelMode(mode[i], addMode);
                                break;

                        }
                    }
                    if (inputPanel.CurrentConnection == connection)
                    {
                        if (mainTabControl.CurrentTab == chan)
                            StatusText(connection.ServerSetting.NickName + " in " + chan.TabCaption + " [" + chan.ChannelModes + "] {" + chan.Connection.ServerSetting.RealServerName + "}");
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorFile("OnChannelMode Error:" + e.Message, e.StackTrace);
            }
        }

        /// <summary>
        /// When a User Invites you to a Channel
        /// </summary>
        /// <param name="connection">Which connection it came from</param>
        /// <param name="channel">The channel you are being invited to</param>
        /// <param name="nick">The nick who invited you</param>
        /// <param name="host">The host of the nick who invited you</param>
        private void OnChannelInvite(IRCConnection connection, string channel, string nick, string host)
        {
            string msg = GetMessageFormat("Channel Invite");
            msg = msg.Replace("$channel", channel).Replace("$nick", nick).Replace("$host", host);
            //((ConsoleTabWindow)tabMain.TabPages[0]).AddText(connection, msg, 1, false);
            mainTabControl.GetTabPage("Console").AddText(connection, msg, 1, false);

        }

        /// <summary>
        /// Received a Channel Notice
        /// </summary>
        /// <param name="connection">The connection the notice was received on</param>
        /// <param name="nick">The nick who sent the notice</param>
        /// <param name="host">The host of the nick who sent the notice</param>
        /// <param name="status">The status char that the notice was sent to</param>
        /// <param name="channel">The channel the notice was sent to</param>
        /// <param name="notice">The notice message</param>
        private void OnChannelNotice(IRCConnection connection, string nick, string host, char status, string channel, string message)
        {
            IceTabPage t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            //TabWindow t = GetWindow(connection, channel, IceTabPage.WindowType.Channel);
            if (t != null)
            {
                string msg = GetMessageFormat("Channel Notice");
                msg = msg.Replace("$nick", nick);
                msg = msg.Replace("$host", host);
                if (status == '0')
                    msg = msg.Replace("$status", "");
                else
                    msg = msg.Replace("$status", status.ToString());

                msg = msg.Replace("$channel", channel);
                msg = msg.Replace("$message", message);
                t.TextWindow.AppendText(msg, 1);

                t.LastMessageType = ServerMessageType.Message;
            }
        }

        /// <summary>
        /// Shows raw Server Data in a Debug Window
        /// </summary>
        /// <param name="connection">The connection the notice was received on</param>
        /// <param name="data">The Raw Server Data</param>
        private void OnRawServerData(IRCConnection connection, string data)
        {
            //check if a Debug Window is open
            IceTabPage t = GetWindow(null, "Debug", IceTabPage.WindowType.Debug);
            if (t != null)
                t.TextWindow.AppendText(connection.ServerSetting.ID + ":" + data, 1);

            PluginArgs args = new PluginArgs(connection);
            args.Message = data;
            foreach (IPluginIceChat ipc in loadedPlugins)
            {
                ipc.ServerRaw(args);
                if (args.Command != null)
                {
                    if (args.Command.Length > 0)
                        ParseOutGoingCommand(connection, args.Command);
                }
            }

        }

        private void OnRawServerOutgoingData(IRCConnection connection, string data)
        {
            //check if a Debug Window is open
            IceTabPage t = GetWindow(null, "Debug", IceTabPage.WindowType.Debug);
            if (t != null)
                t.TextWindow.AppendText("-" + connection.ServerSetting.ID + ":" + data, 1);
        }


        private void OnIALUserData(IRCConnection connection, string nick, string host, string channel)
        {
            //internal addresslist userdata            
            for (int i = 0; i < connection.ServerSetting.StatusModes[1].Length; i++)
                nick = nick.Replace(connection.ServerSetting.StatusModes[1][i].ToString(), string.Empty);

            InternalAddressList ial = new InternalAddressList(nick, host, channel);

            if (!connection.ServerSetting.IAL.ContainsKey(nick))
                connection.ServerSetting.IAL.Add(nick, ial);
            else
            {
                ((InternalAddressList)connection.ServerSetting.IAL[nick]).AddChannel(channel);
                ((InternalAddressList)connection.ServerSetting.IAL[nick]).Host = host;
            }

        }

        private void OnIALUserChange(IRCConnection connection, string oldnick, string newnick)
        {
            //change a nickname in the IAL list
            if (connection.ServerSetting.IAL.ContainsKey(oldnick))
            {
                InternalAddressList ial = (InternalAddressList)connection.ServerSetting.IAL[oldnick];
                connection.ServerSetting.IAL.Remove(oldnick);
                ial.Nick = newnick;
                connection.ServerSetting.IAL.Add(newnick, ial);
            }
        }

        private void OnIALUserQuit(IRCConnection connection, string nick)
        {
            //user has quit, remove from IAL
            if (connection.ServerSetting.IAL.ContainsKey(nick))
                connection.ServerSetting.IAL.Remove(nick);

        }
        private void OnIALUserPart(IRCConnection connection, string nick, string channel)
        {
            //user left a channel, remove from channel list
            InternalAddressList ial = (InternalAddressList)connection.ServerSetting.IAL[nick];
            if (ial != null)
            {
                ial.RemoveChannel(channel);
                //if channels count is 0, remove the nick from the ial
                if (ial.Channels.Count == 0)
                    connection.ServerSetting.IAL.Remove(nick);
            }
        }


    
    }
}