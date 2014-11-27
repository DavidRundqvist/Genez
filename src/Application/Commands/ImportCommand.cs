using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Common.Enumerable;
using Common.WPF;
using Infrastructure.Data;
using Model;
using View;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Application.Commands {
    public class ImportCommand : WpfCommand {

        private readonly PersonRegistry _people;
        private readonly GedcomRepositoryFactory _repositoryFactory;
        public ImportCommand(PersonRegistry people, GedcomRepositoryFactory repositoryFactory) {
            _people = people;
            _repositoryFactory = repositoryFactory;
        }


        public override async void Execute(object parameter) {

            var fileDialog = new OpenFileDialog {DefaultExt = ".ged"};
            if (fileDialog.ShowDialog() == false)
                return;

            var repo = _repositoryFactory.GetGedcomRepository(new FileInfo(fileDialog.FileName));
            var people = await repo.GetAllPeople();

            _people.AddRange(people);
        }
    }
}