using Model.PersonInformation.Events;
using Model.PersonInformation.Relations;

namespace Model.PersonInformation {
    public interface IInformationVisitor<out T> {
        T Visit(Birth  info);
        T Visit(Death  info);
        T Visit(Name   info);
        T Visit(Mother info);
        T Visit(Father info);
        T Visit(Gender info);
        T Visit(Portrait info);
    }
}