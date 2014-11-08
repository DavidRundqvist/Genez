using Infrastructure.Persistence.Information.Events;
using Infrastructure.Persistence.Information.Relations;

namespace Infrastructure.Persistence.Information {
    public interface IDTOVisitor<T> {
        T Visit(MotherDTO dto);
        T Visit(GenderDTO dto); 
        T Visit(FatherDTO dto) ;
        T Visit(DeathDTO dto) ;
        T Visit(BirthDTO dto) ;
        T Visit(NameDTO dto) ;
    }
}