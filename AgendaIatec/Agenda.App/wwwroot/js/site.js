// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.
//Setup - add a text input to each footer cell
//$('#dataTable tfoot th').each(function () {
//    var title = $(this).text();
//    $(this).html('<input type="text" placeholder="Search ' + title + '" />');

//});

//// DataTable
//var table = $('#dataTable').DataTable();

////cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json
//// Apply the search
//table.columns().every(function () {
//    var that = this;

//    $('input', this.footer()).on('keyup change clear', function () {
//        if (that.search() !== this.value) {
//            that
//                .search(this.value)
//                .draw();
//        }
//    });
//});