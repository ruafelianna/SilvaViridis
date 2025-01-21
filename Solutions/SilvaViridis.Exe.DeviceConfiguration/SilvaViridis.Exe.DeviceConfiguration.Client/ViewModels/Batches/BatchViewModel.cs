using SilvaViridis.Components;
using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Batches
{
    public class BatchViewModel : ViewModelBase
    {
        public BatchViewModel(long id, string name, DateTime created)
        {
            Id = id;
            Name = name;
            Created = created;
            LocalCreated = created.ToLocalTime();
        }

        public long Id { get; }

        public string Name { get; }

        public DateTime Created { get; }

        public DateTime LocalCreated { get; }
    }
}
