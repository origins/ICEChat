namespace IceChat2009
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControlOptions = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.textTimeStamp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabDisplay = new System.Windows.Forms.TabPage();
            this.checkSaveWindowPosition = new System.Windows.Forms.CheckBox();
            this.tabLogging = new System.Windows.Forms.TabPage();
            this.checkLogQuery = new System.Windows.Forms.CheckBox();
            this.checkLogChannel = new System.Windows.Forms.CheckBox();
            this.checkLogConsole = new System.Windows.Forms.CheckBox();
            this.tabFonts = new System.Windows.Forms.TabPage();
            this.buttonInputFont = new System.Windows.Forms.Button();
            this.buttonServerListFont = new System.Windows.Forms.Button();
            this.textInputFont = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textInputFontSize = new System.Windows.Forms.TextBox();
            this.textServerListFontSize = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textServerListFont = new System.Windows.Forms.TextBox();
            this.buttonNickListFont = new System.Windows.Forms.Button();
            this.textNickListFontSize = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textNickListFont = new System.Windows.Forms.TextBox();
            this.buttonQueryFont = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textQueryFontSize = new System.Windows.Forms.TextBox();
            this.textQueryFont = new System.Windows.Forms.TextBox();
            this.buttonChannelFont = new System.Windows.Forms.Button();
            this.textChannelFontSize = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textChannelFont = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonConsoleFont = new System.Windows.Forms.Button();
            this.textConsoleFontSize = new System.Windows.Forms.TextBox();
            this.textConsoleFont = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabSounds = new System.Windows.Forms.TabPage();
            this.tabEvents = new System.Windows.Forms.TabPage();
            this.tabServer = new System.Windows.Forms.TabPage();
            this.checkIdentServer = new System.Windows.Forms.CheckBox();
            this.textDefaultNick = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tabControlOptions.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabDisplay.SuspendLayout();
            this.tabLogging.SuspendLayout();
            this.tabFonts.SuspendLayout();
            this.tabServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(420, 250);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(103, 32);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(541, 250);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 32);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabControlOptions
            // 
            this.tabControlOptions.Controls.Add(this.tabMain);
            this.tabControlOptions.Controls.Add(this.tabDisplay);
            this.tabControlOptions.Controls.Add(this.tabLogging);
            this.tabControlOptions.Controls.Add(this.tabFonts);
            this.tabControlOptions.Controls.Add(this.tabSounds);
            this.tabControlOptions.Controls.Add(this.tabEvents);
            this.tabControlOptions.Controls.Add(this.tabServer);
            this.tabControlOptions.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlOptions.Location = new System.Drawing.Point(0, 0);
            this.tabControlOptions.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlOptions.Name = "tabControlOptions";
            this.tabControlOptions.SelectedIndex = 0;
            this.tabControlOptions.Size = new System.Drawing.Size(637, 242);
            this.tabControlOptions.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.textTimeStamp);
            this.tabMain.Controls.Add(this.label2);
            this.tabMain.Location = new System.Drawing.Point(4, 25);
            this.tabMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(4);
            this.tabMain.Size = new System.Drawing.Size(629, 213);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main Settings";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // textTimeStamp
            // 
            this.textTimeStamp.Location = new System.Drawing.Point(115, 8);
            this.textTimeStamp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textTimeStamp.Name = "textTimeStamp";
            this.textTimeStamp.Size = new System.Drawing.Size(150, 23);
            this.textTimeStamp.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 40;
            this.label2.Text = "Time Stamp";
            // 
            // tabDisplay
            // 
            this.tabDisplay.Controls.Add(this.checkSaveWindowPosition);
            this.tabDisplay.Location = new System.Drawing.Point(4, 25);
            this.tabDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.tabDisplay.Name = "tabDisplay";
            this.tabDisplay.Padding = new System.Windows.Forms.Padding(4);
            this.tabDisplay.Size = new System.Drawing.Size(629, 213);
            this.tabDisplay.TabIndex = 1;
            this.tabDisplay.Text = "Display";
            this.tabDisplay.UseVisualStyleBackColor = true;
            // 
            // checkSaveWindowPosition
            // 
            this.checkSaveWindowPosition.AutoSize = true;
            this.checkSaveWindowPosition.Location = new System.Drawing.Point(12, 11);
            this.checkSaveWindowPosition.Name = "checkSaveWindowPosition";
            this.checkSaveWindowPosition.Size = new System.Drawing.Size(229, 20);
            this.checkSaveWindowPosition.TabIndex = 0;
            this.checkSaveWindowPosition.Text = "Save Window Positions on Exit";
            this.checkSaveWindowPosition.UseVisualStyleBackColor = true;
            // 
            // tabLogging
            // 
            this.tabLogging.Controls.Add(this.checkLogQuery);
            this.tabLogging.Controls.Add(this.checkLogChannel);
            this.tabLogging.Controls.Add(this.checkLogConsole);
            this.tabLogging.Location = new System.Drawing.Point(4, 25);
            this.tabLogging.Name = "tabLogging";
            this.tabLogging.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogging.Size = new System.Drawing.Size(629, 213);
            this.tabLogging.TabIndex = 6;
            this.tabLogging.Text = "Logging";
            this.tabLogging.UseVisualStyleBackColor = true;
            // 
            // checkLogQuery
            // 
            this.checkLogQuery.AutoSize = true;
            this.checkLogQuery.Location = new System.Drawing.Point(12, 63);
            this.checkLogQuery.Name = "checkLogQuery";
            this.checkLogQuery.Size = new System.Drawing.Size(66, 20);
            this.checkLogQuery.TabIndex = 3;
            this.checkLogQuery.Text = "Query";
            this.checkLogQuery.UseVisualStyleBackColor = true;
            // 
            // checkLogChannel
            // 
            this.checkLogChannel.AutoSize = true;
            this.checkLogChannel.Location = new System.Drawing.Point(12, 37);
            this.checkLogChannel.Name = "checkLogChannel";
            this.checkLogChannel.Size = new System.Drawing.Size(79, 20);
            this.checkLogChannel.TabIndex = 2;
            this.checkLogChannel.Text = "Channel";
            this.checkLogChannel.UseVisualStyleBackColor = true;
            // 
            // checkLogConsole
            // 
            this.checkLogConsole.AutoSize = true;
            this.checkLogConsole.Location = new System.Drawing.Point(12, 11);
            this.checkLogConsole.Name = "checkLogConsole";
            this.checkLogConsole.Size = new System.Drawing.Size(78, 20);
            this.checkLogConsole.TabIndex = 1;
            this.checkLogConsole.Text = "Console";
            this.checkLogConsole.UseVisualStyleBackColor = true;
            // 
            // tabFonts
            // 
            this.tabFonts.Controls.Add(this.buttonInputFont);
            this.tabFonts.Controls.Add(this.buttonServerListFont);
            this.tabFonts.Controls.Add(this.textInputFont);
            this.tabFonts.Controls.Add(this.label16);
            this.tabFonts.Controls.Add(this.textInputFontSize);
            this.tabFonts.Controls.Add(this.textServerListFontSize);
            this.tabFonts.Controls.Add(this.label15);
            this.tabFonts.Controls.Add(this.textServerListFont);
            this.tabFonts.Controls.Add(this.buttonNickListFont);
            this.tabFonts.Controls.Add(this.textNickListFontSize);
            this.tabFonts.Controls.Add(this.label14);
            this.tabFonts.Controls.Add(this.textNickListFont);
            this.tabFonts.Controls.Add(this.buttonQueryFont);
            this.tabFonts.Controls.Add(this.label10);
            this.tabFonts.Controls.Add(this.textQueryFontSize);
            this.tabFonts.Controls.Add(this.textQueryFont);
            this.tabFonts.Controls.Add(this.buttonChannelFont);
            this.tabFonts.Controls.Add(this.textChannelFontSize);
            this.tabFonts.Controls.Add(this.label9);
            this.tabFonts.Controls.Add(this.textChannelFont);
            this.tabFonts.Controls.Add(this.label8);
            this.tabFonts.Controls.Add(this.buttonConsoleFont);
            this.tabFonts.Controls.Add(this.textConsoleFontSize);
            this.tabFonts.Controls.Add(this.textConsoleFont);
            this.tabFonts.Controls.Add(this.label7);
            this.tabFonts.Controls.Add(this.label6);
            this.tabFonts.Controls.Add(this.label5);
            this.tabFonts.Controls.Add(this.label4);
            this.tabFonts.Controls.Add(this.label3);
            this.tabFonts.Controls.Add(this.label1);
            this.tabFonts.Location = new System.Drawing.Point(4, 25);
            this.tabFonts.Margin = new System.Windows.Forms.Padding(4);
            this.tabFonts.Name = "tabFonts";
            this.tabFonts.Padding = new System.Windows.Forms.Padding(4);
            this.tabFonts.Size = new System.Drawing.Size(629, 213);
            this.tabFonts.TabIndex = 2;
            this.tabFonts.Text = "Fonts";
            this.tabFonts.UseVisualStyleBackColor = true;
            // 
            // buttonInputFont
            // 
            this.buttonInputFont.Location = new System.Drawing.Point(446, 147);
            this.buttonInputFont.Name = "buttonInputFont";
            this.buttonInputFont.Size = new System.Drawing.Size(58, 22);
            this.buttonInputFont.TabIndex = 46;
            this.buttonInputFont.Text = "Select";
            this.buttonInputFont.UseVisualStyleBackColor = true;
            this.buttonInputFont.Click += new System.EventHandler(this.buttonInputFont_Click);
            // 
            // buttonServerListFont
            // 
            this.buttonServerListFont.Location = new System.Drawing.Point(446, 117);
            this.buttonServerListFont.Name = "buttonServerListFont";
            this.buttonServerListFont.Size = new System.Drawing.Size(58, 22);
            this.buttonServerListFont.TabIndex = 45;
            this.buttonServerListFont.Text = "Select";
            this.buttonServerListFont.UseVisualStyleBackColor = true;
            this.buttonServerListFont.Click += new System.EventHandler(this.buttonServerListFont_Click);
            // 
            // textInputFont
            // 
            this.textInputFont.Location = new System.Drawing.Point(98, 147);
            this.textInputFont.Name = "textInputFont";
            this.textInputFont.ReadOnly = true;
            this.textInputFont.Size = new System.Drawing.Size(232, 23);
            this.textInputFont.TabIndex = 44;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(346, 150);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 16);
            this.label16.TabIndex = 43;
            this.label16.Text = "Size";
            // 
            // textInputFontSize
            // 
            this.textInputFontSize.Location = new System.Drawing.Point(391, 146);
            this.textInputFontSize.Name = "textInputFontSize";
            this.textInputFontSize.ReadOnly = true;
            this.textInputFontSize.Size = new System.Drawing.Size(49, 23);
            this.textInputFontSize.TabIndex = 42;
            // 
            // textServerListFontSize
            // 
            this.textServerListFontSize.Location = new System.Drawing.Point(391, 117);
            this.textServerListFontSize.Name = "textServerListFontSize";
            this.textServerListFontSize.ReadOnly = true;
            this.textServerListFontSize.Size = new System.Drawing.Size(49, 23);
            this.textServerListFontSize.TabIndex = 41;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(346, 120);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 16);
            this.label15.TabIndex = 40;
            this.label15.Text = "Size";
            // 
            // textServerListFont
            // 
            this.textServerListFont.Location = new System.Drawing.Point(98, 117);
            this.textServerListFont.Name = "textServerListFont";
            this.textServerListFont.ReadOnly = true;
            this.textServerListFont.Size = new System.Drawing.Size(232, 23);
            this.textServerListFont.TabIndex = 39;
            // 
            // buttonNickListFont
            // 
            this.buttonNickListFont.Location = new System.Drawing.Point(446, 90);
            this.buttonNickListFont.Name = "buttonNickListFont";
            this.buttonNickListFont.Size = new System.Drawing.Size(58, 22);
            this.buttonNickListFont.TabIndex = 38;
            this.buttonNickListFont.Text = "Select";
            this.buttonNickListFont.UseVisualStyleBackColor = true;
            this.buttonNickListFont.Click += new System.EventHandler(this.buttonNickListFont_Click);
            // 
            // textNickListFontSize
            // 
            this.textNickListFontSize.Location = new System.Drawing.Point(391, 89);
            this.textNickListFontSize.Name = "textNickListFontSize";
            this.textNickListFontSize.ReadOnly = true;
            this.textNickListFontSize.Size = new System.Drawing.Size(49, 23);
            this.textNickListFontSize.TabIndex = 37;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(346, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 16);
            this.label14.TabIndex = 36;
            this.label14.Text = "Size";
            // 
            // textNickListFont
            // 
            this.textNickListFont.Location = new System.Drawing.Point(98, 89);
            this.textNickListFont.Name = "textNickListFont";
            this.textNickListFont.ReadOnly = true;
            this.textNickListFont.Size = new System.Drawing.Size(232, 23);
            this.textNickListFont.TabIndex = 35;
            // 
            // buttonQueryFont
            // 
            this.buttonQueryFont.Location = new System.Drawing.Point(446, 62);
            this.buttonQueryFont.Name = "buttonQueryFont";
            this.buttonQueryFont.Size = new System.Drawing.Size(58, 22);
            this.buttonQueryFont.TabIndex = 29;
            this.buttonQueryFont.Text = "Select";
            this.buttonQueryFont.UseVisualStyleBackColor = true;
            this.buttonQueryFont.Click += new System.EventHandler(this.buttonQueryFont_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(346, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 16);
            this.label10.TabIndex = 28;
            this.label10.Text = "Size";
            // 
            // textQueryFontSize
            // 
            this.textQueryFontSize.Location = new System.Drawing.Point(391, 62);
            this.textQueryFontSize.Name = "textQueryFontSize";
            this.textQueryFontSize.ReadOnly = true;
            this.textQueryFontSize.Size = new System.Drawing.Size(49, 23);
            this.textQueryFontSize.TabIndex = 27;
            // 
            // textQueryFont
            // 
            this.textQueryFont.Location = new System.Drawing.Point(98, 62);
            this.textQueryFont.Name = "textQueryFont";
            this.textQueryFont.ReadOnly = true;
            this.textQueryFont.Size = new System.Drawing.Size(232, 23);
            this.textQueryFont.TabIndex = 26;
            // 
            // buttonChannelFont
            // 
            this.buttonChannelFont.Location = new System.Drawing.Point(446, 34);
            this.buttonChannelFont.Name = "buttonChannelFont";
            this.buttonChannelFont.Size = new System.Drawing.Size(58, 22);
            this.buttonChannelFont.TabIndex = 25;
            this.buttonChannelFont.Text = "Select";
            this.buttonChannelFont.UseVisualStyleBackColor = true;
            this.buttonChannelFont.Click += new System.EventHandler(this.buttonChannelFont_Click);
            // 
            // textChannelFontSize
            // 
            this.textChannelFontSize.Location = new System.Drawing.Point(391, 34);
            this.textChannelFontSize.Name = "textChannelFontSize";
            this.textChannelFontSize.ReadOnly = true;
            this.textChannelFontSize.Size = new System.Drawing.Size(49, 23);
            this.textChannelFontSize.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(346, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "Size";
            // 
            // textChannelFont
            // 
            this.textChannelFont.Location = new System.Drawing.Point(98, 34);
            this.textChannelFont.Name = "textChannelFont";
            this.textChannelFont.ReadOnly = true;
            this.textChannelFont.Size = new System.Drawing.Size(232, 23);
            this.textChannelFont.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(346, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Size";
            // 
            // buttonConsoleFont
            // 
            this.buttonConsoleFont.Location = new System.Drawing.Point(446, 7);
            this.buttonConsoleFont.Name = "buttonConsoleFont";
            this.buttonConsoleFont.Size = new System.Drawing.Size(58, 22);
            this.buttonConsoleFont.TabIndex = 20;
            this.buttonConsoleFont.Text = "Select";
            this.buttonConsoleFont.UseVisualStyleBackColor = true;
            this.buttonConsoleFont.Click += new System.EventHandler(this.buttonConsoleFont_Click);
            // 
            // textConsoleFontSize
            // 
            this.textConsoleFontSize.Location = new System.Drawing.Point(391, 7);
            this.textConsoleFontSize.Name = "textConsoleFontSize";
            this.textConsoleFontSize.ReadOnly = true;
            this.textConsoleFontSize.Size = new System.Drawing.Size(49, 23);
            this.textConsoleFontSize.TabIndex = 19;
            // 
            // textConsoleFont
            // 
            this.textConsoleFont.Location = new System.Drawing.Point(98, 7);
            this.textConsoleFont.Name = "textConsoleFont";
            this.textConsoleFont.ReadOnly = true;
            this.textConsoleFont.Size = new System.Drawing.Size(232, 23);
            this.textConsoleFont.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Input Box";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Server List";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Nick List";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Private";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Channel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Console";
            // 
            // tabSounds
            // 
            this.tabSounds.Location = new System.Drawing.Point(4, 25);
            this.tabSounds.Margin = new System.Windows.Forms.Padding(4);
            this.tabSounds.Name = "tabSounds";
            this.tabSounds.Padding = new System.Windows.Forms.Padding(4);
            this.tabSounds.Size = new System.Drawing.Size(629, 213);
            this.tabSounds.TabIndex = 3;
            this.tabSounds.Text = "Sounds";
            this.tabSounds.UseVisualStyleBackColor = true;
            // 
            // tabEvents
            // 
            this.tabEvents.Location = new System.Drawing.Point(4, 25);
            this.tabEvents.Name = "tabEvents";
            this.tabEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tabEvents.Size = new System.Drawing.Size(629, 213);
            this.tabEvents.TabIndex = 4;
            this.tabEvents.Text = "Events";
            this.tabEvents.UseVisualStyleBackColor = true;
            // 
            // tabServer
            // 
            this.tabServer.Controls.Add(this.checkIdentServer);
            this.tabServer.Controls.Add(this.textDefaultNick);
            this.tabServer.Controls.Add(this.label13);
            this.tabServer.Location = new System.Drawing.Point(4, 25);
            this.tabServer.Name = "tabServer";
            this.tabServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabServer.Size = new System.Drawing.Size(629, 213);
            this.tabServer.TabIndex = 5;
            this.tabServer.Text = "Default Server";
            this.tabServer.UseVisualStyleBackColor = true;
            // 
            // checkIdentServer
            // 
            this.checkIdentServer.AutoSize = true;
            this.checkIdentServer.Location = new System.Drawing.Point(11, 42);
            this.checkIdentServer.Name = "checkIdentServer";
            this.checkIdentServer.Size = new System.Drawing.Size(166, 20);
            this.checkIdentServer.TabIndex = 43;
            this.checkIdentServer.Text = "Ident Server Enabled";
            this.checkIdentServer.UseVisualStyleBackColor = true;
            // 
            // textDefaultNick
            // 
            this.textDefaultNick.Location = new System.Drawing.Point(115, 10);
            this.textDefaultNick.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textDefaultNick.Name = "textDefaultNick";
            this.textDefaultNick.Size = new System.Drawing.Size(150, 23);
            this.textDefaultNick.TabIndex = 41;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(8, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 16);
            this.label13.TabIndex = 42;
            this.label13.Text = "Nick name";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 291);
            this.Controls.Add(this.tabControlOptions);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IceChat Settings";
            this.tabControlOptions.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.tabDisplay.ResumeLayout(false);
            this.tabDisplay.PerformLayout();
            this.tabLogging.ResumeLayout(false);
            this.tabLogging.PerformLayout();
            this.tabFonts.ResumeLayout(false);
            this.tabFonts.PerformLayout();
            this.tabServer.ResumeLayout(false);
            this.tabServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControlOptions;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabDisplay;
        private System.Windows.Forms.TabPage tabFonts;
        private System.Windows.Forms.TabPage tabSounds;
        private System.Windows.Forms.TextBox textTimeStamp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkSaveWindowPosition;
        private System.Windows.Forms.TabPage tabEvents;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonConsoleFont;
        private System.Windows.Forms.TextBox textConsoleFontSize;
        private System.Windows.Forms.TextBox textConsoleFont;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textChannelFont;
        private System.Windows.Forms.Button buttonChannelFont;
        private System.Windows.Forms.TextBox textChannelFontSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textQueryFontSize;
        private System.Windows.Forms.TextBox textQueryFont;
        private System.Windows.Forms.Button buttonQueryFont;
        private System.Windows.Forms.TabPage tabServer;
        private System.Windows.Forms.TextBox textDefaultNick;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonNickListFont;
        private System.Windows.Forms.TextBox textNickListFontSize;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textNickListFont;
        private System.Windows.Forms.TextBox textInputFontSize;
        private System.Windows.Forms.TextBox textServerListFontSize;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textServerListFont;
        private System.Windows.Forms.Button buttonInputFont;
        private System.Windows.Forms.Button buttonServerListFont;
        private System.Windows.Forms.TextBox textInputFont;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkIdentServer;
        private System.Windows.Forms.TabPage tabLogging;
        private System.Windows.Forms.CheckBox checkLogConsole;
        private System.Windows.Forms.CheckBox checkLogChannel;
        private System.Windows.Forms.CheckBox checkLogQuery;
    }
}