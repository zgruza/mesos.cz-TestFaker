namespace TestFaker
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.checker = new System.Windows.Forms.Timer(this.components);
            this.noTest = new System.Windows.Forms.NotifyIcon(this.components);
            this.Test = new System.Windows.Forms.NotifyIcon(this.components);
            this.tester = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // checker
            // 
            this.checker.Interval = 10;
            this.checker.Tick += new System.EventHandler(this.checker_Tick);
            // 
            // noTest
            // 
            this.noTest.Icon = ((System.Drawing.Icon)(resources.GetObject("noTest.Icon")));
            this.noTest.Text = "No Test";
            // 
            // Test
            // 
            this.Test.Icon = ((System.Drawing.Icon)(resources.GetObject("Test.Icon")));
            this.Test.Text = "Test";
            // 
            // tester
            // 
            this.tester.Interval = 10;
            this.tester.Tick += new System.EventHandler(this.tester_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 164);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer checker;
        private System.Windows.Forms.NotifyIcon noTest;
        private System.Windows.Forms.NotifyIcon Test;
        private System.Windows.Forms.Timer tester;
    }
}

