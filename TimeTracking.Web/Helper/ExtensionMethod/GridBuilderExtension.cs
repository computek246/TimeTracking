using System;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;

namespace TimeTracking.Web.Helper.ExtensionMethod
{
    public static class GridBuilderExtension
    {
        public static GridBuilder<TModel> Export<TModel>(
            this GridBuilder<TModel> gridBuilder, string fileName, string url)
            where TModel : class
        {
            return gridBuilder
                .Pdf(pdf =>
                {
                    pdf
                        .AllPages()
                        .AvoidLinks()
                        .PaperSize("A4")
                        .Scale(0.8)
                        .Margin("2cm", "1cm", "1cm", "1cm")
                        .Landscape()
                        .RepeatHeaders()
                        .FileName(fileName + ".pdf")
                        .ProxyURL(url);
                })
                .Excel(excel =>
                {
                    excel
                        .Filterable()
                        .AllPages()
                        .Collapsible()
                        .FileName(fileName + ".xlsx")
                        .ProxyURL(url);
                });
        }

        public static GridBuilder<TModel> GetGridBuilder<TModel>(
            this GridBuilder<TModel> gridBuilder,
            Action<GridBuilder<TModel>> configurator = null
        ) where TModel : class
        {
            gridBuilder
                .Pageable(p =>
                {
                    p.PageSizes(new object[] { 5, 10, 20, 50, 100, "All" });
                    p.Info(true);
                    p.Enabled(true);
                    p.Refresh(true);
                    p.ButtonCount(5);
                })
                .Sortable(sortable =>
                {
                    sortable
                        .AllowUnsort(true)
                        .SortMode(GridSortMode.MultipleColumn)
                        .ShowIndexes(true);
                })
                .Scrollable(sc =>
                {
                    sc.Endless(false);
                })
                .NoRecords()
                .Filterable(filterable =>
                {
                    filterable
                        .Extra(false)
                        .Operators(operators =>
                        {
                            operators.ForString(str => str.Clear());
                        });
                })
                .Groupable(builder => builder.ShowFooter(true))
                .Reorderable(builder => builder.Columns(true))
                .Resizable(builder => builder.Columns(true))
                .Selectable(selectable =>
                {
                    selectable
                        .Mode(GridSelectionMode.Multiple)
                        .Type(GridSelectionType.Cell);
                })
                .Navigatable()
                .Mobile(MobileMode.Auto)
                .AllowCopy()
                .Editable(GridEditMode.PopUp);

            configurator?.Invoke(gridBuilder);

            return gridBuilder;
        }


        public static AjaxDataSourceBuilder<TModel> AddEvents<TModel>(this AjaxDataSourceBuilder<TModel> dataSourceBuilder)
            where TModel : class
        {
            dataSourceBuilder
                .Events(events =>
                {
                    events.RequestStart("request_start");
                    events.RequestEnd("request_end");
                    events.Error("error_handler");
                });

            return dataSourceBuilder;
        }

    }
}