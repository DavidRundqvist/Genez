using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Enumerable;
using Common.WPF;
using Model;

namespace Application.Commands {
    public class LoadDiskRepositoryCommand : WpfCommand {
        private readonly IRepository _source;
        private readonly PersonRegistry _target;
        public LoadDiskRepositoryCommand(IRepository source, PersonRegistry target) {
            _source = source;
            _target = target;
        }

        public override async void Execute(object parameter) {
            var people = await _source.GetAllPeople();

            _target.AddRange(people);
        }
    }
}