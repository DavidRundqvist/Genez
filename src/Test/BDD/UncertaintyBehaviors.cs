// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;

namespace Test.BDD {
    /*
     * 
     * When nothing is known about a person
     * Then the person's file contains no information
     * 
     * 
     * When conflicting sources have been found
     * Then the person's file contains several information items where Reliability = Unreliable
     * 
     * 
     * When source is difficult to interpret
     * Then the information has Reliability = Unreliable
     * 
     *  
     * When source can't be trusted
     * Then the information has Reliability = Unreliable     
     * * 
     */


}