// --------------------------------------------------------------------------------
// <copyright filename="ISessionContext.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
namespace engUtil.CRUDService.Interfaces
{
    public interface ISessionContext<T>
    {
        T GetContext();
    }
}
