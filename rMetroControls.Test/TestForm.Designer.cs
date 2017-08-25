namespace rMetroControls.Test
{
    partial class TestForm
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.styledContextMenu1 = new rMetroControls.Controls.StyledContextMenu(this.components);
            this.blaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parentItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.childItem1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.moarChildsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.childItem2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.anotherItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.ControlPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.TypeComboBox = new System.Windows.Forms.ComboBox();
            this.StyleComboBox = new System.Windows.Forms.ComboBox();
            this.styledToolTip1 = new rMetroControls.StyledToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.styledContextMenu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.ContextMenuStrip = this.styledContextMenu1;
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.styledToolTip1.SetToolTip(this.splitContainer1.Panel1, "This is a ToolTip");
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ControlPropertyGrid);
            this.splitContainer1.Panel2.Controls.Add(this.TypeComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.StyleComboBox);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(756, 494);
            this.splitContainer1.SplitterDistance = 491;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // styledContextMenu1
            // 
            this.styledContextMenu1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(222)))), ((int)(((byte)(245)))));
            this.styledContextMenu1.DropShadowEnabled = false;
            this.styledContextMenu1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.styledContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blaToolStripMenuItem,
            this.parentItemToolStripMenuItem,
            this.toolStripMenuItem1,
            this.anotherItemToolStripMenuItem});
            this.styledContextMenu1.Name = "styledContextMenu1";
            this.styledContextMenu1.Size = new System.Drawing.Size(148, 80);
            // 
            // blaToolStripMenuItem
            // 
            this.blaToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.blaToolStripMenuItem.Name = "blaToolStripMenuItem";
            this.blaToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.blaToolStripMenuItem.Text = "Normal Item";
            // 
            // parentItemToolStripMenuItem
            // 
            this.parentItemToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.parentItemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.childItem1ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.moarChildsToolStripMenuItem,
            this.childItem2ToolStripMenuItem});
            this.parentItemToolStripMenuItem.Name = "parentItemToolStripMenuItem";
            this.parentItemToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.parentItemToolStripMenuItem.Text = "Parent Item";
            // 
            // childItem1ToolStripMenuItem
            // 
            this.childItem1ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.childItem1ToolStripMenuItem.Name = "childItem1ToolStripMenuItem";
            this.childItem1ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.childItem1ToolStripMenuItem.Text = "Child Item";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(135, 6);
            // 
            // moarChildsToolStripMenuItem
            // 
            this.moarChildsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.moarChildsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.heheToolStripMenuItem});
            this.moarChildsToolStripMenuItem.Name = "moarChildsToolStripMenuItem";
            this.moarChildsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.moarChildsToolStripMenuItem.Text = "Moar Childs";
            // 
            // heheToolStripMenuItem
            // 
            this.heheToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(222)))), ((int)(((byte)(245)))));
            this.heheToolStripMenuItem.Name = "heheToolStripMenuItem";
            this.heheToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.heheToolStripMenuItem.Text = "Hehe";
            // 
            // childItem2ToolStripMenuItem
            // 
            this.childItem2ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.childItem2ToolStripMenuItem.Name = "childItem2ToolStripMenuItem";
            this.childItem2ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.childItem2ToolStripMenuItem.Text = "Child Item";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(222)))), ((int)(((byte)(245)))));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(144, 6);
            // 
            // anotherItemToolStripMenuItem
            // 
            this.anotherItemToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.anotherItemToolStripMenuItem.Name = "anotherItemToolStripMenuItem";
            this.anotherItemToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.anotherItemToolStripMenuItem.Text = "Another Item";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Show HoHeader-Form";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ControlPropertyGrid
            // 
            this.ControlPropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ControlPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.ControlPropertyGrid.Location = new System.Drawing.Point(0, 42);
            this.ControlPropertyGrid.Name = "ControlPropertyGrid";
            this.ControlPropertyGrid.Size = new System.Drawing.Size(260, 452);
            this.ControlPropertyGrid.TabIndex = 2;
            // 
            // TypeComboBox
            // 
            this.TypeComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeComboBox.FormattingEnabled = true;
            this.TypeComboBox.Location = new System.Drawing.Point(0, 21);
            this.TypeComboBox.Name = "TypeComboBox";
            this.TypeComboBox.Size = new System.Drawing.Size(260, 21);
            this.TypeComboBox.TabIndex = 1;
            this.TypeComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeComboBox_SelectedIndexChanged);
            // 
            // StyleComboBox
            // 
            this.StyleComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.StyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StyleComboBox.FormattingEnabled = true;
            this.StyleComboBox.Location = new System.Drawing.Point(0, 0);
            this.StyleComboBox.Name = "StyleComboBox";
            this.StyleComboBox.Size = new System.Drawing.Size(260, 21);
            this.StyleComboBox.TabIndex = 0;
            this.StyleComboBox.SelectedIndexChanged += new System.EventHandler(this.StyleComboBox_SelectedIndexChanged);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(756, 494);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TestForm";
            this.Text = "rMetroControls Test Application";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.styledContextMenu1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid ControlPropertyGrid;
        private System.Windows.Forms.ComboBox TypeComboBox;
        private StyledToolTip styledToolTip1;
        private Controls.StyledContextMenu styledContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem blaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parentItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem childItem1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem childItem2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem anotherItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moarChildsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heheToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ComboBox StyleComboBox;
        private System.Windows.Forms.Button button1;
    }
}

