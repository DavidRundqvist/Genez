using System.Linq;
using Common;
using Model;

namespace View.Global {
    public class PersonPresentationFactory {
        private readonly WPFImageFactory _imageRepository;
        public PersonPresentationFactory(WPFImageFactory imageRepository) {
            _imageRepository = imageRepository;
        }

        public PersonPresentation CreatePresentation(PersonFile person) {
            var portraitId = person.Portraits.Any()
                ? Maybe.From(person.Portraits.First().ImageFile)
                : Maybe.Empty;

            var image = portraitId.Convert(FileInfo => _imageRepository.GetImage(FileInfo));        
            return new PersonPresentation(person, image);
        }
    }
}