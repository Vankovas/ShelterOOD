namespace Shelter
{
    partial class Form2
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
            this.lbOwnersForm = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbOwnersForm
            // 
            this.lbOwnersForm.FormattingEnabled = true;
            this.lbOwnersForm.Location = new System.Drawing.Point(12, 12);
            this.lbOwnersForm.Name = "lbOwnersForm";
            this.lbOwnersForm.Size = new System.Drawing.Size(730, 407);
            this.lbOwnersForm.TabIndex = 0;
            this.lbOwnersForm.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbOwnersForm_MouseDoubleClick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbOwnersForm);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbOwnersForm;
    }
}