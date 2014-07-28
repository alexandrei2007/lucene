using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneExample
{
    /// <summary>
    /// Custom analyzer.
    /// Replace latin chars (e.g. são paulo ==> sao paulo)
    /// </summary>
    public class CustomAnalyzer : StandardAnalyzer
    {
        Lucene.Net.Util.Version matchVersion;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_matchVersion"></param>
        public CustomAnalyzer(Lucene.Net.Util.Version p_matchVersion)
            : base(p_matchVersion)
        {
            matchVersion = p_matchVersion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public override TokenStream TokenStream(string fieldName, System.IO.TextReader reader)
        {
            Tokenizer tokenizer = new StandardTokenizer(matchVersion, reader);
            TokenStream stream = new StandardFilter(tokenizer);
            stream = new ASCIIFoldingFilter(stream);
            return new LowerCaseFilter(stream);
        }

    }
}
