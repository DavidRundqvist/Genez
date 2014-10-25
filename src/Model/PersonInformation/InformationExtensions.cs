using System.Collections.Generic;
using System.Linq;

namespace Model.PersonInformation {
    public static class InformationExtensions {
        public static IEnumerable<PersonFile> GetMothers(this IEnumerable<Information> self) {
            return self.GetFacts().OfType<Mother>().Select(m => m.Relative);
        }

        public static IEnumerable<PersonFile> GetFathers(this IEnumerable<Information> self) {
            return self.GetFacts().OfType<Father>().Select(m => m.Relative);
        }

        public static IEnumerable<Information> GetFacts(this IEnumerable<Information> self) {
            return self.Where(i => i.Reliability == Reliability.Reliable);
        }
    }
}