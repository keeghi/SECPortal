using System.Threading.Tasks;

namespace SecPortal.Services.Infrastructures
{
    public interface IWriteIdentityServiceAsync<in TIdentity>
    {
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(TIdentity id);
    }
}
