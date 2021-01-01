using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StreamsSockets {
    class InvertCaseWriter {
        public StreamWriter Sw { get; set; }

        public InvertCaseWriter(StreamWriter sw) {
            Sw = sw;
        }

    }
}
