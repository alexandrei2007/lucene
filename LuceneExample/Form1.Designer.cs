namespace LuceneExample
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateIndex = new System.Windows.Forms.Button();
            this.btnSearch2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 38);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(266, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search Fuzzy";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(621, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 110);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(621, 145);
            this.txtResult.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Search result";
            // 
            // btnCreateIndex
            // 
            this.btnCreateIndex.Location = new System.Drawing.Point(526, 37);
            this.btnCreateIndex.Name = "btnCreateIndex";
            this.btnCreateIndex.Size = new System.Drawing.Size(107, 23);
            this.btnCreateIndex.TabIndex = 4;
            this.btnCreateIndex.Text = "Create Index File";
            this.btnCreateIndex.UseVisualStyleBackColor = true;
            this.btnCreateIndex.Click += new System.EventHandler(this.btnCreateIndex_Click);
            // 
            // btnSearch2
            // 
            this.btnSearch2.Location = new System.Drawing.Point(284, 38);
            this.btnSearch2.Name = "btnSearch2";
            this.btnSearch2.Size = new System.Drawing.Size(236, 23);
            this.btnSearch2.TabIndex = 5;
            this.btnSearch2.Text = "Search MultiFieldQueryParser";
            this.btnSearch2.UseVisualStyleBackColor = true;
            this.btnSearch2.Click += new System.EventHandler(this.btnSearch2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 267);
            this.Controls.Add(this.btnSearch2);
            this.Controls.Add(this.btnCreateIndex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Name = "Form1";
            this.Text = "Lucene Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateIndex;
        private System.Windows.Forms.Button btnSearch2;
    }
}

