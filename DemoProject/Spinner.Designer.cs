namespace DemoProject
{
    partial class Spinner
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
            this.csgoSpinner2 = new DemoProject.CSGOSpinner();
            this.SuspendLayout();
            // 
            // csgoSpinner2
            // 
            this.csgoSpinner2.Location = new System.Drawing.Point(12, 12);
            this.csgoSpinner2.Name = "csgoSpinner2";
            this.csgoSpinner2.Size = new System.Drawing.Size(672, 123);
            this.csgoSpinner2.TabIndex = 0;
            // 
            // Spinner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 253);
            this.Controls.Add(this.csgoSpinner2);
            this.Name = "Spinner";
            this.Text = "Spinner";
            this.ResumeLayout(false);

        }

        #endregion
        private CSGOSpinner CSGOSpinner1;
        private CSGOSpinner csgoSpinner2;
    }
}