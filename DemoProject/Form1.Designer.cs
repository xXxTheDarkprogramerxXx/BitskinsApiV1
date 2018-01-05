namespace DemoProject
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
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSecret = new System.Windows.Forms.TextBox();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.txt2FA = new System.Windows.Forms.TextBox();
            this.spinner = new System.Windows.Forms.Button();
            this.btnGetAllItemPrices = new System.Windows.Forms.Button();
            this.lblAccountBalance = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtItemName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtSecret);
            this.splitContainer1.Panel1.Controls.Add(this.txtApiKey);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(897, 319);
            this.splitContainer1.SplitterDistance = 298;
            this.splitContainer1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Secret : ";
            // 
            // txtSecret
            // 
            this.txtSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecret.Location = new System.Drawing.Point(3, 95);
            this.txtSecret.Name = "txtSecret";
            this.txtSecret.Size = new System.Drawing.Size(292, 22);
            this.txtSecret.TabIndex = 3;
            // 
            // txtApiKey
            // 
            this.txtApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApiKey.Location = new System.Drawing.Point(3, 45);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(292, 22);
            this.txtApiKey.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bitskins API Key : ";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.button2);
            this.splitContainer2.Panel1.Controls.Add(this.txt2FA);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtItemName);
            this.splitContainer2.Panel2.Controls.Add(this.spinner);
            this.splitContainer2.Panel2.Controls.Add(this.btnGetAllItemPrices);
            this.splitContainer2.Panel2.Controls.Add(this.lblAccountBalance);
            this.splitContainer2.Panel2.Controls.Add(this.button3);
            this.splitContainer2.Size = new System.Drawing.Size(595, 319);
            this.splitContainer2.SplitterDistance = 45;
            this.splitContainer2.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(354, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 38);
            this.button2.TabIndex = 1;
            this.button2.Text = "Copy";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // txt2FA
            // 
            this.txt2FA.Enabled = false;
            this.txt2FA.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt2FA.Location = new System.Drawing.Point(3, 3);
            this.txt2FA.Name = "txt2FA";
            this.txt2FA.Size = new System.Drawing.Size(337, 38);
            this.txt2FA.TabIndex = 0;
            this.txt2FA.Text = "Your 2FA Will BE Here";
            // 
            // spinner
            // 
            this.spinner.Location = new System.Drawing.Point(17, 158);
            this.spinner.Name = "spinner";
            this.spinner.Size = new System.Drawing.Size(192, 23);
            this.spinner.TabIndex = 3;
            this.spinner.Text = "Get Suggested item price";
            this.spinner.UseVisualStyleBackColor = true;
            this.spinner.Click += new System.EventHandler(this.spinner_Click);
            // 
            // btnGetAllItemPrices
            // 
            this.btnGetAllItemPrices.Location = new System.Drawing.Point(17, 46);
            this.btnGetAllItemPrices.Name = "btnGetAllItemPrices";
            this.btnGetAllItemPrices.Size = new System.Drawing.Size(147, 23);
            this.btnGetAllItemPrices.TabIndex = 2;
            this.btnGetAllItemPrices.Text = "Get All Item Prices";
            this.btnGetAllItemPrices.UseVisualStyleBackColor = true;
            this.btnGetAllItemPrices.Click += new System.EventHandler(this.btnGetAllItemPrices_Click);
            // 
            // lblAccountBalance
            // 
            this.lblAccountBalance.AutoSize = true;
            this.lblAccountBalance.Location = new System.Drawing.Point(443, 1);
            this.lblAccountBalance.Name = "lblAccountBalance";
            this.lblAccountBalance.Size = new System.Drawing.Size(44, 17);
            this.lblAccountBalance.TabIndex = 1;
            this.lblAccountBalance.Text = "$0.00";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(424, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Get Balance";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(17, 130);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(192, 22);
            this.txtItemName.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(897, 319);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSecret;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt2FA;
        private System.Windows.Forms.Label lblAccountBalance;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnGetAllItemPrices;
        private System.Windows.Forms.Button spinner;
        private System.Windows.Forms.TextBox txtItemName;
    }
}

