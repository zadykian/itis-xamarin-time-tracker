using System.Threading.Tasks;

namespace TimeTracker.App.Core.PageModels.Base
{
    internal class PageModelBase : ExtendedBindableObject
    {
        /// <summary>
        /// Performs Page Model initialization Asynchronously
        /// </summary>
        public virtual Task InitializeAsync(object navigationData) => Task.CompletedTask;
    }
}
