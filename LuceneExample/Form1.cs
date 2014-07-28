using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuceneExample
{
    public partial class Form1 : Form
    {
        LuceneSearch lucene = null;

        public Form1()
        {
            lucene = new LuceneSearch();
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var books = lucene.SearchFuzzy(this.txtSearch.Text);
            Print(books);
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            var books = lucene.SearchMultiFieldQueryParser(this.txtSearch.Text);
            Print(books);
        }

        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            lucene.CreateIndex();
        }

        private void Print(List<Book> books)
        {
            if (books != null)
            {
                this.txtResult.Text = "";
                foreach (var b in books)
                {
                    this.txtResult.Text += b.Title + System.Environment.NewLine;
                }
            }
            else
            {
                this.txtResult.Text = "empty";
            }
        }

    }
}
