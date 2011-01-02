﻿/******************************************************************************\
 * IceChat 2009 Internet Relay Chat Client
 *
 * Copyright (C) 2011 Paul Vanderzee <snerf@icechat.net>
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;


namespace IceChat
{
    internal class DccFileStruct
    {
        public Thread Thread;
        public TcpClient Socket;
        public FileStream FileStream;
        public IPAddress IPAddress;
        public IRCConnection Connection;
        
        public TcpListener PassiveSocket;
        public Thread PassiveThread;
        public string passiveID;

        public string Nick;
        public string Host;
        public string Ip;
        public string FileName;
        public string Path;
        public string Port;

        public uint FileSize;
        public uint TotalBytesRead;
        public uint StartFileSize;
        public bool Finished;
        public bool Resume;
        public long StartTime;

        public string Style;        //upload or download
        public int ListingTag;
    }

    public class IceTabPageDCCFile : IceTabPage
    {
        private List<DccFileStruct> dccFiles = new List<DccFileStruct>();
        private delegate void AddDCCFileDelegate(DccFileStruct dcc);
        private delegate void UpdateDCCFileProgressDelegate(DccFileStruct dcc);
        private FlickerFreeListView dccFileList;


        private void InitializeComponent()
        {
            this.dccFileList = new FlickerFreeListView();
            this.dccFileList.SuspendLayout();
            this.SuspendLayout();
            
            Panel dccPanel = new Panel();
            dccPanel.BackColor = Color.LightGray;
            dccPanel.Size = new Size(this.Width, 45);
            dccPanel.Dock = DockStyle.Bottom;

            Button dccCancel = new Button();
            dccCancel.Name = "dccCancel";
            dccCancel.Click += new EventHandler(dccCancel_Click);
            dccCancel.Location = new Point(5, 5);
            dccCancel.Size = new Size(100, 35);
            dccCancel.Text = "Cancel";
            dccCancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dccCancel.UseVisualStyleBackColor = true;
            dccPanel.Controls.Add(dccCancel);

            Button dccOpen = new Button();
            dccOpen.Name = "dccOpen";
            dccOpen.Click += new EventHandler(dccOpen_Click);
            dccOpen.Location = new Point(110, 5);
            dccOpen.Size = new Size(100, 35);
            dccOpen.Text = "Open Folder";
            dccOpen.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dccOpen.UseVisualStyleBackColor = true;
            dccPanel.Controls.Add(dccOpen);

            Button dccRemove = new Button();
            dccRemove.Name = "dccRemove";
            dccRemove.Click += new EventHandler(dccRemove_Click);
            dccRemove.Location = new Point(220, 5);
            dccRemove.Size = new Size(100, 35);
            dccRemove.Text = "Remove";
            dccRemove.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dccRemove.UseVisualStyleBackColor = true;
            dccPanel.Controls.Add(dccRemove);

            this.dccFileList.Dock = DockStyle.Fill;
            this.dccFileList.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dccFileList.View = View.Details;
            this.dccFileList.ShowItemToolTips = true;
            this.dccFileList.MultiSelect = false;
            this.dccFileList.FullRowSelect = true;
            this.dccFileList.HideSelection = false;
            this.dccFileList.DoubleClick += new EventHandler(dccFileList_DoubleClick);

            ColumnHeader fn = new ColumnHeader();
            fn.Text = "File Name";
            fn.Width = 200;
            fn.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.dccFileList.Columns.Add(fn);

            ColumnHeader n = new ColumnHeader();
            n.Text = "Nick";
            n.Width = 80;
            this.dccFileList.Columns.Add(n);

            ColumnHeader fs = new ColumnHeader();
            fs.Text = "File Size";
            fs.Width = 150;
            this.dccFileList.Columns.Add(fs);

            ColumnHeader sp = new ColumnHeader();
            sp.Text = "Speed";
            sp.Width = 100;
            this.dccFileList.Columns.Add(sp);

            ColumnHeader el = new ColumnHeader();
            el.Text = "Elapsed";
            el.Width = 100;
            this.dccFileList.Columns.Add(el);

            ColumnHeader s = new ColumnHeader();
            s.Text = "Status";
            s.Width = 100;
            s.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.dccFileList.Columns.Add(s);

            //store the dcc file style (upload/download)
            ColumnHeader st = new ColumnHeader();
            st.Text = "Style";
            st.Width = 0;
            st.AutoResize(ColumnHeaderAutoResizeStyle.None);
            this.dccFileList.Columns.Add(st);
            this.dccFileList.Columns[6].Width = 0;

            //store the server id of the connection
            ColumnHeader sid = new ColumnHeader();
            sid.Text = "ServerID";
            sid.Width = 0;
            sid.AutoResize(ColumnHeaderAutoResizeStyle.None);
            this.dccFileList.Columns.Add(sid);
            this.dccFileList.Columns[7].Width = 0;


            //store the path/folder for the dcc file
            ColumnHeader pa = new ColumnHeader();
            pa.Text = "Path";
            pa.Width = 0;
            pa.AutoResize(ColumnHeaderAutoResizeStyle.None);
            this.dccFileList.Columns.Add(pa);
            this.dccFileList.Columns[8].Width = 0;

            this.Controls.Add(dccFileList);
            this.Controls.Add(dccPanel);
            this.dccFileList.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        public IceTabPageDCCFile(WindowType windowType, string sCaption) : base(windowType, sCaption)
        {

            InitializeComponent();
            
            dccFiles = new List<DccFileStruct>();

        }
        
        protected override void Dispose(bool disposing)
        {
            foreach (DccFileStruct dcc in dccFiles)
            {
                if (dcc.Thread != null)
                    if (dcc.Thread.IsAlive)
                        dcc.Thread.Abort();

                try
                {
                    if (dcc.Socket.Connected)
                        dcc.Socket.Close();
                }
                catch { }

                if (!dcc.Finished)
                {
                    try
                    {
                        if (dcc.FileStream != null)
                        {
                            dcc.FileStream.Flush();
                            dcc.FileStream.Close();
                        }
                    }
                    catch { }
                }
            }

            dccFiles.Clear();

        }

        /// <summary>
        /// Open the folder for the File Double Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dccFileList_DoubleClick(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in dccFileList.Items)
            {
                if (lvi.Selected)
                {
                    //find the item, and open the folder
                    foreach (DccFileStruct dcc in dccFiles)
                    {
                        if (dcc.ListingTag.ToString() == lvi.Tag.ToString())
                        {
                            System.Diagnostics.Process.Start(dcc.Path);
                            return;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Remove a cancelled or completed item from the Dcc File List
        /// </summary>
        private void dccRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in dccFileList.Items)
            {
                if (lvi.Selected)
                {
                    //check if it is finished
                    if (lvi.SubItems[5].Text == "Completed" || lvi.SubItems[5].Text == "Cancelled")
                    {
                        //we can remove this item
                        dccFileList.Items.Remove(lvi);
                        return;
                    }
                    else
                    {
                        //it is not finished, do you wish to cancel it?
                        //find the appropriate matching item                        
                        foreach (DccFileStruct dcc in dccFiles)
                        {
                            if (dcc.ListingTag.ToString() == lvi.Tag.ToString())
                            {
                                DialogResult dialog = MessageBox.Show("The file is still in progress, do you wish to cancel it?", "Cancel File", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.No)
                                    return;
                                
                                if (dcc.Socket != null)
                                    dcc.Socket.Close();
                                
                                lvi.SubItems[5].Text = "Cancelled";
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Open the folder for the File Selected
        /// </summary>
        private void dccOpen_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in dccFileList.Items)
            {
                if (lvi.Selected)
                {
                    //find the item, and open the folder
                    foreach (DccFileStruct dcc in dccFiles)
                    {
                        if (dcc.ListingTag.ToString() == lvi.Tag.ToString())
                        {
                            System.Diagnostics.Process.Start(dcc.Path);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Cancel the File Transfer for the File Selected
        /// </summary>
        private void dccCancel_Click(object sender, EventArgs e)
        {
            //find which item is being asked to be canceled
            foreach (ListViewItem lvi in dccFileList.Items)
            {
                if (lvi.Selected)
                {
                    //check if it is finished
                    if (lvi.SubItems[5].Text == "Completed" || lvi.SubItems[5].Text == "Cancelled")
                    {
                        //do nothing
                        return;
                    }
                    else
                    {
                        //it is not finished, do you wish to cancel it?
                        //find the appropriate matching item                        
                        foreach (DccFileStruct dcc in dccFiles)
                        {
                            if (dcc.ListingTag.ToString() == lvi.Tag.ToString())
                            {
                                DialogResult dialog = MessageBox.Show("The file is still in progress, do you wish to cancel it?", "Cancel File", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.No)
                                    return;
                                
                                if (dcc.Socket != null)
                                    dcc.Socket.Close();
                                
                                lvi.SubItems[5].Text = "Cancelled";
                                return;
                            }
                        }
                    }
                }
            }
        }

        internal void ResumeDCCFile(IRCConnection connection, string port, uint filePos)
        {
            for (int i = 0; i < dccFiles.Count; i++)
            {
                if (dccFiles[i].Port == port && dccFiles[i].Connection == connection)
                {
                    IPAddress ipAddr = LongToIPAddress(dccFiles[i].Ip);
                    IPEndPoint ep = new IPEndPoint(ipAddr, Convert.ToInt32(dccFiles[i].Port));

                    dccFiles[i].IPAddress = ipAddr;
                    dccFiles[i].Socket = null;
                    dccFiles[i].Socket = new TcpClient();
                    dccFiles[i].Socket.Connect(ep);
                    if (dccFiles[i].Socket.Connected)
                    {
                        System.Diagnostics.Debug.WriteLine("start dcc resume thread");
                        dccFiles[i].Thread = new Thread(new ParameterizedThreadStart(GetDCCData));
                        dccFiles[i].Thread.Start(dccFiles[i]);
                    }
                }
            }
        }

        internal void StartDCCPassive(IRCConnection connection, string nick, string host, string ip, string file, uint fileSize, string id)
        {
            //open a new dcc listening port, and send back to the client
            System.Diagnostics.Debug.WriteLine("start passive dcc - open listener:" + id);
            DccFileStruct dcc = new DccFileStruct();
            dcc.FileName = file;
            dcc.FileSize = fileSize;
            dcc.StartFileSize = 0;
            dcc.Nick = nick;
            dcc.Host = host;
            dcc.Connection = connection;
            dcc.Ip = ip;
            dcc.passiveID = id;

            dcc.Style = "Passive";

            //pick a random incoming port
            Random port = new Random();
            int p = port.Next(FormMain.Instance.IceChatOptions.DCCPortLower, FormMain.Instance.IceChatOptions.DCCPortUpper);
            dcc.Port = p.ToString();

            //create a random number for a tag
            dcc.ListingTag = RandomListingTag();

            try
            {
                string dccPath = FormMain.Instance.IceChatOptions.DCCReceiveFolder;
                //check to make sure the folder exists
                if (!Directory.Exists(dccPath))
                {
                    //add a folder browsing dialog here
                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                    if (fbd.ShowDialog() == DialogResult.OK)
                        dccPath = fbd.SelectedPath;
                    else
                    {
                        //no folder selected, out we go
                        System.Diagnostics.Debug.WriteLine("PASSIVE No folder selected, non-existant dcc receive folder");
                        FormMain.Instance.WindowMessage(connection, "Console", "DCC Passive File Received Failed : DCC Receive Path does not exists", 4, true);
                        return;
                    }
                }

                //check if the file exists
                if (File.Exists(dccPath + System.IO.Path.DirectorySeparatorChar + dcc.FileName))
                {
                    //check the local file size and compare to what is being sent
                    FileInfo fi = new FileInfo(dccPath + System.IO.Path.DirectorySeparatorChar + dcc.FileName);
                    if (fi.Length <= dcc.FileSize)
                    {
                        System.Diagnostics.Debug.WriteLine("PASSIVE appending file:" + fi.Length + ":" + dcc.FileSize + ":" + connection.IsFullyConnected);
                        //send DCC RESUME
                        //wait for a DCC ACCEPT from client, and start resume on this port
                        connection.SendData("PRIVMSG " + nick + " :\x0001DCC RESUME \"" + dcc.FileName + "\" " + dcc.Port + " " + fi.Length.ToString() + "\x0001");
                        dcc.Resume = true;
                        dcc.TotalBytesRead = (uint)fi.Length;
                        dcc.FileStream = new FileStream(dccPath + System.IO.Path.DirectorySeparatorChar + dcc.FileName, FileMode.Append);
                        dcc.Path = dccPath;
                        dcc.StartFileSize = dcc.TotalBytesRead;
                        dccFiles.Add(dcc);
                        return;
                    }
                    else
                    {
                        //file exists, and already complete // set a new filename adding [#] to the end of the fielname
                        int extPos = dcc.FileName.LastIndexOf('.');
                        if (extPos == -1)
                        {
                            int i = 0;
                            do
                            {
                                i++;
                            } while (File.Exists(dccPath + System.IO.Path.DirectorySeparatorChar + dcc.FileName + "(" + i.ToString() + ")"));
                            dcc.FileName += "(" + i.ToString() + ")";
                        }
                        else
                        {
                            string fileName = dcc.FileName.Substring(0, extPos);
                            string ext = dcc.FileName.Substring(extPos + 1);
                            int i = 0;
                            do
                            {
                                i++;
                            } while (File.Exists(dccPath + System.IO.Path.DirectorySeparatorChar + fileName + "(" + i.ToString() + ")." + ext));
                            dcc.FileName = fileName + "(" + i.ToString() + ")." + ext;
                        }
                    }
                }

                dcc.FileStream = new FileStream(dccPath + System.IO.Path.DirectorySeparatorChar + dcc.FileName, FileMode.Create);
                dcc.Path = dccPath;

                dcc.PassiveSocket = new TcpListener(new IPEndPoint(IPAddress.Any, Convert.ToInt32(p)));
                dcc.PassiveThread = new Thread(new ParameterizedThreadStart(StartPassiveSocket));
                dcc.PassiveThread.Start(dcc);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("passive dcc file error:" + ex.Message);
            }
        }

        internal void StartDCCFile(IRCConnection connection, string nick, string host, string ip, string port, string file, uint fileSize)
        {
            DccFileStruct dcc = new DccFileStruct();
            dcc.FileName = file;
            dcc.FileSize = fileSize;
            dcc.StartFileSize = 0;
            dcc.Socket = new TcpClient();
            dcc.Nick = nick;
            dcc.Host = host;
            dcc.Style = "Download";
            dcc.Connection = connection;
            dcc.Port = port;
            dcc.Ip = ip;

            //create a random number for a tag
            dcc.ListingTag = RandomListingTag();

            try
            {
                string dccPath = FormMain.Instance.IceChatOptions.DCCReceiveFolder;
                //check to make sure the folder exists
                if (!Directory.Exists(dccPath))
                {
                    //add a folder browsing dialog here
                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                    if (fbd.ShowDialog() == DialogResult.OK)
                        dccPath = fbd.SelectedPath;
                    else
                    {
                        //no folder selected, out we go
                        System.Diagnostics.Debug.WriteLine("No folder selected, non-existant dcc receive folder");
                        FormMain.Instance.WindowMessage(connection, "Console", "DCC File Received Failed : DCC Receive Path does not exists", 4, true);
                        return;
                    }
                }
                
                //check if the file exists
                if (File.Exists(dccPath + System.IO.Path.DirectorySeparatorChar + dcc.FileName))
                {
                    //check the local file size and compare to what is being sent
                    FileInfo fi = new FileInfo(dccPath + System.IO.Path.DirectorySeparatorChar + dcc.FileName);
                    if (fi.Length <= dcc.FileSize)
                    {
                        System.Diagnostics.Debug.WriteLine("appending file:" + fi.Length + ":" + dcc.FileSize + ":" + connection.IsFullyConnected);
                        //send DCC RESUME
                        //wait for a DCC ACCEPT from client, and start resume on this port
                        connection.SendData("PRIVMSG " + nick + " :\x0001DCC RESUME \"" + dcc.FileName + "\" " + port + " " + fi.Length.ToString() + "\x0001");
                        dcc.Resume = true;
                        dcc.TotalBytesRead = (uint)fi.Length;
                        dcc.FileStream = new FileStream(dccPath + System.IO.Path.DirectorySeparatorChar + dcc.FileName, FileMode.Append);
                        dcc.Path = dccPath;
                        dcc.StartFileSize = dcc.TotalBytesRead;
                        dccFiles.Add(dcc);
                        return;
                    }
                    else
                    {
                        //file exists, and already complete // set a new filename adding [#] to the end of the fielname
                        int extPos = dcc.FileName.LastIndexOf('.');
                        if (extPos == -1)
                        {
                            int i = 0;
                            do
                            {
                                i++;
                            } while (File.Exists(dccPath + System.IO.Path.DirectorySeparatorChar + dcc.FileName + "(" + i.ToString() + ")"));
                            dcc.FileName += "(" + i.ToString() + ")";
                        }
                        else
                        {
                            string fileName = dcc.FileName.Substring(0, extPos);
                            string ext = dcc.FileName.Substring(extPos + 1);
                            int i = 0;
                            do
                            {
                                i++;
                            } while (File.Exists(dccPath + System.IO.Path.DirectorySeparatorChar + fileName + "(" + i.ToString() + ")." + ext));
                            dcc.FileName = fileName + "(" + i.ToString() + ")." + ext;
                        }
                    }
                }

                IPAddress ipAddr = LongToIPAddress(ip);
                IPEndPoint ep = new IPEndPoint(ipAddr, Convert.ToInt32(port));

                dcc.IPAddress = ipAddr;
                
                dcc.Socket.Connect(ep);
                if (dcc.Socket.Connected)
                {
                    dcc.FileStream = new FileStream(dccPath + System.IO.Path.DirectorySeparatorChar + dcc.FileName, FileMode.Create);
                    dcc.Path = dccPath;

                    //start the thread to get the data
                    dcc.Thread = new Thread(new ParameterizedThreadStart(GetDCCData));
                    dcc.Thread.Start(dcc);
                    
                }
            }
            catch (SocketException se)
            {
                System.Diagnostics.Debug.WriteLine("dcc file connection error:" + se.Message);
            }
        }

        /// <summary>
        /// Add the specific Download File/Data to the DCC File List
        /// </summary>
        private void AddDCCFile(DccFileStruct dcc)
        {
            if (this.InvokeRequired)
            {
                AddDCCFileDelegate add = new AddDCCFileDelegate(AddDCCFile);
                this.Invoke(add, new object[] { dcc });
            }
            else
            {
                if (dcc.Resume)
                {
                    //try and find a match and continue on with that one
                    foreach (ListViewItem l in dccFileList.Items)
                    {
                        if (l.Text == dcc.FileName && l.SubItems[1].Text == dcc.Nick && l.SubItems[7].Text == dcc.Connection.ServerSetting.ID.ToString())
                        {
                            //close enough for a match, use this file
                            l.Tag = dcc.ListingTag;
                            return;
                        }
                    }
                }
                
                ListViewItem lvi = new ListViewItem(dcc.FileName);
                lvi.SubItems.Add(dcc.Nick);
                lvi.SubItems.Add(dcc.FileSize.ToString());
                lvi.SubItems.Add("");   //speed blank initially
                if (dcc.Resume)
                    lvi.SubItems.Add("Resuming");
                else
                    lvi.SubItems.Add("Status");
                lvi.SubItems.Add(dcc.Style);
                lvi.SubItems.Add(dcc.Connection.ServerSetting.ID.ToString());
                lvi.Tag = dcc.ListingTag;
                lvi.ToolTipText = dcc.FileName;                
                dccFileList.Items.Add(lvi);
            }
        }

        private void StartPassiveSocket(object dccObject)
        {
            DccFileStruct dcc = (DccFileStruct)dccObject;

            dcc.PassiveSocket.Start();
            bool keepListeningPassive = true;

            string localIP = IPAddressToLong(dcc.Connection.ServerSetting.LocalIP).ToString();
            if (FormMain.Instance.IceChatOptions.DCCLocalIP != null && FormMain.Instance.IceChatOptions.DCCLocalIP.Length > 0)
            {
                localIP = IPAddressToLong(IPAddress.Parse(FormMain.Instance.IceChatOptions.DCCLocalIP)).ToString();
            }

            dcc.Connection.SendData("PRIVMSG " + dcc.Nick + " :DCC SEND " + dcc.FileName + " " + localIP + " " + dcc.Port + " " + dcc.FileSize + " " + dcc.passiveID + "");
            System.Diagnostics.Debug.WriteLine("PRIVMSG " + dcc.Nick + " :DCC SEND " + dcc.FileName + " " + localIP + " " + dcc.Port + " " + dcc.FileSize + " " + dcc.passiveID + "");

            while (keepListeningPassive)
            {
                dcc.Socket = dcc.PassiveSocket.AcceptTcpClient();
                dcc.PassiveSocket.Stop();

                //string msg = FormMain.Instance.GetMessageFormat("DCC Passive Connect");
                //msg = msg.Replace("$nick", dcc.Nick);
                //textWindow.AppendText(msg, 1);
                System.Diagnostics.Debug.WriteLine("dcc passive socket connected with " + dcc.Nick);
                dcc.Thread = new Thread(new ParameterizedThreadStart(GetDCCData));
                dcc.Thread.Start(dcc);
                
                keepListeningPassive = false;
            }
            
            System.Diagnostics.Debug.WriteLine("startpassive socket completed");

        }
        
        /// <summary>
        /// Get the DCC File Data for the Specified DCC Object
        /// </summary>
        private void GetDCCData(object dccObject)
        {
            DccFileStruct dcc = (DccFileStruct)dccObject;
            
            //add it to the Download List
            AddDCCFile(dcc);
            
            if (!dcc.Resume)
                dccFiles.Add(dcc);

            dcc.StartTime = DateTime.Now.Ticks;

            while (true)
            {
                try
                {
                    int buffSize = 0;
                    byte[] buffer = new byte[8192];
                    NetworkStream ns = dcc.Socket.GetStream();
                    buffSize = dcc.Socket.ReceiveBufferSize;
                    int bytesRead = ns.Read(buffer, 0, buffSize);
                    //dcc file data
                    if (bytesRead == 0)
                    {
                        //we have a disconnection/error
                        break;
                    }
                    //write it to the file
                    if (dcc.FileStream != null)
                    {
                        dcc.FileStream.Write(buffer, 0, bytesRead);
                        dcc.FileStream.Flush();
                        dcc.TotalBytesRead += (uint)bytesRead;

                        //update the UI progress bar accordingly
                        UpdateDCCFileProgress(dcc);
                        if (dcc.TotalBytesRead == dcc.FileSize)
                        {
                            System.Diagnostics.Debug.WriteLine("should be finished");
                            dcc.Finished = true;
                            break;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("null filestream");
                        break;
                    }
                }
                catch (SocketException se)
                {
                    System.Diagnostics.Debug.WriteLine("GetDCCData Socket Exception:" + se.Message + ":" + se.StackTrace);
                }
                catch (Exception ex)
                {
                    //we have an error
                    System.Diagnostics.Debug.WriteLine("GetDCCData Error:" + ex.Message);
                    break;
                }

            }
            
            
            UpdateDCCFileProgress(dcc);
            
            System.Diagnostics.Debug.WriteLine("dcc file disconnected:" + dcc.TotalBytesRead + "/" + dcc.FileSize);
            
            try
            {
                dcc.FileStream.Flush();
            }
            catch { }
            
            dcc.FileStream.Close();
            dcc.Socket.Close();

        }

        private IPAddress LongToIPAddress(string longIP)
        {
            byte[] quads = BitConverter.GetBytes(long.Parse(longIP, System.Globalization.CultureInfo.InvariantCulture));
            return IPAddress.Parse(quads[3] + "." + quads[2] + "." + quads[1] + "." + quads[0]);
        }

        private long IPAddressToLong(IPAddress ip)
        {
            byte[] bytes = ip.GetAddressBytes();
            return (long)((bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3]);
        }

        private int RandomListingTag()
        {
            Random r = new Random();
            int t = r.Next(10000, 9999999);
            //check if currently in use by looking through dccFiles list


            return t;
        }

        /// <summary>
        /// Show the updated file progress in the DCC File List
        /// </summary>
        private void UpdateDCCFileProgress(DccFileStruct dcc)
        {
            if (this.InvokeRequired)
            {
                UpdateDCCFileProgressDelegate u = new UpdateDCCFileProgressDelegate(UpdateDCCFileProgress);
                this.Invoke(u, new object[] { dcc });
            }
            else
            {
                foreach (ListViewItem lvi in dccFileList.Items)
                {
                    if (lvi.Tag.ToString() == dcc.ListingTag.ToString())
                    {
                        lvi.SubItems[2].Text = dcc.TotalBytesRead + "/" + dcc.FileSize;
                        
                        //calculate the bp/sec
                        long elasped = DateTime.Now.Ticks - dcc.StartTime;
                        
                        if (elasped > 0 && (dcc.TotalBytesRead > dcc.StartFileSize))
                        {
                            float b = (elasped / 10000000f);
                            float bps = (dcc.TotalBytesRead - dcc.StartFileSize) / b;
                            
                            lvi.SubItems[3].Text = bps.ToString() + " b/s";
                            if (bps > 0)
                            {
                                //calculate speed and set to bytes/kb/mb/gb

                            }
                        }
                        else
                            lvi.SubItems[3].Text = "0 b/s";

                        lvi.SubItems[4].Text = GetDurationTicks(elasped);

                        if (dcc.TotalBytesRead == dcc.FileSize || dcc.Finished)
                            lvi.SubItems[5].Text = "Completed";
                        else if (dcc.Resume)
                            lvi.SubItems[5].Text = "Resuming";
                        else
                            lvi.SubItems[5].Text = "Downloading";

                        return;
                    }
                }
            }

        }
        private string GetDurationTicks(long ticks)
        {
            TimeSpan t = new TimeSpan(ticks);

            string s = t.Seconds.ToString() + "." + t.Milliseconds.ToString() + " secs";
            if (t.Minutes > 0)
                s = t.Minutes.ToString() + " mins " + s;
            if (t.Hours > 0)
                s = t.Hours.ToString() + " hrs " + s;
            if (t.Days > 0)
                s = t.Days.ToString() + " days " + s;

            return s;
        }

    }
}