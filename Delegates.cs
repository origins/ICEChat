﻿/******************************************************************************\
 * IceChat 2009 Internet Relay Chat Client
 *
 * Copyright (C) 2009 Paul Vanderzee <snerf@icechat.net>
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
using System.Text;

namespace IceChat2009
{
    //public delegate void OutgoingMessageDelegate(IRCConnection connection, string window, string data, int color);
    public delegate void OutGoingCommandDelegate(IRCConnection connection, string data);
    public delegate void RawServerIncomingDataDelegate(IRCConnection connection, string data);
    public delegate void RawServerOutgoingDataDelegate(IRCConnection connection, string data);
    
    public delegate void ChannelMessageDelegate(IRCConnection connection, string channel, string nick, string host, string message);
    public delegate void ChannelActionDelegate(IRCConnection connection, string channel, string nick, string host, string message);
    public delegate void QueryMessageDelegate(IRCConnection connection, string nick, string host, string message);
    public delegate void QueryActionDelegate(IRCConnection connection, string nick, string host, string message);
    public delegate void GenericChannelMessageDelegate(IRCConnection connection, string channel, string message);
    public delegate void ChannelNoticeDelegate(IRCConnection connection, string nick, string host, char status, string channel, string message);
    
    public delegate void JoinChannelDelegate(IRCConnection connection, string channel, string nick, string host, bool refresh);
    public delegate void PartChannelDelegate(IRCConnection connection, string channel, string nick, string host, string reason);
    public delegate void QuitServerDelegate(IRCConnection connection, string nick, string host, string reason);

    public delegate void AddNickNameDelegate(IRCConnection connection, string channel, string nick);
    public delegate void RemoveNickNameDelegate(IRCConnection connection, string channel, string nick);
    public delegate void ClearNickListDelegate(IRCConnection connection, string channel);

    public delegate void KickNickDelegate(IRCConnection connection, string channel, string nick, string reason, string kickUser);
    public delegate void KickMyselfDelegate(IRCConnection connection, string channel, string reason, string kickUser);
    public delegate void ChangeNickDelegate(IRCConnection connection, string oldnick, string newnick, string host);
    public delegate void UserNoticeDelegate(IRCConnection connection, string nick, string message);
    public delegate void ServerNoticeDelegate(IRCConnection connection, string message);

    public delegate void JoinChannelMyselfDelegate(IRCConnection connection, string channel);
    public delegate void PartChannelMyselfDelegate(IRCConnection connection, string channel);

    public delegate void ChannelTopicDelegate(IRCConnection connection, string channel, string nick, string host, string topic);
    
    public delegate void UserModeChangeDelegate(IRCConnection connection, string nick, string mode);
    public delegate void ChannelModeChangeDelegate(IRCConnection connection, string modeSetter, string modeSetterHost, string channel, string fullmode);

    public delegate void ServerMessageDelegate(IRCConnection connection, string message);
    public delegate void ServerMOTDDelegate(IRCConnection connection, string message);    
    public delegate void ServerErrorDelegate(IRCConnection connection, string message);
    public delegate void WhoisDataDelegate(IRCConnection connection, string nick, string data);
    public delegate void CtcpMessageDelegate(IRCConnection connection, string nick, string ctcp);

    public delegate void ChannelListDelegate(IRCConnection connection, string channel, string users, string topic);
    public delegate void ChannelInviteDelegate(IRCConnection connection, string channel, string nick, string host);

    //for the Server Tree
    public delegate void NewServerConnectionDelegate(ServerSetting serverSetting);
    
    //public delegate void ServerDisconnectDelegate(object sender);
    //public delegate void ServerAddDelegate(object sender, ServerSetting serverSetting);
    
    //public delegate void AddConsoleTabDelegate(IRCConnection connection);


    //public delegate void WindowMessageDelegate(IRCConnection connection, string name, string data, int color);
    //public delegate void SaveServersDelegate();


}
