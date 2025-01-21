using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using SilvaViridis.Common.Localization.Abstractions;
using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Batches;
using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.ViewModels.Batches
{
    public class BatchListViewModelProxy : ViewModelBase
    {
        public BatchListViewModelProxy(BatchListViewModel source)
        {
            Source = source;

            TableSource = new(source.Batches)
            {
                Columns = {
                    new TextColumn<BatchViewModel, long>(
                        CreateColumnHeader(Strings.BatchesList_Table_Id),
                        vm => vm.Id
                    ),
                    new TextColumn<BatchViewModel, string>(
                        CreateColumnHeader(Strings.BatchesList_Table_Name),
                        vm => vm.Name
                    ),
                    new TextColumn<BatchViewModel, DateTime>(
                        CreateColumnHeader(Strings.BatchesList_Table_Created),
                        vm => vm.LocalCreated
                    ),
                },
            };
        }

        public BatchListViewModel Source { get; }

        public FlatTreeDataGridSource<BatchViewModel> TableSource { get; }

        private static TextBlock? CreateColumnHeader(
            ITranslationUnit tUnit
        ) => new()
            {
                [!TextBlock.TextProperty]
                    = tUnit.ValueObservable.ToBinding()
            };
    }
}
