using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneExample
{
    public class LuceneSearch
    {
        List<Book> books = null;
        List<Author> Authors = null;
        string path;
        const string BOOK_ID_KEY = "id";
        const string BOOK_TITLE_KEY = "title";
        const string BOOK_AUTHOR_KEY = "author";
        const string BOOK_TAGS_KEY = "tags";

        public LuceneSearch()
        {
            path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);

            ////////////////////////////////////////////////////////////////////////////////////////////////
            // populate test case
            ////////////////////////////////////////////////////////////////////////////////////////////////
            this.books = new List<Book>();
            this.Authors = new List<Author>();

            Author a1 = new Author() { Id = 1, Name = "Dan Brown" };
            Author a2 = new Author() { Id = 2, Name = "Stephen King" };

            Book b1 = new Book() { Id = 1, Title = "O Código da Vinci" };
            b1.Tags = new List<string>() { "código", "da vinci" };
            b1.Author = a1;

            Book b2 = new Book() { Id = 2, Title = "O Iluminado" };
            b2.Tags = new List<string>() { "iluminado", "stanley kubrick" };
            b2.Author = a2;

            Book b3 = new Book() { Id = 3, Title = "Pet Sematary" };
            b3.Tags = new List<string>() { "pet", "sematary", "ramones" };
            b3.Author = a2;

            this.books.Add(b1);
            this.books.Add(b2);
            this.books.Add(b3);

            this.Authors.Add(a1);
            this.Authors.Add(a2);
        }

        /// <summary>
        /// Create lucene index file.
        /// </summary>
        public void CreateIndex()
        {
            // lucene temporary path
            string temporaryBooksPath = path + @"\books-lucene";

            using (var analyzer = new CustomAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
            {
                // create lucene writer;
                using (IndexWriter writer = CreateLuceneIndexWriter(temporaryBooksPath, analyzer))
                {
                    foreach (Book book in books)
                    {
                        // create lucene document with book fields.
                        Document doc = CreateLuceneDocument(book);

                        // add entry to index
                        writer.AddDocument(doc);
                    }

                    writer.Optimize();
                }
            }
        }

        /// <summary>
        /// Create lucene index writer.
        /// </summary>
        /// <param name="folder">Folder to write.</param>
        /// <param name="analyzer">Lucene analyzer.</param>
        private IndexWriter CreateLuceneIndexWriter(string folder, CustomAnalyzer analyzer)
        {
            Lucene.Net.Store.Directory luceneIndexDirectory;
            luceneIndexDirectory = FSDirectory.Open(new DirectoryInfo(folder));

            // create lucene index writer
            IndexWriter writer;
            writer = new IndexWriter(luceneIndexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED);

            return writer;
        }

        /// <summary>
        /// Create Lucene document with book fields
        /// </summary>
        /// <param name="book">Book to Create document</param>
        /// <returns>Lucene document.</returns>
        private Document CreateLuceneDocument(Book book)
        {
            StringBuilder tags = new StringBuilder();

            if (book.Tags != null)
            {
                foreach (string tag in book.Tags)
                {
                    tags.Append(tag + ", ");
                }
            }

            // add new index entry
            var doc = new Document();

            // add database fields mapped to lucene fields
            doc.Add(new Field(BOOK_ID_KEY, book.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field(BOOK_TITLE_KEY, book.Title, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field(BOOK_AUTHOR_KEY, book.Author.Name, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field(BOOK_TAGS_KEY, tags.ToString(), Field.Store.YES, Field.Index.ANALYZED));

            return doc;
        }

        /// <summary>
        /// Search by MultiFieldQueryParser.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public List<Book> SearchMultiFieldQueryParser(string searchTerm)
        {
            return this.Search(searchTerm, false);
        }

        /// <summary>
        /// Search by Fuzzy.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public List<Book> SearchFuzzy(string searchTerm)
        {
            return this.Search(searchTerm, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        private List<Book> Search(string searchTerm, bool fuzzy)
        {
            string temporaryBooksPath = path + @"\books-lucene";

            Lucene.Net.Store.Directory luceneIndexDirectory;
            luceneIndexDirectory = FSDirectory.Open(new DirectoryInfo(temporaryBooksPath));

            // set up lucene searcher
            using (var searcher = new IndexSearcher(luceneIndexDirectory, false))
            {
                var hits_limit = 100; // max number records

                Query query = CreateBooksQuery(searchTerm, fuzzy);

                var hits = searcher.Search(query, null, hits_limit, Sort.RELEVANCE).ScoreDocs;
                var resultQuery = hits.Select(hit => System.Convert.ToInt32(searcher.Doc(hit.Doc).Get(BOOK_ID_KEY)));

                List<int> bookIds = resultQuery.Distinct().ToList();

                // parse ids to books
                if (bookIds != null && bookIds.Count > 0)
                {
                    List<Book> temp = new List<Book>();

                    foreach (var id in bookIds)
                    {
                        var b = this.books.Where(p => p.Id == id).FirstOrDefault();
                        if (b != null)
                            temp.Add(b);
                    }
                    
                    return temp;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        private Query CreateBooksQuery(string searchQuery, bool fuzzy)
        {
            if (fuzzy)
            {
                BooleanQuery finalQuery = new BooleanQuery();

                // remove accent
                searchQuery = TextUtil.RemoveDiacritics(searchQuery.ToLower());

                Query query;

                query = QueryTerm(BOOK_TITLE_KEY, searchQuery, 0.7f);
                query.Boost = 1.0f;
                finalQuery.Add(query, Occur.SHOULD);

                query = QueryTerm(BOOK_AUTHOR_KEY, searchQuery, 0.7f);
                query.Boost = 0.9f;
                finalQuery.Add(query, Occur.SHOULD);

                query = QueryTerm(BOOK_TAGS_KEY, searchQuery, 0.9f);
                query.Boost = 0.5f;
                finalQuery.Add(query, Occur.SHOULD);

                return finalQuery;
            }
            else
            {
                using (CustomAnalyzer analyzer = new CustomAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
                {
                    MultiFieldQueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30,
                        new string[] { BOOK_TITLE_KEY, BOOK_AUTHOR_KEY, BOOK_TAGS_KEY }, analyzer);

                    Query queryX = parser.Parse(searchQuery);
                    queryX.Boost = 0;
                    return queryX;

                }
            }
        }

        /// <summary>
        /// Creates a query by a single word or a phrase.
        /// </summary>
        /// <param name="term"></param>
        /// <param name="query"></param>
        /// <param name="similarity"></param>
        /// <returns></returns>
        private Query QueryTerm(string term, string query, float similarity)
        {
            string[] str = query.Trim().Split(' ');

            // phrase?
            if (str.Length > 1)
            {
                BooleanQuery boolQuery = new BooleanQuery();
                foreach (string s in str)
                {
                    FuzzyQuery q2 = new FuzzyQuery(new Term(term, s.ToLower()), similarity);
                    boolQuery.Add(q2, Occur.MUST);
                }

                return boolQuery;
            }
            else
            {
                return new FuzzyQuery(new Term(term, query.Trim()), similarity);
            }


        }
    }
}
