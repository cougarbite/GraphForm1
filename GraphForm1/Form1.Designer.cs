
namespace GraphForm1
{
    partial class Form1
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mouseStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.radioDrawNode = new System.Windows.Forms.RadioButton();
            this.radioDrawEdge = new System.Windows.Forms.RadioButton();
            this.nodesListBox = new System.Windows.Forms.ListBox();
            this.edgesListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.deleteNodeButton = new System.Windows.Forms.Button();
            this.deleteEdgeButton = new System.Windows.Forms.Button();
            this.grafPanel = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 427);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(762, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mouseStatus
            // 
            this.mouseStatus.Name = "mouseStatus";
            this.mouseStatus.Size = new System.Drawing.Size(28, 17);
            this.mouseStatus.Text = "x: y:";
            // 
            // radioDrawNode
            // 
            this.radioDrawNode.AutoSize = true;
            this.radioDrawNode.Checked = true;
            this.radioDrawNode.Location = new System.Drawing.Point(528, 12);
            this.radioDrawNode.Name = "radioDrawNode";
            this.radioDrawNode.Size = new System.Drawing.Size(79, 17);
            this.radioDrawNode.TabIndex = 1;
            this.radioDrawNode.TabStop = true;
            this.radioDrawNode.Text = "Draw Node";
            this.radioDrawNode.UseVisualStyleBackColor = true;
            // 
            // radioDrawEdge
            // 
            this.radioDrawEdge.AutoSize = true;
            this.radioDrawEdge.Location = new System.Drawing.Point(644, 12);
            this.radioDrawEdge.Name = "radioDrawEdge";
            this.radioDrawEdge.Size = new System.Drawing.Size(78, 17);
            this.radioDrawEdge.TabIndex = 2;
            this.radioDrawEdge.Text = "Draw Edge";
            this.radioDrawEdge.UseVisualStyleBackColor = true;
            // 
            // nodesListBox
            // 
            this.nodesListBox.FormattingEnabled = true;
            this.nodesListBox.Location = new System.Drawing.Point(528, 59);
            this.nodesListBox.Name = "nodesListBox";
            this.nodesListBox.Size = new System.Drawing.Size(99, 95);
            this.nodesListBox.TabIndex = 3;
            // 
            // edgesListBox
            // 
            this.edgesListBox.FormattingEnabled = true;
            this.edgesListBox.Location = new System.Drawing.Point(644, 59);
            this.edgesListBox.Name = "edgesListBox";
            this.edgesListBox.Size = new System.Drawing.Size(99, 95);
            this.edgesListBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(641, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Edges:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(525, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nodes:";
            // 
            // deleteNodeButton
            // 
            this.deleteNodeButton.Location = new System.Drawing.Point(528, 176);
            this.deleteNodeButton.Name = "deleteNodeButton";
            this.deleteNodeButton.Size = new System.Drawing.Size(99, 23);
            this.deleteNodeButton.TabIndex = 7;
            this.deleteNodeButton.Text = "Delete Node";
            this.deleteNodeButton.UseVisualStyleBackColor = true;
            this.deleteNodeButton.Click += new System.EventHandler(this.deleteNodeButton_Click);
            // 
            // deleteEdgeButton
            // 
            this.deleteEdgeButton.Location = new System.Drawing.Point(644, 176);
            this.deleteEdgeButton.Name = "deleteEdgeButton";
            this.deleteEdgeButton.Size = new System.Drawing.Size(99, 23);
            this.deleteEdgeButton.TabIndex = 8;
            this.deleteEdgeButton.Text = "Delete Edge";
            this.deleteEdgeButton.UseVisualStyleBackColor = true;
            this.deleteEdgeButton.Click += new System.EventHandler(this.deleteEdgeButton_Click);
            // 
            // grafPanel
            // 
            this.grafPanel.BackColor = System.Drawing.Color.Transparent;
            this.grafPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grafPanel.Location = new System.Drawing.Point(0, 0);
            this.grafPanel.Name = "grafPanel";
            this.grafPanel.Size = new System.Drawing.Size(500, 400);
            this.grafPanel.TabIndex = 9;
            this.grafPanel.Click += new System.EventHandler(this.grafPanel_Click);
            this.grafPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.grafPanel_Paint);
            this.grafPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.grafPanel_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 449);
            this.Controls.Add(this.grafPanel);
            this.Controls.Add(this.deleteEdgeButton);
            this.Controls.Add(this.deleteNodeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edgesListBox);
            this.Controls.Add(this.nodesListBox);
            this.Controls.Add(this.radioDrawEdge);
            this.Controls.Add(this.radioDrawNode);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel mouseStatus;
        private System.Windows.Forms.RadioButton radioDrawNode;
        private System.Windows.Forms.RadioButton radioDrawEdge;
        private System.Windows.Forms.ListBox nodesListBox;
        private System.Windows.Forms.ListBox edgesListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button deleteNodeButton;
        private System.Windows.Forms.Button deleteEdgeButton;
        private System.Windows.Forms.Panel grafPanel;
    }
}

