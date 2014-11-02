using System;
using System.Linq;
using Common.Coding;
using Common.Enumerable;
using Infrastructure.IO;
using Model;

namespace Infrastructure.Persistence {
    public class RegistryPersistence {
        private readonly IRepository _repository;
        public RegistryPersistence(IRepository repository) {
            _repository = repository;
        }

        public void AttachTo(PersonRegistry registry) {
            registry.CollectionChanged += RegistryCollectionChanged;
            Add(registry.ToArray());
        }

        private void RegistryCollectionChanged(object sender, CollectionEventArgs<PersonFile> args) {
            Add(args.Added);
            Remove(args.Removed);
        }

        private void Add(PersonFile[] personFiles) {
            _repository.Store(personFiles);
            personFiles.ForEach(p => p.Changed += PersonFileChanged);
        }

        private void Remove(PersonFile[] personFiles) {
            personFiles.ForEach(p => p.Changed -= PersonFileChanged);
            _repository.Remove(personFiles);            
        }

        private void PersonFileChanged(object sender, EventArgs eventArgs) {
            var p = sender as PersonFile;
            _repository.Store(new[]{p});
        }

    }
}