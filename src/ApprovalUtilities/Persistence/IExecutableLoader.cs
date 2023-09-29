namespace ApprovalUtilities.Persistence;

public interface IExecutableLoader<T> :
    IExecutableQuery,
    ILoader<T>;