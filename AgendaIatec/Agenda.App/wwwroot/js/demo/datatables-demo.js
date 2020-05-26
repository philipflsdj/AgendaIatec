//// Call the dataTables jQuery plugin
//$(document).ready(function() {
//  $('#dataTable').DataTable();
//});
// Write your JavaScript code.
//Setup - add a text input to each footer cell
$('#dataTable  th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" placeholder=' + title + '"/>');

});

// DataTable
var table = $('#dataTable').DataTable();

//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json
// Apply the search
table.columns().every(function () {
    var that = this;

    $('input', this.footer()).on('keyup change clear', function () {
        if (that.search() !== this.value) {
            that
                .search(this.value)
                .draw();
        }
    });
});